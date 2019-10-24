// <copyright file="StapelKaartenFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Kaarten
{
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Creat nieuwe stapel kaart.
    /// </summary>
    public static class StapelKaartenFactory
    {
        /// <summary>
        /// Verzamelen de gegevens voor een kaart.
        /// </summary>
        /// <param name="aantalPakken">Hoeveel pakken wij willen in de stapelkaart er in zetten.</param>
        /// <returns>Nieuwe spapel kaarten.</returns>
        public static StapelKaarten CreateBlackJackKaarten(int aantalPakken)
        {
            // Behandel de kaarten.
            StapelKaarten stapelKaarten = new StapelKaarten(aantalPakken, KaartTekenHelper.GetKaartTekenZonderJoker());
            stapelKaarten.Shuffle(2);

            return stapelKaarten;
        }
    }
}
