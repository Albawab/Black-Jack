// <copyright file="FichesBak.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.SpelSpullen
{
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// Slaan de fiches op. En behandel de fiches bak.
    /// </summary>
    public class FichesBak
    {
        private int aantelRijenVanFiches = 0;
        private List<FichesKleur> fichesKleur = null;

        /// <summary>
        /// De fiches die in de fichesBak zitten.
        /// </summary>
        private IList<Fiche> fiches = new List<Fiche>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FichesBak"/> class.
        /// </summary>
        /// <param name="aantelRij">Hoe veel rij van fiches we hebben nodig.</param>
        /// <param name="fichesKleur">De kleur van de fiches.</param>
        public FichesBak(int aantelRij, List<FichesKleur> fichesKleur)
        {
            this.fichesKleur = fichesKleur;
            this.aantelRijenVanFiches = aantelRij;
            this.Reset();
        }

        /// <summary>
        /// Deze method geeft een fiche aan de dealer.
        /// verwijdeert de fiches vanuit de list van de fiches.
        /// </summary>
        /// <param name="huidigeFiche">De fiche die de speler wil kopen.</param>
        /// <returns>De fiche.</returns>
        public Fiche NeemEenFiche(Fiche huidigeFiche)
        {
            Fiche eenFiche = null;
            foreach (Fiche fiche in this.fiches)
            {
                if (huidigeFiche == fiche)
                {
                    eenFiche = fiche;
                    break;
                }
            }

            return eenFiche;
        }

        /// <summary>
        /// Zoek op een fiche die de waarde van het gelijk aan de waarde die de speler wil kopen.
        /// </summary>
        /// <param name="waarde">De waarde.</param>
        /// <returns>Een fiche.</returns>
        public Fiche ZoekEenFiche(FichesWaarde waarde)
        {
            foreach (Fiche fiche in this.fiches)
            {
                if (fiche.Waarde == waarde)
                {
                    return fiche;
                }
            }

            return null;
        }

        /// <summary>
        /// Reset de fiches bak.
        /// </summary>
        private void Reset()
        {
            this.fiches = new List<Fiche>();

            for (int i = 0; i < this.aantelRijenVanFiches; i++)
            {
                foreach (FichesKleur kleur in this.fichesKleur)
                {
                    this.fiches.Add(new Fiche(kleur));
                }
            }
        }
    }
}
