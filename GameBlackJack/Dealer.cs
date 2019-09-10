// <copyright file="Dealer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Settings;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Hier controleert de dealer het spel.
    /// </summary>
    public class Dealer : Persoon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dealer"/> class.
        /// </summary>
        /// <param name="naam">De naam van de dealer.</param>
        public Dealer(string naam)
            : base(naam)
        {
        }

        /// <summary>
        /// Gets or Sets De plek waar de dealer zit aan de tafel.
        /// </summary>
        private Plek PlekAanTafel { get; set; }

        /// <summary>
        /// Gets or Sets de hand van de dealer.
        /// </summary>
        private Hand Hand { get; set; }

        /// <summary>
        /// Als de dealer verandert.
        /// </summary>
        /// <param name="tafel">Huidige hand.</param>
        public void GaAanTafelZitten(Tafel tafel)
        {
            tafel.WijzigDealer(this);
        }

        /// <summary>
        /// Kijk wat elke hand heeft.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>De waarde in de hand.</returns>
        public int BeoordeelHand(Hand hand)
        {
            int deWaardeInHand = 0;
            foreach (Kaart kaart in hand.Kaarten)
            {
                deWaardeInHand += kaart.Waarde;
            }

            return deWaardeInHand;
        }
    }
}
