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
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Speler: A");
            Console.ResetColor();
            Console.WriteLine("Leuk je komt Black Jack spelen. Wilt je me je naam vertelen?");
            string naamSpelerA = Console.ReadLine();
            Speler spelerA = new Speler(naamSpelerA);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Speler: B");
            Console.ResetColor();
            Console.WriteLine("Leuk je komt Black Jack spelen. Wilt je me je naam vertelen?");
            string naamSpelerB = Console.ReadLine();
            Speler spelerB = new Speler(naamSpelerB);

            // koopt fiches vbij de cassiere
            spelerA.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(90, 20, true));
            spelerB.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(90, 20, true));

            Console.WriteLine();
            Console.WriteLine(spelerA.Naam + "Je hebt gekocht");
            FichesConsolePrinter.PrintWaardeFiches(spelerA.Fiches);
            FichesConsolePrinter.PrintFiches(spelerA.Fiches);
            Console.WriteLine();
            Console.WriteLine(spelerB.Naam + "Je hebt gekocht");
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
