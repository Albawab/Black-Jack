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
        /// <param name="fichesKleur">De kleur van de fiches.</param>
        public Fiche(FichesKleur fichesKleur)
        {
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
