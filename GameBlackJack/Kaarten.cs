// <copyright file="Kaarten.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De kals van de kaarten.
    /// </summary>
    public class Kaarten
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Kaarten"/> class.
        /// </summary>
        /// <param name="kleur">De kleur van de kaart.</param>
        /// <param name="teken">Het teken van de kaart.</param>
        /// <param name="waarde">De waarde van de kaart.</param>
        public Kaarten(KaartKleur kleur, TekenVanKaart teken, KaartWaarde waarde)
        {
            this.KaartKleur = kleur;
            this.KaartWaarde = waarde;
            this.TekenKaart = teken;
        }

        /// <summary>
        /// Gets or Sets De kleur van een kaart.
        /// </summary>
        public KaartKleur KaartKleur { get; set; }

        /// <summary>
        /// Gets or Sets De waarde van een kaart.
        /// </summary>
        public KaartWaarde KaartWaarde { get; set; }

        /// <summary>
        /// Gets or Sets De Teken van een kaart.
        /// </summary>
        public TekenVanKaart TekenKaart { get; set; }
    }
}