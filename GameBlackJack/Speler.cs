// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System.Collections.Generic;
    using HenE.GameBlackJack.HelperEnum;

    /// <summary>
    /// De klas van de Speler.
    /// </summary>
    public abstract class Speler : Persoon
    {
        /// <summary>
        /// Hier staan de handen van de spelers.
        /// </summary>
        private readonly List<Hand> hands = new List<Hand>();

        /// <summary>
        /// Hoeveel fiches de speler heeft.
        /// </summary>
        private readonly List<Fiches> portemonnee = new List<Fiches>();

        /// <summary>
        /// Help de fiches.
        /// </summary>
        private HelperFiches helperFiches;
        private Dealer dealer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Speler"/> class.
        /// </summary>
        /// <param name="naam"> De naam van de speler.</param>
        /// <param name="plek">De plek waar de speler zit.</param>
        protected Speler(string naam, Plek plek)
            : base(naam)
        {
            this.PlekAanTafel = plek;
        }

        /// <summary>
        /// Gets or Sets Waar de speler wil zitten.
        /// </summary>
        private Plek PlekAanTafel { get; set; }

        /// <summary>
        /// Gets or Sets de hand van de speler.
        /// </summary>
        private Hand Hand { get; set; }

        /// <summary>
        /// Check of de plek waar de speler wil zitten vrij is.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="eenPlek">Waar de speler wil zitten.</param>
        public void OpEenPlekZitten(Tafels tafel, int eenPlek)
        {
            if (tafel.IsDezePlekVrij(eenPlek))
            {
                Plek plek = new Plek(eenPlek);
                this.PlekAanTafel = tafel.DezePlekIsNietMeerVrij(plek);
            }
        }

        /// <summary>
        /// De speler bepaalt wat hij wil kopen.
        /// </summary>
        /// <param name="hetBedrag">De waarde van de fiches.</param>
        public void Koopfiches(int hetBedrag)
        {
            Fiches createFiche = this.helperFiches.OmzettenWaardeDieDeSpelerwil_TotEenFiche(hetBedrag);
            Hand hand = null;
            foreach (Hand hand1 in this.hands)
            {
                hand = hand1;
            }

            Fiches fiche = this.dealer.GeefEenFiche(hand, createFiche);
            this.portemonnee.Add(fiche);
        }

        /// <summary>
        /// Controleer of de speler mag de waarde zetten.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="huidigeFiche">Huidige fiche.</param>
        public void ZetFiches(Tafels tafel, Hand hand, Fiches huidigeFiche)
        {
            foreach (Fiches fiche in this.portemonnee)
            {
                if (fiche == huidigeFiche)
                {
                    this.portemonnee.Remove(fiche);

                    hand.VoegEenFichesIn(fiche);
                }
            }
        }

        /// <summary>
        /// De speler beslist wat wil hij doen.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="wilDoen">Wat wil hij doen.</param>
        /// <param name="dealer">De dealer.</param>
        /// <param name="fiches">De class van de fiches.</param>
        /// <param name="huidigeHand">Huidige hand.</param>
        public void DeSpelerWilDoen(Tafels tafel, string wilDoen, Dealer dealer, Fiches fiches, Hand huidigeHand)
        {
            Hand hand1 = null;
            foreach (Hand hand in this.hands)
            {
                if (hand == huidigeHand)
                {
                    hand1 = hand;
                }
            }

            switch (wilDoen)
            {
                case "koop":
                    hand1.VoegKaartIn(dealer.GeefEenKaart(hand1));
                    break;

                case "passen":
                    // dealer.NaarVolgendeHand(tafel, this);
                    break;

                case "verdubbelen":
                    Hand newHand = new Hand();
                    Fiches fiches1 = null;
                    this.hands.Add(newHand);
                    foreach (Hand hand in this.hands)
                    {
                        if (hand == huidigeHand)
                        {
                            fiches1 = hand.FichesInHand(hand);
                        }
                    }

                    newHand.VoegEenFichesIn(fiches1);
                    dealer.GeefEenKaart(newHand);
                    break;

                case "Splitsen":
                    Hand splitsenHand = new Hand();
                    Fiches splitsenFiches = null;
                    this.hands.Add(splitsenHand);
                    foreach (Hand hand in this.hands)
                    {
                        if (hand == huidigeHand)
                        {
                            splitsenFiches = hand.FichesInHand(hand);
                        }
                    }

                    splitsenHand.VoegEenFichesIn(splitsenFiches);
                    dealer.GeefEenKaart(splitsenHand);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Voeg de neuwe waarde van de fiches aan de Portemonnee van de speler.
        /// Als de speler winnaar is.
        /// </summary>
        /// <param name="fiches">De waarde van de fiches.</param>
        public void VerzamelenDeFiches(Fiches fiches)
        {
            this.portemonnee.Add(fiches);
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
            this.hands.Remove(huidigeHand);
        }

        /// <summary>
        /// Neem de hands van de speler.
        /// </summary>
        /// <returns>Deze hand.</returns>
        public Hand HandVanDeSpeler()
        {
            foreach (Hand hand in this.hands)
            {
                return hand;
            }

            return null;
        }
    }
}