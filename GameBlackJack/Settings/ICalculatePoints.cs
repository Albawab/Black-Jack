// <copyright file="ICalculatePoints.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Settings
{
    using System.Collections.Generic;

    /// <summary>
    /// Deze om punten te verzamelen.
    /// </summary>
    public interface ICalculatePoints
    {
        /// <summary>
        /// verzameel de punten.
        /// </summary>
        /// <param name="kaarten">List of carts.</param>
        /// <returns>punten.</returns>
        int CalculatePoints(List<Kaart> kaarten);
    }
}
