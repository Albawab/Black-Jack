// <copyright file="Spel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Behandel het spel.
    /// </summary>
    public class Spel
    {
        /// <summary>
        /// De lijst van de spelers die gaan spelen.
        /// </summary>
        private List<Speler> spelers = new List<Speler>();
        private int huidigeIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spel"/> class.
        /// </summary>
        public Spel()
        {
            this.Handen = new List<Hand>();
        }

        /// <summary>
        /// Gets de handen.
        /// </summary>
        /// <remarks>Geeft een collectie met unieke handen terug.</remarks>
        public List<Hand> Handen { get; private set; }

        /// <summary>
        /// Gets de next hand.
        /// </summary>
        public Hand NextHand
        {
            get
            {
                // geef de eerst hand terug waarvan de status inSpel is
                this.huidigeIndex++;
                return this.Handen[this.huidigeIndex];
            }
        }

        /// <summary>
        /// Voegt een hand toe aan de collectie.
        /// </summary>
        /// <param name="hand">De hand die toegevoegd moet worden.</param>
        public void VoegEenHandToe(Hand hand)
        {
            // TODO welke controle moet ik hier doen?
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

            this.Handen.Insert(positie, hand);
        }

        /// <summary>
        /// Voeg een speler aan het spel toe.
        /// </summary>
        /// <param name="spelerDieWilDeelnemenAanHetSpel">De speler die wordt ingevoegd.</param>
        /// <returns>De hand van de speler.</returns>
        public Hand SpelerToevoegen(Speler spelerDieWilDeelnemenAanHetSpel)
        {
            // een hand wordt aangemaakt,
            Hand hand = new Hand(spelerDieWilDeelnemenAanHetSpel);

            // aan de collectie toegevoegd.
            this.spelers.Add(spelerDieWilDeelnemenAanHetSpel);

            // en de hand wordt teruggegeven
            spelerDieWilDeelnemenAanHetSpel.ZetFichesBijHandIn(hand);
            return hand;
        }

        /// <summary>
        /// Voeg een dealer aan het spel toe.
        /// </summary>
        /// <param name="dealer">De dealer die wordt toegevoegd.</param>
        /// <returns>De hand van de dealer.</returns>
        public Hand DealerToevoegen(Dealer dealer)
        {
            // een hand wordt aangemaakt,
            Hand hand = new Hand(dealer);

            // aan de collectie toegevoegd.
            this.Handen.Add(hand);

            // en de hand wordt teruggegeven
            return hand;
        }

        /// <summary>
        /// Splits de hand.
        /// </summary>
        /// <param name="handDieGesplitstMoetWorden">De hand die wordt gesplitst.</param>
        /// <returns>De hand.</returns>
        public Hand SplitsHand(Hand handDieGesplitstMoetWorden)
        {
            Hand nieuweHand = null;

            // zoek de postitie va de handDieGesplitstMoetWorden
            for (int index = 0; index < this.Handen.Count; index++)
            {
                if (this.Handen[index] == handDieGesplitstMoetWorden)
                {
                    // clone de oudehand
                    nieuweHand = handDieGesplitstMoetWorden.Splits();
                    if (index == this.Handen.Count)
                    {
                        // dan moet ik toevoegen
                        this.Handen.Add(nieuweHand);
                    }
                    else
                    {
                        // anders voeg je de hand in op de postite van de oude + 1
                        this.Handen.Insert(index, nieuweHand);
                    }
                }
            }

            return nieuweHand;
        }

        /// <summary>
        /// Maak de lei schoon.
        /// </summary>
        /// <param name="dealer">De Dealer.</param>
        /// <param name="spelers">De spelers die willen spelen.</param>
        public void Start(Dealer dealer, List<Speler> spelers)
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
        }

        public void Close()
        {
            // oke, roepe close tegen alle handen
            // geen idee iof de hand als gesloten is, maar dat boiet mij niet
            // ik doe gewoon mijn ding
            // todo
        }
    }
}
