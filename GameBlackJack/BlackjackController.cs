// <copyright file="BlackjackController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
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
                    hand.ZetStatus(HandStatussen.Gestart);
                    this.spel.VoegEenHandIn(hand);
                }
            }

            // Geef de dealer een hand.
            Hand dealerHand = new Hand(this.tafel.Dealer);
            dealerHand.ZetStatus(HandStatussen.Gestart);
            this.spel.VoegEenHandIn(dealerHand);

            // start
            this.ControleHand();
        }

        /// <summary>
        /// dit kan alleen de eerste keer.
        /// </summary>
        private void GeefIedereHandEenEersteKaart()
        {
            foreach (Hand hand in this.spel.Handen)
            {
                this.GeefDeHandEenKaart(hand);
            }
        }

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
        /// De speler gaat spelen.
        /// </summary>
        private void ControleHand()
        {
            foreach (Hand hand in this.spel.Handen)
            {
                if (hand.HuidigeSpeler() == this.speler)
                {
                    .
                    this.speler = hand.HuidigeSpeler();
                    int ficheWaarde = this.speler.FicheWaardeDeSpelerWilZetten();
                    while (!this.tafel.BepaaltOfDeWaardetussenMaxInzetEnMinInzet(ficheWaarde))
                    {
                        Console.WriteLine("Een onjuiste waarde.");
                        ficheWaarde = this.speler.FicheWaardeDeSpelerWilZetten();
                    }

                    this.speler.ZetFichesBijHandIn(hand, ficheWaarde);
                    this.KaartenVerdelen();
                    int waardeInDeHand = this.pointsCalculator.CalculatePoints(hand.Kaarten);
                    if (this.blackJackPointsCalculator.IsBlackJack(hand.Kaarten))
                    {
                        this.CloseHand(hand, waardeInDeHand);
                    }

                    while (hand.Status == HandStatussen.InSpel)
                    {
                        this.BepaalActiesDieEenSpelerMagDoenOpEenHand(hand);
                    }
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
            switch (actie)
            {
                case Actie.Splitsen:
                    Console.WriteLine("Je mag splitsen. Wil je dat doen J of N?");
                    if (this.speler.CheckAntwoord())
                    {
                        this.speler.HeeftSpelerNogFiches();
                        this.SplitHand(hand);
                    }

                    break;
                case Actie.Verdubbelen:
                    Console.WriteLine("Je mag je inzet Verdubbelen. Wil ja dat doen J of N?");
                    if (this.speler.CheckAntwoord())
                    {
                        this.speler.HeeftSpelerNogFiches();
                        this.VerdubbelenHand(hand);
                    }

                    break;
                case Actie.Kopen:
                    this.GeefDeHandEenKaart(hand);
                    this.BeoordeelHand(hand);
                    break;
                case Actie.Passen:

                    break;
            }

           // BepaalNextStep();
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
            foreach (Kaart kaart in hand.Kaarten)
            {
                nieuweHand.AddKaart(kaart);
            }

            foreach (Fiche fiche in hand.Inzet.ReadOnlyFiches)
            {
                hand.Inzet.Add(fiche);
            }
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
                hand.ZetStatus(HandStatussen.IsDood);
                this.CloseHand(hand, this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten));

                // zet de status van de hand
            }
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="hand"></param>

        private void KeerUit(Hand hand)
        {
        }

 /*       private double BepaalFactorInzet(Hand hand)
        {
            // betaal uit
            switch (hand.Status)
            {
                case Enum.Status.BlackJack:
                    return 1.5;
                    break;
                case Enum.Status.
            }
        }*/

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
        /// Verdeelt de kaarten.
        /// </summary>
        private void KaartenVerdelen()
        {
            this.GeefIedereHandEenEersteKaart();

            // oke iedere hand heeft nu een kaart.
            foreach (Hand hand in this.spel.Handen)
            {
                if (hand.Persoon as Dealer != this.tafel.Dealer)
                {
                    this.GeefDeHandEenKaart(hand);
                }
            }
        }

        /// <summary>
        /// Controleert of de speler mag kopen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Of de speler kan kopen of niet.</returns>
        private bool MagDeSpelerKopen(Hand hand)
        {
            if (hand.Status == HandStatussen.Gestart)
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
            if (hand.Status == HandStatussen.Gestart)
            {
                return true;
            }

            return false;
        }

        private bool MoetDeDealerKopen(Hand hand)
        {
            // moet de bak kopen,
            // <17 kopen
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) < 17;
        }

        private bool MoetDeDealerPassen(Hand hand)
        {
            // moet de bak kopen,
            // <17 kopen
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 17;
        }
    }
}
