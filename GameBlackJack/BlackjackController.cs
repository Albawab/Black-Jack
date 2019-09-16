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

            // Geef elke speler een hand.
            for (int i = 0; i < this.tafel.Plekken.Length; i++)
            {
                if (this.tafel.Plekken[i].Speler != null)
                {
                    Hand hand = new Hand(this.tafel.Plekken[i].Speler);
                    hand.ChangeStatus(HandStatussen.InSpel);
                    this.spel.VoegEenHandIn(hand);
                    hand.ChangeStatus(HandStatussen.InSpel);
                }
            }

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

            // status van de hand
            hand.AddKaart(this.tafel.StapelKaarten.NeemEenKaart());
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
                if (hand.HuidigeSpeler() == this.speler)
                {
                    if (hand.HuidigeSpeler() != null)
                    {
                        int ficheWaarde = this.speler.FicheWaardeDeSpelerWilZetten();
                        while (!this.tafel.BepaaltOfDeWaardetussenMaxInzetEnMinInzet(ficheWaarde))
                        {
                            Console.WriteLine("Een onjuiste waarde.");
                            ficheWaarde = this.speler.FicheWaardeDeSpelerWilZetten();
                        }

                        this.speler.ZetFichesBijHandIn(hand, ficheWaarde);
                    }

                    // De dealer deelt een kaart.
                    this.KaartenVerdelen(hand);

                    int waardeInDeHand = this.pointsCalculator.CalculatePoints(hand.Kaarten);
                    if (this.blackJackPointsCalculator.IsBlackJack(hand.Kaarten))
                    {
                        hand.ChangeStatus(HandStatussen.BlackJeck);
                        this.CloseHand(hand, waardeInDeHand);
                    }
                }
                else
                {
                    dealerHand = hand;
                }
            }

            foreach (Hand hand in this.spel.Handen)
            {
                if (hand.HuidigeSpeler() != null)
                {
                    while (hand.Status == HandStatussen.InSpel)
                    {
                        this.BepaalActiesDieEenSpelerMagDoenOpEenHand(hand);
                    }
                }
            }

            this.BehandelDeDealer(dealerHand);
            this.BeeindHetSpel(this.spel.Handen);
        }

        /// <summary>
        /// functie om de individuele hand te controleren.
        /// kijk of de waarde van de kaarten
        /// hoeveel punten de hand heeft .
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        private void ControleerHand(Hand hand)
        {
            this.speler = hand.HuidigeSpeler();
            if (hand.HuidigeSpeler() == this.speler)
            {
                if (hand.HuidigeSpeler() != null)
                {
                    int ficheWaarde = this.speler.FicheWaardeDeSpelerWilZetten();
                    while (!this.tafel.BepaaltOfDeWaardetussenMaxInzetEnMinInzet(ficheWaarde))
                    {
                        Console.WriteLine("Een onjuiste waarde.");
                        ficheWaarde = this.speler.FicheWaardeDeSpelerWilZetten();
                    }

                    this.speler.ZetFichesBijHandIn(hand, ficheWaarde);

                    // De dealer deelt een kaart.
                    this.KaartenVerdelen(hand);

                    int waardeInDeHand = this.pointsCalculator.CalculatePoints(hand.Kaarten);
                    if (this.blackJackPointsCalculator.IsBlackJack(hand.Kaarten))
                    {
                        this.CloseHand(hand, waardeInDeHand);
                    }
                }

                while (hand.Status == HandStatussen.InSpel)
                {
                    this.BepaalActiesDieEenSpelerMagDoenOpEenHand(hand);
                }
            }
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
                    Console.WriteLine("Je mag splitsen. Wil je dat doen J of N?");
                    if (this.speler.CheckAntwoord())
                    {
                        if (!this.speler.HeeftSpelerNogFiches())
                        {
                            this.speler.Koopfiches(this.tafel);
                        }

                        this.SplitHand(hand);
                    }

                    break;
                case Actie.Verdubbelen:
                    Console.WriteLine("Je mag je inzet Verdubbelen. Wil ja dat doen J of N?");
                    if (this.speler.CheckAntwoord())
                    {
                        if (!this.speler.HeeftSpelerNogFiches())
                        {
                            this.speler.Koopfiches(this.tafel);
                        }

                        this.VerdubbelenHand(hand);
                        this.GeefDeHandEenKaart(hand);
                        hand.ChangeStatus(HandStatussen.Versubbelen);
                    }

                    break;
                case Actie.Kopen:
                    Console.WriteLine("Je mag je inzet Kopen. Wil ja dat doen J of N?");
                    if (this.speler.CheckAntwoord())
                    {
                        this.GeefDeHandEenKaart(hand);
                        this.BeoordeelHand(hand);
                    }
                    else
                    {
                        this.PassenDeHand(hand);
                    }

                    break;
            }
        }

        /// <summary>
        /// Verdubblen de hand.Fiches bij de hand zetten.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        private void VerdubbelenHand(Hand hand)
        {
            int waardeInHand = hand.Inzet.WaardeVanDeFiches;
            this.speler.ZetFichesBijHandIn(hand, waardeInHand);
        }

        /// <summary>
        /// Geef een nieuwe hand met kaarten en fiches.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        private void SplitHand(Hand hand)
        {
            Hand nieuweHand = new Hand(hand.Persoon);
            this.spel.VoegEenHandIn(nieuweHand);
            this.GeefDeHandEenKaart(nieuweHand);
        }

        /// <summary>
        /// Bepaal wat de speler mag doen.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        private void BepaalActiesDieEenSpelerMagDoenOpEenHand(Hand hand)
        {
            if (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 0 && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 21 && hand.Kaarten.Count == 2)
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
                this.CloseHand(hand, this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten));

                // zet de status van de hand
            }
        }

        /// <summary>
        /// Beaal aan de Hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        private void KeerUit(Hand hand)
        {
            switch (BepaalFactorInzet(hand))
            {
                case 1.5:
                    this.BetaalUit(1.5, hand);
                case 1:
                    this.BetaalUit(1, hand);
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

                case HandStatussen.Winnaar:
                    return 1.0;

                default:
                    return 0;
            }
        }

        private void CloseHand(Hand hand, int score)
        {
            // wat is de score
            // aantal, blackjack of dood
            // moet ik uitbetalen
            // zoja hoeveel
            // betaal uit
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
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 17;
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
            Speler speler;

            foreach (Hand hand in handen)
            {
                if (this.BepaalDeWinnaar(hand, waardeVanDeDealerHand))
                {
                    speler = hand.HuidigeSpeler();
                    this.KeerUit(hand);
                }
            }
        }

        /// <summary>
        /// Bepaal of deze hand heeft gewonnen of niet.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="waardeVanDeDealerHand">De waarde van de hand van de dealer.</param>
        /// <returns>Heeft deze hand gewonnen of niet.</returns>
        private bool BepaalDeWinnaar(Hand hand, int waardeVanDeDealerHand)
        {
            if (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) > waardeVanDeDealerHand && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) < 21)
            {
                return true;
            }

            return false;
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
                if (hand.HuidigeSpeler() == null)
                {
                    return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten);
                }
            }

            return 0;
        }
    }
}
