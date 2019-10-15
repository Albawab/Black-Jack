// <copyright file="Hand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Heeft de kaarten en de fiches van de speler en ook heeft eigen situatie.
    /// </summary>
    public class DealerHand : Hand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DealerHand"/> class.
        /// </summary>
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