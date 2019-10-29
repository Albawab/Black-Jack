// <copyright file="BlackjackController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
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
        private readonly KaartenExtensions kaartenExtensions = new KaartenExtensions();
        private readonly Tafel tafel;
        private readonly ICommunicate communicator = null;
        private readonly Spel spel = null;
        private SpelerHand spelerHand = null;
        private DealerHand dealerHand = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackjackController"/> class.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="communicator">De communicator.</param>
        public BlackjackController(Tafel tafel, ICommunicate communicator)
        {
            this.communicator = communicator;
            this.spel = new Spel(communicator);
            this.tafel = tafel;
        }

        /// <summary>
        /// Check of het spel is klaar om te starten.
        /// </summary>
        /// <returns>Klaar of nog niet.</returns>
        public bool Start()
        {
            // check of ik alles heb.
            if (this.tafel.Dealer == null)
            {
                throw new ArgumentNullException("Er is geen dealer.");
            }

            this.BepaalWaarElkeSpelerGaatZitten();
            this.spel.InitialiseerHetSpel(this.tafel.Dealer, this.tafel.Spelers);

            this.StartRonde();
            return true;
        }

        /// <summary>
        /// Vraag de speler om fiches bij de hand te inzetten.
        /// </summary>
        /// <param name="spelerhand">De hand van de speler.</param>
        /// <param name="waarde">De waarde van een fiche.</param>
        /// <returns>Fiches die de speler wil inzetten bij de hand.</returns>
        public bool VraagAanSpelerInzetVoorEenHand(SpelerHand spelerhand, out int waarde)
        {
            if (this.communicator.AskFichesInzetten(spelerhand, out waarde))
            {
                while (waarde > this.tafel.MaximaleInZet || waarde < this.tafel.MinimalenZet)
                {
                    // todo tell.
                    this.communicator.TellPlayer(spelerhand.Speler, Meldingen.OngeldigeInzet);

                    // todo doen we een loop?
                    if (!this.VraagFichesInzetten(spelerhand, waarde))
                    {
                        while (this.tafel.BepaaltOfDeWaardetussenMaxInzetEnMinInzet(waarde))
                        {
                            // Vertel aan de speler het is geen juiste waarde.
                            this.communicator.TellPlayer(spelerhand.Speler, Meldingen.OngeldigeInzet);

                            if (this.VraagFichesInzetten(spelerhand, waarde))
                            {
                                this.communicator.TellHand(this.spelerHand, Meldingen.ToonInzet, string.Empty);
                            }
                            else
                            {
                                // speler wil of kan niet inzetten.
                                return false;
                            }
                        }
                    }
                }

                // Heeft de speler genoeg fiches of niet.
                while (waarde > this.spelerHand.Speler.Fiches.WaardeVanDeFiches)
                {
                    this.communicator.TellPlayer(spelerhand.Speler, Meldingen.GeenFiches);
                    if (!this.FichesKopen(spelerhand.Speler))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// De speler zet een fiche in bij de hand in.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="waarde">De waarde die de speler wil bij de hand .</param>
        /// <returns>Heeft fiches of niet.<returns>
        public bool ZetFichesBijHandIn(SpelerHand hand, int waarde)
        {
            Fiches fiches = hand.Speler.Fiches.GeefMeFischesTerWaardeVan(waarde, 50, true);
            if (fiches == null)
            {
                return false;
            }
            else
            {
                hand.Inzet.Add(fiches);
                return true;
            }
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

            this.spel.ResetHanden();

            if (this.Beginnen())
            {
                while (this.spel.GaNaarDeVolgendeSpeelbareHand() != null)
                {
                    Hand spelerHand = this.spel.HuidigeHand;
                    this.spelerHand = spelerHand as SpelerHand;

                    if (spelerHand.Status != HandStatussen.BlackJeck && spelerHand.Status != HandStatussen.IsDood)
                    {
                        List<Acties> mogelijkActies = this.ControleerHand(spelerHand);
                        if (!spelerHand.IsDealerHand)
                        {
                            // Check of de speler Black jack.
                            if (this.IsBlackJack(spelerHand))
                            {
                                this.HandelBlackJack(this.spelerHand);
                                this.communicator.TellHand(spelerHand, Meldingen.KaartenVanDeHand, string.Empty);
                                this.communicator.TellHand(spelerHand, Meldingen.BlackJack, string.Empty);

                                // this.TellToPlayers(this.tafel.Spelers, Meldingen.BlackJack, spelerHand, string.Empty);
                            }

                            if (mogelijkActies.Count == 0)
                            {
                                // dan kan ik niks en ga ik naar de volgende hand. Bijv, omdat de hand is gesloten.
                                this.communicator.TellHand(this.spelerHand, Meldingen.GeenActie, string.Empty);
                                continue;
                            }

                            while ((mogelijkActies.Count > 0 && spelerHand.Status == HandStatussen.InSpel)
                                || spelerHand.Status == HandStatussen.Verdubbelen
                                || spelerHand.Status == HandStatussen.Gesplitst
                                || spelerHand.Status == HandStatussen.Gekochtocht)
                            {
                                if (mogelijkActies.Count == 1)
                                {
                                    this.ProcessActie(this.spelerHand, mogelijkActies[0]);
                                }
                                else
                                {
                                    // er zijn meerdere acties mogelijk, vraag aan de speler wat hij/zij wil.
                                    Acties gekozenActie = this.AskActie(mogelijkActies, this.spelerHand);
                                    if (this.ProcessActie(this.spelerHand, gekozenActie))
                                    {
                                        // Zoek de acties.
                                        mogelijkActies = this.ControleerHand(spelerHand);
                                    }
                                    else
                                    {
                                        // Als de speler mag de actie niet doen dan verwijdert dit actie.
                                        mogelijkActies.Remove(gekozenActie);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (this.MagDealerBehandelen())
                            {
                                // start behandelen met de dealer.
                                this.dealerHand = spelerHand as DealerHand;
                                this.BehandelDeDealer(this.dealerHand);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                this.BeeindHetSpel(this.spel.Handen);

                if (this.AskNieuwRondje(this.tafel.Spelers))
                {
                    this.StartRonde();
                }
                else
                {
                    // this.communicator.TellPlayer(this.spelerHand.Speler, Meldingen.ToonFichesInPortemonnee);
                    this.communicator.SleuitHetSpel();
                }
            }
        }

        /// <summary>
        /// Doet wat de speler wil doen.
        /// </summary>
        /// <param name="huidigeHand">De hand van de speler.</param>
        /// <param name="actie">De actie die de speler heeft gekozen.</param>
        /// <returns> heeft een actie gedaan of niet.</returns>
        private bool ProcessActie(SpelerHand huidigeHand, Acties actie)
        {
            // this.spel.PrintMessage(huidigeHand);
            if (this.VoerActieUit(this.spelerHand, actie))
            {
                if (this.blackJackPointsCalculator.CalculatePoints(this.spelerHand.Kaarten) > 21)
                {
                    this.communicator.TellHand(huidigeHand, Meldingen.KaartenVanDeHand, string.Empty);
                    this.communicator.TellHand(huidigeHand, Meldingen.YouDied, string.Empty);

                    // this.TellToPlayers(this.spel.Spelers, Meldingen.YouDied, this.spelerHand, string.Empty);
                    this.spelerHand.ChangeStatus(HandStatussen.IsDood);
                }
                else if (this.blackJackPointsCalculator.CalculatePoints(this.spelerHand.Kaarten) == 21)
                {
                    this.communicator.TellHand(huidigeHand, Meldingen.KaartenVanDeHand, string.Empty);
                    this.communicator.TellHand(huidigeHand, Meldingen.GeenActie, string.Empty);

                    this.spelerHand.ChangeStatus(HandStatussen.Gepassed);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// De eerste processen van het spel voor dat het spel start.
        /// Vraag de speler om fiches bij de hand te inzetten.
        /// Geef elke hand eerste kaart.Ook geeft allen de spelers tweede kaart.
        /// Dus de dealer krijgt een kaart.
        /// </summary>
        private bool Beginnen()
        {
            foreach (Speler speler in this.tafel.Spelers)
            {
                int waardeVanDeInzetten = 0;

                // todo, loopje
                while (speler.Fiches.WaardeVanDeFiches < speler.HuidigeTafel.MinimalenZet)
                {
                    this.communicator.TellPlayer(speler, Meldingen.VoorwaardenTafelFiches);
                    if (!this.FichesKopen(speler))
                    {
                        // Wil niet mee doen.
                        // de speler wil niet inzetten en doet dus niet mee
                        this.communicator.TellPlayer(speler, Meldingen.GeenFiches);
                        if (this.AskNieuwRondje(this.tafel.Spelers))
                        {
                            this.StartRonde();
                            return false;
                        }
                        else
                        {
                            this.communicator.TellPlayer(speler, Meldingen.Gestopt);
                            this.communicator.SleuitHetSpel();
                            return false;
                        }
                    }
                }

                // Vraag om te inzetten.
                foreach (Hand hand in this.spel.Handen)
                {
                    if (!hand.IsDealerHand)
                    {
                        this.spelerHand = hand as SpelerHand;
                        bool magInzetten = true;
                        if (this.spelerHand.Speler == speler)
                        {
                            while (!this.VraagAanSpelerInzetVoorEenHand(this.spelerHand, out waardeVanDeInzetten))
                            {
                                magInzetten = false;
                                break;
                            }

                            // als mag inzetten, dan laat de speler inzetten.
                            if (magInzetten)
                            {
                                this.spelerHand.Inzet.Add(this.spelerHand.Speler.Fiches.GeefMeFischesTerWaardeVan(waardeVanDeInzetten, 50, true));
                                this.communicator.TellHand(this.spelerHand, Meldingen.ToonInzet, string.Empty);
                            }
                            else
                            {
                                // Wil niet mee doen.
                                // de speler wil niet inzetten en doet dus niet mee
                                this.communicator.TellPlayer(speler, Meldingen.GeenFiches);
                                if (this.AskNieuwRondje(this.tafel.Spelers))
                                {
                                    this.StartRonde();
                                    return false;
                                }
                                else
                                {
                                    this.communicator.TellPlayer(speler, Meldingen.Gestopt);
                                    this.communicator.SleuitHetSpel();
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            // geef elke hand een kaart
            foreach (Hand hand in this.spel.Handen)
            {
                Kaart kaart = this.tafel.StapelKaarten.NeemEenKaart();
                if (kaart != null)
                {
                    hand.AddKaart(kaart);
                    if (!hand.IsDealerHand)
                    {
                        this.communicator.TellHand(hand, Meldingen.KaartenVanDeHand, string.Empty);
                    }

                    // this.TellToPlayers(this.spel.Spelers, Meldingen.KaartenVanDeHand, hand, string.Empty);
                }
                else
                {
                    // Als er geen kaarten in de stapel kaarten staan.
                    this.tafel.StapelKaarten = StapelKaartenFactory.CreateBlackJackKaarten(2);
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
                        // Als er geen kaarten in de stapel kaarten staan.
                        this.tafel.StapelKaarten = StapelKaartenFactory.CreateBlackJackKaarten(2);
                    }
                }

                if (!hand.IsDealerHand)
                {
                    SpelerHand spelerhand = hand as SpelerHand;

                    // Dan laat de andere spelers weten wat is gebeurt.
                    //  this.TellToPlayers(this.spel.Spelers, Meldingen.KaartenVanDeHand, spelerhand, string.Empty);
                }
            }

            return true;
        }

        /// <summary>
        /// Voer de actie die de speler heeft gekozen uit.
        /// </summary>
        /// <param name="hand">De huidige hand.</param>
        /// <param name="deActie">De actie die de speler wil doen.</param>
        /// <returns>Heeft actie gedaan of niet.</returns>
        private bool VoerActieUit(SpelerHand hand, Acties deActie)
        {
            switch (deActie)
            {
                case Acties.IsDefined:
                    throw new ArgumentNullException("Er is geen actie.");

                case Acties.Splitsen:
                    if (this.SplitsHand(hand) != null)
                    {
                        hand.ChangeStatus(HandStatussen.Gesplitst);
                        return true;
                    }
                    else
                    {
                        this.communicator.TellPlayer(hand.Speler, Meldingen.MagNietSplitsen);
                        return false;
                    }

                case Acties.Verdubbelen:
                    if (this.Verdubbelen(hand))
                    {
                        hand.ChangeStatus(HandStatussen.Verdubbelen);
                        return true;
                    }
                    else
                    {
                        this.communicator.TellPlayer(hand.Speler, Meldingen.MagNietVerdubbelen);
                        return false;
                    }

                case Acties.Kopen:
                    this.spel.Kopen(hand, this.tafel.StapelKaarten);
                    hand.ChangeStatus(HandStatussen.Gekochtocht);
                    return true;

                case Acties.Passen:
                    hand.ChangeStatus(HandStatussen.Gepassed);
                    return true;
            }

            return false;
        }

        /// <summary>
        /// functie om te checken of de speler mag vedubbelen of niet.
        /// Als mag dan ga dat doen.
        /// </summary>
        /// <param name="hand">De hand die wordt verdubbelt.</param>
        /// <returns>Mag verdubbeln of niet.</returns>
        private bool Verdubbelen(SpelerHand hand)
        {
            // ok, de speler heeft gekozen om te verdubbelen
            // heeft hij nog fiche ter waarde van hand?
            while (hand.Inzet.WaardeVanDeFiches > hand.Speler.Fiches.WaardeVanDeFiches)
            {
                this.communicator.TellPlayer(hand.Speler, Meldingen.GeenFiches);
                if (!this.FichesKopen(hand.Speler))
                {
                    // de speler wil niet inzetten en doet dus niet mee
                    this.TellToPlayers(this.tafel.Spelers, Meldingen.GeenFiches, hand, string.Empty);
                    return false;
                }
            }

            if (!this.VraagFichesInzetten(hand, hand.Inzet.WaardeVanDeFiches))
            {
                return false;
            }

            /*           !this.ZetFichesBijHandIn(hand, hand.Inzet.WaardeVanDeFiches))
                       {
                           this.communicator.TellPlayer(this.spelerHand.Speler, Meldingen.NietGelijkFiches);
                           if (this.communicator.AskFichesKopen(hand.Speler, out waardeVanFiches))
                           {
                               hand.Speler.Fiches.Add(this.tafel.Fiches.GeefMeFischesTerWaardeVan(waardeVanFiches, 100, true));
                               this.communicator.TellPlayer(hand.Speler, Meldingen.Verdienen);
                           }
                           else
                           {

                           }
                       }*/

            // inzetten bij de hand
            // oke, koop dan maar een kaart
            return this.spel.Verdubbelen(this.spelerHand, this.tafel.StapelKaarten);
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

            while (this.kaartenExtensions.MagSplitsen(hand))
            {
                acties.Add(Acties.Splitsen);
                break;
            }

            return acties;
        }

        /// <summary>
        /// Vermenigvuldig de waarde van de fisches in de hand met de factor.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="factor">De factor die wordt betaald.</param>
        private void BetaalUit(SpelerHand hand, double factor)
        {
            // todo, ik wil hier naar afronden in het voordeel van de speler
            this.FichesVerdienen(hand, factor);
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
                // Black Jack.
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
        private Acties AskActie(List<Acties> mogelijkActies, SpelerHand huidigeHand)
        {
            this.communicator.TellHand(huidigeHand, Meldingen.KaartenVanDeHand, string.Empty);

            // De actie die de speler wil doen.
            int deActie = this.communicator.AskWhichAction(huidigeHand, mogelijkActies);

            // omdat de lijst start vanaf nummer 0 en de keuze start vanaf nummer 1 moest hier min -1 doen.
            Acties actie = mogelijkActies[deActie - 1];
            mogelijkActies.Remove(mogelijkActies[deActie - 1]);

            // this.TellToPlayers(this.spel.Spelers, Meldingen.ActieGekozen, huidigeHand, Helper.Helper.ChangeEnumToString(actie));
            return actie;
        }

        /// <summary>
        /// Als de hand klaar is dan betaal aan de hand en close die hand.
        /// </summary>
        /// <param name="hand">De hand die klaar is.</param>
        private void BetaalHand(SpelerHand hand)
        {
            if (hand.Status == HandStatussen.BlackJeck || hand.Status == HandStatussen.Gewonnen)
            {
                this.BetaalUit(hand, this.BepaalFactorInzet(hand));
            }
            else if (hand.Status == HandStatussen.OnHold)
            {
                this.communicator.TellHand(hand, Meldingen.Hold, string.Empty);
                hand.Speler.Fiches.Add(hand.Inzet.GeefMeFischesTerWaardeVan(hand.Inzet.WaardeVanDeFiches, 100, true));
            }
            else
            {
                // Als de hand verliezer is dan moet de dealer dit fiches verzamelen van uit dit hand.
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
        /// Controleert de hand van de dealer.
        /// </summary>
        /// <param name="dealerHand">De hand van de dealer.</param>
        private void BehandelDeDealer(DealerHand dealerHand)
        {
            this.TellToPlayers(this.tafel.Spelers, Meldingen.KaartenVanDeHand, dealerHand, string.Empty);
            while (this.MoetDeDealerKopen(dealerHand))
            {
                this.spel.Kopen(dealerHand, this.tafel.StapelKaarten);
                this.TellToPlayers(this.tafel.Spelers, Meldingen.KaartenVanDeHand, dealerHand, string.Empty);
            }

            if (this.MoetDeDealerPassen(dealerHand))
            {
                dealerHand.ChangeStatus(HandStatussen.Gepassed);
                this.TellToPlayers(this.tafel.Spelers, Meldingen.DealerGepassed, dealerHand, string.Empty);
            }
            else if (this.IsDealerDood(dealerHand))
            {
                dealerHand.ChangeStatus(HandStatussen.IsDood);
                this.TellToPlayers(this.tafel.Spelers, Meldingen.DealerDied, dealerHand, string.Empty);
            }
        }

        /// <summary>
        /// Splits de hand.
        /// geef kaarten . en ook geef ficehs aan de hand .
        /// De fiches zijn gelijk op de fiches die bij de hand die wordet gesplist.
        /// De Kaarten zijn gelijk op de Kaarten die bij de hand die wordet gesplist.
        /// </summary>
        /// <param name="handDieGesplitstMoetWorden">De hand die wordt gesplitst.</param>
        /// <returns>De hand.</returns>
        private Hand SplitsHand(SpelerHand handDieGesplitstMoetWorden)
        {
            // ok, de speler heeft gekozen om te verdubbelen
            // heeft hij nog fiche ter waarde van hand?
            while (handDieGesplitstMoetWorden.Inzet.WaardeVanDeFiches > handDieGesplitstMoetWorden.Speler.Fiches.WaardeVanDeFiches)
            {
                this.communicator.TellPlayer(handDieGesplitstMoetWorden.Speler, Meldingen.GeenFiches);
                if (!this.FichesKopen(handDieGesplitstMoetWorden.Speler))
                {
                    return null;
                }
            }

            // als ik hier kom, dan heeft de speler voldoende fiches en kan er gesplist worden.
            SpelerHand nieuweHand = null;

            // zoek de postitie va de handDieGesplitstMoetWorden
            for (int index = 0; index < this.spel.Handen.Count; index++)
            {
                if (this.spel.Handen[index] == handDieGesplitstMoetWorden)
                {
                    // clone de oudehand
                    nieuweHand = handDieGesplitstMoetWorden.Splits();

                    if (index == this.spel.Handen.Count)
                    {
                        // dan moet ik toevoegen
                        this.spel.VoegEenHandToe(nieuweHand);
                    }
                    else
                    {
                        // TODO voeg je de hand in op de postite van de oude + 1
                        this.spel.VoegEenHandIn(index + 1, nieuweHand);
                    }

                    this.spel.Kopen(handDieGesplitstMoetWorden, this.tafel.StapelKaarten);
                    this.spel.Kopen(nieuweHand, this.tafel.StapelKaarten);

                    // ok de hand mag splitst worden en is dus al gesplitst
                    if (nieuweHand != null)
                    {
                        if (!this.VraagFichesInzetten(nieuweHand, handDieGesplitstMoetWorden.Inzet.WaardeVanDeFiches))
                        {
                            return null;
                        }
                    }
                }
            }

            this.communicator.TellPlayer(handDieGesplitstMoetWorden.Speler, Meldingen.NieuweHand);
            return nieuweHand;
        }

        /// <summary>
        /// Check de punten.
        /// Geef de winnaar fiches.
        /// Neem van de losser fiches.
        /// beeindig het spel.
        /// </summary>
        /// <param name="handen">Een lijst met handen.</param>
        private void BeeindHetSpel(List<Hand> handen)
        {
            int waardeVanDeDealerHand = this.WaardeVanDeDealerHand(handen);
            foreach (Hand hand in handen)
            {
                if (!hand.IsDealerHand)
                {
                    SpelerHand spelerHand = hand as SpelerHand;
                    if (spelerHand.Speler != null)
                    {
                        if (hand.Status != HandStatussen.BlackJeck && hand.Status != HandStatussen.IsDood && hand.Status != HandStatussen.Gestopt)
                        {
                            if (waardeVanDeDealerHand <= 21)
                            {
                                this.DefinieerResultaten(spelerHand, waardeVanDeDealerHand);
                            }
                            else
                            {
                                hand.ChangeStatus(HandStatussen.Gewonnen);
                            }
                        }

                        // betaal aan de hand.
                        this.BetaalHand(spelerHand);
                    }
                }
                else
                {
                    // close de hand van de dealer.
                    hand.Close();
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
                DealerHand dealerHand = hand as DealerHand;
                if (dealerHand != null)
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
        private void FichesVerdienen(SpelerHand hand, double betaalAanHand)
        {
            int keerEnHalfUitWordtBetaald = 0;
            if (betaalAanHand == 1.5)
            {
                double keerEnHalfUit = hand.Inzet.WaardeVanDeFiches * 1.5;
                double mathCeiling = Math.Ceiling(keerEnHalfUit);
                keerEnHalfUitWordtBetaald = (int)mathCeiling;
                hand.Inzet.Add(this.tafel.Fiches.GeefMeFischesTerWaardeVan(keerEnHalfUitWordtBetaald, 100, true));
            }
            else
            {
                hand.Inzet.Add(this.tafel.Fiches.GeefMeFischesTerWaardeVan(hand.Inzet.WaardeVanDeFiches, 100, true));
            }

            this.communicator.TellHand(hand, Meldingen.Verdienen, hand.Inzet.WaardeVanDeFiches.ToString());
        }

        /// <summary>
        /// Neem de fiches van de hand uit.
        /// </summary>
        /// <param name="hand">De hand.</param>
        private void VerzameelDeFiches(SpelerHand hand)
        {
            this.TellToPlayers(this.tafel.Spelers, Meldingen.Verliezen, hand, string.Empty);
            this.tafel.Fiches.Add(hand.Inzet.GeefMeFischesTerWaardeVan(hand.Inzet.WaardeVanDeFiches, 50, true));
        }

        /// <summary>
        /// Contreel of de speler is gewonnen of niet. Change de situatie van de hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="waardeVanDeDealerHand">De waarde van de hand van de dealer.</param>
        private void DefinieerResultaten(SpelerHand hand, int waardeVanDeDealerHand)
        {
            int waardeVanDeSpeler = this.WaardeVanDeSpelerHand(hand);

            if (waardeVanDeSpeler < waardeVanDeDealerHand)
            {
                hand.ChangeStatus(HandStatussen.Verloren);
            }
            else if (waardeVanDeSpeler == waardeVanDeDealerHand)
            {
                hand.ChangeStatus(HandStatussen.OnHold);
            }
            else if (waardeVanDeSpeler > waardeVanDeDealerHand && waardeVanDeSpeler <= 21)
            {
                hand.ChangeStatus(HandStatussen.Gewonnen);
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
        private void HandelBlackJack(SpelerHand hand)
        {
            hand.ChangeStatus(HandStatussen.BlackJeck);

            // this.BetaalHand(hand);
        }

        /// <summary>
        /// functie om naar de spelers wat te zeggen.
        /// </summary>
        /// <param name="spelers">De lijst van de spelers.</param>
        /// <param name="melding">De text van de melding.</param>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="meerInformatie">Geef aan de spelers meer informatie die zij nodig hebben.</param>
        private void TellToPlayers(List<Speler> spelers, Meldingen melding, Hand hand, string meerInformatie)
        {
            // todo nog iets verzinnen, voor de verschillende spelers;
            foreach (Speler speler in spelers)
            {
                if (!hand.IsDealerHand)
                {
                    SpelerHand spelerHand = hand as SpelerHand;
                    if (spelerHand.Speler == speler)
                    {
                        this.communicator.TellHand(spelerHand, melding, meerInformatie);
                    }
                    else
                    {
                        this.communicator.TellPlayer(speler, melding, hand, meerInformatie);
                    }
                }
                else
                {
                    this.communicator.TellPlayer(speler, melding, hand, meerInformatie);
                }
            }
        }

        /// <summary>
        /// Check de handen zijn in spel, als ja dan laat de dealer door gaan.
        /// </summary>
        /// <returns>Mag de dealer mee gaan of niet.</returns>
        private bool MagDealerBehandelen()
        {
            foreach (Hand hand in this.spel.Handen)
            {
                if (hand.Status != HandStatussen.BlackJeck && hand.Status != HandStatussen.IsDood && !hand.IsDealerHand)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ask de speler of hij een nieuw rondje wil doen.
        /// </summary>
        /// <param name="spelers">De lijst van de spelers die aan het spel bezig zijn.</param>
        /// <returns>wil de speler een nieuw rondje doen of niet.</returns>
        private bool AskNieuwRondje(List<Speler> spelers)
        {
            foreach (Speler speler in spelers)
            {
                if (this.communicator.AskNieuwRondje(speler))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Vraag de speler om fiches in de hand te inzetten.
        /// </summary>
        /// <param name="hand">De hand die een fiches krijgt. </param>
        /// <param name="inzetten">De waarde die de speler wil inzetten.</param>
        /// <returns>Heeft de speler ingezet of niet.</returns>
        private bool VraagFichesInzetten(SpelerHand hand, int inzetten)
        {
            while (!this.ZetFichesBijHandIn(hand, inzetten))
            {
                this.communicator.TellPlayer(this.spelerHand.Speler, Meldingen.NietGelijkFiches);
                if (!this.FichesKopen(hand.Speler))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Ask de speler om fiches te kopen.
        /// </summary>
        /// <param name="speler">De speler die een fiches gaat kopen.</param>
        /// <returns>Heeft de speler fiches gekocht of niet.</returns>
        private bool FichesKopen(Speler speler)
        {
            int waardeVanFiches;

            // inzet is ok
            // een hand wordt aangemaakt,
            // ok de speler heeft ingezet en wil dus meedoen
            // aan de collectie toegevoegd.
            if (this.communicator.AskFichesKopen(speler, out waardeVanFiches))
            {
                speler.Fiches.Add(this.tafel.Fiches.GeefMeFischesTerWaardeVan(waardeVanFiches, 100, true));
                this.communicator.TellPlayer(speler, Meldingen.Verdienen);
                return true;
            }
            else
            {
                // de speler wil niet inzetten en doetr dus niet mee.
                return false;
            }
        }
    }
}
