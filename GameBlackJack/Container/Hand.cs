// <copyright file="Hand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// Heeft de kaarten en de fiches van de speler en ook heeft eigen situatie.
    /// </summary>
    public abstract class Hand
    {
        /// <summary>
        /// Een lijst van kaarten die bij de hand zijn.
        /// </summary>
        private readonly List<Kaart> kaarten = new List<Kaart>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/> class.
        /// </summary>
        protected Hand()
        {
            this.Status = HandStatussen.NogNietGestart;
        }

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
        /// Gets a value indicating whether gets De hand van de dealer.
        /// </summary>
        public abstract bool IsDealerHand { get; }

        /// <summary>
        /// Voeg een kaart aan de hand toe.
        /// </summary>
        /// <param name="kaart">Nieuwe kaart.</param>
        public void AddKaart(Kaart kaart)
        {
            this.kaarten.Add(kaart);
            this.Status = HandStatussen.InSpel;
        }

        /// <summary>
        /// Als de hand klaar is, veraandert de status van de hand.
        /// </summary>
        public void Close()
        {
            this.Status = HandStatussen.Gestopt;

            // en gooi alle kaarten weg.
            this.kaarten.Clear();
            this.ChangeStatus(HandStatussen.Gestopt);
        }

        /// <summary>
        /// Check of de hand heeft meer dan 21 score.
        /// </summary>
        /// <param name="waardeVanKaarten">De waarde die bij de hand is.</param>
        /// <returns>Meer dan 21 score of minder.</returns>
        public bool IsDood(int waardeVanKaarten) => waardeVanKaarten > 21;

        /// <summary>
        /// Verandert de status van de hand.
        /// </summary>
        /// <param name="status">Nieuwe status.</param>
        public void ChangeStatus(HandStatussen status)
        {
            this.Status = status;
        }
    }
}