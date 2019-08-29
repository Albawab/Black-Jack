﻿// <copyright file="Fiches.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De kals van de fiches.
    /// </summary>
    public class Fiches
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fiches"/> class.
        /// </summary>
        /// <param name="bedrag">Het bedrag.</param>
        /// <param name="fichesKleur">De kleur van de fiches.</param>
        public Fiches(FichesWaarde bedrag, FichesKleur fichesKleur)
        {
            this.Waarde = bedrag;
            this.FicheKleur = fichesKleur;
        }

        /// <summary>
        /// Gets or sets de waarde van een fiche.
        /// </summary>
        public FichesWaarde Waarde { get; set; }

        /// <summary>
        /// Gets or sets de kleur van een fiche.
        /// </summary>
        private FichesKleur FicheKleur { get; set; }
    }
}
