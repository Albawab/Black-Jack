// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
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
        /// <param name="tafel">Huidige tafel.</param>
        public void Koopfiches(Tafel tafel)
        {
            Console.WriteLine("Je heeft geen fiches. Wil je fiches kopen J of N?");
            if (this.CheckAntwoord())
            {
                this.Fiches.Add(tafel.Fiches.GeefMeFischesTerWaardeVan(20, 10, true));
            }

            // HelperFiches helperFiches = new HelperFiches();
            // Fiche createFiche = helperFiches.OmzettenWaardeDieDeSpelerwil_TotEenFiche(hetBedrag, fichesBak, dealer);
            // this.Fiches.Add(createFiche);
        }

        /// <summary>
        /// De speler zet een fiche in bij de hand in.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="waarde">De waarde.</param>
        public void ZetFichesBijHandIn(Hand hand, int waarde)
        {
            if (this.fiches.ReadOnlyFiches.Count != 0)
            {
                foreach (Fiche fiche in this.fiches.ReadOnlyFiches)
                {
                    if (fiche.Waarde == waarde)
                    {
                        hand.Inzet.Add(this.Fiches.GeefMeFischesTerWaardeVan(waarde, 10, false));
                    }
                    else
                    {
                        Console.WriteLine("Je hebt geen fiche die de zelfde waarde heeft. Wil je een fiche kopen J of N?");
                        if (this.CheckAntwoord())
                        {
                            this.Fiches.GeefMeFischesTerWaardeVan(waarde, 10, false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Als de speler heeft geen fiches meer.
        /// Vraag hem of hij wil Kopen.
        /// </summary>
        /// <returns>Heeft de speler fiches of niet.</returns>
        public bool HeeftSpelerNogFiches()
        {
            if (this.Fiches.ReadOnlyFiches.Count == 0)
            {
                return false;
            }

            return true;
        }

        /*
        /// <summary>
        /// Neem De fiches vanuit de hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="waarde">De waarde van</param>
        public void PakFichesVanDeHand(Hand hand, int waarde)
        {
            this.Fiches.Add(hand.Inzet.GeefMeFischesTerWaardeVan(waarde, 10, false));
        }
        */

        /// <summary>
        /// De waarde die de speler wil zetten.
        /// </summary>
        /// <returns>De waarde.</returns>
        public int FicheWaardeDeSpelerWilZetten()
        {
            Console.WriteLine("Wat voor waarde wil je zet in?");
            return 10;
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
        /// Voeg een hand aan de lijst van de handen.
        /// </summary>
        /// <param name="hand">Nieuwe hand.</param>
        public void VoegEenHandIn(Hand hand)
        {
            this.handen.Add(hand);
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

        /// <summary>
        /// Vrgaag de speler of hij wil iets doen of niet.
        /// </summary>
        /// <returns>Wil doen of niet.</returns>
        public bool CheckAntwoord()
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