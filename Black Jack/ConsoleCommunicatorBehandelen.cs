// <copyright file="ConsoleCommunicatorBehandelen.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Interface;
    using HenE.GameBlackJack.Settings;

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
        public void TellHand(Hand hand, Meldingen melding, string meerInformatie)
        {
            SpelerHand spelerHand = hand as SpelerHand;
            switch (melding)
            {
                case Meldingen.ToonInzet:
                    this.ToonInzet(spelerHand);
                    break;
                case Meldingen.Verliezen:
                    this.Verliezen(spelerHand);
                    break;
                case Meldingen.KaartenVanDeHand:
                    this.KaartenVanDeHand(spelerHand.Speler, spelerHand);
                    break;
                case Meldingen.Fout:
                    this.FoutMelding(spelerHand);
                    break;
                case Meldingen.Hold:
                    this.HoldHand(spelerHand);
                    break;
                case Meldingen.Gewonnen:
                    this.Gewonnen(spelerHand);
                    break;
                case Meldingen.ActieGekozen:
                    this.ActieGekozen(spelerHand, meerInformatie);
                    break;
                case Meldingen.Gestopt:
                    this.SpelerGestopt(spelerHand);
                    break;
                case Meldingen.Verdienen:
                    this.Verdienen(hand);
                    break;
                case Meldingen.YouDied:
                    this.TellDied(spelerHand);
                    break;
                case Meldingen.BlackJack:
                    this.BlackJack(spelerHand);
                    break;
                case Meldingen.BlackJackVerdienen:
                    this.BlackJackVerdienen(spelerHand, meerInformatie);
                    break;
                case Meldingen.GeenActie:
                    this.TellGeenActie(spelerHand);
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
                case Meldingen.FichesKoppen:
                    this.TellFiches(speler);
                    break;
                case Meldingen.MagNietVerdubbelen:
                    this.TellMagNietVerdubbeln(speler);
                    break;
                case Meldingen.MagNietSplitsen:
                    this.TellMagNietSplitsen(speler);
                    break;
                case Meldingen.NieuweHand:
                    this.TellNieuweHand(speler);
                    break;
                case Meldingen.NietGelijkFiches:
                    this.TellNietGelijkFiches(speler);
                    break;
                case Meldingen.VoorwaardenTafelFiches:
                    this.TellTafelMaxMinInzet(speler);
                    break;
                case Meldingen.Gestopt:
                    this.SpelerGestopt(speler);
                    break;
            }
        }

        /// <summary>
        /// Geef informatie over wat gebeurt.
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
            Console.WriteLine($"{hand.Speler.Naam} je mag een van deze acties kiezen.");
            for (int actie = 1; actie <= mogelijkActies.Count; actie++)
            {
                Console.WriteLine($" {actie.ToString()}- {mogelijkActies[actie - 1]}");
            }

            Console.WriteLine("Kies maar een van die acties. Type maar het nummer van een actie.");
            string answer = Console.ReadLine();
            while (!this.IsGeldigWaarde(answer, out actieNummer) || actieNummer > mogelijkActies.Count || actieNummer < 1)
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
            Console.WriteLine();
            Console.WriteLine($"Je hebt {hand.Speler.Fiches.WaardeVanDeFiches} als waarde van de fiches die in je portemonnee zijn.");
            Console.WriteLine();
            Console.WriteLine($"Type maar alleen nummers tussen {hand.Speler.HuidigeTafel.MinimalenZet} en {hand.Speler.HuidigeTafel.MaximaleInZet}.");
            ColorConsole.WriteLine(ConsoleColor.Cyan, $"{hand.Speler.Naam} Wat voor waarde wil je inzetten?");
            string answerWarde = Console.ReadLine();
            int waardeDieDeSpelerWilInzetten;
            while (!this.IsGeldigWaarde(answerWarde, out waardeDieDeSpelerWilInzetten) || waardeDieDeSpelerWilInzetten > hand.Speler.HuidigeTafel.MaximaleInZet || waardeDieDeSpelerWilInzetten < hand.Speler.HuidigeTafel.MinimalenZet)
            {
                Console.WriteLine();
                Console.WriteLine($"Type maar alleen nummers tussen {hand.Speler.HuidigeTafel.MinimalenZet} en {hand.Speler.HuidigeTafel.MaximaleInZet}.");
                if (waardeDieDeSpelerWilInzetten <= 0)
                {
                    Console.WriteLine("Mag geen letters of nummer die null of minder dan null zijn");
                }

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
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} je hebt {speler.Fiches.WaardeVanDeFiches} waarde van fiches in je portemonnee.");
            int waardeDieDeSpelerWilInzetten = 0;
            Thread.Sleep(2000);
            Console.WriteLine("Wil je fiches kopen.. J of N?");
            string answer = Console.ReadLine().ToLower();
            while (!this.IsAntwoordGoed(answer))
            {
                Console.WriteLine("Je mag alleen J of N typen!");
                answer = Console.ReadLine();
            }

            if (answer == "j")
            {
                Console.WriteLine($"{speler.Naam} Wat voor waarde wil je kopen?");
                string answerWarde = Console.ReadLine();

                while (!this.IsGeldigWaarde(answerWarde, out waardeDieDeSpelerWilInzetten) || waardeDieDeSpelerWilInzetten > speler.HuidigeTafel.Fiches.WaardeVanDeFiches || waardeDieDeSpelerWilInzetten <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Type maar alleen nummers tussen 1 en {speler.HuidigeTafel.Fiches.WaardeVanDeFiches}.");
                    if (waardeDieDeSpelerWilInzetten <= 0)
                    {
                        Console.WriteLine($"Mag niet het nummer null of minder dan null zijn of hoger dan{speler.HuidigeTafel.Fiches.WaardeVanDeFiches}.");
                    }
                    else
                    {
                        Console.WriteLine("Type maar een nummer.");
                    }

                    answerWarde = Console.ReadLine();
                }

                waarde = waardeDieDeSpelerWilInzetten;
                return true;
            }

            waarde = waardeDieDeSpelerWilInzetten;
            return false;
        }

        /// <inheritdoc/>
        public bool AskNieuwRondje(Speler speler)
        {
            Console.WriteLine();
            Thread.Sleep(2000);
            Console.WriteLine("Wil je nu een nieuw rondje doen of niet.. J of N?");
            string answer = Console.ReadLine().ToLower();
            while (!this.IsAntwoordGoed(answer))
            {
                Console.WriteLine("Je mag alleen J of N typen!");
                answer = Console.ReadLine();
            }

            if (answer == "j")
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void SleuitHetSpel()
        {
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Tot Ziens!");
        }
    }
}
