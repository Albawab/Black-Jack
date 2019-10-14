// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Interface;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Slaan de gegevens van de speler op.
    /// bepaal de actie die de speler wil doen.Koop fiches ook zet fiches bij de hand in.
    /// </summary>
    public class Speler : Persoon
    {
        /// <summary>
        /// Hoeveel fiches de speler heeft.
        /// </summary>
        private readonly Fiches fiches = new Fiches();

        /// <summary>
        /// Initializes a new instance of the <see cref="Speler"/> class.
        /// </summary>
        /// <param name="naam"> De naam van de speler.</param>
        /// <param name="communicate">De commuicator.</param>
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
                    /*                   while (!this.HeeftDitBedragInFichesbak(waarde))
                                       {
                                           if (this.CheckAntwoord())
                                           {
                                               this.Fiches.GeefMeFischesTerWaardeVan(20, 10, false);
                                           }
                                       }*/

                    hand.Inzet.Add(this.Fiches.GeefMeFischesTerWaardeVan(waarde, 1, false));
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
                /*                while (!this.HeeftDitBedragInFichesbak(hand.Inzet.WaardeVanDeFiches))
                                {
                                                       if (this.CheckAntwoord())
                                    {
                                        this.Fiches.GeefMeFischesTerWaardeVan(20, 10, false);
                                    }
                                }*/

                hand.Inzet.Add(this.Fiches.GeefMeFischesTerWaardeVan(20, 1, false));
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
        /// Vraag of de speler dat bedrag inde fichesbak heeft.
        /// </summary>
        /// <param name="bedrag">Bedrag die bij de hand moet zijn.</param>
        /// <returns>Check of heet dat bedrag of niet.</returns>
        private bool HeeftDitBedragInFichesbak(int bedrag) => bedrag <= this.fiches.WaardeVanDeFiches;
    }
}