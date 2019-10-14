// <copyright file="BlackjackController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Interface;
    using HenE.GameBlackJack.Kaarten;
    using HenE.GameBlackJack.Settings;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Controller op het spel.Is het spelr gestart.Vraagt de dealer om iets te doen. vraagt ook de spelet om iets te doen.
    /// </summary>
    public class BlackjackController
    {
        private readonly BlackJackPointsCalculator blackJackPointsCalculator = new BlackJackPointsCalculator();
        private readonly Tafel tafel;
        private readonly KaartenExtensions kaartenHelper = new KaartenExtensions();
        private readonly ICommunicate communicator;
        private readonly ActiesHelper actiesHelper = new ActiesHelper();
        private SpelerHand spelerHand = null;
        private Spel spel = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackjackController"/> class.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="communicator">De communicator.</param>
        public BlackjackController(Tafel tafel, ICommunicate communicator)
        {
            this.spel = new Spel(communicator);
            this.tafel = tafel;
        }

        /// <summary>
        /// Check of het spel is klaar om te starten.
        /// </summary>
        /// <returns>Klaar of nog niet.</returns>
        public bool Start()
        {
            // check of ik alles heb
            if (this.tafel.Dealer == null)
            {
                throw new ArgumentNullException("Er is geen dealer.");
            }

            this.StartRonde();
            return true;
        }

        /// <summary>
        /// Start een rondje.
        /// </summary>
        private void StartRonde()
        {
            if (this.tafel.Plekken == null)
            {
                throw new ArgumentNullException("Er zijn geen plekken met spelers.");
            }

            this.BepaalWaarElkeSpelerGaatZitten();
            this.spel.InitialiseerHetSpel(this.tafel.Dealer, this.tafel.Spelers);
            this.Beginnen();

            while (this.spel.GaNaarDeVolgendeSpeelbareHand() != null)
            {
                Hand spelerHand = this.spel.HuidigeHand;
                if (this.IsBlackJack(spelerHand))
                {
                    this.HandelBlackJack(spelerHand);
                }

                if (spelerHand.Status != HandStatussen.BlackJeck)
                {
                    List<Acties> mogelijkActies = this.ControleerHand(spelerHand);
                    if (!spelerHand.IsDealerHand)
                    {
                        this.spelerHand = spelerHand as SpelerHand;
                        if (mogelijkActies.Count == 0)
                        {
                            // dan kan ik niks en ga ik naar de volgende hand. Bijv, omdat de hand is gesloten
                            this.communicator.TellHand(this.spelerHand, Meldingen.GeenActie);
                            continue;
                        }

                        while ((mogelijkActies.Count > 0 && spelerHand.Status == HandStatussen.InSpel) || spelerHand.Status == HandStatussen.Verdubbelen || spelerHand.Status == HandStatussen.Gesplitst || spelerHand.Status == HandStatussen.Gekochtocht)
                        {
                            if (mogelijkActies.Count == 1)
                            {
                                this.ProcessActie(this.spel.HuidigeHand, mogelijkActies[0]);
                            }
                            else
                            {
                                // er zijn meerdere acties mogelijk, vraag aan de speler wat hij/zij wil
                                Acties gekozenActie = this.AskActie(mogelijkActies, this.spel.HuidigeHand);
                                this.ProcessActie(this.spel.HuidigeHand, gekozenActie);
                            }

                            mogelijkActies = this.ControleerHand(spelerHand);
                        }
                    }
                    else
                    {
                        // start behandelen met de dealer.
                        this.BehandelDeDealer(spelerHand);
                    }
                }
            }

            this.BeeindHetSpel(this.spel.Handen);
        }

        /// <summary>
        /// Doet wat de speler wil doen.
        /// </summary>
        /// <param name="huidigeHand">De hand van de speler.</param>
        /// <param name="actie">De actie die de speler heeft gekozen.</param>
        private void ProcessActie(Hand huidigeHand, Acties actie)
        {
            // ben ik een speler of een dealerhand?
            // het verschil is dat ik niet tegen de dealer praat maar tegen de spelers
            List<Speler> spelersToTell = new List<Speler>();
            SpelerHand spelerHand = null;

            if (huidigeHand.IsDealerHand)
            {
                // aan meerdere spelers vertellen
                spelersToTell = this.spel.Spelers;
            }
            else
            {
                spelerHand = huidigeHand as SpelerHand;
                spelersToTell.Add(spelerHand.Speler);
            }

            this.TellToPlayers(spelersToTell, Meldingen.ToonHand);

            // this.spel.PrintMessage(huidigeHand);
            this.VoerActieUit(huidigeHand, actie);

            if (huidigeHand.IsDood(this.blackJackPointsCalculator.CalculatePoints(huidigeHand.Kaarten)))
            {
                if (huidigeHand.IsDealerHand)
                {
                    this.TellToPlayers(spelersToTell, Meldingen.DealerDied);
                }
                else
                {
                    this.TellToPlayers(spelersToTell, Meldingen.YouDied);
                }

                //this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, "Helaas je hebt meer dan 21 punten, Sorry je moet dus stoppen.");
                this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, $"Je hebt {huidigeHand.Inzet.WaardeVanDeFiches} verliezen.");
                huidigeHand.ChangeStatus(HandStatussen.IsDood);
            }
            else if (huidigeHand.Status == HandStatussen.Gesplitst)
            {
                this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, $"{huidigeHand.Persoon.Naam} je hebt twee handen!");
                List<Hand> handenVanSpeler = this.spel.HandenVanDeSpeler(spelerHand);
                for (int index = 0; index < handenVanSpeler.Count; index++)
                {
                    if (index == 0)
                    {
                        this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.WaardeVanDeHand);
                        foreach (Kaart kaart in handenVanSpeler[0].Kaarten)
                        {
                            this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, $"{kaart.Kleur} van {kaart.Teken}");
                        }
                    }
                    else if (index == 1)
                    {
                        this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, $"bij tweede hand heb je : ");
                        foreach (Kaart kaart in handenVanSpeler[0].Kaarten)
                        {
                            this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, $"{kaart.Kleur} van {kaart.Teken}");
                        }
                    }
                }
            }

            this.spel.PrintMessage(huidigeHand);
        }

        /// <summary>
        /// De eerste prossesn van het spel voor dat het spel start.
        /// Vraag de speler om fiches bij de hand te inzitten.
        /// Geef elke hand eerste kaart.Ook geeft allen de spelers tweede kaart.
        /// Dus de dealer krijgt een kaart.
        /// </summary>
        private void Beginnen()
        {
            foreach (Speler speler in this.tafel.Spelers)
            {
                Fiches ingezetteFiches = null;

                // heb een tijdelijke hand ndoig voor de speler
                SpelerHand hand = new SpelerHand(speler);

                // todo, loopje
                while (true)
                {
                    if (this.communicator.AskFichesInzetten(hand, this.tafel.MinimalenZet, out ingezetteFiches))
                    {
                        if (this.tafel.BepaaltOfDeWaardetussenMaxInzetEnMinInzet(ingezetteFiches.WaardeVanDeFiches))
                        {
                            // inzet is ok
                            // een hand wordt aangemaakt,
                            // ok de speler heeft ingezet en wil dus meedoen
                            // aan de collectie toegevoegd.
                            SpelerHand spelerhand = this.spel.SpelerToevoegen(speler);
                            spelerhand.Inzet = ingezetteFiches;
                            this.communicator.Tell(speler, Meldingen.ToonInzet);
                            break;
                        }
                        else
                        {
                            this.communicator.Tell(speler, Meldingen.OngeldigeInzet);
                        }
                    }
                    else
                    {
                        // de speler wil niet inzetten en doetr dus niet mee
                        break; // while true
                    }
                }
                //this.VraagOmfichesBijDeHandTeInZetten(speler, waarde);
                //this.communicator.Tell(speler, Meldingen.GeenActie, $"{speler.Naam} je hebt {ficheWaarde} ingezet.");
            }

            // geef elke hand een kaart
            foreach (Hand hand in this.spel.Handen)
            {
                Kaart kaart = this.tafel.StapelKaarten.NeemEenKaart();
                if (kaart != null)
                {
                    hand.AddKaart(kaart);
                }
                else
                {
                    // todo, wat gaan we dioen als we geen kaarten meer hebben?
                }
            }

            // geef elke hand een kaart, behalve de dealer
            foreach (Hand hand in this.spel.Handen)
            {
                if (!hand.IsDealerHand)
                {
                    Kaart kaart = this.tafel.StapelKaarten.NeemEenKaart();
                    if (kaart != null)
                    {
                        hand.AddKaart(kaart);
                    }
                    else
                    {
                        // todo, wat gaan we dioen als we geen kaarten meer hebben?
                    }
                }
            }
        }

        /// <summary>
        /// Voer de actie die de speler heeft gekozen uit.
        /// </summary>
        /// <param name="hand">De huidige hand.</param>
        /// <param name="deActie">De actie die de speler wil doen.</param>
        private void VoerActieUit(Hand hand, Acties deActie)
        {
            switch (deActie)
            {
                case Acties.Splitsen:
                    this.spel.SplitsHand(hand);
                    hand.ChangeStatus(HandStatussen.Gesplitst);
                    break;
                case Acties.Verdubbelen:
                    this.spel.Verdubbelen(hand, this.tafel.StapelKaarten);
                    hand.ChangeStatus(HandStatussen.Verdubbelen);
                    break;
                case Acties.Kopen:
                    this.spel.Kopen(hand, this.tafel.StapelKaarten);
                    hand.ChangeStatus(HandStatussen.Gekochtocht);
                    break;
                case Acties.Passen:
                    hand.ChangeStatus(HandStatussen.Gepassed);
                    break;
            }
        }

        private bool Verdubbelen(Hand hand)
        {
            // ok, de speler heeft gekozen om te verdubbelen
            // heeft hij nog fiche ter waarde van hand?
            if (hand.IsDealerHand)
            {
                // de dealer kan niet verdubbelen
                return false;
            }

            SpelerHand spelerHand = hand as SpelerHand;
            Fiches fiches = spelerHand.Speler.Fiches.GeefMeFischesTerWaardeVan(spelerHand.Inzet.WaardeVanDeFiches, 0, true);
            if (fiches == null)
            {
                // dan heeft de speler niet de juiste hoeveelheid fiches. Hij kan dan eventueel inwisselen of kopen, 
                // inwisselen is kopen met als inleg een fiche
                string answer = this.communicator.Ask(spelerHand.Speler, FichesKopen);

                // bepaal antwoord 
                // indien kopen, koop de fiches en vraag weer de juiste hoeveelheid uit?
                // indien niet kopen, return false;
                // todo doe dit in een loopje
                fiches = spelerHand.Speler.Fiches.GeefMeFischesTerWaardeVan(spelerHand.Inzet.WaardeVanDeFiches, 0, true);
            }

            // inzetten bij de hand

            // oke, koop dan maar een kaart
            return this.spel.Verdubbelen(spelerhand, this.tafel.StapelKaarten);
        }

        public Fiches VraagAanSpelerInzetVoorEenHand(SpelerHand spelerhand)
        {
            Fiches fiches = null;
            if (!this.communicator.AskFichesKopen(spelerhand.Speler, out fiches))
            {
                // speler wil of kan niet inzetten
                return null;
            }

            // min waarde hoef ik huier niet te controleren, omdat dat eerder in de flow is gedaan.
            if (spelerhand.Inzet.WaardeVanDeFiches + fiches.WaardeVanDeFiches > this.tafel.MaximaleInZet)
            {
                // todo tell
                // todo doen we een loop?
            }

            // controlleer de inzet (min/max)
            return fiches;
        }

        /// <summary>
        /// Bepaal wat de speler mag doen.En welke actie mag returen.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        private List<Acties> ControleerHand(Hand hand)
        {
            List<Acties> acties = new List<Acties>();

            while (this.MagDeSpelerKopen(hand))
            {
                acties.Add(Acties.Kopen);
                break;
            }

            while (this.MagDeSpelerPassen(hand))
            {
                acties.Add(Acties.Passen);
                break;
            }

            while (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 9 && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 11 && hand.Kaarten.Count == 2)
            {
                acties.Add(Acties.Verdubbelen);
                break;
            }

            while (this.kaartenHelper.MagSplitsen(hand))
            {
                acties.Add(Acties.Splitsen);
                break;
            }

            return acties;
        }

        /// <summary>
        /// Beaal aan de Hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="wordtBetaal">De waarde die worde gebetaald.</param>
        private void KeerUit(Hand hand, double wordtBetaal)
        {
            if (wordtBetaal == 1.5)
            {
                float betaal = hand.Inzet.WaardeVanDeFiches * 1.5f;
                int moetBetalenAanHand = (int)betaal;
                this.communicator.Tell(hand.HuidigeSpeler(), Meldingen.GeenActie, $"{hand.Persoon.Naam}Je heeft {moetBetalenAanHand} verdient.");
                this.FichesVerdienen(hand, moetBetalenAanHand);
            }
            else if (wordtBetaal == 1.0)
            {
                this.FichesVerdienen(hand, hand.Inzet.WaardeVanDeFiches);
                this.communicator.Tell(hand.HuidigeSpeler(), Meldingen.GeenActie, $"{hand.Persoon.Naam}Je heeft {hand.Inzet.WaardeVanDeFiches} verdient.");
            }
        }

        /// <summary>
        /// Neem de status van de hand en bepaal Hoeveel keer aan de speler moet terug betalen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Hoeveel keer moet aan de speler moet betalen.</returns>
        private double BepaalFactorInzet(Hand hand)
        {
            // betaal uit
            switch (hand.Status)
            {
                case HandStatussen.BlackJeck:
                    return 1.5;

                case HandStatussen.Gewonnen:
                    return 1.0;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Vraag de speler wat hij wil doen.
        /// </summary>
        /// <param name="mogelijkActies">Lijst van de acties die de speler mag van uit het mag kiezen is.</param>
        /// <param name="huidigeHand">De huidige hand.</param>
        private Acties AskActie(List<Acties> mogelijkActies, Hand huidigeHand)
        {
            // De actie die de speler wil doen.
            int deActie = 0;
            string deSpelerwilDoen = string.Empty;
            this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, $"{huidigeHand.Persoon.Naam} je hebt nu {this.blackJackPointsCalculator.CalculatePoints(huidigeHand.Kaarten)} punten bij je hand.");
            deSpelerwilDoen = this.communicator.Ask(huidigeHand.HuidigeSpeler(), Vragen.KiesActie, $"{huidigeHand.Persoon.Naam} Kies maar een van de acties die hieronder staat! Je mag alleen nummer gebruiken.");

            // keuze aan de klant laten
            for (int index = 0; index < mogelijkActies.Count; index++)
            {
                this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, $"{index.ToString()} {this.ActieTotStrign(mogelijkActies[index])}");
            }

            while (!int.TryParse(deSpelerwilDoen, out deActie) || deActie > mogelijkActies.Count)
            {
                this.communicator.Tell(huidigeHand.HuidigeSpeler(), Meldingen.GeenActie, "Je hebt geen nummer ingevoegd of een nummer die boven niet bestaat. Voeg maar een nummer in.");
                deSpelerwilDoen = this.communicator.Ask(huidigeHand.HuidigeSpeler(), Vragen.KiesActie, $"{huidigeHand.Persoon.Naam} Kies maar een van de acties die hieronder staat! Je mag alleen nummer gebruiken.");
            }

            Acties actie = mogelijkActies[deActie];
            mogelijkActies.Remove(mogelijkActies[deActie]);
            return actie;
        }

        /// <summary>
        /// Vraag de speler om fiches bij de hand in zetten.
        /// </summary>
        /// <param name="speler">De lijst van de spelers die zijn in het spelr bezig.</param>
        /// <param name="waarde">De waarde van een fiche.</param>
        private int VraagOmfichesBijDeHandTeInZetten(Speler speler, int waarde)
        {
            foreach (Hand hand in this.spel.Handen)
            {
                if (hand.Persoon == speler)
                {
                    speler.ZetFichesBijHandIn(hand, waarde);
                    waarde = hand.Inzet.WaardeVanDeFiches;
                }
            }

            return waarde;
        }

        /// <summary>
        /// Als de hand klaar is dan doe hij dicht.
        /// </summary>
        /// <param name="hand">De hand die klaar is.</param>
        private void CloseHand(Hand hand)
        {
            if (hand.Status == HandStatussen.BlackJeck)
            {
                this.KeerUit(hand, this.BepaalFactorInzet(hand));
            }
            else if (hand.Status == HandStatussen.OnHold)
            {
            }
            else if (hand.Status == HandStatussen.Gewonnen)
            {
                this.KeerUit(hand, this.BepaalFactorInzet(hand));
            }
            else
            {
                this.VerzameelDeFiches(hand);
            }

            hand.Close();
        }

        /// <summary>
        /// Check of de speler mag kopen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Of de speler kan kopen of niet.</returns>
        private bool MagDeSpelerKopen(Hand hand) => this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 21 && hand.Status != HandStatussen.Gepassed;

        /// <summary>
        /// Check of de speler mag passen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Mag de speler passen of niet.</returns>
        private bool MagDeSpelerPassen(Hand hand) => this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 21 && hand.Status != HandStatussen.BlackJeck;

        /// <summary>
        /// Check of de dealer mag kopen.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns>Mag de dealer kopen of mag niet.</returns>
        private bool MoetDeDealerKopen(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) < 17;
        }

        /// <summary>
        /// Check of de dealer moet passen.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns> mag passen of niet.</returns>
        private bool MoetDeDealerPassen(Hand hand) => this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 17 && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 21;

        /// <summary>
        /// Check als de dealer meer dan 21 punten heeft dan maak de situatie van de hand Dood.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns>Heeft de dealer meer dan 21 punten of niet.</returns>
        private bool IsDealerDood(Hand hand) => this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) > 21;

        /// <summary>
        /// Controleer aan de hand van de dealer.
        /// </summary>
        /// <param name="dealerHand">De hand van de dealer.</param>
        private void BehandelDeDealer(Hand dealerHand)
        {
            while (this.MoetDeDealerKopen(dealerHand))
            {
                this.spel.GeefDeHandEenKaart(dealerHand, this.tafel.StapelKaarten);
            }

            if (this.MoetDeDealerPassen(dealerHand))
            {
                dealerHand.ChangeStatus(HandStatussen.Gepassed);
            }
            else if (this.IsDealerDood(dealerHand))
            {
                dealerHand.ChangeStatus(HandStatussen.IsDood);
            }
        }

        /// <summary>
        /// Check de punten.
        /// Geef de winnaar fiches.
        /// Neem van de losser fiches.
        /// beeind het spel.
        /// </summary>
        /// <param name="handen">De handen als lijst.</param>
        private void BeeindHetSpel(List<Hand> handen)
        {
            int waardeVanDeDealerHand = this.WaardeVanDeDealerHand(handen);
            foreach (Hand hand in handen)
            {
                if (hand.HuidigeSpeler() != null)
                {
                    if (hand.Status != HandStatussen.BlackJeck && hand.Status != HandStatussen.IsDood && hand.Status != HandStatussen.Gestopt)
                    {
                        if (waardeVanDeDealerHand <= 21)
                        {
                            this.DefinieerResultaten(hand, waardeVanDeDealerHand);
                        }
                        else
                        {
                            hand.ChangeStatus(HandStatussen.Gewonnen);
                        }
                    }

                    this.CloseHand(hand);
                }
            }
        }

        /// <summary>
        /// Geef de waarde van de hand van de dealer.
        /// </summary>
        /// <param name="handen">Handen van het spel.</param>
        /// <returns>De waarde van de dealers hand.</returns>
        private int WaardeVanDeDealerHand(List<Hand> handen)
        {
            foreach (Hand hand in handen)
            {
                if (hand.Persoon != hand.HuidigeSpeler())
                {
                    return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten);
                }
            }

            return 0;
        }

        /// <summary>
        /// Calculate Hoe veel score er in de hand van de speler staat.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <returns>De score van de kaarten die op de hand van de speler staat. </returns>
        private int WaardeVanDeSpelerHand(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten);
        }

        /// <summary>
        /// betaal aan de hand de fiches die de speler heeft gwonnen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="betaalAanHand">Het bedrag die moet betalen worden.</param>
        private void FichesVerdienen(Hand hand, double betaalAanHand)
        {
            if (betaalAanHand == 1.5)
            {
                double keerEnHalfUit = hand.Inzet.WaardeVanDeFiches * 1.5;
                int keerEnHalfUitWordtBetaald = (int)keerEnHalfUit;
                hand.Inzet.GeefMeFischesTerWaardeVan(keerEnHalfUitWordtBetaald, 2, true);
            }

            hand.Inzet.Add(this.tafel.Fiches.GeefMeFischesTerWaardeVan((int)betaalAanHand));
        }

        /// <summary>
        /// Neem de fiches van de hand uit.
        /// </summary>
        /// <param name="hand">De hand.</param>
        private void VerzameelDeFiches(Hand hand)
        {
            this.communicator.Tell(hand.HuidigeSpeler(), Meldingen.GeenActie, $"{hand.Persoon.Naam} je hebt {hand.Inzet.WaardeVanDeFiches} verliezen.");
            this.tafel.Fiches.Add(hand.Inzet.GeefMeFischesTerWaardeVan(hand.Inzet.WaardeVanDeFiches));
        }

        /// <summary>
        /// Contreel of de speler is gewonnen of niet. Change de situatie van de hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="waardeVanDeDealerHand">De waarde van de hand van de dealer.</param>
        private void DefinieerResultaten(Hand hand, int waardeVanDeDealerHand)
        {
            int waardeVanDeSpeler = this.WaardeVanDeSpelerHand(hand);

            if (waardeVanDeSpeler < waardeVanDeDealerHand)
            {
                hand.ChangeStatus(HandStatussen.Verloren);
                this.communicator.Tell(hand.HuidigeSpeler(), Meldingen.GeenActie, $"{hand.Persoon.Naam} je bent helaas verloren want je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
            }
            else if (waardeVanDeSpeler == waardeVanDeDealerHand)
            {
                hand.ChangeStatus(HandStatussen.OnHold);
                this.communicator.Tell(hand.HuidigeSpeler(), Meldingen.GeenActie, $"{hand.Persoon.Naam} Je mag wachten totdat het volgend rondje start want je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
            }
            else if (waardeVanDeSpeler > waardeVanDeDealerHand && waardeVanDeSpeler <= 21)
            {
                hand.ChangeStatus(HandStatussen.Gewonnen);
                this.communicator.Tell(hand.HuidigeSpeler(), Meldingen.GeenActie, $"Wat leuk, {hand.Persoon.Naam} Je bent gewonnen want je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
            }
        }

        /// <summary>
        /// Geef elke persoon een plek.
        /// </summary>
        private void BepaalWaarElkeSpelerGaatZitten()
        {
            for (int i = 0; i < this.tafel.Plekken.Length; i++)
            {
                if (this.tafel.Plekken[i].Speler != null)
                {
                    int plek = i + 1;
                }
            }
        }

        /// <summary>
        /// Check of de hand Black jack is, dus moet de hand 21 score hebben.
        /// </summary>
        /// <param name="hand">Een hand.</param>
        /// <returns>Of de hand Black Jack of niet.</returns>
        private bool IsBlackJack(Hand hand)
        {
            if (hand == null)
            {
                throw new ArgumentNullException("Er zijn geen hand staat.");
            }

            if (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) == 21)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Als de hand Black Jack dan change de status van de hand en close de hand.
        /// </summary>
        /// <param name="hand">De hand die Balck Jack wordt.</param>
        private void HandelBlackJack(Hand hand)
        {
            hand.ChangeStatus(HandStatussen.BlackJeck);
            this.CloseHand(hand);
        }

        /// <summary>
        /// Zet de actie tot string.
        /// </summary>
        /// <param name="acties">De actie die wordt omgezet.</param>
        /// <returns>De actie als string.</returns>
        private string ActieTotString(Acties acties)
        {
            return this.actiesHelper.ZetEnumTotStringOm(acties);
        }

        /// <summary>
        /// functie om naar de spelers wat te zeggen
        /// </summary>
        /// <param name="spelers"></param>
        /// <param name="melding"></param>
        /// <returns></returns>
        private string TellToPlayers(List<Speler> spelers, Meldingen melding)
        {
            // todo nog iets verzinnen, voor de verschillende spelers;
            foreach (Speler speler in spelers)
            {
                this.communicator.Tell(speler, melding);
            }
        }
    }
}
