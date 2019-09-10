// <copyright file="Status.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Enum
{
    /// <summary>
    /// De status van de speler.
    /// </summary>
    public enum HandStatussen
    {
        /// <summary>
        /// De speler nog niet gestart.
        /// </summary>
        NogNietGestart,

        /// <summary>
        /// Als de speler gestart.
        /// </summary>
        Gestart,

        /// <summary>
        /// als je gepasset.
        /// </summary>
        OnHold,

        /// <summary>
        /// De speler wil stopen.
        /// </summary>
        Gestopt,
    }
}
