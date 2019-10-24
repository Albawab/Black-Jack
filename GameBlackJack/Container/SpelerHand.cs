// <copyright file="SpelerHand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Heeft de kaarten en de fiches van de speler en ook heeft eigen situatie.
    /// </summary>
    public class SpelerHand : Hand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpelerHand"/> class.
        /// </summary>
        /// <param name="speler">Huidige persoon.</param>
        public SpelerHand(Speler speler)
            : base()
        {
            if (speler is null)
            {
                throw new ArgumentNullException("Persoon mag niet leeg zijn.");
            }

            this.Speler = speler;
            this.Inzet = new Fiches();
        }

        /// <summary>
        /// Gets a value indicating whether gets de hand van de dealer.
        /// </summary>
        public override bool IsDealerHand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets De fiches die in de hand zijn.
        /// </summary>
        public Fiches Inzet { get; set; }

        /// <summary>
        /// Gets geef de huidige speler terug.
        /// </summary>
        /// <returns>Huidige speler.</returns>
        public Speler Speler { get; private set; }

        /// <summary>
        /// Splits de hand.
        /// </summary>
        /// <returns>Indien de hand gesplitst kan worden De nieuwe hand, anders de oude hand.</returns>
        public SpelerHand Splits()
        {
            // todo, wat zijn de voorwaarden om te splitsen?
            // kaarten moeten gelijk zijn
            // kaarten moeten een even aantal zijn (== twee).
            // welke controle moet ik doen
            if (this.Kaarten.Count != 2 &&
                this.Kaarten[0].Waarde != this.Kaarten[1].Waarde)
            {
                return null;
            }

            SpelerHand hand = new SpelerHand(this.Speler);
            hand.Kaarten.Add(this.Kaarten[0]);
            this.Kaarten.Remove(this.Kaarten[0]);

            return hand;
        }
    }
}