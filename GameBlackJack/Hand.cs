// <copyright file="Hand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De klas van de hand.
    /// </summary>
    public class Hand
    {
        private readonly IList<Fiches> fiches = new List<Fiches>();
        private readonly IList<Kaarten> kaartens = new List<Kaarten>();

        /// <summary>
        /// Gets or sets De spelers.
        /// </summary>
        private Speler Speler { get; set; }

        /// <summary>
        /// Gets or sets de kaarten.
        /// </summary>
        private Kaarten Kaarten { get; set; }

        /// <summary>
        /// Gets or sets de status van de hand.
        /// </summary>
        private Status StatusVanDeHand { get; set; }

        /// <summary>
        /// Voeg de fiches er in.
        /// </summary>
        /// <param name="fiche">Een fiche.</param>
        public void VoegEenFichesIn(Fiches fiche)
        {
            this.fiches.Add(fiche);
        }

        /// <summary>
        /// Voeg de fiches er in.
        /// </summary>
        /// <param name="kaart">Een Kaart.</param>
        public void VoegKaartIn(Kaarten kaart)
        {
            this.kaartens.Add(kaart);
        }

        /// <summary>
        /// De fiches in de hand van de speler.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>De fiches.</returns>
        public Fiches FichesInHand(Hand hand)
        {
            foreach (Fiches fiche in this.fiches)
            {
                if (hand.fiches == fiche)
                {
                    return fiche;
                }
            }

            return null;
        }
    }
}