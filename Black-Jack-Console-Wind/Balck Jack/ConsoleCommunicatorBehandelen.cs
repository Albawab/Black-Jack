// <copyright file="ConsoleCommunicatorBehandelen.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Interface;
    using HenE.GameBlackJack.Settings;
    using HenEConsole_Balck_Jack;

    /// <summary>
    /// De console die gaat communicte tussen de speler en het spel doen.
    /// </summary>
    public partial class ConsoleCommunicatorBehandelen : ICommunicate
    {
        private readonly BlackJackPointsCalculator blackJackPointsCalculator = new BlackJackPointsCalculator();

        /// <summary>
        /// geef informatie over iets gebeurt.
        /// Wijs melding naar de juiste methode die de melding toont.
        /// </summary>
        /// <param name="hand">De hand die krijgt een melding.</param>
        /// <param name="melding">De text van een melding.</param>
        /// <param name="meerInformatie">Geef aan de spelers meer informatie die zij nodig hebben.</param>
        public void TellHand(SpelerHand hand, Meldingen melding, string meerInformatie)
        {
            switch (melding)
            {
                case Meldingen.ToonInzet:
                    this.ToonInzet(hand);
                    break;
                case Meldingen.Verliezen:
                    this.Verliezen(hand);
                    break;
                case Meldingen.KaartenVanDeHand:
                    this.KaartenVanDeHand(hand.Speler, hand);
                    break;
                case Meldingen.Fout:
                    this.FoutMelding(hand);
                    break;
                case Meldingen.Hold:
                    this.HoldHand(hand);
                    break;
                case Meldingen.Gewonnen:
                    this.Gewonnen(hand);
                    break;
                case Meldingen.ActieGekozen:
                    this.ActieGekozen(hand, meerInformatie);
                    break;
                case Meldingen.Gestopt:
                    this.SpelerGestopt(hand);
                    break;
                case Meldingen.BlackJack:
                    this.BlackJack(hand);
                    break;
                case Meldingen.Verdienen:
                    this.Verdienen(hand);
                    break;
            }
        }

        /// <summary>
        /// geef informatie over iets gebeurt.
        /// </summary>
        /// <param name="speler">De speler die een melding krijgt. </param>
        /// <param name="melding">De text van de melding.</param>
        public void TellPlayer(Speler speler, Meldingen melding)
        {
            switch (melding)
            {
                case Meldingen.OngeldigeInzet:
                    this.OngeldigeInzet(speler);
                    break;
                case Meldingen.ToonInzet:
                    this.ToonFiches(speler);
                    break;
                case Meldingen.GeenFiches:
                    this.GeenFiches(speler);
                    break;
            }
        }

        /// <summary>
        /// geef informatie over iets gebeurt.
        /// </summary>
        /// <param name="speler">De speler die een melding krijgt. </param>
        /// <param name="melding">De text van de melding.</param>
        /// <param name="hand">De hand van een speler.</param>
        /// <param name="meerInformatie">Geef aan de spelers meer informatie die zij nodig hebben.</param>
        public void TellPlayer(Speler speler, Meldingen melding, Hand hand, string meerInformatie)
        {
            switch (melding)
            {
                case Meldingen.OngeldigeInzet:
                    this.OngeldigeInzet(speler);
                    break;
                case Meldingen.ToonInzet:
                    SpelerHand hand1 = hand as SpelerHand;
                    this.ToonFiches(speler, hand1);
                    break;
                case Meldingen.KaartenVanDeHand:
                    this.KaartenVanDeHand(speler, hand);
                    break;
                case Meldingen.ActieGekozen:
                    SpelerHand spelerHand = hand as SpelerHand;
                    this.ActieGekozen(spelerHand, meerInformatie);
                    break;
                case Meldingen.Verliezen:
                    SpelerHand spelerHandVerliezen = hand as SpelerHand;
                    this.Verliezen(spelerHandVerliezen);
                    break;
                case Meldingen.Hold:
                    SpelerHand spelerHandHold = hand as SpelerHand;
                    this.HoldHand(spelerHandHold);
                    break;
                case Meldingen.Gewonnen:
                    SpelerHand spelerHandGewonnen = hand as SpelerHand;
                    this.Gewonnen(spelerHandGewonnen);
                    break;
                case Meldingen.Gestopt:
                    this.SpelerGestopt(speler, hand);
                    break;
                case Meldingen.BlackJack:
                    this.BlackJack(hand);
                    break;
                case Meldingen.YouDied:
                    this.TellDied(speler, hand);
                    break;
                case Meldingen.DealerDied:
                    this.TellDealerDied(speler, hand);
                    break;
                case Meldingen.DealerGepassed:
                    this.TellDealerGepassed(speler, hand);
                    break;
                case Meldingen.Verdienen:
                    this.Verdienen(speler, hand);
                    break;
            }
        }

        /// <inheritdoc/>
        public int AskWhichAction(SpelerHand hand, List<Acties> mogelijkActies)
        {
            int actieNummer;
            Console.WriteLine($"{hand.Speler.Naam} je mag een van de acties kiezen.");
            for (int actie = 1; actie <= mogelijkActies.Count; actie++)
            {
                Console.WriteLine($" {actie.ToString()}- {mogelijkActies[actie - 1]}");
            }

            Console.WriteLine("Kies maar een van die actie. Type maar het nummer van de actie.");
            string answer = Console.ReadLine();
            while (!this.IsGeldigWaarde(answer, out actieNummer) || (actieNummer > mogelijkActies.Count || actieNummer < 1))
            {
                Console.WriteLine("Type maar een nummer of een juiste keuze.");
                answer = Console.ReadLine();
            }

            return actieNummer;
        }

        /// <summary>
        /// Ask de speler om fiches voor de hand te inzetten.
        /// </summary>
        /// <param name="hand">De hand die een fiches krijgt.</param>
        /// <param name="waarde">de waarde van de fiches.</param>
        /// <returns>Is de speler wil inzetten of niet.</returns>
        public bool AskFichesInzetten(SpelerHand hand, out int waarde)
        {
            Console.WriteLine();
            int waardeDieDeSpelerWilInzetten = 0;
            ColorConsole.WriteLine(ConsoleColor.Cyan, $"{hand.Speler.Naam} Wat voor waarde wil je inzetten?");
            string answerWarde = Console.ReadLine();
            while (!this.IsGeldigWaarde(answerWarde, out waardeDieDeSpelerWilInzetten))
            {
                Console.WriteLine(hand.Speler.Naam + " Type maar een nummer.");
                answerWarde = Console.ReadLine();
            }

            waarde = waardeDieDeSpelerWilInzetten;
            return true;
        }

        /// <summary>
        /// Ask de speler om fiches te kopen.
        /// </summary>
        /// <param name="speler">De speler die gaat fiches kopen.</param>
        /// <param name="waarde">De waarde die de speler wil kopen van fiches.</param>
        /// <returns>Is de speler gekocht fiches of niet.</returns>
        public bool AskFichesKopen(Speler speler, out int waarde)
        {
            int waardeDieDeSpelerWilInzetten = 0;
            Console.WriteLine("Wil je fiches kopen Y of N?");
            string answer = Console.ReadLine().ToLower();
            while (!this.IsAntwoordGoed(answer))
            {
                Console.WriteLine("Je mag alleen Y of N typen!");
                answer = Console.ReadLine();
            }

            if (answer == "y")
            {
                Console.WriteLine($"{speler.Naam} Wat voor waarde wil je kopen?");
                string answerWarde = Console.ReadLine();
                while (!this.IsGeldigWaarde(answerWarde, out waardeDieDeSpelerWilInzetten))
                {
                    Console.WriteLine("Type maar een nummer.");
                    answerWarde = Console.ReadLine();
                }

                waarde = waardeDieDeSpelerWilInzetten;
                return true;
            }

            waarde = waardeDieDeSpelerWilInzetten;
            return false;
        }
    }
}
