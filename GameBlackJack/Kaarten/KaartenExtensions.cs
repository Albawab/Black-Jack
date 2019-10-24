// <copyright file="KaartenExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Kaarten
{
    /// <summary>
    /// extensie die kijkt of alle kaarten in de list gelijk zijn, wat betreft het teken.
    /// </summary>
    public class KaartenExtensions
    {
        /// <summary>
        /// Mag de hand splitsen of niet.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Mag splitsen of niet.</returns>
        public bool MagSplitsen(Hand hand)
        {
            if (hand.Kaarten.Count != 2)
            {
                return false;
            }
            else
            {
                return this.ZijnDeTekensgelijk(hand.Kaarten[0], hand.Kaarten[1]);
            }
        }

        /// <summary>
        /// controleert of de teken van de kaarten gelijk zijn.
        /// </summary>
        /// <param name="kaartLeft">Eerst kaart.</param>
        /// <param name="kaartRight">Tweede kaart.</param>
        /// <returns>Zijn gelijk of niet.</returns>
        private bool ZijnDeTekensgelijk(Kaart kaartLeft, Kaart kaartRight)
        {
            return kaartLeft.Teken == kaartRight.Teken;
        }
    }
}