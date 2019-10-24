// <copyright file="DealerHand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    /// <summary>
    /// Heeft de kaarten en de fiches van de speler en ook heeft eigen situatie.
    /// </summary>
    public class DealerHand : Hand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DealerHand"/> class.
        /// </summary>
        /// <param name="dealer">De dealer van het spel.</param>
        public DealerHand(Dealer dealer)
        {
        }

        /// <inheritdoc/>
        public override bool IsDealerHand
        {
            get
            {
                return true;
            }
        }
    }
}