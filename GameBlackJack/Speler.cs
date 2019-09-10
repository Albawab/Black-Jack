// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Slaan de gegevens van de speler op.
    /// </summary>
    public class Speler : Persoon
    {
        /// <summary>
        /// Hier staan de handen van de spelers.
        /// </summary>
        private readonly List<Hand> handen = new List<Hand>();

        /// <summary>
        /// Hoeveel fiches de speler heeft.
        /// </summary>
        private readonly Fiches fiches = new Fiches();

        /// <summary>
        /// Initializes a new instance of the <see cref="Speler"/> class.
        /// </summary>
        /// <param name="naam"> De naam van de speler.</param>
        public Speler(string naam)
            : base(naam)
        {
        }

        /// <summary>
        /// Gets Waar de speler wil zitten.
        /// </summary>
        public Tafel HuidigeTafel { get; private set; }

        /// <summary>
        /// Gets de hand van de speler.
        /// </summary>
        public Hand Hand { get; private set; }

        /// <summary>
        /// Gets de fiches die met de speler zijn.
        /// </summary>
        public Fiches Fiches
        {
            get
            {
                return this.fiches;
            }
        }

        /// <summary>
        /// Waar de speler wil zitten.
        /// </summary>
        /// <param name="tafel">De tafel waar de speler wil zitten.</param>
        /// <param name="positie">De nummer van de plek.</param>
        /// <returns>of mag zitten of niet.</returns>
        public bool GaatAanTafelZitten(Tafel tafel, uint positie)
        {
            // controleren of tafel niet null is

            // als je al ergens anders zat, moet je daar weg gaan
            if (this.HuidigeTafel != null)
            {
                this.VerlaatTafel();
            }

            return tafel.SpelerNeemtPlaats(this, positie);
        }

        /// <summary>
        /// Als de speler heeft de tafe verlaten.
        /// </summary>
        /// <returns>Heeft de speler de tafel verlaten of niet.</returns>
        public bool VerlaatTafel()
        {
            // als ik nergens zit, is dat prima
            if (this.HuidigeTafel == null)
            {
                return true;
            }

            return this.HuidigeTafel.SpelerVerlaatTafel(this);
        }

        /// <summary>
        /// De speler bepaalt wat hij wil kopen.
        /// </summary>
        /// <param name="hetBedrag">De waarde van de fiches.</param>
        /// <param name="dealer">Huidige dealer.</param>
        /// <param name="fichesBak">Huidige fiches bak.</param>
        public void Koopfiches(int hetBedrag, Dealer dealer, Fiches fichesBak)
        {
            // HelperFiches helperFiches = new HelperFiches();
            // Fiche createFiche = helperFiches.OmzettenWaardeDieDeSpelerwil_TotEenFiche(hetBedrag, fichesBak, dealer);
            // this.Fiches.Add(createFiche);
        }

        /// <summary>
        /// Zoek in de portemonnee voor een fiche.
        /// </summary>
        /// <param name="gekozen">Wat de speler heeft gekozen.</param>
        /// <param name="spel">Dit Spel.</param>
        public void FichesZetten(List<int> gekozen, Spel spel)
        {
            /*List<Fiche> itemGekozen = new List<Fiche>();
            foreach (int item in gekozen)
            {
                Fiche fiche = this.portemonnee[item - 1];
                Hand hand = new Hand(this);
                spel.VoegEenHandIn(hand);
                this.handen.Add(hand);
                hand.VoegEenFichesIn(fiche);
                itemGekozen.Add(fiche);
            }

            foreach (Fiche fiche1 in itemGekozen)
            {
                this.portemonnee.Remove(fiche1);
            }
            */
        }

        /// <summary>
        /// Geef de hande terug.
        /// </summary>
        /// <returns>De lijst van de handen.</returns>
        public List<Hand> GeefHanden()
        {
            return this.handen;
        }

        /// <summary>
        /// Voeg een hand aan de lijst van de handen.
        /// </summary>
        /// <param name="hand">Nieuwe hand.</param>
        public void VoegEenHandIn(Hand hand)
        {
            this.handen.Add(hand);
        }

        /// <summary>
        /// Controleer of de speler mag de waarde zetten.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="huidigeFiche">Huidige fiche.</param>
        public void ZetFiches(SpelSpullen.Tafel tafel, Hand hand, Fiche huidigeFiche)
        {
            /*foreach (Fiches fiche in this.Portemonnee)
            {
                if (fiche == huidigeFiche)
                {
                    this.Portemonnee.Remove(fiche);

                    hand.VoegEenFichesIn(fiche);
                }
            }*/
        }

        /// <summary>
        /// De speler beslist wat wil hij doen.
        /// </summary>
        /// <param name="beslissing">Wat de speler wil.</param>
        /// <returns>Wil de speler mee doen of niet.</returns>
        public bool WilDoen(Beslissing beslissing)
        {
            bool beslissen = false;
            switch (beslissing)
            {
                case Beslissing.Verdubbelen:
                    Console.WriteLine("Je hebt socre tussen 9 t/m 11 dus je mag verdubbelen. Wil ja verdubbelen J of N?");
                    return this.CheckAntwoord();
                case Beslissing.Gesplitst:
                    Console.WriteLine("Je mag splitsen. Wil je dat doen?");
                    return this.CheckAntwoord();
            }

            return beslissen;
        }

        /// <summary>
        /// Voeg de neuwe waarde van de fiches aan de Portemonnee van de speler.
        /// Als de speler winnaar is.
        /// </summary>
        /// <param name="fiches">De waarde van de fiches.</param>
        public void VerzamelenDeFiches(Fiche fiches)
        {
            // this.Portemonnee.Add(fiches);
        }

        /// <summary>
        /// Voeg de neuwe waarde van de fiches aan de Portemonnee van de speler.
        /// Als de speler verliezer is.
        /// </summary>
        public void VerlizenDefiches()
        {
        }

        /// <summary>
        /// Als de speler wil stoppen.
        /// </summary>
        /// <param name="huidigeHand">De hand van de speler.</param>
        public void SluitDeHand(Hand huidigeHand)
        {
            // this.Hands.Remove(huidigeHand);
        }

        /// <summary>
        /// Neem de hands van de speler.
        /// </summary>
        /// <returns>Deze hand.</returns>
        public Hand HandVanDeSpeler()
        {
            /*foreach (Hand hand in this.Hands)
            {
                return hand;
            }
            */
            return null;
        }

        private bool CheckAntwoord()
        {
            ConsoleKeyInfo antwoord;
            antwoord = Console.ReadKey();
            while (antwoord.Key != ConsoleKey.J && antwoord.Key != ConsoleKey.N)
            {
                Console.WriteLine("Type graag J of N!");
                antwoord = Console.ReadKey();
            }

            if (antwoord.Key == ConsoleKey.J)
            {
                return true;
            }

            return false;
        }
    }
}