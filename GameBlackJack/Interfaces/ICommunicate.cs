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
        /// </summary>
        /// <param name="message">De info.</param>
        void TellHand(SpelerHand hand, Meldingen melding);

        void TellPlayer(Speler speler, Meldingen melding);

        /// <summary>
        /// functie om te vragen om fiches
        /// </summary>
        /// <param name="speler">aan wie vraag je het</param>
        /// <param name="minWaarde">de minimale waarde die ingezet moet.</param>
        /// <param name="fiches">indien fiches dan zijn dit de gevraagde fiches.</param>
        /// <returns>true als het is gelukt, false bij een cancel of zo.</returns>
        bool AskFichesInzetten(SpelerHand hand, int minWaarde, out Fiches fiches);

        bool AskWhichAction(SpelerHand hand, out Acties actie);
    }
}
