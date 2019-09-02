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
        /// <param name="vijPlek">Is deze plek bezet of niet.</param>
        public Plek(bool vijPlek)
        {
            this.VrijBlek = vijPlek;
        }

        /// <summary>
        /// Gets a value indicating whether gets or sets De plek waar de speler gaat zitten.
        /// </summary>
        public bool VrijBlek { get; private set; }

        /// <summary>
        /// Mag niet meer de plek gebruiken.
        /// </summary>
        /// <param name="plek">Huidige plek.</param>
        public void DoeDePlekBezet(Plek plek)
        {
            plek.VrijBlek = false;
        }
    }
}