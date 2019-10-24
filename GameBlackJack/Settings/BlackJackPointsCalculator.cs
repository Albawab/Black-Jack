// <copyright file="BlackJackPointsCalculator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// Bepaal de waarde van de Aas.
    /// Reken de score van de kaarten die in de hand zijn.
    /// </summary>
    public class BlackJackPointsCalculator : ICalculatePoints
    {
        private const int BlackJackScore = 21;

        /// <summary>
        /// Die gaat calculate de points.
        /// </summary>
        /// <param name="kaarten">De kaarten die in de hand zijn.</param>
        /// <returns>Hoeveel points in de hand zijn.</returns>
        public int CalculatePoints(List<Kaart> kaarten)
        {
            int waarde = 0;

            // ik order descending, zodat ik de waarde van de overige kaarten heb, als ik bij de azen aankom.
            foreach (Kaart kaart in kaarten.OrderByDescending(a => a.Teken))
            {
                switch (kaart.Teken)
                {
                    case KaartTeken.Boer:
                    case KaartTeken.Vrouw:
                    case KaartTeken.Heer:
                        // deze hebben een waarde van 10
                        waarde += 10;
                        break;
                    case KaartTeken.Aas:
                        // bepalen of dit 1 of 11 is
                        waarde += this.BepaalAasWaarde(waarde);
                        break;
                    case KaartTeken.IsNotDefined:
                    case KaartTeken.Joker:
                        // dit mag niet voorkomen
                        throw new ArgumentOutOfRangeException("Kaart is niet geldig voor dit spel.");

                    default:
                        // in alle ander gevallen is het de weaarde van de kaart
                        waarde += (int)kaart.Teken;
                        break;
                }
            }

            return waarde;
        }

        /// <summary>
        /// functie om te bepalen of het 1 of 11 moet zijn.
        /// de speler beslist, dus je kan het hem eventueel vragen.
        /// voor nu is het zo, dat het 11 is, behalve als je dood bent.
        /// </summary>
        /// <param name="waardeVanDeAndereKaarten">De totale waarde van de andere kaarten.</param>
        /// <returns>geeft 1 o1 11 terug naarmate de waarde van de aas.</returns>
        private int BepaalAasWaarde(int waardeVanDeAndereKaarten)
        {
            int result = 11;

            // is de waarde van de hand meer dan 21?
            if (waardeVanDeAndereKaarten + 11 > BlackJackScore)
            {
                return 1;
            }

            return result;
        }
    }
}
