// <copyright file="Spel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System.Collections.Generic;

    /// <summary>
    /// Behandel het spel.
    /// </summary>
    public class Spel
    {
        private List<Hand> handen = new List<Hand>();

        /// <summary>
        /// Start het spel.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        public void VoegEenHandIn(Hand hand)
        {
            this.handen.Add(hand);
        }

        /// <summary>
        /// Zoek in de handen lijst en geet een hand terug.
        /// </summary>
        /// <returns>Een lijst van handen.</returns>
        public List<Hand> NeemHand()
        {
            List<Hand> terugGeven = new List<Hand>();
            foreach (Hand hand in this.handen)
            {
                terugGeven.Add(hand);
            }

            return terugGeven;
        }
    }
}
