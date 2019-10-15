// <copyright file="ConsoleCommunicator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack
{
    using System;
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Interface;
    using HenE.GameBlackJack.SpelSpullen;

    public class ConsoleCommunicator : ICommunicate
    {

        /// <summary>
        /// geef informatie over iets gebeurt.
        /// Wijs melding naar de juiste methode die de melding toont.
        /// </summary>
        /// <param name="hand">De hand die krijgt een melding.</param>
        /// <param name="melding">De text van een melding.</param>
        public void TellHand(SpelerHand hand, Meldingen melding)
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
                    this.KaartenVanDeHand(hand);
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
                case Meldingen.Verdienen:
                    this.Verdienen(speler);
                    break;
                case Meldingen.OngeldigeInzet:
                    this.OngeldigeInzet(speler);
                    break;
                case Meldingen.ToonInzet:
                    this.ToonFiches(speler);
                    break;
            }
        }

        /// <summary>
        /// Laat de speler zien de inzet van de hand.
        /// </summary>
        /// <param name="hand">Speler hand.</param>
        private void ToonInzet(SpelerHand hand)
        {
            Console.WriteLine("Uw inzet is {0}", hand.Inzet.WaardeVanDeFiches);
        }

        /// <summary>
        /// Laat de speler weet hoeveel fiches hij hij heeft.
        /// </summary>
        /// <param name="speler">Speler.</param>
        private void ToonFiches(Speler speler)
        {
            Console.WriteLine("Uw inzet is {0}", speler.Fiches.WaardeVanDeFiches);
        }

        /// <summary>
        /// method om te vertelen dat de hand is verliezen.
        /// </summary>
        /// <param name="hand">De hand die wordt verliezen.</param>
        private void Verliezen(SpelerHand hand)
        {
            Console.WriteLine($"Je hebt {hand.Inzet.WaardeVanDeFiches} verliezen.");
        }

        /// <summary>
        /// Als de speler heeft fiches verdient.
        /// </summary>
        private void Verdienen(Speler speler)
        {
            Console.WriteLine($"{speler.Naam}Je heeft ... verdient.");
        }

        /// <summary>
        /// Vertelt hoe veel kaarten bij de hand staan.
        /// </summary>
        /// <param name="hand">de hand van de speler.</param>
        private void KaartenVanDeHand(SpelerHand hand)
        {
            Console.WriteLine($"{hand.Speler.Naam} je hebt nu ... punten bij je hand.");
        }

        /// <summary>
        /// Als de speler heeft fout gedaan dan laat hem dat weten.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        private void FoutMelding(SpelerHand hand)
        {
            Console.WriteLine("Je hebt geen nummer ingevoegd of een nummer die boven niet bestaat. Voeg maar een nummer in.");
        }

        private void HoldHand(SpelerHand hand)
        {
            Console.WriteLine($"{hand.Speler.Naam}Je mag wachten totdat het volgend rondje start want je heeft ... punten en de dealer heeft ...");
        }

        private void Gewonnen(SpelerHand hand)
        {
            Console.WriteLine($"Wat leuk, {hand.Speler.Naam} Je bent gewonnen want je heeft ... punten en de dealer heeft ... ");
        }

        public bool AskWhichAction(SpelerHand hand, Vragen vragen)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ask de speler om fiches voor de hand te inzetten.
        /// </summary>
        /// <param name="hand">De hand die een fiches krijgt.</param>
        /// <param name="waarde">de waarde van de fiches.</param>
        /// <returns>Is de speler wil inzetten of niet.</returns>
        public bool AskFichesInzetten(SpelerHand hand, out int waarde)
        {
            int waardeDieDeSpelerWilInzetten = 0;
            Console.WriteLine("Wat voor waarde wil je kopen?");
            string answerWarde = Console.ReadLine();
            while (!this.IsGeldigWaarde(answerWarde, out waardeDieDeSpelerWilInzetten))
            {
                Console.WriteLine("Type maar een nummer.");
                answerWarde = Console.ReadLine();
            }

            waarde = waardeDieDeSpelerWilInzetten;
            return true;
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
            Console.WriteLine($"{speler.Naam} Je mag andere inzetten kiezen. Je mag alleen tussen {speler.HuidigeTafel.MinimalenZet} en {speler.HuidigeTafel.MaximaleInZet}");
        }

        public bool AskFichesKopen(Speler speler, out int Waarde)
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
                Console.WriteLine("Wat voor waarde wil je kopen?");
                string answerWarde = Console.ReadLine();
                while (!this.IsGeldigWaarde(answerWarde, out waardeDieDeSpelerWilInzetten))
                {
                    Console.WriteLine("Type maar een nummer.");
                    answerWarde = Console.ReadLine();
                }

                Waarde = waardeDieDeSpelerWilInzetten;
                return true;
            }

            Waarde = waardeDieDeSpelerWilInzetten;
            return false;
        }
    }
}
