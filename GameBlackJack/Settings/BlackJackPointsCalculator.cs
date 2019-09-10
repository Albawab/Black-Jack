// <copyright file="BlackJackPointsCalculator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Settings
{
    using System.Collections.Generic;

    /// <summary>
    /// Gaat op het spel controleren .
    /// </summary>
    public class BlackJackPointsCalculator : ICalculatePoints
    {
        private const int BlackJackScore = 21;

        /// <summary>
        /// Die gaat calculate de points.
        /// </summary>
        /// <param name="kaarten">De kaarten die in de hand zijn.</param>
        /// <returns>Hoeveel points in de hand zijn.</returns>
        public int CalculatePoints(List<Kaart> kaarten)
        {
            int result = 0;

            foreach (Kaart kaart in kaarten)
            {
                result += kaart.Waarde;
            }

            return result;
        }

        /// <summary>
        /// Als de speler heeft 21 punten.
        /// </summary>
        /// <param name="kaarten">De kaarten die in de hand zijn.</param>
        /// <returns>Hoeveel punten in de hand zijn.</returns>
        public bool IsBlackJack(List<Kaart> kaarten)
        {
            return kaarten.Count == 2 && this.CalculatePoints(kaarten) == BlackJackScore;
        }
    }
}
