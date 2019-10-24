// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Balck_Jack
{
    using System;
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.SpelSpullen;
    using HenEBalck_Jack;
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
            Fiches cassiereFiches = FicheFactory.CreateFiches(5000);

            // FichesConsolePrinter.PrintWaardeFiches(cassiereFiches);

            // tafel
            Tafel tafel = Tafel.CreateBlackJackTafel(cassiereFiches.GeefMeFischesTerWaardeVan(2500, 50, false));

            // FichesConsolePrinter.PrintWaardeFiches(tafel.Fiches);

            // is   de waarde vban de fiches nu 500?
            // FichesConsolePrinter.PrintWaardeFiches(cassiereFiches);

            // dealer
            // dealer aanmaken en toewijzen aan een tafel
            Dealer dealer = new Dealer("Dealer");
            dealer.GaAanTafelZitten(tafel);

            // spelers, komen binnen en kopen bij het cassiere fiches
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Leuk dat je Black Jack komt spelen. Wat is je naam?");
            bool isLetters = false;
            Speler spelerA = null;

            while (!isLetters)
            {
                Console.WriteLine("Je mag alleen letters typen.");
                int chetIsNotLetter = 0;
                string naamSpelerA = Console.ReadLine();

                spelerA = new Speler(naamSpelerA);
                char[] lettersOfName = naamSpelerA.ToCharArray();
                naamSpelerA.ToCharArray();
                for (int i = 0; i < naamSpelerA.Length; i++)
                {
                    if (!char.IsLetter(lettersOfName[i]))
                    {
                        chetIsNotLetter++;
                        break;
                    }
                }

                if (chetIsNotLetter == 0 && naamSpelerA.Length != 0 && naamSpelerA.Length <= 15)
                {
                    isLetters = true;
                }
                else
                {
                    if (naamSpelerA.Length == 0)
                    {
                        Console.WriteLine("Mag niet zonder naam spelen.");
                        Console.WriteLine("Wat is je naam?");
                        Console.WriteLine();
                    }
                    else if (naamSpelerA.Length > 15)
                    {
                        Console.WriteLine("Je mag t/m 15 letters gebruiken.");
                        Console.WriteLine();
                        Console.WriteLine("Je mag geen nummers gebruiken.");
                    }
                    else
                    {
                        Console.WriteLine("Je hebt fout gedaan!");
                        Console.WriteLine("Je mag geen nummers gebruiken.");
                    }
                }
            }

            Console.ResetColor();
            Console.WriteLine();
            /*            Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Speler: B");
                        Console.ResetColor();
                        Console.WriteLine("Leuk je komt Black Jack spelen. Wilt je me je naam vertelen?");

                        // string naamSpelerB = Console.ReadLine();
                        Speler spelerB = new Speler("B");
                        */

            // koopt fiches vbij de cassiere
            Console.WriteLine("Wat zou je de waarde van fiches kopen?");
            Console.WriteLine($"Mag alleen tussen 1 en {tafel.Fiches.WaardeVanDeFiches}");
            string waardeFiches = Console.ReadLine();
            int waarde;
            while (!int.TryParse(waardeFiches, out waarde) || waarde > tafel.Fiches.WaardeVanDeFiches || waarde <= 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Type maar alleen nummers tussen 1 en {tafel.Fiches.WaardeVanDeFiches} gebruiken.");
                if (!int.TryParse(waardeFiches, out waarde))
                {
                    Console.WriteLine("Je mag geen letters gebruiken.");
                }

                waardeFiches = Console.ReadLine();
            }

            spelerA.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(waarde, 50, true));
            /*            spelerB.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(90, 20, true));*/

            Console.WriteLine();
            Console.WriteLine(spelerA.Naam + " Je hebt gekocht");
            FichesConsolePrinter.PrintWaardeFiches(spelerA.Fiches);
            FichesConsolePrinter.PrintFiches(spelerA.Fiches);
            Console.WriteLine();
            /*            Console.WriteLine(spelerB.Naam + " Je hebt gekocht");
                        FichesConsolePrinter.PrintWaardeFiches(spelerB.Fiches);
                        FichesConsolePrinter.PrintFiches(spelerB.Fiches);*/

            // FichesConsolePrinter.PrintWaardeFiches(cassiereFiches);
            if (!spelerA.GaatAanTafelZitten(tafel, 1))
            {
                throw new ArgumentOutOfRangeException("Het plek is niet meer beschikbaar.");
            }

            /*            else if (!spelerB.GaatAanTafelZitten(tafel, 2))
                        {
                            throw new ArgumentOutOfRangeException("Het plek is niet meer beschikbaar.");
                        }*/

            BlackjackController blackJackController = new BlackjackController(tafel, new ConsoleCommunicatorBehandelen());
            Console.WriteLine("Er is een fout gegaan.");

            // blackJackController.Start();
        }
    }
}
