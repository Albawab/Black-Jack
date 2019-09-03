// <copyright file="Fiche.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De gegevins van de fiche.
    /// </summary>
    public class Fiche
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fiche"/> class.
        /// </summary>
        /// <param name="fichesKleure">De kleur van een fiche.</param>
        public Fiche(FichesKleur fichesKleure)
        {
            this.FicheKleur = fichesKleure;
        }

        /// <summary>
        /// Gets de waarde van een fiche.
        /// </summary>
        public FichesWaarde Waarde { get; private set; }

        /// <summary>
        /// Gets de kleur van een fiche.
        /// </summary>
        public FichesKleur FicheKleur { get; private set; }
    }
}
