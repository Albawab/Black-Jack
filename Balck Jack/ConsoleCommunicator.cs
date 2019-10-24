// <copyright file="ConsoleCommunicator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack
{
    using System;
    using System.Threading;
    using HenE.GameBlackJack;
    using HenEBalck_Jack.Helpers;

    /// <summary>
    /// De console die gaat communicte tussen de speler en het spel doen.
    /// </summary>
    public partial class ConsoleCommunicatorBehandelen
    {
        /// <summary>
        /// Laat de speler zien de inzet van de hand.
        /// </summary>
        /// <param name="hand">Speler hand.</param>
        private void ToonInzet(SpelerHand hand)
        {
            Console.WriteLine();
            Console.WriteLine("Uw inzet is {0}", hand.Inzet.WaardeVanDeFiches);
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
            Console.WriteLine(" verdient.");
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
            Console.WriteLine($"{speler.Naam} let op \n {spelerHand.Speler.Naam} Je hebt {spelerHand.Inzet.WaardeVanDeFiches} verdient.");
        }

        /// <summary>
        /// Vertelt hoe veel kaarten bij de hand staan.
        /// </summary>
        /// <param name="speler">Speler die een message zal krijgen.</param>
        /// <param name="hand">de hand van de speler.</param>
        private void KaartenVanDeHand(Speler speler, SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.Write($"{speler.Naam} je hebt nu ");
            ColorConsole.Write(ConsoleColor.Red, this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten).ToString());
            Console.WriteLine(" punten bij je hand.");
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} Je hebt nu");
            foreach (Kaart kaart in hand.Kaarten)
            {
                ColorConsole.WriteLine(ConsoleColor.Green, $" {kaart.Kleur} van {kaart.Teken}");
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
                Console.WriteLine($"De waarde van de kaarten die bij de hand van de dealer is {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)}");
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
                ColorConsole.WriteLine(ConsoleColor.Green, $" {kaart.Kleur} van {kaart.Teken}");
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
            ColorConsole.WriteLine(ConsoleColor.Green, $"Wat leuk, {hand.Speler.Naam} Je bent gewonnen want je heeft {hand.Inzet.WaardeVanDeFiches} punten en de dealer heeft ... ");
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
            Console.WriteLine($"{speler.Naam} mag andere inzetten kiezen. Je mag alleen tussen {speler.HuidigeTafel.MinimalenZet} en {speler.HuidigeTafel.MaximaleInZet}");
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
            Console.WriteLine($"{speler.Naam} Je hebt geen fiches of zij zijn niet genoeg.");
        }

        /// <summary>
        /// laat een speler weten dat hij  wordt gestopt.
        /// </summary>
        /// <param name="speler">De speler die wordt geïnformeerd.</param>
        private void SpelerGestopt(Speler speler)
        {
            Thread.Sleep(2000);
            ColorConsole.WriteLine(ConsoleColor.Red, $"{speler.Naam} je wordt gestopt.");
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
            Console.WriteLine($"{hand.Speler.Naam} je wordt gestopt.");
        }

        /// <summary>
        /// Laat de persoon weten dat de andere speler is black jack.
        /// </summary>
        /// <param name="hand">De hand die black jack is.</param>
        private void BlackJack(SpelerHand hand)
        {
            Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine($"{hand.Speler.Naam} je hebt 21 score. je bent de Black Jack.");
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
            Console.WriteLine($"{spelerHand.Speler.Naam} Dit hand wordt gestopt want dit hand heeft {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
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
            Console.WriteLine($"dealer is dood want hij heeft {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
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
            Console.WriteLine($"dealer heeft {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
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
            Thread.Sleep(3000);
            FichesConsolePrinter.PrintFiches(speler.Fiches);
            Thread.Sleep(3000);
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
            Console.WriteLine($"{speler.Naam} je Mag niet verdubbelen.");
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
            Console.WriteLine($"{speler.Naam} je hebt een nieuwe hand gekregen.");
        }

        /// <summary>
        /// tell de speler dat hij fiches heeft verdienen.
        /// </summary>
        /// <param name="spelerHand">De hand van de speler.</param>
        /// <param name="info">Meer informatie.</param>
        private void BlackJackVerdienen(SpelerHand spelerHand, string info)
        {
            Console.WriteLine();
            Console.WriteLine($"{spelerHand.Speler.Naam} je hebt {info} verdient.");
        }

        /// <summary>
        /// laat de speler dat weten hij geen actie heeft.
        /// </summary>
        /// <param name="spelerHand">de hand van de speler.</param>
        private void TellGeenActie(SpelerHand spelerHand)
        {
            Console.WriteLine();
            Thread.Sleep(2000);
            Console.WriteLine($"{spelerHand.Speler.Naam} je heeft geen actie. Je mag wachten.");
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
            Console.WriteLine($"{speler.Naam} je moet  minimale waarde van {speler.HuidigeTafel.MinimalenZet} in je portemonnee staan.");
        }
    }
}
