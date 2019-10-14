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
    /// Heeft de kaarten en de fiches van de speler en ook heeft eigen situatie.
    /// </summary>
    public class SpelerHand : Hand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/> class.
        /// </summary>
        /// <param name="persoon">Huidige persoon.</param>
        public SpelerHand(Speler speler) : base()
        {
            if (speler is null)
            {
                throw new ArgumentNullException("Persoon mag niet leeg zijn.");
            }

            this.Speler = speler;
            this.Inzet = new Fiches();
        }

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
        /// <returns>De hand die wordt gesplitst.</returns>
        public SpelerHand Splits()
        {
            // todo, wat zijn de voorwaarden om te splitsen?
            if (this.Kaarten.Count == 2)
            {
                if (this.Kaarten[0].Waarde == this.Kaarten[1].Waarde)
                {
                    // kaarten moeten gelijk zijn
                    // kaarten moeten een even aantal zijn (== twee).
                    // welke controle moet ik doen
                    SpelerHand nieuweHand = new SpelerHand(this.Speler);

                    for (int index = 0; index < this.Kaarten.Count; index++)
                    {
                        nieuweHand.Kaarten.Add(this.Kaarten[index]);
                    }

                    return nieuweHand;
                }
            }

            return null;

            // wat betekent dit?
            // dat ik een nieuwe hand moet maken

            // en dat ik de kaarten van deze hand moet delen en verplaatsen naar de nieuwe hand
        }

        /// <summary>
        /// Heef fiches bij de hand van de speler.
        /// </summary>
        /// <returns>Geeft de speler fiches bij de hand of niet.</returns>
        public bool GeefFichesBijHand()
        {
            foreach (Fiche fiche in this.Inzet.ReadOnlyFiches)
            {
                this.Speler.ZetFichesBijHandIn(this, fiche.Waarde);
            }

            return true;
        }

        /// <summary>
        /// Zoek op in hand die wordt gesplits voor de fiches.
        /// Geef de nieuwe hand de zelfde fiches.
        /// </summary>
        /// <param name="handWordtGesplits">De hand die gesplits wordt.</param>
        public void GeefFichesBijHandDieWordtGesplits(Hand handWordtGesplits)
        {
            foreach (Fiche fiche in handWordtGesplits.Inzet.ReadOnlyFiches)
            {
                this.Speler.ZetFichesBijHandIn(this, fiche.Waarde);
            }
        }
    }
}