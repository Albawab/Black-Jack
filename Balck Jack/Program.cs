// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Balck_Jack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.HelperEnum;
    using HenE.GameBlackJack.SpelSpullen;

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
            Console.WriteLine("Hello World!");

            // Behandel de kaarten.
            StapelKaarten stapelKaarten = new StapelKaarten(2, KaartTekenHelper.GetKaartTekenZonderJoker());
            foreach (Kaart kaart in stapelKaarten.Kaarten)
            {
                Console.WriteLine(kaart.ToString());
            }

            Console.WriteLine("Shuffle");
            stapelKaarten.Shuffle(1);

            foreach (Kaart kaart in stapelKaarten.Kaarten)
            {
                Console.WriteLine(kaart.ToString());
            }

            // Behandel de fiches bak.
            FichesBak fichesBak = new FichesBak(10, HelperFiches.GetFichesKleur());

            // Add de dealer.
            Dealer dealer = new Dealer("Jos");
            Spel spel = new Spel();

            // Behandel de tafel.
            Tafel tafel = new Tafel(4, fichesBak, stapelKaarten, dealer, spel);

            Console.WriteLine("Leuk dat je hier bent, wil je me je naam vertellen.");

            // string naam = Console.ReadLine();
            Speler speler = new Speler("A");
            tafel.AddEenSpeler(speler);
            Speler speler1 = new Speler("Kees");
            tafel.AddEenSpeler(speler1);

            int waarde = 0;
            string kopen = "10";
            Console.WriteLine("Ik ben de dealer Mijn naam is Jos. Ik heb fiches van 5, 10, 15, 20, en 25 euro. Wat wil je dan koppen?");
            Console.WriteLine("Graag type 5, 10, 15, 20 of 25.");

            // kopen = Console.ReadLine();
            while (!int.TryParse(kopen, out waarde))
            {
                Console.WriteLine("Type een nummer!");
                kopen = Console.ReadLine();
            }

            speler.Koopfiches(waarde, dealer, fichesBak);
            string fiches = speler.FichesInPortemonnee();
            Console.WriteLine($"{speler.Naam} je heeft {fiches} fiche/fiches.");
            int nummer = 1;
            List<int> gekozen = new List<int>();
            gekozen.Add(nummer);
            speler.FichesZetten(gekozen, spel);

            dealer.DeelDeBeginKaarten(spel, tafel);
            dealer.DeelDeTweedeRondjeVanDeKaarten(spel, tafel);
        }
    }
}
