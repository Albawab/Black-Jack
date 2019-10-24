// <copyright file="Tafel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.SpelSpullen
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Kaarten;

    /// <summary>
    /// De klas waar de spullen van het tafel staan.
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
        /// <param name="maximaleInZet">Bepaalt de max inzet voor deze tafel.</param>
        /// <param name="minimaleInZet">Bepaalt de min inzet voor deze tafel.</param>
        private Tafel(int aantalPlekken, Fiches fiches, StapelKaarten stapelKaarten, int maximaleInZet, int minimaleInZet)
        {
            this.StapelKaarten = stapelKaarten;
            this.Fiches = fiches;
            this.MaximaleInZet = maximaleInZet;
            this.MinimalenZet = minimaleInZet;
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
        /// Gets hier staat de speler die willen spelen.
        /// </summary>
        public List<Speler> Spelers
        {
            get
            {
                List<Speler> spelers = new List<Speler>();
                for (int index = 0; index <= this.plekken.Length - 1; index++)
                {
                    if (this.plekken[index].Speler != null)
                    {
                        spelers.Add(this.plekken[index].Speler);
                    }
                }

                return spelers;
            }
        }

        /// <summary>
        /// Gets or sets De fiches.
        /// </summary>
        public Fiches Fiches { get; set; }

        /// <summary>
        /// Gets or sets ts De stapel van de kaarten.
        /// </summary>
        public StapelKaarten StapelKaarten { get; set; }

        /// <summary>
        /// Gets minimale bedrag wat op deze tafel ingezet moet worden.
        /// </summary>
        public int MinimalenZet { get; private set; }

        /// <summary>
        /// Gets minimale bedrag wat op deze tafel ingezet moet worden.
        /// </summary>
        public int MaximaleInZet { get; private set; }

        /// <summary>
        /// Maak een nieuwe tafel.
        /// </summary>
        /// <param name="fiches">De fiches die aan de tafel zijn.</param>
        /// <returns>De nieuwe tafel.</returns>
        public static Tafel CreateBlackJackTafel(Fiches fiches)
        {
            Tafel tafel = new Tafel(6, fiches, StapelKaartenFactory.CreateBlackJackKaarten(2), 100, 5);
            return tafel;
        }

        /// <summary>
        /// functie om van dealer te wisselen.
        /// </summary>
        /// <param name="newDealer">nieuwe dealer.</param>
        /// <returns>oude dealer.</returns>
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
            if (this.ZitErEenSpelerOpDezePlek(positie))
            {
                // er zit al een speler op die plek
                return false;
            }

            this.plekken[positie - 1].Speler = speler ?? throw new ArgumentNullException("Speler mag niet null zijn.");

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
        public bool BepaaltOfDeWaardetussenMaxInzetEnMinInzet(int spelerWilzetten)
        {
            if (spelerWilzetten < 0)
            {
                throw new ArgumentNullException("De waarde mag niet mider dan null zijn.");
            }

            if (this.MinimalenZet < spelerWilzetten && this.MaximaleInZet > spelerWilzetten)
            {
                return true;
            }

            return false;
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
    }
}
