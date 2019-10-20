// <copyright file="Fiche.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// De gegevens van de fiche. Met een fiche kan gespeeld worden. Een fiche heeftr een kleur en een waarde
    /// De waarde is in hele Euro's en kan alleen 1, 5, 10, 20, 50 en 100 zijn.
    /// </summary>
    public class Fiche
    {
        /// <summary>
        /// De waarde van de fiches.
        /// </summary>
        private int waarde;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fiche"/> class.
        /// </summary>
        /// <param name="ficheKleur">De kleur van een fiche.</param>
        /// <param name="waarde">De waarde van het fiche veranderd niet.</param>
        private Fiche(FichesKleur ficheKleur, int waarde)
        {
            this.Kleur = ficheKleur;
            this.Waarde = waarde;
        }

        /// <summary>
        /// Gets de kleur van een fiche.
        /// </summary>
        public FichesKleur Kleur { get; private set; }

        /// <summary>
        /// Gets de waarde in Euros van een fiche. Afgesproken is dat fiches alleen hele waardes kunnen hebben.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Als de waarde niet 1, 5, 10, 20, 50, 100 is wordt deze execptie gegooid.</exception>
        public int Waarde
        {
            get
            {
                return this.waarde;
            }

            private set
            {
                if (!this.CheckWaarde(value))
                {
                    throw new ArgumentOutOfRangeException("De waarde van de fiche klopt niet.");
                }

                this.waarde = value;
            }
        }

        /// <summary>
        /// enige class die fiches kan aanmaken en uitgeven
        /// zodat niet iedereen fiches kan aanmaken.
        /// dus nog wel iets van rechten toekennen , wie deze
        /// class mag aanroepen.
        /// </summary>
        public static class FicheFactory
        {
            /// <summary>
            /// Create nieuwe fiches.
            /// </summary>
            /// <param name="waarde">De waarde van een fiche.</param>
            /// <returns>De fiche.</returns>
            public static Fiches CreateFiches(int waarde)
            {
                Fiches returnFiches = new Fiches();

                while (returnFiches.WaardeVanDeFiches < waarde)
                {
                    FichesKleur randomKleur = FichesKleur.Blue;
                    int waardeVanFiche = 10; // random maken

                    returnFiches.Add(CreateFiche(randomKleur, waardeVanFiche, false));
                }

                return returnFiches;
            }

            /// <summary>
            /// Create a fiches.
            /// </summary>
            /// <param name="kleur">De kleur van een fiche.</param>
            /// <param name="waarde">De waarde van een fiche.</param>
            /// <param name="copy">true of false.</param>
            /// <returns>Nieuwe fiche.</returns>
            public static Fiche CreateFiche(FichesKleur kleur, int waarde, bool copy = false)
            {
                return new Fiche(kleur, waarde);
            }
        }

        /// <summary>
        /// Checkt de waarde van de fiches.
        /// </summary>
        /// <param name="waarde">De waarde van de fiches.</param>
        /// <returns>Heeft gecheckt of niet.</returns>
        private bool CheckWaarde(int waarde)
        {
            if (waarde == 1 ||
                waarde == 5 ||
                waarde == 10 ||
                waarde == 20 ||
                waarde == 50 ||
                waarde == 100)
            {
                return true;
            }

            return false;
        }
    }
}
