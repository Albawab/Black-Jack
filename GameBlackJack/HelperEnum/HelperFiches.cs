// <copyright file="HelperFiches.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.HelperEnum
{
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Omzetten de waarde tot fiche.
    /// </summary>
    public class HelperFiches
    {
        private FichesBak FichesBak { get; set; }

        /// <summary>
        /// Creat een lijst van de fiches.
        /// </summary>
        /// <returns>List van de fiches waarde.</returns>
        public static List<FichesKleur> GetFichesKleur()
        {
            return new List<FichesKleur>()
            {
                FichesKleur.Blue,
                FichesKleur.Geel,
                FichesKleur.Groen,
                FichesKleur.Rood,
            };
        }

        /// <summary>
        /// Omzetten de waarde die wil de speler kopen tot fiches.
        /// </summary>
        /// <param name="hetBedrag">Het bedrag.</param>
        /// <param name="fichesBak">Het fiches.</param>
        /// <param name="dealer">Huidige dealer.</param>
        /// <returns>Een fiche.</returns>
        public Fiche OmzettenWaardeDieDeSpelerwil_TotEenFiche(int hetBedrag, FichesBak fichesBak, Dealer dealer)
        {
            this.FichesBak = fichesBak;
            FichesWaarde waarde = this.OmzettenWaardeDieDeSpelerwilTotFiche(hetBedrag);
            Fiche fiche = this.FichesBak.ZoekEenFiche(waarde, dealer);

            return fiche;
        }

        /// <summary>
        /// De waarde die de speler wil kopen.
        /// </summary>
        /// <param name="waarde">de waarde.</param>
        /// <returns>de waarde als enum.</returns>
        public FichesWaarde OmzettenWaardeDieDeSpelerwilTotFiche(int waarde)
        {
            switch (waarde)
            {
                case 10:
                    return FichesWaarde.Tien;
                case 15:
                    return FichesWaarde.Vijfentwintig;
                case 20:
                    return FichesWaarde.Twintig;
                case 25:
                    return FichesWaarde.Vijfentwintig;
                default:
                    return FichesWaarde.IsDefined;
            }
        }
    }
}
