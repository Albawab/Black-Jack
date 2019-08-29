// <copyright file="Status.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Enum
{
    /// <summary>
    /// De status van de speler.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Id define.
        /// </summary>
        IsDefined,

        /// <summary>
        /// De speler is Black Jack.
        /// </summary>
        BlackJack,

        /// <summary>
        /// De speler wil stopen.
        /// </summary>
        Gestopt,

        /// <summary>
        /// De speler mag verdubbelen.
        /// </summary>
        Verdubbelen,

        /// <summary>
        /// de speler mag splitsen.
        /// </summary>
        Splitsen,

        /// <summary>
        /// De speler blijft staan.
        /// </summary>
        Staan,
    }
}
