// <copyright file="Tafels.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System.Collections.Generic;

    /// <summary>
    /// De klas van de tafels.
    /// </summary>
    public class Tafels
    {
        private readonly List<Plek> pleks = new List<Plek>();

        /// <summary>
        /// Gets or sets De dealer van het spel.
        /// </summary>
        private Dealer Dealer { get; set; }

        /// <summary>
        /// Gets or sets De speler.
        /// </summary>
        private Speler Spelers { get; set; }

        /// <summary>
        /// Gets or sets De fiches.
        /// </summary>
        private Fiches FichesBak { get; set; }

        /// <summary>
        /// Gets or Sets De stapel van de kaarten.
        /// </summary>
        private StapelKaarten StapelKaarten { get; set; }

        /// <summary>
        /// Gets or sets minimale bedrag wat op deze tafel ingezet moet worden.
        /// </summary>
        private int MinimalnZet { get; set; }

        /// <summary>
        /// Gets or sets minimale bedrag wat op deze tafel ingezet moet worden.
        /// </summary>
        private int MaximaleInZet { get; set; }

        /// <summary>
        /// Deze Plek beschikbaar of niet.
        /// </summary>
        /// <param name="dePlek">De plek.</param>
        /// <returns>Vrij of niet.</returns>
        public bool IsDezePlekVrij(int dePlek)
        {
            foreach (Plek plek in this.pleks)
            {
                if (plek.DePlek == dePlek)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Doe deze plek niet meer beschikbaar.
        /// </summary>
        /// <param name="eenPlek">De plek.</param>
        /// <returns>Deze plek.</returns>
        public Plek DezePlekIsNietMeerVrij(Plek eenPlek)
        {
            this.pleks.Add(eenPlek);
            return eenPlek;
        }

        /// <summary>
        /// Check of de waarde tusse de grens is.
        /// </summary>
        /// <param name="spelerWilzetten">De waarde die de speler wil zetten.</param>
        /// <returns>true of false.</returns>
        public bool BepaalOfHetBedragTussenTweeGrens(int spelerWilzetten)
        {
            if (this.MinimalnZet != spelerWilzetten && this.MaximaleInZet != spelerWilzetten)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// De plek die bezet is op de tafel.
        /// </summary>
        /// <returns>De plek als list.</returns>
        public List<Plek> EenPlek()
        {
            List<Plek> bezetPlek = new List<Plek>();
            foreach (Plek plek in this.pleks)
            {
                if (plek != null)
                {
                    bezetPlek.Add(plek);
                }
            }

            return bezetPlek;
        }
    }
}
