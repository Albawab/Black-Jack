// <copyright file="Dealer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De class van de dealer.
    /// </summary>
    public class Dealer : Persoon
    {
        private Kaarten kaart;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dealer"/> class.
        /// </summary>
        /// <param name="naam">De naam van de dealer.</param>
        protected Dealer(string naam)
            : base(naam)
        {
            this.Naam = this.Naam;
            this.Hand = this.hand;
        }

        /// <summary>
        /// Gets or sets en sets de kaarten van de dealers.
        /// </summary>
        private Kaarten kaarten { get; set; }

        /// <summary>
        /// Gets or sets de hand van de dealer.
        /// </summary>
        private Hand hand { get; set; }

        /// <summary>
        /// De dealer deelt de begin kaarten.
        /// </summary>
        /// <param name="speler">Aan de speler.</param>
        /// <param name="tafel">Op de tafel.</param>
        /// <param name="kaarten">De kaart .</param>
        /// <returns>De kaart die de dealer aan de hand geeft.</returns>
        public void DeelDeBeginKaarten(Speler speler, Tafel tafel, StapelKaarten stapelKaarten)
        {
            foreach (Hand hand in ...)
            {
                Kaarten = this.kaarten;
                this.kaarten = stapelKaarten.NeemEenKaart();
                this.hand.ZetEenKaart(this.kaarten);
            }

        }

        /// <summary>
        /// De dealer deelt de tweede rondje van de kaarten.
        /// </summary>
        /// <param name="speler">Aan de speler.</param>
        /// <param name="tafel">Op de tafel.</param>
        /// <param name="kaarten">De kaart .</param>
        /// <returns>De kaart die de dealer aan de hand geeft.</returns>
        public void DeelDeTweedeRondjeVanDeKaarten(Speler speler, Tafel tafel, StapelKaarten stapelKaarten)
        {
            Kaarten = this.kaarten;
            foreach (Hand hand in ...)
            {
                if (this.hand != this.hand)
                {
                    this.kaarten = stapelKaarten.NeemEenKaart();
                    this.hand.ZetEenKaart(this.kaarten);
                    this.kaart = this.kaarten;
                }
                else
                {
                    while (this.hand.waarde >= 16 && this.hand.waarde <= 21)
                    {
                        this.hand.ZetEenKaart(this.kaarten);
                        this.kaart = this.kaarten;
                    }
                }
            }

        }

        /// <summary>
        /// De dealer neemt een kaart van de stapel kaarten.
        /// </summary>
        /// <returns></returns>
        public Kaarten KrijgEenKaart()
        {
            Kaarten kaart;
            kaart = stapelKaarten.NeemEenKaart();
            return kaart;
        }

        /// <summary>
        /// De dealer neemt een fiches vanuit de de fiches bak.
        /// </summary>
        /// <param name="fiches">De fiche.</param>
        /// <returns>Een fiche.</returns>
        public Waarde_Van_Enum NeemEenFiche(int hetBedrag)
        {
            switch (hetBedrag)
            {
                case 10:
                    return Waarde_Van_Enum.Tien;
                case 15:
                    return Waarde_Van_Enum.Vijfentwintig;
                case 20:
                    return Waarde_Van_Enum.Twintig;
                case 25:
                    return Waarde_Van_Enum.Vijfentwintig;
                default:
                    break;
            }
        }
    }
}
