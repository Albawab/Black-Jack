// <copyright file="Dealer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// De dealer die het spel controleert.
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
        /// Als de dealer verandert.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        public void GaAanTafelZitten(Tafel tafel)
        {
            tafel.WijzigDealer(this);
        }

        /// <summary>
        /// Kijk wat elke hand heeft.
        /// </summary>
        /// <param name="hand">Huidige hand van de dealer.</param>
        /// <returns>De waarde van de fiches in de hand.</returns>
        public int WaardevanDeHand(Hand hand)
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
