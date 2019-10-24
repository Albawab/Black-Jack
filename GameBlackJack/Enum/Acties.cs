// <copyright file="Acties.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Enum
{
    /// <summary>
    /// Hier staat de beslissingen die de speler mag gebruiken.
    /// </summary>
    public enum Acties
    {
        /// <summary>
        /// Geen actie.
        /// </summary>
        IsDefined,

        /// <summary>
        /// Als de speler zou Koppen.
        /// </summary>
        Kopen,

        /// <summary>
        /// Als de speler zou passen.
        /// </summary>
        Passen,

        /// <summary>
        /// Als de speler zou verdubbelen.
        /// </summary>
        Verdubbelen,

        /// <summary>
        /// Als de speler de hand zou splitsen.
        /// </summary>
        Splitsen,
    }
}
