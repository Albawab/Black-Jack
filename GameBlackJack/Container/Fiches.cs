// <copyright file="Fiches.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.SpelSpullen
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static HenE.GameBlackJack.Fiche;

    /// <summary>
    /// Slaan de fiches op. En behandel de fiches bak.
    /// </summary>
    public class Fiches
    {
        /// <summary>
        /// De fiches die in de fichesBak zitten.
        /// </summary>
        private readonly List<Fiche> fiches = new List<Fiche>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Fiches"/> class.
        /// </summary>
        public Fiches()
        {
        }

        /// <summary>
        /// Gets de waarde van de fiches.
        /// </summary>
        public int WaardeVanDeFiches
        {
            get
            {
                int waarde = 0;
                foreach (Fiche fiche in this.fiches)
                {
                    waarde += fiche.Waarde;
                }

                return waarde;

                // return this.fiches.Sum(a => a.Waarde);
            }
        }

        /// <summary>
        /// Gets geeft een readonly lijst met kopies van een fiche fiches, zodat die egbryuitk kan worden om te printen of zo
        /// maar niet gebruitk kan worden om fiches toe te voegen of te verwijderen
        /// dat gaat via de functies en heb ik er controle op.
        /// </summary>
        public List<Fiche> ReadOnlyFiches
        {
            get
            {
                List<Fiche> copyFiches = new List<Fiche>();

                foreach (Fiche fiche in this.fiches)
                {
                    copyFiches.Add(FicheFactory.CreateFiche(fiche.Kleur, fiche.Waarde, true));
                }

                return copyFiches;
            }
        }

        /// <summary>
        /// Add een fiche.
        /// </summary>
        /// <param name="fiche">Een fiche.</param>
        public void Add(Fiche fiche)
        {
            Debug.Assert(fiche != null, "fiche mag niet null zijn");
            this.fiches.Add(fiche);
        }

        /// <summary>
        /// Add meer dan een fiches.
        /// </summary>
        /// <param name="fiches">fiches die wordt toegevoegd.</param>
        public void Add(Fiches fiches)
        {
            Debug.Assert(fiches != null, "fiches mag niet null zijn");
            Debug.Assert(fiches.fiches != null, "interne list fiches mag niet null zijn");

            if (fiches != null && fiches.fiches != null)
            {
                this.fiches.AddRange(fiches.fiches);
            }
        }

        /// <summary>
        /// functie om fiches te krijgen.
        /// </summary>
        /// <param name="waardeInEuros">de waarde die je wilt.</param>
        /// <param name="maxFicheWaarde"> de gewenste grootte van de waarde van de fiches. </param>
        /// <param name="exact">of je wel of niet exact moet uitkomen.</param>
        /// <returns>de lijst met fiches.</returns>
        public Fiches GeefMeFischesTerWaardeVan(int waardeInEuros, int maxFicheWaarde, bool exact)
        {
            if (this.WaardeVanDeFiches < waardeInEuros)
            {
                // gooi een exceptie
                throw new ArgumentOutOfRangeException("er zijn niet genoeg fiches beschikbaar voor dit bedrag.");
            }

            // hoe kom ik aan de fiches
            Fiches returnFiches = new Fiches();

            // check eerst of ik fiches heb die de maxWaarde hebben maxFiches
            foreach (Fiche fiche in this.fiches)
            {
                if (fiche.Waarde <= maxFicheWaarde)
                {
                    if (returnFiches.WaardeVanDeFiches + fiche.Waarde <= waardeInEuros)
                    {
                        // ok, voeg toe aan de return wfiches
                        returnFiches.Add(fiche);
                    }
                }
            }

            // heb ik dan nog waarde over ? en kan ik dat dan terug geven
            // met kleinere fiches.
            if (returnFiches.WaardeVanDeFiches < waardeInEuros)
            {
                foreach (Fiche fiche in this.fiches)
                {
                    if (fiche.Waarde < maxFicheWaarde)
                    {
                        if (returnFiches.WaardeVanDeFiches + fiche.Waarde <= waardeInEuros)
                        {
                            // ok, voeg tioe aan de return wfiches
                            returnFiches.Add(fiche);
                        }
                    }
                }
            }

            // heb ik dan nog waarde over ? en kan ik dat dan terug geven
            // met grotere fiches.
            if (returnFiches.WaardeVanDeFiches < waardeInEuros)
            {
                foreach (Fiche fiche in this.fiches)
                {
                    if (fiche.Waarde > maxFicheWaarde)
                    {
                        if (returnFiches.WaardeVanDeFiches + fiche.Waarde <= waardeInEuros)
                        {
                            // ok, voeg tioe aan de return wfiches
                            returnFiches.Add(fiche);
                        }
                    }
                }
            }

            // heb ik dan nog waarde over ? en kan ik dat dan terug geven
            // met kleinere fiches.
            if (exact && returnFiches.WaardeVanDeFiches < waardeInEuros)
            {
                // wat doe ik nu, geef ik het wel terug of zeg ik dat het niet kan?
                // als ik zeg dat het niet kan?
                returnFiches = null;
            }

            // ok alle fiches die ik terug geef verwijderen uit mijn lijst
            if (returnFiches != null)
            {
                foreach (Fiche fiche in returnFiches.fiches)
                {
                    this.Remove(fiche);
                }
            }

            return returnFiches;
        }

        /// <summary>
        /// Zet een fiche in de fiches bak in.
        /// </summary>
        /// <param name="fiche">De fiche die ingezet wordt.</param>
        public void AddFicheAanFichesBak(Fiche fiche)
        {
            this.fiches.Add(fiche);
        }

        /// <summary>
        /// Verwijder een fiche vanuit de fichebak.
        /// </summary>
        /// <param name="fiche">De fiche die wordt verwijderen.</param>
        public void Remove(Fiche fiche)
        {
            this.fiches.Remove(fiche);
        }
    }
}
