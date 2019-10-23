// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Slaan de gegevens van de speler op.
    /// bepaal de actie die de speler wil doen.Koop fiches ook zet fiches bij de hand in.
    /// </summary>
    public class Speler : Persoon
    {
        /// <summary>
        /// Hoeveel fiches de speler heeft.
        /// </summary>
        private readonly Fiches fiches = new Fiches();

        /// <summary>
        /// Initializes a new instance of the <see cref="Speler"/> class.
        /// </summary>
        /// <param name="naam"> De naam van de speler.</param>
        public Speler(string naam)
            : base(naam)
        {
        }

        /// <summary>
        /// Gets Waar de speler wil zitten.
        /// </summary>
        public Tafel HuidigeTafel { get; private set; }

        /// <summary>
        /// Gets de fiches die met de speler zijn.
        /// </summary>
        public Fiches Fiches
        {
            get
            {
                return this.fiches;
            }
        }

        /// <summary>
        /// Waar de speler wil zitten.
        /// </summary>
        /// <param name="tafel">De tafel waar de speler wil zitten.</param>
        /// <param name="positie">De nummer van de plek.</param>
        /// <returns>of mag zitten of niet.</returns>
        public bool GaatAanTafelZitten(Tafel tafel, uint positie)
        {
            // geef de huidige tafel de value.
            this.HuidigeTafel = tafel;

            // controleren of tafel niet null is

            // als je al ergens anders zat, moet je daar weg gaan
            if (this.HuidigeTafel == null)
            {
                this.VerlaatTafel();
            }

            return tafel.SpelerNeemtPlaats(this, positie);
        }

        /// <summary>
        /// Als de speler heeft de tafel verlaten.
        /// </summary>
        /// <returns>Heeft de speler de tafel verlaten of niet.</returns>
        public bool VerlaatTafel()
        {
            // als ik nergens zit, is dat prima
            if (this.HuidigeTafel == null)
            {
                return true;
            }

            return this.HuidigeTafel.SpelerVerlaatTafel(this);
        }
    }
}