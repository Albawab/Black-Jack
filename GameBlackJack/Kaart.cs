﻿// <copyright file="Kaart.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De kals van de kaarten.
    /// </summary>
    public class Kaart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Kaart"/> class.
        /// </summary>
        /// <param name="kleur">De kleur van de kaart.</param>
        /// <param name="teken">Het teken van de kaart.</param>
        public Kaart(KaartKleur kleur, KaartTeken teken)
        {
            this.Kleur = kleur;
            this.Teken = teken;
        }

        /// <summary>
        /// Gets De kleur van een kaart.
        /// </summary>
        public KaartKleur Kleur { get; private set; }

        /// <summary>
        /// Gets De Teken van een kaart.
        /// </summary>
        public KaartTeken Teken { get; private set; }

        /// <summary>
        /// Change the kaart to string.
        /// </summary>
        /// <returns>De kleur en het teken van de kaart als string.</returns>
        public override string ToString()
        {
            return $"{this.Kleur} {this.Teken}";
        }
    }
}