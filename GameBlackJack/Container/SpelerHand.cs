// <copyright file="SpelerHand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
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
        /// <param name="handDieGesplitstMoetWorden">De hand die wordt gesplitst.</param>
        /// <returns>De nieuwe hand.</returns>
        public SpelerHand Splits(SpelerHand handDieGesplitstMoetWorden)
        {
            // todo, wat zijn de voorwaarden om te splitsen?
            if (handDieGesplitstMoetWorden.Kaarten.Count == 2)
            {
                if (true/*this.Kaarten[0].Waarde == this.Kaarten[1].Waarde*/)
                {
                    // kaarten moeten gelijk zijn
                    // kaarten moeten een even aantal zijn (== twee).
                    // welke controle moet ik doen
                    this.Kaarten.Add(handDieGesplitstMoetWorden.Kaarten[0]);
                    handDieGesplitstMoetWorden.Kaarten.Remove(this.Kaarten[0]);

                    return this;
                }
            }

            return null;

            // wat betekent dit?
            // dat ik een nieuwe hand moet maken

            // en dat ik de kaarten van deze hand moet delen en verplaatsen naar de nieuwe hand
        }

/*        /// <summary>
        /// Zoek op in hand die wordt gesplits voor de fiches.
        /// Geef de nieuwe hand de zelfde fiches.
        /// </summary>
        /// <param name="handWordtGesplits">De hand die gesplits wordt.</param>
        /// <param name="blackjackController">De black jack controller.</param>
        public void GeefFichesBijHandDieWordtGesplits(SpelerHand handWordtGesplits, BlackjackController blackjackController)
        {
            foreach (Fiche fiche in handWordtGesplits.Inzet.ReadOnlyFiches)
            {
                blackjackController.ZetFichesBijHandIn(this, fiche.Waarde);
            }
        }*/
    }
}