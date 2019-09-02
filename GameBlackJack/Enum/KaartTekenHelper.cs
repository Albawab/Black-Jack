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
        IsDefined = 0,

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
        Drie,

        /// <summary>
        /// Het teken vier.
        /// </summary>
        Vier,

        /// <summary>
        /// Het teken vijf.
        /// </summary>
        Vijf,

        /// <summary>
        /// Het teken zes.
        /// </summary>
        Zes,

        /// <summary>
        /// Het teken zeven.
        /// </summary>
        Zeven,

        /// <summary>
        /// Het teken acht.
        /// </summary>
        Acht,

        /// <summary>
        /// Het teken negen.
        /// </summary>
        Negen,

        /// <summary>
        /// Het teken tien.
        /// </summary>
        Tien,

        /// <summary>
        /// Het teken Boer.
        /// </summary>
        Boer,

        /// <summary>
        /// Het teken vrouw.
        /// </summary>
        Vrouw,

        /// <summary>
        /// Het teken heer.
        /// </summary>
        Heer,

        /// <summary>
        /// Het teken Joker.
        /// </summary>
        Joker,
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
