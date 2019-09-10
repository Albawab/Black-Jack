﻿// <copyright file="Tafel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.SpelSpullen
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Kaarten;

    /// <summary>
    /// Hier staat de spullen van het spel.
    /// </summary>
    public class Tafel
    {
        private readonly Plek[] plekken;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tafel"/> class.
        /// </summary>
        /// <param name="aantalPlekken">Hoe veel plekken aan de tafel .</param>
        /// <param name="fiches">Waar de fiches staat.</param>
        /// <param name="stapelKaarten">Waar de kaarten staat.</param>
        private Tafel(int aantalPlekken, Fiches fiches, StapelKaarten stapelKaarten)
        {
            this.StapelKaarten = stapelKaarten;
            this.Fiches = fiches;
            this.plekken = new Plek[aantalPlekken];

            for (int i = 0; i < this.plekken.Length; i++)
            {
                this.plekken[i] = new Plek();
            }
        }

        /// <summary>
        /// Gets de dealer van het spel.
        /// </summary>
        public Dealer Dealer { get; private set; }

        /// <summary>
        /// Gets de plekken met de spelers die aan de tafel zijn.
        /// </summary>
        public Plek[] Plekken
        {
            get
            {
                return this.plekken;
            }
        }

        /// <summary>
        /// Gets or sets De fiches.
        /// </summary>
        public Fiches Fiches { get; set; }

        /// <summary>
        /// Gets ts De stapel van de kaarten.
        /// </summary>
        public StapelKaarten StapelKaarten { get; private set; }

        /// <summary>
        /// Gets or sets minimale bedrag wat op deze tafel ingezet moet worden.
        /// </summary>
        private int MinimalnZet { get; set; }

        /// <summary>
        /// Gets or sets minimale bedrag wat op deze tafel ingezet moet worden.
        /// </summary>
        private int MaximaleInZet { get; set; }

        /*
        /// <summary>
        /// Add een hand aan de dealer.
        /// </summary>
        /// <param name="dealer">Huidige dealer.</param>
        /// <param name="spel">Huidig spel.</param>
        public void VoegDealerIn(Dealer dealer, Spel spel)
        {
            Hand hand = new Hand(dealer);
            spel.VoegEenHandIn(hand);
        }
        */

            /// <summary>
            /// Maak een nieuwe tafel.
            /// </summary>
            /// <param name="fiches">De fiches die aan de tafel zijn.</param>
            /// <returns>De nieuwe tafel.</returns>
        public static Tafel CreateBlackJackTafel(Fiches fiches)
        {
            Tafel tafel = new Tafel(6, fiches, StapelKaartenFactory.CreateBlackJackKaarten(2));
            return tafel;
        }

        /// <summary>
        /// functie om van dealer te wisselen.
        /// </summary>
        /// <param name="newDealer">nieuwe dealer.</param>
        /// <returns>odue dealer.</returns>
        public Dealer WijzigDealer(Dealer newDealer)
        {
            Dealer oldDealer = this.Dealer;

            this.Dealer = newDealer;
            return oldDealer;
        }

        /// <summary>
        /// Als de speler verlaat de tafel.wordt de plaats vij.
        /// </summary>
        /// <param name="speler">Huidige speler.</param>
        /// <returns>Of de speler nog zit of hij al de plaats heeft verlaten.</returns>
        public bool SpelerVerlaatTafel(Speler speler)
        {
            if (speler == null)
            {
                throw new ArgumentNullException("Speler mag niet null zijn.");
            }

            for (int positie = 0; positie < this.plekken.Length; positie++)
            {
                // let op hier 0 based
                if (this.plekken[positie].Speler == speler)
                {
                    this.plekken[positie].Speler = null;
                }
            }

            return true;
        }

        /// <summary>
        /// De spler gaat op een plek zitten.
        /// </summary>
        /// <param name="speler">huidige speler.</param>
        /// <param name="positie">Waar de speler wil zitten.</param>
        /// <returns>Of de plaats beschikbaar is of niet.</returns>
        public bool SpelerNeemtPlaats(Speler speler, uint positie)
        {
            if (speler == null)
            {
                throw new ArgumentNullException("Speler mag niet null zijn.");
            }

            if (this.ZitErEenSpelerOpDezePlek(positie))
            {
                // er zit al een speler op die plek
                return false;
            }

            this.plekken[positie - 1].Speler = speler;

            return true;
        }

        /// <summary>
        /// functie om te kijken of een speler op deze plek zit.
        /// </summary>
        /// <param name="positie">positie van de plek , 1 based.</param>
        /// <returns>true als er een speler op die positie zit.</returns>
        public bool ZitErEenSpelerOpDezePlek(uint positie)
        {
            // unsigned genomen dus dam kan het geen < 0 zijn.
            if (positie < 1 || positie > this.plekken.Length)
            {
                throw new ArgumentOutOfRangeException($"positie moet tussen 1 en {this.plekken.Length} vallen.");
            }

            return this.plekken[positie - 1].Speler != null;
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
            foreach (Plek plek in this.plekken)
            {
                if (plek != null)
                {
                    bezetPlek.Add(plek);
                }
            }

            return bezetPlek;
        }

        /*
        /// <summary>
        /// Check of de plek beschikbaar is.
        /// </summary>
        /// <returns>De plek als vrij is.</returns>
        private Plek VrijPlek()
        {
            foreach (Plek plek in this.plekken)
            {
                if (plek.VrijBlek)
                {
                    return plek;
                    break;
                }
            }

            return null;
        }
        */
    }
}
