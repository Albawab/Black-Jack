// <copyright file="KaartTekenHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Enum
{
    using System.Collections.Generic;

    /// <summary>
    /// Hier staat het teken van een kaart.
    /// </summary>
    public enum KaartTeken
    {
        /// <summary>
        /// Geen waarde.
        /// </summary>
        IsNotDefined = 0,

        /// <summary>
        /// Het teken aas.
        /// </summary>
        Aas = 1,

        /// <summary>
        /// Het teken twee.
        /// </summary>
        Twee = 2,

        /// <summary>
        /// Het teken drie.
        /// </summary>
        Drie = 3,

        /// <summary>
        /// Het teken vier.
        /// </summary>
        Vier = 4,

        /// <summary>
        /// Het teken vijf.
        /// </summary>
        Vijf = 5,

        /// <summary>
        /// Het teken zes.
        /// </summary>
        Zes = 6,

        /// <summary>
        /// Het teken zeven.
        /// </summary>
        Zeven = 7,

        /// <summary>
        /// Het teken acht.
        /// </summary>
        Acht = 8,

        /// <summary>
        /// Het teken negen.
        /// </summary>
        Negen = 9,

        /// <summary>
        /// Het teken tien.
        /// </summary>
        Tien = 10,

        /// <summary>
        /// Het teken Boer.
        /// </summary>
        Boer = 11,

        /// <summary>
        /// Het teken vrouw.
        /// </summary>
        Vrouw = 12,

        /// <summary>
        /// Het teken heer.
        /// </summary>
        Heer = 13,

        /// <summary>
        /// Het teken Joker.
        /// </summary>
        Joker = 25,
    }

    /// <summary>
    /// De tekenen die wij gebruiken worden.
    /// </summary>
    public static class KaartTekenHelper
    {
        /// <summary>
        /// Wij willen die teken gebruiken op de kaarten.
        /// </summary>
        /// <returns>De teken als list.</returns>
        public static List<KaartTeken> GetKaartTekenZonderJoker()
        {
            return new List<KaartTeken>()
            {
                KaartTeken.Aas,
                KaartTeken.Twee,
                KaartTeken.Drie,
                KaartTeken.Vier,
                KaartTeken.Vijf,
                KaartTeken.Zes,
                KaartTeken.Zeven,
                KaartTeken.Acht,
                KaartTeken.Negen,
                KaartTeken.Tien,
                KaartTeken.Boer,
                KaartTeken.Vrouw,
                KaartTeken.Heer,
            };
        }
    }
}
