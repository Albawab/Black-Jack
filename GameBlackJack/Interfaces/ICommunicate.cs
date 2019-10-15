// <copyright file="ICommunicate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Maak contact between de speler en dealer met de controller van het spel.
    /// </summary>
    public interface ICommunicate
    {
        /// <summary>
        /// geef informatie over iets gebeurt.
        /// Wijs melding naar de juiste methode die de melding toont.
        /// </summary>
        /// <param name="hand">De hand die krijgt een melding.</param>
        /// <param name="melding">De text van een melding.</param>
        void TellHand(SpelerHand hand, Meldingen melding);

        /// <summary>
        /// geef informatie over iets gebeurt.
        /// </summary>
        /// <param name="speler">De speler die een melding krijgt. </param>
        /// <param name="melding">De text van de melding.</param>
        void TellPlayer(Speler speler, Meldingen melding);

        /// <summary>
        /// functie om te vragen om fiches bij de hand te inzetten.
        /// </summary>
        /// <param name="hand">aan wie vraag je het</param>
        /// <param name="minWaarde">de minimale waarde die ingezet moet.</param>
        /// <param name="fiches">indien fiches dan zijn dit de gevraagde fiches.</param>
        /// <returns>true als het is gelukt, false bij een cancel of zo.</returns>
        bool AskFichesInzetten(SpelerHand hand, out int minWaarde);

        bool AskWhichAction(SpelerHand hand, Vragen vragen);

        bool AskFichesKopen(Speler speler, out int vragen);
    }
}
