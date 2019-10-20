// <copyright file="FichesConsolePrinter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack.Helpers
{
    using System;
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// class om de fiches in de console te printen.
    /// </summary>
    public static class FichesConsolePrinter /*: ICanPrintFiches*/
    {
        /// <summary>
        /// fubnctuie om de fiches te printen.
        /// </summary>
        /// <param name="fiches"> de fiches die ik wil printen.</param>
        public static void PrintFiches(Fiches fiches)
        {
            foreach (Fiche fiche in fiches.ReadOnlyFiches)
            {
                Console.WriteLine($"{fiche.Kleur} € {fiche.Waarde}");
            }
        }

        /// <summary>
        /// Printe de waarde van de fiche.
        /// </summary>
        /// <param name="fiches">De fiche.</param>
        public static void PrintWaardeFiches(Fiches fiches)
        {
            Console.WriteLine($"De waarde is € {fiches.WaardeVanDeFiches}");
        }
    }
}
