// <copyright file="StapelKaarten.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// De klas van de stapel kaarten.
    /// </summary>
    public class StapelKaarten
    {
        /// <summary>
        /// Hier staat de kaarten van de spel.
        /// </summary>
        private List<Kaarten> kaarten = new List<Kaarten>();

        /// <summary>
        /// Gets or sets de kaarten.
        /// </summary>
        private Kaarten Kaarten { get; set; }

        /// <summary>
        /// Neem een kaart van de stapel kaarten.
        /// </summary>
        /// <returns>Kaart.</returns>
        public Kaarten NeemEenKaart()
        {
            Kaarten kaart;
            kaart = this.kaarten.First();
            this.kaarten.Remove(kaart);
            return kaart;
        }

        /// <summary>
        /// De Dealer neemt een kaart van de stapel kaarten.
        /// </summary>
        /// <param name="huidigekaart">De kaart die de dealer heeft genomen.</param>
        public void VerliezKaart(Kaarten huidigekaart)
        {
            this.kaarten.Remove(huidigekaart);
        }
    }
}