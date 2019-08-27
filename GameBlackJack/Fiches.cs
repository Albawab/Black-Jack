// <copyright file="Fiches.cs" company="PlaceholderCompany">
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
        public Fiches(Waarde_Van_Enum waarde, FichesEnum fichesKleur)
        {
            this.Waarde = waarde;
            this.FicheKleur = fichesKleur;
        }

        /// <summary>
        /// Gets or sets de waarde van een fiche.
        /// </summary>
        private Waarde_Van_Enum Waarde { get; set; }

        /// <summary>
        /// Gets or sets de kleur van een fiche.
        /// </summary>
        private FichesEnum FicheKleur { get; set; }
    }
}
