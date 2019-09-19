// <copyright file="BlackjackController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Kaarten;
    using HenE.GameBlackJack.Settings;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Controller op het spel.Is het spelr gestart.Vraagt de dealer om iets te doen. vraagt ook de spelet om iets te doen.
    /// </summary>
    public class BlackjackController
    {
        private readonly ICalculatePoints pointsCalculator = new BlackJackPointsCalculator();
        private readonly BlackJackPointsCalculator blackJackPointsCalculator = new BlackJackPointsCalculator();
        private readonly Tafel tafel;
        private readonly KaartenExtensions kaartenHelper = new KaartenExtensions();
        private Spel spel = new Spel();
        private Speler speler = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackjackController"/> class.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        public BlackjackController(Tafel tafel)
        {
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

        private void StartRonde()
        {
            if (this.tafel.Plekken == null)
            {
                throw new ArgumentNullException("Er zijn geen plekken met spelers.");
            }

            this.BepaalWaarElkeSpelerGaatZitten();

            // Geef de dealer een hand.
            Hand dealerHand = new Hand(this.tafel.Dealer);
            dealerHand.ChangeStatus(HandStatussen.InSpel);
            this.spel.VoegEenHandIn(dealerHand);

            // start
            this.ControleerHanden();
        }

        /// <summary>
        /// dit kan alleen de eerste keer.
        /// </summary>
        private void GeefIedereHandEenEersteKaart(Hand hand)
        {
            this.GeefDeHandEenKaart(hand);
        }

        /// <summary>
        /// Geef een kaart aan de hand.
        /// </summary>
        /// <param name="hand">Huidige habd.</param>
        private void GeefDeHandEenKaart(Hand hand)
        {
            // controleer hand
            if (hand == null)
            {
                throw new ArgumentNullException("Er is geen hand.");
            }

            Kaart kaart = this.tafel.StapelKaarten.NeemEenKaart();

            // status van de hand
            Console.WriteLine();
            Console.WriteLine($"{hand.Persoon.Naam} Je krijgt een kaart {kaart.Kleur} van {kaart.Teken}.");
            hand.AddKaart(kaart);
            Console.WriteLine($"{hand.Persoon.Naam} je hebt {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
            Console.WriteLine();
        }

        /// <summary>
        /// Geef een kaart aan de hand en vraagt de speler om een fiche bij de hand inzetten wil.
        /// Check of de hand Black jeck.
        /// Controleer wat de hand heeft.
        /// </summary>
        private void ControleerHanden()
        {
            Hand dealerHand = null;
            foreach (Hand hand in this.spel.Handen)
            {
                this.speler = hand.HuidigeSpeler();
                if (hand.Persoon == hand.HuidigeSpeler())
                {
                    this.BehandelDeSpeler(hand, this.speler);
                }
                else
                {
                    dealerHand = hand;
                    this.KaartenVerdelen(hand);
                }
            }

            this.HandelKaarten();
            this.BehandelDeDealer(dealerHand);
            this.BeeindHetSpel(this.spel.Handen);
        }

        /// <summary>
        /// Doet wat de speler wil doen.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="actie">Wat de speler wil doen.</param>
        private void VoerActiesVanDeSpeleruit(Hand hand, Actie actie)
        {
            this.speler = hand.HuidigeSpeler();
            switch (actie)
            {
                case Actie.Splitsen:
                    this.Splitsen(hand);
                    this.GeefDeHandEenKaart(hand);
                    break;
                case Actie.Verdubbelen:
                    this.Verdubbelen(hand);
                    this.GeefDeHandEenKaart(hand);

                    break;
                case Actie.Kopen:
                    this.Kopen(hand);
                    break;
            }
        }

        /// <summary>
        /// Verdubblen de hand.Fiches bij de hand zetten.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        private void VerdubbelenHand(Hand hand)
        {
            this.speler.ZetFichesBijHandIn(hand);
        }

        /// <summary>
        /// Geef een nieuwe hand met kaarten en fiches.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        private void SplitHand(Hand hand)
        {
            Hand nieuweHand = new Hand(hand.Persoon);
            hand.HuidigeSpeler().VoegEenHandIn(nieuweHand);
            this.spel.VoegEenHandIn(nieuweHand);
            this.GeefDeHandEenKaart(nieuweHand);
        }

        /// <summary>
        /// Bepaal wat de speler mag doen.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        private void BepaalActiesDieEenSpelerMagDoenOpEenHand(Hand hand)
        {
            if (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 9 && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 11 && hand.Kaarten.Count == 2)
            {
                this.VoerActiesVanDeSpeleruit(hand, Actie.Verdubbelen);
            }
            else if (this.kaartenHelper.MagSplitsen(hand))
            {
                this.VoerActiesVanDeSpeleruit(hand, Actie.Splitsen);
            }
            else if (this.MagDeSpelerKopen(hand))
            {
                this.VoerActiesVanDeSpeleruit(hand, Actie.Kopen);
            }
            else if (this.MagDeSpelerPassen(hand))
            {
                this.VoerActiesVanDeSpeleruit(hand, Actie.Passen);
            }
        }

        private void BeoordeelHand(Hand hand)
        {
            if (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) > 21)
            {
                hand.ChangeStatus(HandStatussen.IsDood);
                this.CloseHand(hand);
            }
        }

        /// <summary>
        /// Beaal aan de Hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="wordtBetaal">De waarde die worde gebetaald.</param>
        private void KeerUit(Hand hand, double wordtBetaal)
        {
            int moetBetalenAanHand = 0;
            if (wordtBetaal == 1.5)
            {
                moetBetalenAanHand = hand.Inzet.WaardeVanDeFiches * (int)1.5;
                this.GeefDeHandFiche(hand, moetBetalenAanHand);
            }
            else if (wordtBetaal == 1.0)
            {
                this.GeefDeHandFiche(hand, hand.Inzet.WaardeVanDeFiches);
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
        /// Verdeel de kaarten bij de hand.
        /// </summary>
        private void KaartenVerdelen(Hand hand)
        {
            this.GeefIedereHandEenEersteKaart(hand);

            // oke iedere hand heeft nu een kaart.
            if (hand.Persoon as Dealer != this.tafel.Dealer)
            {
                this.GeefDeHandEenKaart(hand);
            }
        }

        /// <summary>
        /// Controleert of de speler mag kopen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Of de speler kan kopen of niet.</returns>
        private bool MagDeSpelerKopen(Hand hand)
        {
            if (hand.Status == HandStatussen.InSpel)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// controleert of de speler mag passen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Mag de speler passen of niet.</returns>
        private bool MagDeSpelerPassen(Hand hand)
        {
            if (hand.Status == HandStatussen.InSpel)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Als de speler wil passen.Dan changed de status van de hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        private void PassenDeHand(Hand hand)
        {
            hand.ChangeStatus(HandStatussen.Gepassed);
        }

        /// <summary>
        /// Controleer of de dealer mag kopen.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns>Mag de dealer kopen of mag niet.</returns>
        private bool MoetDeDealerKopen(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) < 17;
        }

        /// <summary>
        /// Controleer of de dealer moet passen.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns> mag passen of niet.</returns>
        private bool MoetDeDealerPassen(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 17 && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 21;
        }

        /// <summary>
        /// Check als de dealer meer dan 21 punten heeft dan maak de situatie van de hand Dood.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns>Heeft de dealer meer dan 21 punten of niet.</returns>
        private bool IsDealerDood(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) > 21;
        }

        /// <summary>
        /// Controleer aan de hand van de dealer.
        /// </summary>
        /// <param name="dealerHand">De hand van de dealer.</param>
        private void BehandelDeDealer(Hand dealerHand)
        {
            while (this.MoetDeDealerKopen(dealerHand))
            {
                this.GeefDeHandEenKaart(dealerHand);
            }

            if (this.MoetDeDealerPassen(dealerHand))
            {
                dealerHand.ChangeStatus(HandStatussen.Gepassed);
                Console.WriteLine("Je heeft meer dan 17 punten dus je moet passen.");
            }
            else if (this.IsDealerDood(dealerHand))
            {
                dealerHand.ChangeStatus(HandStatussen.IsDood);
            }
        }

        /// <summary>
        /// Controleer de punten.
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
                            this.BepaalDeWinnaar(hand, waardeVanDeDealerHand);
                        }
                        else
                        {
                            hand.ChangeStatus(HandStatussen.Gewonnen);
                        }

                        this.CloseHand(hand);
                    }
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
        private void GeefDeHandFiche(Hand hand, int betaalAanHand)
        {
            /*     if (betaalAanHand / 5 != 0)
                 {
                     hand.Inzet.GeefMeFischesTerWaardeVan(1, 2, true); ===========================> betaal een keer en half.
                 }*/

            hand.Inzet.Add(this.tafel.Fiches.GeefMeFischesTerWaardeVan(betaalAanHand));
        }

        /// <summary>
        /// Neem de fiches van de hand uit.
        /// </summary>
        /// <param name="hand">De hand.</param>
        private void VerzameelDeFiches(Hand hand)
        {
            this.tafel.Fiches.Add(hand.Inzet.GeefMeFischesTerWaardeVan(hand.Inzet.WaardeVanDeFiches));
        }

        /// <summary>
        /// Controol of wie is gewonnen. Change de situatie van de hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="waardeVanDeDealerHand">De waarde van de hand van de dealer.</param>
        private void BepaalDeWinnaar(Hand hand, int waardeVanDeDealerHand)
        {
            int waardeVanDeSpeler = this.WaardeVanDeSpelerHand(hand);

            if (waardeVanDeSpeler < waardeVanDeDealerHand)
            {
                hand.ChangeStatus(HandStatussen.Verloren);
                Console.WriteLine($"{hand.Persoon.Naam} je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
                Console.WriteLine("Helaas, je bent verloren!");
            }
            else if (waardeVanDeSpeler == waardeVanDeDealerHand)
            {
                hand.ChangeStatus(HandStatussen.OnHold);
                Console.WriteLine($"{hand.Persoon.Naam} je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
                Console.WriteLine("Je mag wachten totdat het volgend rondje start.");
            }
            else if (waardeVanDeSpeler > waardeVanDeDealerHand && waardeVanDeSpeler <= 21)
            {
                hand.ChangeStatus(HandStatussen.Gewonnen);
                Console.WriteLine($"{hand.Persoon.Naam} je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
                Console.WriteLine("Wat leuk, Je bent gewonnen.");
            }
        }

        /// <summary>
        /// Gaat de hand splitsen.
        /// </summary>
        /// <param name="hand">De hand die gesplitst worde.</param>
        private void Splitsen(Hand hand)
        {
            Console.WriteLine(hand.HuidigeSpeler().Naam + " : Je mag splitsen. Wil je dat doen J of N?");
            if (this.speler.CheckAntwoord())
            {
                if (!this.speler.HeeftSpelerNogFiches())
                {
                    this.speler.Koopfiches(this.tafel);
                }

                this.SplitHand(hand);
            }
        }

        /// <summary>
        /// Verdubbel de hand.
        /// </summary>
        /// <param name="hand">De hand die verdubbelt wordt.</param>
        private void Verdubbelen(Hand hand)
        {
            Console.WriteLine(hand.HuidigeSpeler().Naam + " : Je mag je inzet Verdubbelen. Wil ja dat doen J of N?");
            if (this.speler.CheckAntwoord())
            {
                if (!this.speler.HeeftSpelerNogFiches())
                {
                    this.speler.Koopfiches(this.tafel);
                }

                this.VerdubbelenHand(hand);
            }
        }

        /// <summary>
        /// Vraag of de speler wil kopen.
        /// </summary>
        /// <param name="hand">De hand die een kaart wil kopen.</param>
        private void Kopen(Hand hand)
        {
            Console.WriteLine(hand.HuidigeSpeler().Naam + " : Je mag Een kaart Kopen. Wil ja dat doen J of N?");
            if (this.speler.CheckAntwoord())
            {
                this.GeefDeHandEenKaart(hand);
                this.BeoordeelHand(hand);
            }
            else
            {
                this.PassenDeHand(hand);
            }
        }

        /// <summary>
        /// Geef een kaart aan de speler. Vraag de speler om fiches bij zij hant te zetten.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="speler">Huidige speler.</param>
        private void BehandelDeSpeler(Hand hand, Speler speler)
        {
            if (hand.HuidigeSpeler() == speler)
            {
                int ficheWaarde = speler.FicheWaardeDeSpelerWilZetten();
                while (!this.tafel.BepaaltOfDeWaardetussenMaxInzetEnMinInzet(ficheWaarde))
                {
                    Console.WriteLine("Een onjuiste waarde.");
                    ficheWaarde = speler.FicheWaardeDeSpelerWilZetten();
                }

                this.speler.ZetFichesBijHandIn(hand, ficheWaarde);
                Console.WriteLine();
                Console.WriteLine($"{speler.Naam} Je zet bij je hand {hand.Inzet.WaardeVanDeFiches} in.");
            }

            // De dealer deelt een kaart.
            this.KaartenVerdelen(hand);

            int waardeInDeHand = this.pointsCalculator.CalculatePoints(hand.Kaarten);
            if (this.blackJackPointsCalculator.IsBlackJack(hand.Kaarten))
            {
                hand.ChangeStatus(HandStatussen.BlackJeck);
                this.CloseHand(hand);
            }
        }

        /// <summary>
        /// Verdeelt de kaarten tussen de spelers.
        /// </summary>
        private void HandelKaarten()
        {
            foreach (Plek plek in this.tafel.Plekken)
            {
                if (plek.Speler != null)
                {
                    foreach (Hand hand in plek.Speler.Hand) // =======> Hoe kan ik de tweede hand invoegen?
                    {
                        while (hand.Status == HandStatussen.InSpel)
                        {
                            this.BepaalActiesDieEenSpelerMagDoenOpEenHand(hand);
                        }
                    }
                }
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
                    this.GeefElkeSpelerEenHand(this.tafel.Plekken[i].Speler);

                    Console.WriteLine();
                    int plek = i + 1;
                    Console.WriteLine(" Je gaat op de plek " + plek + " aan de tafel zitten");
                }
            }
        }

        /// <summary>
        /// Geef elke speler een hand. En die hand geef een nieuwe situatie.
        /// </summary>
        /// <param name="speler">De speler die een nieuwe hand krijgt.</param>
        private void GeefElkeSpelerEenHand(Speler speler)
        {
            Hand hand = new Hand(speler);
            if (hand.HuidigeSpeler() != null)
            {
                hand.HuidigeSpeler().VoegEenHandIn(hand);
            }

            hand.ChangeStatus(HandStatussen.InSpel);
            this.spel.VoegEenHandIn(hand);
            hand.ChangeStatus(HandStatussen.InSpel);
        }
    }
}
