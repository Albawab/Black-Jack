// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Balck_Jack
{
    using System;
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.SpelSpullen;
    using HenEBalck_Jack.Helpers;
    using static HenE.GameBlackJack.Fiche;

    /// <summary>
    /// Program van het spel.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">args.</param>
        public static void Main(string[] args)
        {
            // fiches
            // de hoofdbak met fiches
            Fiches cassiereFiches = FicheFactory.CreateFiches(1000);
            FichesConsolePrinter.PrintWaardeFiches(cassiereFiches);

            // tafel
            Tafel tafel = Tafel.CreateBlackJackTafel(cassiereFiches.GeefMeFischesTerWaardeVan(500));
            FichesConsolePrinter.PrintWaardeFiches(tafel.Fiches);

            // is   de waarde vban de fiches nu 500?
            FichesConsolePrinter.PrintWaardeFiches(cassiereFiches);

            // dealer
            // dealer aanmaken en toewijzen aan een tafel
            Dealer dealer = new Dealer("Kees");
            dealer.GaAanTafelZitten(tafel);

            // spelers, komen binnen en kopen bij het cassiere fiches
            Speler spelerA = new Speler("Abdul");
            Speler spelerB = new Speler("Piet");

            // koopt fiches vbij de cassiere
            spelerA.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(20, 10, true));
            spelerB.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(20, 10, true));

            FichesConsolePrinter.PrintWaardeFiches(spelerA.Fiches);
            FichesConsolePrinter.PrintFiches(spelerA.Fiches);
            FichesConsolePrinter.PrintWaardeFiches(spelerB.Fiches);
            FichesConsolePrinter.PrintFiches(spelerB.Fiches);

            FichesConsolePrinter.PrintWaardeFiches(cassiereFiches);

            if (!spelerA.GaatAanTafelZitten(tafel, 1))
            {
                throw new ArgumentOutOfRangeException("Het plek is niet meer beschikbaar.");
            }
            else if (!spelerB.GaatAanTafelZitten(tafel, 2))
            {
                throw new ArgumentOutOfRangeException("Het plek is niet meer beschikbaar.");
            }

            BlackjackController blackJackController = new BlackjackController(tafel);

            blackJackController.Start();
        }
    }
}
