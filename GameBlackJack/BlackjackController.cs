// <copyright file="BlackjackController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using HenE.GameBlackJack.Enum;
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
                    this.spel.VoegEenHandIn(hand);
                }
                else
                {
                    break;
                }
            }

            // Geef de dealer een hand.
            Hand dealerHand = new Hand(this.tafel.Dealer);
            this.spel.VoegEenHandIn(dealerHand);

            // start
            this.SpeelHand();
        }

        /// <summary>
        /// dit kan alleen de eerste keer.
        /// </summary>
        private void GeefIedereHandEenEersteKaart()
        {
            foreach (Hand hand in this.spel.Handen)
            {
                this.GeefEenHandEenKaart(hand);
            }
        }

        private void GeefEenHandEenKaart(Hand hand)
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
        private void SpeelHand()
        {
            foreach (Hand hand in this.spel.Handen)
            {
                this.speler = hand.HuidigeSpeler();
                this.KaartenVerdelen();
                int waardeInHand = this.pointsCalculator.CalculatePoints(hand.Kaarten);
                if (this.blackJackPointsCalculator.IsBlackJack(hand.Kaarten))
                {
                    this.CloseHand(hand, waardeInHand);
                }

                while (true)
                {
                    this.MagSpelerDoen(hand, this.speler);
                }
            }
        }

        /// <summary>
        /// Wat mag de speler doen.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="speler">Huidige speler.</param>
        private void MagSpelerDoen(Hand hand, Speler speler)
        {
            // wat wil de speler doen?
            // Dat  hang oop zijn score en kaarten af.
            while (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) > 9 && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) < 11)
            {
                if (speler.WilDoen(Beslissing.Verdubbelen))
                {
                    this.GeefEenHandEenKaart(hand);
                }
            }

            if (hand.Kaarten.Count == 2)
            {
                foreach (Kaart kaart in hand.Kaarten)
                {
                    while (kaart == hand.AndereKaart(kaart))
                    {
                        if (speler.WilDoen(Beslissing.Gesplitst))
                        {
                        }
                    }
                }
            }
        }

        /*
        private void BeoordeelHand(Hand hand)
        {
            if (hand.IsBlackJack())
            {
                hand.VeranderDeStatusVanDeHand(Enum.Status.BlackJack);

                // zet de status van de hand
            }
        }
        */

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
                    // Geef niets aan de dealer.
                }
            }
        }
    }
}
