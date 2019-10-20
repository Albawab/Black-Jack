// <copyright file="Persoon.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    /// <summary>
    /// De persoon die aan de tafel gaat zitten met de naam.
    /// </summary>
    public abstract class Persoon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Persoon"/> class.
        /// </summary>
        /// <param name="naam">De naam van de persoon.</param>
        public Persoon(string naam)
        {
            this.Naam = naam;
        }

        /// <summary>
        /// Gets or sets en sets de naam van de persoon.
        /// </summary>
        public string Naam { get; set; }
    }
}
