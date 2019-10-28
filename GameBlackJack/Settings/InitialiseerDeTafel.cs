// <copyright file="InitialiseerDeTafel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Interface;
    using HenE.GameBlackJack.SpelSpullen;
    using static HenE.GameBlackJack.Fiche;

    /// <summary>
    /// Initialiseer de tafel, de spelers, de fiches, en de kaarten.
    /// </summary>
    public class InitialiseerDeTafel
    {
        private readonly ICommunicate communicator = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialiseerDeTafel"/> class.
        /// </summary>
        /// <param name="communicate">De communicator.</param>
        public InitialiseerDeTafel(ICommunicate communicate)
        {
            this.communicator = communicate;
        }

        private Tafel Tafel { get; set; }

        /// <summary>
        /// Create een nieuwe fices bak.
        /// initialiseer de tafel.
        /// Voeg een dealer toe.
        /// Voeg een speler toe.
        /// vraag de speler om fiches te kopen.
        /// </summary>
        public void Initialiseer()
        {
            // fiches
            // de hoofdbak met fiches
            Fiches cassiereFiches = FicheFactory.CreateFiches(5000);

            // tafel
            this.Tafel = Tafel.CreateBlackJackTafel(cassiereFiches.GeefMeFischesTerWaardeVan(2500, 50, false));

            // dealer
            // dealer aanmaken en toewijzen aan een tafel
            Dealer dealer = new Dealer("Dealer");
            dealer.GaAanTafelZitten(this.Tafel);

            // spelers, komen binnen en kopen bij het cassiere fiches.
            this.communicator.TellPersoon(Meldingen.Welkom);
            bool isLetters = false;
            Speler speler = null;
            string naamSpeler = string.Empty;

            while (!isLetters)
            {
                int charIsNotLetter = 0;
                naamSpeler = this.communicator.AskPersoon(Meldingen.DeNaam);
                char[] lettersOfName = naamSpeler.ToCharArray();
                naamSpeler.ToCharArray();
                for (int i = 0; i < naamSpeler.Length; i++)
                {
                    if (!char.IsLetter(lettersOfName[i]))
                    {
                        charIsNotLetter++;
                        break;
                    }
                }

                if (charIsNotLetter == 0 && naamSpeler.Length != 0 && naamSpeler.Length <= 15)
                {
                    isLetters = true;
                }
                else
                {
                    if (naamSpeler.Length == 0)
                    {
                        this.communicator.TellPersoon(Meldingen.Fout);
                        naamSpeler = this.communicator.AskPersoon(Meldingen.DeNaam);
                    }
                }
            }

            speler = new Speler(naamSpeler);
            this.communicator.TellPlayer(speler, Meldingen.Welkom);
            if (!speler.GaatAanTafelZitten(this.Tafel, 1))
            {
                throw new ArgumentOutOfRangeException("Het plek is niet meer beschikbaar.");
            }

            speler.Fiches.Add(this.Tafel.Fiches.GeefMeFischesTerWaardeVan(100, 50, true));
            this.communicator.TellPlayer(speler, Meldingen.Verdienen);
        }

        /// <summary>
        /// Geef de nieuwe tafel terug.
        /// </summary>
        /// <returns>De nieuwe tafel.</returns>
        public Tafel HuidigeTafel()
        {
            return this.Tafel;
        }
    }
}
