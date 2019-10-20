// <copyright file="ConsoleCommunicator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack
{
    using System;
    using HenE.GameBlackJack;

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
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} heeft {speler.Fiches.WaardeVanDeFiches} ingezet.");
        }

        /// <summary>
        /// Laat de speler weet hoeveel fiches hij hij heeft.
        /// </summary>
        /// <param name="speler">Speler.</param>
        /// <param name="hand">Hand van een speler.</param>
        private void ToonFiches(Speler speler, SpelerHand hand)
        {
            Console.WriteLine();
            Console.WriteLine($"Let op {speler.Naam}, De speler {hand.Speler.Naam} heeft {hand.Inzet.WaardeVanDeFiches} ingezet.");
        }

        /// <summary>
        /// method om te vertelen dat de hand is verliezen.
        /// </summary>
        /// <param name="hand">De hand die wordt verliezen.</param>
        private void Verliezen(SpelerHand hand)
        {
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Red, $"{hand.Speler.Naam} heeft {hand.Inzet.WaardeVanDeFiches} verliezen.");
        }

        /// <summary>
        /// Als de speler heeft fiches verdient.
        /// </summary>
        private void Verdienen(Hand hand)
        {
            SpelerHand spelerHand = hand as SpelerHand;
            Console.WriteLine();
            Console.WriteLine($"{spelerHand.Speler.Naam} Je heeft {spelerHand.Inzet.WaardeVanDeFiches}  verdient.");
        }

        /// <summary>
        /// Als de speler heeft fiches verdient.
        /// </summary>
        private void Verdienen(Speler speler, Hand hand)
        {
            SpelerHand spelerHand = hand as SpelerHand;
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} let op \n {spelerHand.Speler.Naam} Je heeft {spelerHand.Inzet.WaardeVanDeFiches} verdient.");
        }

        /// <summary>
        /// Vertelt hoe veel kaarten bij de hand staan.
        /// </summary>
        /// <param name="speler">Speler.</param>
        /// <param name="hand">de hand van de speler.</param>
        private void KaartenVanDeHand(Speler speler, SpelerHand hand)
        {
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} je hebt nu {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} punten bij je hand.");
            Console.WriteLine($"{speler.Naam} Je hebt nu");
            foreach (Kaart kaart in hand.Kaarten)
            {
                ColorConsole.WriteLine(ConsoleColor.Green, $" {kaart.Kleur} van {kaart.Teken}");
            }
        }

        /// <summary>
        /// Vertelt hoe veel kaarten bij de hand staan.
        /// </summary>
        /// <param name="speler">De speler die een meldje gekregen.</param>
        /// <param name="hand">de hand van de speler.</param>
        private void KaartenVanDeHand(Speler speler, Hand hand)
        {
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

            foreach (Kaart kaart in hand.Kaarten)
            {
                ColorConsole.WriteLine(ConsoleColor.Green, $" {kaart.Kleur} van {kaart.Teken}");
            }
        }

        /// <summary>
        /// Als de speler heeft fout gedaan dan laat hem dat weten.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        private void FoutMelding(SpelerHand hand)
        {
            Console.WriteLine();
            Console.WriteLine("Je hebt een fout gehad.");
        }

        private void HoldHand(SpelerHand hand)
        {
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Yellow, $"{hand.Speler.Naam} mag wachten totdat het volgend rondje start want je heeft {hand.Inzet.WaardeVanDeFiches} punten en de dealer heeft ...");
        }

        private void Gewonnen(SpelerHand hand)
        {
            Console.WriteLine();
            ColorConsole.WriteLine(ConsoleColor.Green, $"Wat leuk, {hand.Speler.Naam} bent gewonnen want je heeft {hand.Inzet.WaardeVanDeFiches} punten en de dealer heeft ... ");
        }

        /// <summary>
        /// Check of het antwoord geldig is of niet.
        /// </summary>
        /// <param name="answer">Het antwoorde die de speler wil doen.</param>
        /// <returns>Is Het antwoord good is of niet.</returns>
        private bool IsAntwoordGoed(string answer) => answer == "y" || answer == "n";

        /// <summary>
        /// Check of het waarde nummer is of niet.
        /// </summary>
        /// <param name="answer">Het antwoord.</param>
        /// <param name="waarde">De waarde als het een nummer is.</param>
        /// <returns>Is het antwoord goed is of niet.</returns>
        private bool IsGeldigWaarde(string answer, out int waarde) => int.TryParse(answer, out waarde);

        private void OngeldigeInzet(Speler speler)
        {
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
            Console.WriteLine();
            Console.WriteLine($"{hand.Speler.Naam} wil {actie}.");
        }

        /// <summary>
        /// Geef een melding aan de speler dat geen fiches heeft of niet veel fiches.
        /// </summary>
        /// <param name="speler">De speler die wordt geïnformeerd.</param>
        private void GeenFiches(Speler speler)
        {
            Console.WriteLine($"{speler.Naam} Je hebt geen fiches of het niet genoeg is.");
        }

        /// <summary>
        /// laat een speler weten dat een andere speler wordt gestopt.
        /// </summary>
        /// <param name="speler">De speler die wordt geïnformeerd.</param>
        /// <param name="hand">De hand van de speler die wordt gestopt.</param>
        private void SpelerGestopt(Speler speler, Hand hand)
        {
            if (hand.IsDealerHand)
            {
                Console.WriteLine($"{speler.Naam} let op dat de dealer wordt gestopt.");
            }
            else
            {
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
            Console.WriteLine();
            Console.WriteLine($"{hand.Speler.Naam} je wordt gestopt.");
        }

        /// <summary>
        /// Laat de persoon weten dat de andere speler is black jack.
        /// </summary>
        /// <param name="hand">De hand die black jack is.</param>
        private void BlackJack(Hand hand)
        {
            Console.WriteLine();
            SpelerHand spelerHand = hand as SpelerHand;
            Console.WriteLine($"{spelerHand.Speler.Naam} heeft 21 score. Hij is de Black Jack.");
        }

        /// <summary>
        /// Tell de speler dat hij dood is.
        /// </summary>
        /// <param name="hand">De hand die dood wordt.</param>
        private void TellDied(Hand hand)
        {
            Console.WriteLine();
            SpelerHand spelerHand = hand as SpelerHand;
            Console.WriteLine($"{spelerHand.Speler.Naam} je bent gestopt want je hebt {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
        }

        /// <summary>
        /// Tell de speler dat de andere dood is.
        /// </summary>
        /// <param name="speler">De speler die een message ontvangt.</param>
        /// <param name="hand">De hand die dood wordt.</param>
        private void TellDied(Speler speler, Hand hand)
        {
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
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} let op,");
            Console.WriteLine($"dealer is dood want hij heet {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
        }

        /// <summary>
        /// Tell de speler dat de dealer heeft punten tussen 17 en 21 score.
        /// </summary>
        /// <param name="speler">Speler die krijgt een message.</param>
        /// <param name="hand">huidige hand.</param>
        private void TellDealerGepassed(Speler speler, Hand hand)
        {
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} let op,");
            Console.WriteLine($"dealer is gepassed want hij heet {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} score.");
        }

        /// <summary>
        /// Als de speler heeft fiches gekocht, dan toon het waarde van zijn waarde.
        /// </summary>
        /// <param name="speler">De speler die heeft gekocht.</param>
        private void TellFiches(Speler speler)
        {
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} je hebt nu {speler.Fiches.WaardeVanDeFiches} waarde van fiches.");
        }

        /// <summary>
        /// Tell de speler dat hij mag niet verdubellen.
        /// </summary>
        /// <param name="speler">De speler die een message heeft gekregen.</param>
        private void TellMagNietVerdubbeln(Speler speler)
        {
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} je Mag niet verdubbeln.");
        }

        /// <summary>
        /// Tell de speler dat hij niet mag splitsen.
        /// </summary>
        /// <param name="speler">De speler die een massage ontvangt.</param>
        private void TellMagNietSplitsen(Speler speler)
        {
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} Je mag niet splitsen.");
        }
    }
}
