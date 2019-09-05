// <copyright file="Hand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// De klas van de hand.
    /// </summary>
    public class Hand
    {
        private readonly List<Fiche> fiches = new List<Fiche>();
        private readonly List<Kaart> kaarten = new List<Kaart>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/> class.
        /// </summary>
        /// <param name="persoon">Huidige persoon.</param>
        public Hand(Persoon persoon)
        {
            if (persoon is null)
            {
                throw new ArgumentNullException("Persoon mag niet leeg zijn.");
            }

            if (persoon is Speler)
            {
                Console.WriteLine(persoon as Dealer);
                this.Speler = (Speler)persoon;
            }
        }

        /// <summary>
        /// Gets or sets de punten.
        /// </summary>
        private int Punten { get; set; }

        /// <summary>
        /// Gets or sets De spelers.
        /// </summary>
        private Speler Speler { get; set; }

        /// <summary>
        /// Gets or sets de kaarten.
        /// </summary>
        private Kaart Kaarten { get; set; }

        /// <summary>
        /// Gets or sets de status van de hand.
        /// </summary>
        private Status StatusVanDeHand { get; set; }

        /// <summary>
        /// Voeg de fiches er in.
        /// </summary>
        /// <param name="fiche">Een fiche.</param>
        public void VoegEenFichesIn(Fiche fiche)
        {
            this.fiches.Add(fiche);
        }

        /// <summary>
        /// Voeg de fiches er in.
        /// </summary>
        /// <param name="kaart">Een Kaart.</param>
        public void VoegKaartIn(Kaart kaart)
        {
            this.kaarten.Add(kaart);
        }

        /// <summary>
        /// De fiches in de hand van de speler.
        /// </summary>
        /// <returns>De fiches.</returns>
        public List<Fiche> FichesInHand()
        {
            return this.fiches;
        }

        /// <summary>
        /// Geef de lijst van de kaarten terug.
        /// </summary>
        /// <returns>Lijst van de kaarten.</returns>
        public List<Kaart> NeemKaarten()
        {
            List<Kaart> terugKaarten = new List<Kaart>();
            foreach (Kaart kaart in this.kaarten)
            {
                terugKaarten.Add(kaart);
            }

            return terugKaarten;
        }

        /// <summary>
        /// Voeg de punten aan de hand van de speler.
        /// </summary>
        /// <param name="punten">de punten.</param>
        public void AddEenPunten(int punten)
        {
            this.Punten += punten;
        }

        /// <summary>
        /// Als de hand heeft 21 punten dan het is Black Jack.
        /// </summary>
        /// <returns>Black jack of nee.</returns>
        public bool BlackJeck()
        {
            if (this.Punten == 21)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verandert de situatie van de hand tot Blackjack.
        /// </summary>
        public void PutBlackJack()
        {
            this.StatusVanDeHand = Status.BlackJack;
        }

        /// <summary>
        /// Vraagt of de status van de hand is defined.
        /// </summary>
        /// <param name="hand">huidige hand.</param>
        /// <returns>Is de hand defined of nee.</returns>
        public bool HandStatusDefined(Hand hand)
        {
            if (hand.StatusVanDeHand == Status.IsDefined)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// geeft de punten terug.
        /// </summary>
        /// <returns>Hoeveel punten de speler heeft.</returns>
        public int HuidigePunten()
        {
            return this.Punten;
        }

        /// <summary>
        /// Verander de staus van de hand.
        /// </summary>
        /// <param name="status">De status.</param>
        public void VeranderDeStatusVanDeHand(Status status)
        {
            this.StatusVanDeHand = status;
        }

        /// <summary>
        /// Geef de huidige speler terug.
        /// </summary>
        /// <returns>Huidige speler.</returns>
        public Speler HuidigeSpeler()
        {
            return this.Speler;
        }

        public void Verdubbelen(Dealer dealer, Tafel tafel, Spel spel)
        {
            this.VeranderDeStatusVanDeHand(Status.Verdubbelen);
            FichesBak fichesBak = tafel.HuidigeFichesBak();
            List<Fiche> fiches = this.FichesInHand();
            List<Fiche> fichesWilZetten = this.Speler.ZetGelijkFiches(fiches, dealer, fichesBak);
            foreach (Fiche fiche in fichesWilZetten)
            {
                this.VoegEenFichesIn(fiche);
            }

            Console.WriteLine($"{this.Speler.Naam}, wil je kopen of passen K of P?");

            if (this.Punten == 21)
            {
                // Fiche fiche = huidigeHand.FichesInHand();
                this.StatusVanDeHand = Status.BlackJack;
                //..                       dealer.GeefEenFicheAanDeHand(this); // ==> Hier Een Keer en half moet zijn.

                if (this.Punten >= 9 && this.Punten <= 11)
                {
                    this.Verdubbelen(dealer, tafel, spel);
                }
            }


        }

        public void Splits(Dealer dealer, Tafel tafel, Spel spel)
        {
            this.VeranderDeStatusVanDeHand(Status.Verdubbelen);
            Hand nieuweHand = new Hand(this.Speler);
            spel.VoegEenHandIn(nieuweHand);
            this.Speler.VoegEenHandIn(nieuweHand);
            FichesBak fichesBak = tafel.HuidigeFichesBak();
            List<Fiche> fiches = this.FichesInHand();
            List<Fiche> fichesWilZetten = this.Speler.ZetGelijkFiches(fiches, dealer, fichesBak);
            foreach (Fiche fiche in fichesWilZetten)
            {
                this.VoegEenFichesIn(fiche);
            }

            foreach (Hand eenHandVanDeSpeler in this.Speler.GeefHanden())
            {
                dealer.DeelEenKaart(eenHandVanDeSpeler);
                dealer.CheckDeHand(eenHandVanDeSpeler);
                if (this.Punten <= 21)
                {
                    if (this.BlackJeck())
                    {
                        // Fiche fiche = huidigeHand.FichesInHand();
                        this.StatusVanDeHand = Status.BlackJack;
                        //..                       dealer.GeefEenFicheAanDeHand(this); // ==> Hier Een Keer en half moet zijn.
                    }
                    else
                    {
                        if (this.Punten >= 9 && this.Punten <= 11)
                        {
                            this.Verdubbelen(dealer, tafel, spel);
                        }
                    }
                }

                Console.WriteLine($"{this.Speler.Naam}, wil je kopen of passen?");
            }
        }
    }
}