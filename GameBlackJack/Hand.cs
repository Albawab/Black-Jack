// <copyright file="Hand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De klas van de hand.
    /// </summary>
    public class Hand
    {
        private readonly IList<Fiche> fiches = new List<Fiche>();
        private readonly IList<Kaart> kaarten = new List<Kaart>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/> class.
        /// </summary>
        /// <param name="persoon">Huidige persoon.</param>
        public Hand(Persoon persoon)
        {
            if (persoon is null)
            {
                throw new ArgumentNullException("Persoon mag niet leeg zijn.");
            }
        }

        /// <summary>
        /// Gets or sets de punten.
        /// </summary>
        private int Punten{ get; set; }

        /// <summary>
        /// Gets or sets De spelers.
        /// </summary>
        private Speler Speler { get; set; }

        /// <summary>
        /// Gets or sets de kaarten.
        /// </summary>
        private Kaart Kaarten { get; set; }

        /// <summary>
        /// Gets or sets de status van de hand.
        /// </summary>
        private Status StatusVanDeHand { get; set; }

        /// <summary>
        /// Voeg de fiches er in.
        /// </summary>
        /// <param name="fiche">Een fiche.</param>
        public void VoegEenFichesIn(Fiche fiche)
        {
            this.fiches.Add(fiche);
        }

        /// <summary>
        /// Voeg de fiches er in.
        /// </summary>
        /// <param name="kaart">Een Kaart.</param>
        public void VoegKaartIn(Kaart kaart)
        {
            this.kaarten.Add(kaart);
        }

        /// <summary>
        /// De fiches in de hand van de speler.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>De fiches.</returns>
        public Fiche FichesInHand(Hand hand)
        {
            foreach (Fiche fiche in this.fiches)
            {
                if (hand.fiches == fiche)
                {
                    return fiche;
                }
            }

            return null;
        }

        /// <summary>
        /// Geef de lijst van de kaarten terug.
        /// </summary>
        /// <returns>Lijst van de kaarten.</returns>
        public List<Kaart> NeemKaarten()
        {
            List<Kaart> terugKaarten = new List<Kaart>();
            foreach (Kaart kaart in this.kaarten)
            {
                terugKaarten.Add(kaart);
            }

            return terugKaarten;
        }

        /// <summary>
        /// Voeg de punten aan de hand van de speler.
        /// </summary>
        /// <param name="punten">de punten.</param>
        public void AddEenPunten(int punten)
        {
            this.Punten += punten;
        }

        /// <summary>
        /// Als de hand heeft 21 punten dan het is Black Jack.
        /// </summary>
        /// <returns>Black jack of nee.</returns>
        public bool BlackJeck()
        {
            if (this.Punten == 21)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verandert de situatie van de hand tot Blackjack.
        /// </summary>
        public void PutBlackJack()
        {
            this.StatusVanDeHand = Status.BlackJack;
        }
    }
}