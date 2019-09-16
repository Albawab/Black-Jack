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
        /// De hand is wachten.
        /// </summary>
        OnHold,

        /// <summary>
        /// Als de speler heeft 21 punten.
        /// </summary>
        BlackJeck,

        /// <summary>
        /// De hand is gestopt.
        /// </summary>
        Gestopt,

        /// <summary>
        /// Als de hand boven dan 21 score heeft.
        /// </summary>
        IsDood,

        /// <summary>
        /// Als de hand bezig is.
        /// </summary>
        InSpel,

        /// <summary>
        /// Als de hand gepassed wordt.
        /// </summary>
        Gepassed,

        /// <summary>
        /// Als de hand wil verdubbelen.
        /// </summary>
        Versubbelen,

        /// <summary>
        /// Als de speler is gewonnen.
        /// </summary>
        Winnaar,
    }
}
