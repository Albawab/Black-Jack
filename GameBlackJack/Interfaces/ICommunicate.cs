// <copyright file="ICommunicate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Maak contact between de speler en dealer met de controller van het spel.
    /// </summary>
    public interface ICommunicate
    {
        /// <summary>
        /// Een vraag stgellen.
        /// Een antwoord krijgen.
        /// </summary>
        /// <param name="message">De tekst van de vraag.</param>
        /// <returns>Het antwoord die wordt terug gestuurd.</returns>
        string Ask(string message);

        /// <summary>
        /// geef informatie over iets gebeurt.
        /// </summary>
        /// <param name="message">De info.</param>
        void Tell(string message);
    }
}
