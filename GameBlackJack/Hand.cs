// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using HenE.GameBlackJack.Enum;

namespace HenE.GameBlackJack
{
    /// <summary>
    /// De klas van de hand.
    /// </summary>
    internal class Hand
    {
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
    }
}