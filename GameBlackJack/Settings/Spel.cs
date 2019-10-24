// <copyright file="Spel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Interface;
    using HenE.GameBlackJack.Settings;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Behandel het spel.
    /// </summary>
    public class Spel
    {
        /// <summary>
        /// De lijst van de spelers die gaan spelen.
        /// </summary>
        private readonly List<Speler> spelers = new List<Speler>();
        private readonly BlackJackPointsCalculator blackJackPointsCalculator = new BlackJackPointsCalculator();
        private readonly ICommunicate communicator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spel"/> class.
        /// </summary>
        /// <param name="communicator">Communicator.</param>
        public Spel(ICommunicate communicator)
        {
            this.communicator = communicator;
            this.HuidigeHand = null;
            this.Handen = new List<Hand>();
        }

        /// <summary>
        ///  Gets geeft de huidige hand van het spel.
        /// </summary>
        public Hand HuidigeHand { get; private set; }

        /// <summary>
        /// Gets de handen.
        /// </summary>
        /// <remarks>Geeft een collectie met unieke handen terug.</remarks>
        public List<Hand> Handen { get; private set; }

        /// <summary>
        /// Gets de spelers die aan het spel zijn.
        /// </summary>
        public List<Speler> Spelers
        {
            get
            {
                List<Speler> spelers = new List<Speler>();
                foreach (Hand hand in this.Handen)
                {
                    if (!hand.IsDealerHand)
                    {
                        SpelerHand spelerhand = hand as SpelerHand;
                        if (!spelers.Contains(spelerhand.Speler))
                        {
                            spelers.Add(spelerhand.Speler);
                        }
                    }
                }

                return spelers;
            }
        }

        /// <summary>
        /// set de huidige hand naar de volgende speelbare hand, als er nog geen hand is gezet, dan de eerste pakken.
        /// als ik de laatste ben, weer terug naar het begin.
        /// </summary>
        /// <returns>de huidige hand of null indien niet gevonden.</returns>
        public Hand GaNaarDeVolgendeSpeelbareHand()
        {
            int indexHuidigehand = 0;

            if (this.HuidigeHand != null)
            {
                for (int index = 0; index < this.Handen.Count; index++)
                {
                    if (this.Handen[index] == this.HuidigeHand)
                    {
                        indexHuidigehand = index;
                    }
                }
            }

            // als ik null ben, geef me dan de eerste speelbase hand;
            for (int i = indexHuidigehand; i < this.Handen.Count; i++)
            {
                if (this.Handen[i].Status == HandStatussen.InSpel)
                {
                    this.HuidigeHand = this.Handen[i];
                    return this.HuidigeHand;
                }
            }

            return null;
        }

        /// <summary>
        /// Voegt een hand toe aan de collectie.
        /// </summary>
        /// <param name="hand">De hand die toegevoegd moet worden.</param>
        public void VoegEenHandToe(Hand hand)
        {
            // null check
            if (hand == null)
            {
                throw new ArgumentNullException("Hand mag niet nuul zijn.");
            }

            // of de hand niet al bestaat in de collectie
            foreach (Hand hand1 in this.Handen)
            {
                if (hand1 == hand)
                {
                    throw new ArgumentException("Die hand bestaat al.");
                }
            }

            hand.ChangeStatus(HandStatussen.InSpel);

            // zit de hand wel in dit spel
            this.Handen.Add(hand);
        }

        /// <summary>
        /// Voegt een hand in in de collectie.
        /// </summary>
        /// <param name="positie">de plaats waar de hand ingevoegd moet worden.</param>
        /// <param name="hand">De hand die ingevoegd moet worden.</param>
        public void VoegEenHandIn(int positie, Hand hand)
        {
            // TODO welke controle moet ik hier doen?
            if (hand == null)
            {
                throw new ArgumentNullException("Hand mag niet nuul zijn.");
            }

            // grootte van de list moet kleiner zijn dan de positie.
            if (this.Handen.Count < positie)
            {
                throw new ArgumentOutOfRangeException("De positie moet kleiner dan list");
            }

            hand.ChangeStatus(HandStatussen.InSpel);
            this.Handen.Insert(positie, hand);
        }

        /// <summary>
        /// Voeg een speler aan het spel toe.
        /// </summary>
        /// <param name="spelerDieWilDeelnemenAanHetSpel">De speler die wordt ingevoegd.</param>
        /// <returns>De hand van de speler.</returns>
        public SpelerHand SpelerToevoegen(Speler spelerDieWilDeelnemenAanHetSpel)
        {
            // een hand wordt aangemaakt,
            SpelerHand hand = new SpelerHand(spelerDieWilDeelnemenAanHetSpel);

            // aan de collectie toegevoegd.
            this.spelers.Add(spelerDieWilDeelnemenAanHetSpel);

            this.VoegEenHandToe(hand);

            // en de hand wordt teruggegeven
            return hand;
        }

        /// <summary>
        /// Verwijdert een speler.
        /// </summary>
        /// <param name="speler">De speler die wordt verwijderd.</param>
        public void SpelerVerwijderen(Speler speler)
        {
            this.Spelers.Remove(speler);
        }

        /// <summary>
        /// Voeg een dealer aan het spel toe.
        /// </summary>
        /// <param name="dealer">De dealer die wordt toegevoegd.</param>
        /// <returns>De hand van de dealer.</returns>
        public DealerHand DealerToevoegen(Dealer dealer)
        {
            // een hand wordt aangemaakt,
            DealerHand hand = new DealerHand(dealer);

            // aan de collectie toegevoegd.
            this.VoegEenHandToe(hand);

            // en de hand wordt teruggegeven
            return hand;
        }

        /// <summary>
        /// Maak de lei schoon.
        /// </summary>
        /// <param name="dealer">De Dealer.</param>
        /// <param name="spelers">De spelers die willen spelen.</param>
        public void InitialiseerHetSpel(Dealer dealer, List<Speler> spelers)
        {
            // wat willen we dan doen.
            // in ieder geval beginnen met een schone lei.
            // dus lege collectie van handen.
            this.Handen.Clear();

            foreach (Speler speler in spelers)
            {
                this.SpelerToevoegen(speler);
            }

            // als laatste de dealer toevoegen
            this.DealerToevoegen(dealer);

            // we beginnenn een nieuw spel, dus even weer resetten.
            this.HuidigeHand = null;
        }

        /// <summary>
        /// Verdubbel de hand.
        /// </summary>
        /// <param name="hand">De hand die verdubbelt wordt.</param>
        /// <param name="stapelKaarten">De stapel kaarten van het spel.</param>
        /// <returns>Heeft de speler het verdubbelt of niet.</returns>
        public bool Verdubbelen(SpelerHand hand, StapelKaarten stapelKaarten)
        {
            // is eigenlijk Kopen
            return this.Kopen(hand, stapelKaarten);
        }

        /// <summary>
        /// Reste de huidige hand tot eerste hand van de lijst van de handen die bij het spel bezig zijn.
        /// </summary>
        public void ResetHanden()
        {
            this.HuidigeHand = this.Handen[0];
            List<SpelerHand> spelerHanden = new List<SpelerHand>();
            for (int index = 0; index < this.Handen.Count; index++)
            {
                spelerHanden.Clear();
                foreach (Hand hand in this.Handen)
                {
                    if (!hand.IsDealerHand)
                    {
                        if (!this.Handen[index].IsDealerHand)
                        {
                            SpelerHand spelerHand = hand as SpelerHand;
                            SpelerHand spelerHand1 = this.Handen[index] as SpelerHand;
                            if (spelerHand1.Speler == spelerHand.Speler)
                            {
                                spelerHanden.Add(spelerHand);
                            }
                        }
                    }
                }

                this.RemoveHamden(spelerHanden);
            }
        }

        /// <summary>
        /// Geef een kaart uit.
        /// </summary>
        /// <param name="hand">De hand die een kaart krijgt.</param>
        /// <param name="stapelKaarten">De stapel kaarten van het sperl.</param>
        /// <returns>Heeft de speler gekocht of niet.</returns>
        public bool Kopen(Hand hand, StapelKaarten stapelKaarten)
        {
            Kaart kaart = stapelKaarten.NeemEenKaart();
            if (kaart != null)
            {
                hand.AddKaart(kaart);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Hoeveel handen de speler heeft.
        /// </summary>
        /// <param name="spelerHand">De hand van de speler.</param>
        /// <returns>De lijst van de handen van de speler.</returns>
        public List<Hand> HandenVanDeSpeler(SpelerHand spelerHand)
        {
            List<Hand> handen = null;
            foreach (Hand hand in this.Handen)
            {
                SpelerHand handspeler = hand as SpelerHand;
                if (handspeler.Speler == spelerHand.Speler)
                {
                    handen.Add(hand);
                }
            }

            return handen;
        }

        /// <summary>
        /// Loopt door de handen van de speler.
        /// Als de speler heeft meer dan een hand , dan verwijdert de reste van de handen en bewaart alleen de eerste hand.
        /// </summary>
        /// <param name="spelerHanden">De lijst van de handen van een speler.</param>
        private void RemoveHamden(List<SpelerHand> spelerHanden)
        {
            if (spelerHanden.Count > 1)
            {
                for (int index = 1; index < spelerHanden.Count; index++)
                {
                    this.Handen.Remove(spelerHanden[index]);
                }
            }
        }
    }
}
