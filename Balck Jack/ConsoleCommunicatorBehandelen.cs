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
    using HenEBalck_Jack.Helpers;

    /// <summary>
    /// De console die gaat communicte tussen de speler en het spel doen.
    /// </summary>
    public partial class ConsoleCommunicatorBehandelen : ICommunicate
    {
        private readonly BlackJackPointsCalculator blackJackPointsCalculator = new BlackJackPointsCalculator();

        /// <inheritdoc/>
        public void TellPersoon(Meldingen melding)
        {
            switch (melding)
            {
                case Meldingen.Welkom:
                    this.Welkom();
                    break;
                case Meldingen.Fout:
                    this.TellFout();
                    break;
                default:
                    break;
            }
        }

        /// <inheritdoc/>
        public string AskPersoon(Meldingen melding)
        {
            switch (melding)
            {
                case Meldingen.DeNaam:
                    return this.VraagOmNaam();
                default:
                    return string.Empty;
            }
        }

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
                case Meldingen.Welkom:
                    this.WelkomSpeler(speler);
                    break;
                case Meldingen.ToonFichesInPortemonnee:
                    this.TellFichesInPortemonnee(speler);
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
            Thread.Sleep(1000);
            int actieNummer;
            Console.WriteLine($"{hand.Speler.Naam} je mag een van deze acties kiezen:");
            for (int actie = 1; actie <= mogelijkActies.Count; actie++)
            {
                Console.WriteLine($" {actie.ToString()}- {mogelijkActies[actie - 1]}");
            }

            Thread.Sleep(1000);
            Console.WriteLine("Kies het nummer van één van de bovenstaande acties.");
            string answer = Console.ReadLine();
            while (!this.IsGeldigWaarde(answer, out actieNummer) || actieNummer > mogelijkActies.Count || actieNummer < 1)
            {
                Console.WriteLine("Dit nummer is ongeldig.");
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
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine($"In jouw portemonnee heb je een waarde van {hand.Speler.Fiches.WaardeVanDeFiches} fiches.");
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Cyan, $"{hand.Speler.Naam}: Wat voor waarde wil je inzetten?");
            Thread.Sleep(1000);
            ColorConsole.WriteLine(ConsoleColor.Cyan, $"{hand.Speler.Naam}: Je moet minimaal een waarde van {hand.Speler.HuidigeTafel.MinimalenZet} fiches en maximaal een waarde van {hand.Speler.HuidigeTafel.MaximaleInZet} fiches inzetten.");
            string answerWarde = Console.ReadLine();
            int waardeDieDeSpelerWilInzetten;
            while (!this.IsGeldigWaarde(answerWarde, out waardeDieDeSpelerWilInzetten) || waardeDieDeSpelerWilInzetten > hand.Speler.HuidigeTafel.MaximaleInZet || waardeDieDeSpelerWilInzetten < hand.Speler.HuidigeTafel.MinimalenZet)
            {
                Console.WriteLine();
                Console.WriteLine($"Kies een geldige waarde tussen {hand.Speler.HuidigeTafel.MinimalenZet} en {hand.Speler.HuidigeTafel.MaximaleInZet}.");
                if (waardeDieDeSpelerWilInzetten <= 0)
                {
                    Console.WriteLine("De waarde moet een nummer zijn.");
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
            Console.WriteLine($"In je portemonnee heb je een waarde van {speler.Fiches.WaardeVanDeFiches} fiches.");
            Console.WriteLine("Je hebt :");
            Console.WriteLine();
            int waardeDieDeSpelerWilInzetten = 0;
            Thread.Sleep(2000);
            Console.WriteLine("Wil je fiches kopen J of N?");
            string answer = Console.ReadLine().ToLower();
            while (!this.IsAntwoordGoed(answer))
            {
                Console.WriteLine("Je mag alleen J of N typen!");
                answer = Console.ReadLine();
            }

            if (answer == "j")
            {
                Console.WriteLine($"{speler.Naam}: Voor welke bedrag wil je fiches kopen?");
                string answerWarde = Console.ReadLine();

                while (!this.IsGeldigWaarde(answerWarde, out waardeDieDeSpelerWilInzetten) || waardeDieDeSpelerWilInzetten > speler.HuidigeTafel.Fiches.WaardeVanDeFiches || waardeDieDeSpelerWilInzetten <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Je mag alleen tussen 1 en {speler.HuidigeTafel.Fiches.WaardeVanDeFiches} fiches kopen.");
                    if (waardeDieDeSpelerWilInzetten <= 0)
                    {
                        Console.WriteLine($"Het aantal fiches mag niet het nummer nul, minder dan nul of hoger zijn dan {speler.HuidigeTafel.Fiches.WaardeVanDeFiches}.");
                    }
                    else
                    {
                        Console.WriteLine("De waarde moet een nummer zijn.");
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
            Console.WriteLine("Wil je nu een nieuw ronde J of N?");
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
            ColorConsole.WriteLine(ConsoleColor.Green, "Bedankt voor je spelen.");
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Tot Ziens!");
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Laat de speler zien de inzet van de hand.
        /// </summary>
        /// <param name="hand">Speler hand.</param>
        private void ToonInzet(SpelerHand hand)
        {
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Je inzet is {0}", hand.Inzet.WaardeVanDeFiches);
        }

        /// <summary>
        /// Laat de speler weet hoeveel fiches hij hij heeft.
        /// </summary>
        /// <param name="speler">Speler.</param>
        private void ToonFiches(Speler speler)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} heeft {speler.Fiches.WaardeVanDeFiches} ingezet.");
        }

        /// <summary>
        /// Laat de speler weet hoeveel fiches hij hij heeft.
        /// </summary>
        /// <param name="speler">Speler die een message zal krijgen.</param>
        /// <param name="hand">Hand van een speler.</param>
        private void ToonFiches(Speler speler, SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"Let op {speler.Naam}, De speler {hand.Speler.Naam} heeft {hand.Inzet.WaardeVanDeFiches} ingezet.");
        }

        /// <summary>
        /// method om te vertelen dat de hand is verliezen.
        /// </summary>
        /// <param name="hand">De hand die wordt verliezen.</param>
        private void Verliezen(SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Red, $"{hand.Speler.Naam} heeft {hand.Inzet.WaardeVanDeFiches} verloren.");
        }

        /// <summary>
        /// Als de speler heeft fiches verdient.
        /// </summary>
        /// <param name="hand">De hand van een speler.</param>
        private void Verdienen(Hand hand)
        {
            Thread.Sleep(2000);
            SpelerHand spelerHand = hand as SpelerHand;
            Console.WriteLine();
            Console.Write($"{spelerHand.Speler.Naam} Je hebt ");
            ColorConsole.Write(ConsoleColor.Red, spelerHand.Inzet.WaardeVanDeFiches.ToString());
            Console.WriteLine(" verdiend.");
        }

        /// <summary>
        /// Als de speler heeft fiches verdient.
        /// </summary>
        /// <param name="speler">Speler die een message zal krijgen.</param>
        /// <param name="hand">De hand van een speler.</param>
        private void Verdienen(Speler speler, Hand hand)
        {
            Thread.Sleep(2000);
            SpelerHand spelerHand = hand as SpelerHand;
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} let op \n {spelerHand.Speler.Naam} Je hebt {spelerHand.Inzet.WaardeVanDeFiches} verdiend.");
        }

        /// <summary>
        /// Vertelt hoe veel kaarten bij de hand staan.
        /// </summary>
        /// <param name="speler">Speler die een message zal krijgen.</param>
        /// <param name="hand">de hand van de speler.</param>
        private void KaartenVanDeHand(Speler speler, SpelerHand hand)
        {
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.Write($"{speler.Naam}: Je hebt nu ");
            ColorConsole.Write(ConsoleColor.Red, this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten).ToString());
            Console.WriteLine(" punten in je hand.");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine($"{speler.Naam}: Je hebt nu");
            foreach (Kaart kaart in hand.Kaarten)
            {
                ColorConsole.WriteLine(ConsoleColor.Green, $" {kaart.Kleur} {kaart.Teken}");
            }
        }

        /// <summary>
        /// Vertelt hoe veel kaarten bij de hand staan.
        /// </summary>
        /// <param name="speler">Speler die een message zal krijgen.</param>
        /// <param name="hand">de hand van de speler.</param>
        private void KaartenVanDeHand(Speler speler, Hand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"Let Op {speler.Naam}");
            if (hand.IsDealerHand)
            {
                Console.Write($"De waarde van de kaarten die in de hand van de dealer is ");
                ColorConsole.WriteLine(ConsoleColor.Yellow, $"{this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)}");
                Console.WriteLine($"de dealer heeft nu");
            }
            else
            {
                SpelerHand spelerHand = hand as SpelerHand;
                Console.WriteLine($"{spelerHand.Speler.Naam} heeft nu");
            }

            Thread.Sleep(2000);
            foreach (Kaart kaart in hand.Kaarten)
            {
                ColorConsole.WriteLine(ConsoleColor.Green, $" {kaart.Kleur} {kaart.Teken}");
            }
        }

        /// <summary>
        /// Als de speler heeft fout gedaan dan laat hem dat weten.
        /// </summary>
        /// <param name="hand">De hand van een speler.</param>
        private void FoutMelding(SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine("Je hebt een fout gehad.");
        }

        private void HoldHand(SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Yellow, $"{hand.Speler.Naam} mag wachten met dit hand totdat het volgend rondje start want je heeft {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score en de dealer heeft het zelfde.");
        }

        private void Gewonnen(SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Green, $"Wat leuk, {hand.Speler.Naam}: Je bent gewonnen want je heeft {hand.Inzet.WaardeVanDeFiches} punten en de dealer heeft .");
        }

        /// <summary>
        /// Check of het antwoord geldig is of niet.
        /// </summary>
        /// <param name="answer">Het antwoorde die de speler wil doen.</param>
        /// <returns>Is Het antwoord good is of niet.</returns>
        private bool IsAntwoordGoed(string answer) => answer == "j" || answer == "n";

        /// <summary>
        /// Check of het waarde nummer is of niet.
        /// </summary>
        /// <param name="answer">Het antwoord.</param>
        /// <param name="waarde">De waarde als het een nummer is.</param>
        /// <returns>Is het antwoord goed is of niet.</returns>
        private bool IsGeldigWaarde(string answer, out int waarde) => int.TryParse(answer, out waarde);

        private void OngeldigeInzet(Speler speler)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} mag andere inzetten kiezen. Je mag alleen tussen {speler.HuidigeTafel.MinimalenZet} en {speler.HuidigeTafel.MaximaleInZet} inzetten.");
        }

        /// <summary>
        /// Toont een melding over de actie die de speler heeft gekozen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="actie">De actie die de speler heeft gekozen.</param>
        private void ActieGekozen(SpelerHand hand, string actie)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{hand.Speler.Naam} wil {actie}.");
        }

        /// <summary>
        /// Geef een melding aan de speler dat geen fiches heeft of niet veel fiches.
        /// </summary>
        /// <param name="speler">De speler die wordt geïnformeerd.</param>
        private void GeenFiches(Speler speler)
        {
            Thread.Sleep(2000);
            Console.WriteLine($"{speler.Naam}: Je hebt geen fiches of zij zijn niet genoeg.");
        }

        /// <summary>
        /// laat een speler weten dat hij  wordt gestopt.
        /// </summary>
        /// <param name="speler">De speler die wordt geïnformeerd.</param>
        private void SpelerGestopt(Speler speler)
        {
            Thread.Sleep(2000);
            ColorConsole.WriteLine(ConsoleColor.Red, $"{speler.Naam}: Je wordt gestopt.");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// laat een speler weten dat een andere speler wordt gestopt.
        /// </summary>
        /// <param name="speler">De speler die wordt geïnformeerd.</param>
        /// <param name="hand">De hand van de speler die wordt gestopt.</param>
        private void SpelerGestopt(Speler speler, Hand hand)
        {
            Thread.Sleep(2000);
            if (hand.IsDealerHand)
            {
                Console.WriteLine($"{speler.Naam} let op dat de dealer wordt gestopt.");
            }
            else
            {
                Thread.Sleep(2000);
                SpelerHand spelerHand = hand as SpelerHand;
                Console.WriteLine($"{speler.Naam} let op dat {spelerHand.Speler.Naam} wordt gestopt.");
            }
        }

        /// <summary>
        /// laat de speler weten dat hij wordt gestopt.
        /// </summary>
        /// <param name="hand">De hand van de speler die wordt gestopt,.</param>
        private void SpelerGestopt(SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{hand.Speler.Naam}: Je wordt gestopt.");
        }

        /// <summary>
        /// Laat de persoon weten dat de andere speler is black jack.
        /// </summary>
        /// <param name="hand">De hand die black jack is.</param>
        private void BlackJack(SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{hand.Speler.Naam}: Je hebt 21 score. Je bent de Black Jack.");
        }

        /// <summary>
        /// Tell de speler dat hij dood is.
        /// </summary>
        /// <param name="hand">De hand die dood wordt.</param>
        private void TellDied(SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            SpelerHand spelerHand = hand as SpelerHand;
            Console.WriteLine($"{spelerHand.Speler.Naam}: Deze hand wordt gestopt want je score is hoger dan 21. je hebt {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
        }

        /// <summary>
        /// Tell de speler dat de andere dood is.
        /// </summary>
        /// <param name="speler">De speler die een message ontvangt.</param>
        /// <param name="hand">De hand die dood wordt.</param>
        private void TellDied(Speler speler, Hand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            SpelerHand spelerHand = hand as SpelerHand;
            Console.WriteLine($"{speler.Naam} let op {spelerHand.Speler.Naam} hij is gestopt want hij heeft {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
        }

        /// <summary>
        /// Tell de speler dat weten de dealer dood is.
        /// </summary>
        /// <param name="speler">De speler die een message krijget.</param>
        /// <param name="hand">De hand.</param>
        private void TellDealerDied(Speler speler, Hand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} let op,");
            Console.WriteLine($"Dealer is dood want hij heeft {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
        }

        /// <summary>
        /// Tell de speler dat de dealer heeft punten tussen 17 en 21 score.
        /// </summary>
        /// <param name="speler">Speler die krijgt een message.</param>
        /// <param name="hand">huidige hand.</param>
        private void TellDealerGepassed(Speler speler, Hand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} let op,");
            Console.WriteLine($"Dealer heeft {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
            Thread.Sleep(2000);
            Console.WriteLine("Hij gaat de resultaten bereken dan weet je of je gewonnen bent of niet.");
            Thread.Sleep(2000);
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Red, "Wacht even!");
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Als de speler heeft fiches gekocht, dan toon het waarde van zijn waarde.
        /// </summary>
        /// <param name="speler">De speler die heeft gekocht.</param>
        private void TellFiches(Speler speler)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            FichesConsolePrinter.PrintWaardeFiches(speler.Fiches);
            Thread.Sleep(2000);
            FichesConsolePrinter.PrintFiches(speler.Fiches);
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} je hebt nu {speler.Fiches.WaardeVanDeFiches} waarde van fiches.");
        }

        /// <summary>
        /// Tell de speler dat hij mag niet verdubellen.
        /// </summary>
        /// <param name="speler">De speler die een message heeft gekregen.</param>
        private void TellMagNietVerdubbeln(Speler speler)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam}: Je Mag niet verdubbelen.");
        }

        /// <summary>
        /// Tell de speler dat hij niet mag splitsen.
        /// </summary>
        /// <param name="speler">De speler die een massage ontvangt.</param>
        private void TellMagNietSplitsen(Speler speler)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} Je mag niet splitsen.");
        }

        /// <summary>
        /// laat de speler weten dat hij een nieuwe hand heeft gekregen.
        /// </summary>
        /// <param name="speler">De speler die een message heeft gekregen.</param>
        private void TellNieuweHand(Speler speler)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam}: Je hebt een nieuwe hand gekregen.");
        }

        /// <summary>
        /// tell de speler dat hij fiches heeft verdienen.
        /// </summary>
        /// <param name="spelerHand">De hand van de speler.</param>
        /// <param name="info">Meer informatie.</param>
        private void BlackJackVerdienen(SpelerHand spelerHand, string info)
        {
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine($"{spelerHand.Speler.Naam}: Je hebt {info} verdiend.");
        }

        /// <summary>
        /// laat de speler dat weten hij geen actie heeft.
        /// </summary>
        /// <param name="spelerHand">de hand van de speler.</param>
        private void TellGeenActie(SpelerHand spelerHand)
        {
            Console.WriteLine();
            Thread.Sleep(2000);
            Console.WriteLine($"{spelerHand.Speler.Naam}: Je heeft geen actie. Je mag wachten.");
        }

        /// <summary>
        /// Laat de speler weten dat De fiches met de speler en de fiches die de speler wil inzetten zijn niet gelijk.
        /// </summary>
        /// <param name="speler">De speler die een message kreeg.</param>
        private void TellNietGelijkFiches(Speler speler)
        {
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine($"{speler.Naam} de fiches die je hem wil bij je hand inzetten en de fiches die in jouw portemonnee staan zijn niet gelijk.");
            Console.WriteLine("Of je heeft geen fiches.");
        }

        /// <summary>
        /// Laat de speler weten de mixinzet en mininzet van de tafel.
        /// </summary>
        /// <param name="speler">De speler die een message zal krijgen.</param>
        private void TellTafelMaxMinInzet(Speler speler)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam}: Je moet  minimale waarde van {speler.HuidigeTafel.MinimalenZet} in je portemonnee staan.");
        }

        /// <summary>
        /// Stuur een welkome message aan de speler.
        /// </summary>
        private void Welkom()
        {
            ColorConsole.WriteLine(ConsoleColor.Green, "=====> Welkom! <======");
            Console.WriteLine("Leuk dat je Black Jack komt spelen.");
        }

        /// <summary>
        /// Vraag de speler om zijn naam te geven.
        /// </summary>
        /// <returns>De naam van de speler.</returns>
        private string VraagOmNaam()
        {
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Wat is je naam?");
            Console.WriteLine("Je mag alleen letters typen.");
            return Console.ReadLine();
        }

        /// <summary>
        /// Laat de speler weten dat hij een fout heeft gedaan.
        /// </summary>
        private void TellFout()
        {
            Console.WriteLine("Je hebt een fout gedaan!");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Type maar een name.");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Je mag t/m 15 letters gebruiken.");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Je mag geen nummers gebruiken.");
        }

        /// <summary>
        /// Zeg welkom tegen de speler.
        /// </summary>
        /// <param name="speler">de speler die een message heeft gekregen.</param>
        private void WelkomSpeler(Speler speler)
        {
            Console.Write("Hoi ");
            ColorConsole.WriteLine(ConsoleColor.Yellow, $"{speler.Naam}!");
        }

        private void TellFichesInPortemonnee(Speler speler)
        {
            Console.WriteLine($"In jouw portemonnee heb je een waarde van {speler.Fiches.WaardeVanDeFiches} fiches.");
            Thread.Sleep(1000);
            Console.WriteLine();
            FichesConsolePrinter.PrintFiches(speler.Fiches);
        }
    }
}
