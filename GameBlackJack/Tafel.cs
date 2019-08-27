// <copyright file="Tafel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// De klas van de tafel.
    /// </summary>
    public class Tafel
    {
        private List<Plek> pleks = new List<Plek>();

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
        /// Gets or sets minimale bedrag wat op deze tafel ingezet moet worden.
        /// </summary>
        private int MinimalnZet { get; set; }

        /// <summary>
        /// Gets or sets minimale bedrag wat op deze tafel ingezet moet worden.
        /// </summary>
        private int MaximaleInZet { get; set; }

        /// <summary>
        /// Gets or sets Stapel kaarten.
        /// </summary>
        private StapelKaarten StapelKaarten { get; set; }

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

        public bool BepaalOfHetBedragTussenTweeGrens(int spelerWilzetten)
        {
            if (this.MinimalnZet != spelerWilzetten && this.MaximaleInZet != spelerWilzetten)
            {
                return false;
            }

            return true;
        }
    }
}
