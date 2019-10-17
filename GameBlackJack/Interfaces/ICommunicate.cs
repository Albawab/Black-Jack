﻿// <copyright file="ICommunicate.cs" company="PlaceholderCompany">
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
        /// <param name="meerInformatie">Geef aan de spelers meer informatie die zij nodig hebben.</param>
        void TellHand(SpelerHand hand, Meldingen melding, string meerInformatie);

        /// <summary>
        /// geef informatie over iets gebeurt.
        /// </summary>
        /// <param name="speler">De speler die een melding krijgt. </param>
        /// <param name="melding">De text van de melding.</param>
        void TellPlayer(Speler speler, Meldingen melding);

        /// <summary>
        /// geef informatie over iets gebeurt.
        /// </summary>
        /// <param name="speler">De speler die een melding krijgt. </param>
        /// <param name="melding">De text van de melding.</param>
        /// <param name="hand">De hand van een speler.</param>
        /// <param name="meerInformatie">Geef aan de spelers meer informatie die zij nodig hebben.</param>
        void TellPlayer(Speler speler, Meldingen melding, SpelerHand hand, string meerInformatie);

        /// <summary>
        /// functie om te vragen om fiches bij de hand te inzetten.
        /// </summary>
        /// <param name="hand">aan wie vraag je het.</param>
        /// <param name="minWaarde">de minimale waarde die ingezet moet.</param>
        /// <returns>true als het is gelukt, false bij een cancel of zo.</returns>
        bool AskFichesInzetten(SpelerHand hand, out int minWaarde);

        /// <summary>
        /// Vraag de speler om actie te doen.
        /// </summary>
        /// <param name="hand">De huidige hand .</param>
        /// <param name="acties">De lijst van de moegelijkheden die de speler mag van uit kiezen.</param>
        /// <returns>Heef gekozen of niet.</returns>
        int AskWhichAction(SpelerHand hand, List<Acties> acties);

        /// <summary>
        /// Vraag de speler om fiches te kopen.
        /// </summary>
        /// <param name="speler">De speler die gaat fiches kopen.</param>
        /// <param name="waarde">De waarde van fiches die de speler wil kopen.</param>
        /// <returns>Heeft de speler gekocht of niet.</returns>
        bool AskFichesKopen(Speler speler, out int waarde);
    }
}
