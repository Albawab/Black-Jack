// <copyright file="FichesBak.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Tafel
{
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De klas van de fiches bak.
    /// </summary>
    public class FichesBak
    {
        /// <summary>
        /// De fiches die in de fichesBak zitten.
        /// </summary>
        private IList<Fiches> fiches = new List<Fiches>();

        /// <summary>
        /// Deze method geeft een fiche aan de dealer.
        /// verwijdeert de fiches vanuit de list van de fiches.
        /// </summary>
        /// <param name="huidigeFiche">De fiche die de speler wil kopen.</param>
        /// <returns>De fiche.</returns>
        public Fiches NeemEenFiche(Fiches huidigeFiche)
        {
            Fiches eenFiche = null;
            foreach (Fiches fiche in this.fiches)
            {
                if (huidigeFiche == fiche)
                {
                    eenFiche = fiche;
                    break;
                }
            }

            return eenFiche;
        }

        /// <summary>
        /// Zoek op een fiche die de waarde van het gelijk aan de waarde die de speler wil kopen.
        /// </summary>
        /// <param name="waarde">De waarde.</param>
        /// <returns>Een fiche.</returns>
        public Fiches ZoekEenFiche(FichesWaarde waarde)
        {
            foreach (Fiches fiche in this.fiches)
            {
                if (fiche.Waarde == waarde)
                {
                    return fiche;
                }
            }

            return null;
        }
    }
}
