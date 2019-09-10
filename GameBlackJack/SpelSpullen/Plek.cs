// <copyright file="Plek.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.SpelSpullen
{
    /// <summary>
    /// De plek waar de speler zit.
    /// </summary>
    public class Plek
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Plek"/> class.
        /// </summary>
        public Plek()
        {
        }

        /// <summary>
        /// Gets or sets de speler die wil spelen.
        /// </summary>
        public Speler Speler { get; set; }
    }
}