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
            Console.WriteLine($"{speler.Naam} heeft {0} ingezet.", speler.Fiches.WaardeVanDeFiches);
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
            ColorConsole.WriteLine(ConsoleColor.Red, $"{hand.Speler.Naam} hebt {hand.Inzet.WaardeVanDeFiches} verliezen.");
        }

        /// <summary>
        /// Als de speler heeft fiches verdient.
        /// </summary>
        private void Verdienen(Speler speler)
        {
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam}Je heeft ... verdient.");
        }

        /// <summary>
        /// Vertelt hoe veel kaarten bij de hand staan.
        /// </summary>
        /// <param name="hand">de hand van de speler.</param>
        private void KaartenVanDeHand(SpelerHand hand)
        {
            Console.WriteLine();
            Console.WriteLine($"{hand.Speler.Naam} je hebt nu {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} punten bij je hand.");
            Console.WriteLine($"{hand.Speler.Naam} Je hebt nu");
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
        private void KaartenVanDeHand(Speler speler, SpelerHand hand)
        {
            Console.WriteLine();
            Console.WriteLine($"Let Op {speler.Naam}");
            Console.WriteLine($"{hand.Speler.Naam} heeft nu");
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
    }
}
