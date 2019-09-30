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
        /// Hoeveel fiches de speler heeft.
        /// </summary>
        private readonly Fiches fiches = new Fiches();
        private readonly ActiesHelper actiesHelper = new ActiesHelper();

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
        /// Vraag de speler wat hij wil doen.
        /// </summary>
        /// <param name="mogelijkActies">Lijst van de acties die de speler mag van uit het mag kiezen is.</param>
        /// <param name="huidigeHand">De huidige hand.</param>
        /// <returns>De actie die de speler heeft gekozen.</returns>
        public Acties AskActie(List<Acties> mogelijkActies, Hand huidigeHand)
        {
            // keuze aan de klant laten
            for (int index = 0; index < mogelijkActies.Count; index++)
            {
                Console.WriteLine();
                Console.WriteLine($"{index.ToString()} {this.ActieTotStrign(mogelijkActies[index])}");
            }

            // De actie die de speler wil doen.
            int deActie = 0;
            string deSpelerwilDoen = string.Empty;
            Console.WriteLine("Kies maar een van de acties! Je mag alleen nummer gebruiken.");
            deSpelerwilDoen = Console.ReadLine();
            while (!int.TryParse(deSpelerwilDoen, out deActie) || deActie > mogelijkActies.Count)
            {
                Console.WriteLine("Je hebt geen nummer ingevoegd of een nummer die boven niet bestaat. Voeg maar een nummer in.");
                deSpelerwilDoen = Console.ReadLine();
            }

            Acties actie = mogelijkActies[deActie];
            return actie;
        }

        /// <summary>
        /// Als de speler heeft de tafel verlaten.
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
        public void Koopfiches()
        {
            Console.WriteLine("Je heeft geen fiches. Wil je fiches kopen J of N?");
            if (this.CheckAntwoord())
            {
                this.Fiches.Add(this.HuidigeTafel.Fiches.GeefMeFischesTerWaardeVan(20, 10, true));
            }
        }

        /// <summary>
        /// De speler zet een fiche in bij de hand in.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="waarde">De waarde die de speler wil bij de hand .</param>
        public void ZetFichesBijHandIn(Hand hand, int waarde)
        {
            if (this.fiches.ReadOnlyFiches.Count != 0)
            {
                foreach (Fiche fiche in this.fiches.ReadOnlyFiches)
                {
                    while (!this.HeeftDitBedragInFichesbak(waarde))
                    {
                        Console.WriteLine("Je hebt geen fiche die de zelfde waarde heeft. Wil je een fiche kopen J of N?");
                        if (this.CheckAntwoord())
                        {
                            this.Fiches.GeefMeFischesTerWaardeVan(20, 10, false);
                        }
                    }

                    hand.Inzet.Add(this.Fiches.GeefMeFischesTerWaardeVan(waarde, 10, false));
                    break;
                }
            }
        }

        /// <summary>
        /// De speler zet een fiche in bij de hand in.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        public void ZetFichesBijHandIn(Hand hand)
        {
            if (this.fiches.ReadOnlyFiches.Count != 0)
            {
                while (!this.HeeftDitBedragInFichesbak(hand.Inzet.WaardeVanDeFiches))
                {
                    Console.WriteLine("Je hebt geen fiche die de zelfde waarde heeft. Wil je een fiche kopen J of N?");
                    if (this.CheckAntwoord())
                    {
                        this.Fiches.GeefMeFischesTerWaardeVan(20, 10, false);
                    }
                }

                hand.Inzet.Add(this.Fiches.GeefMeFischesTerWaardeVan(20, 2, false));
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

        /// <summary>
        /// De waarde die de speler wil zetten.
        /// </summary>
        /// <returns>De waarde.</returns>
        public int FicheWaardeDeSpelerWilZetten()
        {
            Console.WriteLine("Wat voor waarde wil je zet in?");
            return 20;
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

        /// <summary>
        /// Vraag of de speler dat bedrag inde fichesbak heeft.
        /// </summary>
        /// <param name="bedrag">Bedrag die bij de hand moet zijn.</param>
        /// <returns>Check of heet dat bedrag of niet.</returns>
        private bool HeeftDitBedragInFichesbak(int bedrag)
        {
            if (bedrag <= this.fiches.WaardeVanDeFiches)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Zet de actie tot string.
        /// </summary>
        /// <param name="acties">De actie die wordt omgezet.</param>
        /// <returns>De actie als string.</returns>
        private string ActieTotStrign(Acties acties)
        {
            return this.actiesHelper.ZetEnumTotStringOm(acties);
        }
    }
}