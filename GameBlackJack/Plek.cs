// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    /// <summary>
    /// De klas van de plek.
    /// </summary>
    public class Plek
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Plek"/> class.
        /// </summary>
        /// <param name="dePlekWaarDeSpelerWilZitten">De plek aan de tafel.</param>
        public Plek(int dePlekWaarDeSpelerWilZitten)
        {
            this.DePlek = dePlekWaarDeSpelerWilZitten;
        }

        /// <summary>
        /// Gets or sets De plek waar de speler gaat zitten.
        /// </summary>
        public int DePlek { get; set; }
    }
}