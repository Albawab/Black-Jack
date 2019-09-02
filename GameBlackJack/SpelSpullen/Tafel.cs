// <copyright file="Tafel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.SpelSpullen
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// De klas van de tafels.
    /// </summary>
    public class Tafel
    {
        private readonly List<Plek> plekken = new List<Plek>();
        private readonly List<Speler> spelers = new List<Speler>();
        private int aantelPlekken = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tafel"/> class.
        /// </summary>
        /// <param name="aantelPlekken">Hoe veel plekken aan de tafel .</param>
        /// <param name="fichesBak">Waar de fiches staat.</param>
        /// <param name="stapelKaarten">Waar de kaarten staat.</param>
        /// <param name="dealer">De dealer van heet spel.</param>
        public Tafel(int aantelPlekken, FichesBak fichesBak, StapelKaarten stapelKaarten, Dealer dealer)
        {
            this.Dealer = dealer;
            this.StapelKaarten = stapelKaarten;
            this.FichesBak = fichesBak;
            this.aantelPlekken = aantelPlekken;
            this.VoegEenPlekAanTafelIn();
        }

        /// <summary>
        /// Gets or sets de dealer van het spel.
        /// </summary>
        private Dealer Dealer { get; set; }

        /// <summary>
        /// Gets or sets De fiches.
        /// </summary>
        private FichesBak FichesBak { get; set; }

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
        /// Voeg de plek aan de tafel in.
        /// </summary>
        public void VoegEenPlekAanTafelIn()
        {
            for (int plek = 0; plek < this.aantelPlekken; plek++)
            {
                this.plekken.Add(new Plek(true));
            }
        }

        /// <summary>
        /// Voeg een speler aan het spel in.
        /// </summary>
        /// <param name="speler">Nieuwe speler.</param>
        public void AddEenSpeler(Speler speler)
        {
            Hand hand = new Hand(speler);
            // voeg een hand.
            Plek plek = this.VrijPlek();
            speler.NeemtEenPlek(plek);
            plek.DoeDePlekBezet(plek);
            this.spelers.Add(speler);
        }

        /*
        /// <summary>
        /// Deze Plek beschikbaar of niet.
        /// </summary>
        /// <param name="dePlek">De plek.</param>
        /// <returns>Vrij plek.</returns>
        public Plek VrijPlek()
        {
            foreach (Plek plek in this.plekken)
            {
                if (plek)
                {

                }
            }

        } */

        /// <summary>
        /// Doe deze plek niet meer beschikbaar.
        /// </summary>
        /// <param name="eenPlek">De plek.</param>
        /// <returns>Deze plek.</returns>
        public Plek DezePlekIsNietMeerVrij(Plek eenPlek)
        {
            this.plekken.Add(eenPlek);
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
            foreach (Plek plek in this.plekken)
            {
                if (plek != null)
                {
                    bezetPlek.Add(plek);
                }
            }

            return bezetPlek;
        }

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
    }
}
