// <copyright file="Hand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// De klas van de hand.
    /// </summary>
    public class Hand
    {
        private readonly List<Kaart> kaarten = new List<Kaart>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/> class.
        /// </summary>
        /// <param name="persoon">Huidige persoon.</param>
        public Hand(Persoon persoon)
        {
            if (persoon is null)
            {
                throw new ArgumentNullException("Persoon mag niet leeg zijn.");
            }

            this.Persoon = persoon;
            this.Inzet = new Fiches();
            this.Status = HandStatussen.NogNietGestart;
        }

        /// <summary>
        /// Gets de spelers.
        /// </summary>
        public Persoon Persoon { get; private set; }

        /// <summary>
        /// Gets de kaarten.
        /// </summary>
        public List<Kaart> Kaarten
        {
            // todo copy lijst teruggeven
            get
            {
                return this.kaarten;
            }
        }

        /// <summary>
        /// Gets de status van de hand.
        /// </summary>
        public HandStatussen Status { get; private set; }

        /// <summary>
        /// Gets or sets De fiches die in de hand zijn.
        /// </summary>
        public Fiches Inzet { get; set; }

        /// <summary>
        /// Geef de huidige speler terug.
        /// </summary>
        /// <returns>Huidige speler.</returns>
        public Speler HuidigeSpeler()
        {
            return this.Persoon as Speler;
        }

        /// <summary>
        /// Add een kaart aan de hand.
        /// </summary>
        /// <param name="kaart">Nieuwe kaart.</param>
        public void AddKaart(Kaart kaart)
        {
            this.kaarten.Add(kaart);
            this.Status = HandStatussen.Gestart;
        }

        /// <summary>
        /// Als de hand klaar is, veraandert de status van de hand.
        /// </summary>
        public void Close()
        {
            this.Status = HandStatussen.Gestopt;
        }

        /// <summary>
        /// Geeft de andere kaart in de hand terug.
        /// </summary>
        /// <param name="huidigeKaart">Huidige kaart.</param>
        /// <returns>Andere kaart.</returns>
        public Kaart AndereKaart(Kaart huidigeKaart)
        {
            if (this.kaarten.Count != 2)
            {
                return null;
            }

            foreach (Kaart kaart in this.kaarten)
            {
                if (huidigeKaart != kaart)
                {
                    return kaart;
                }
            }

            return null;
        }

        /// <summary>
        /// Verandert de status van de hand.
        /// </summary>
        /// <param name="status">Nieuwe status.</param>
        public void ZetStatus( HandStatussen status)
        {
            this.Status = status;
        }
    }
}