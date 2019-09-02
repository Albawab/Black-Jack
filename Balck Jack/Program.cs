// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Balck_Jack
{
    using System;
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

            // Behandel de tafel.
            Tafel tafel = new Tafel(4, fichesBak, stapelKaarten, dealer);

            Speler speler = new Speler("Piet");
            tafel.AddEenSpeler(speler);
            Speler speler1 = new Speler("Kees");
            tafel.AddEenSpeler(speler1);

            Spel spel = new Spel();
            spel.Start(tafel);
        }
    }
}
