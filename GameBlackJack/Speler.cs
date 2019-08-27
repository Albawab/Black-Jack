// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De klas van de Speler.
    /// </summary>
    public abstract class Speler : Persoon
    {
        /// <summary>
        /// Hier staan de handen van de spelers.
        /// </summary>
        private List<Hand> hands;

        /// <summary>
        /// Initializes a new instance of the <see cref="Speler"/> class.
        /// </summary>
        /// <param name="naam">De naam van de speler.</param>
        protected Speler(string naam)
            : base(naam)
        {
        }

        /// <summary>
        /// Gets or Sets Waar de speler wil zitten.
        /// </summary>
        private Plek PlekAanEenTafel { get; set; }

        /// <summary>
        /// Gets or sets or stes Wat de speler fieches heeft.
        /// </summary>
        private List<Fiches> Portemonnee { get; set; }

        /// <summary>
        /// Gets or sets de hand van de speler.
        /// </summary>
        private Hand Hand { get; set; }

        /// <summary>
        /// Check of de plek waar de speler wil zitten vrij is.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="eenPlek">Waar de speler wil zitten.</param>
        public void OpEenPlekZitten(Tafel tafel, int eenPlek)
        {
            if (tafel.IsDezePlekVrij(eenPlek))
            {
                Plek plek = new Plek(eenPlek);
                this.PlekAanEenTafel = tafel.DezePlekIsNietMeerVrij(plek);
            }
        }

        /// <summary>
        /// De fiches kopen.
        /// </summary>
        /// <param name="hetBedrag">De waarde van de fiches.</param>
        /// <param name="dealer">De dealer.</param>
        public void Koopfiches(int hetBedrag, Dealer dealer)
        {
            Waarde_Van_Enum waarde = Waarde_Van_Enum.IsDefined;
            FichesEnum fiches1 = FichesEnum.IsDefined;
            Fiches fiches = null;
            Waarde_Van_Enum bedrag = dealer.NeemEenFiche(hetBedrag);
            switch (bedrag)
            {
                case Waarde_Van_Enum.Tien:
                    waarde = Waarde_Van_Enum.Tien;
                    fiches1 = FichesEnum.Groen;
                    fiches = new Fiches(waarde, fiches1);
                    break;
                case Waarde_Van_Enum.Vijftien:
                    waarde = Waarde_Van_Enum.Vijftien;
                    fiches1 = FichesEnum.Geel;
                    fiches = new Fiches(waarde, fiches1);
                    break;
                case Waarde_Van_Enum.Twintig:
                    waarde = Waarde_Van_Enum.Twintig;
                    fiches1 = FichesEnum.Blue;
                    fiches = new Fiches(waarde, fiches1);
                    break;
                case Waarde_Van_Enum.Vijfentwintig:
                    waarde = Waarde_Van_Enum.Vijfentwintig;
                    fiches1 = FichesEnum.Rood;
                    fiches = new Fiches(waarde, fiches1);
                    break;
            }

            this.Portemonnee.Add(fiches);
        }

        /// <summary>
        /// Controleer of de speler mag de waarde zetten.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        public void ZetFiches(Tafel tafel, Hand hand, int Bedrag)
        {
            if (tafel.BepaalOfHetBedragTussenTweeGrens(Bedrag))
            {
                this.Portemonnee.Remove();
            }
        }

        /// <summary>
        /// De speler beslist wat wil hij doen.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="wilDoen">Wat wil hij doen.</param>
        /// <param name="dealer">De dealer.</param>
        /// <param name="fiche">De waarde van de fiches.</param>
        /// <param name="fiches">De class van de fiches.</param>
        /// <returns></returns>
        public void DeSpelerWilDoen(Tafel tafel, string wilDoen, Dealer dealer, int fiche, Fiches fiches)
        {
            this.Hand = this.huidigeHand;
            foreach (Hand hand in this.hands)
            {
                if (hand == this.hand)
                {
                    this.huidigeHand = hand;
                }
            }

            switch (wilDoen)
            {
                case "koop":
                    this.huidigeHand += dealer.KrijgEenKaart(tafel, this);
                    break;
                case "passen":
                    dealer.NaarVolgendeHand(tafel, this);
                    break;
                case "verdubbelen":
                    ZetFiches(tafel, fiches.waarde);
                    dealer.GeefEenKaart(tafel, this);
                    break;
                case "Splitsen":
                    dealer.SplitsDeKaarten(tafel, this);
                    this.ZetFiches(tafel, fiche);
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
        public void VerzamelenDeFiches(int fiches)
        {
            this.Portemonnee.Add(fiches);
        }

        /// <summary>
        /// Voeg de neuwe waarde van de fiches aan de Portemonnee van de speler.
        /// Als de speler verliezer is.
        /// </summary>
        /// <param name="fiches">De waarde van de fiches.</param>
        public void VerlizenDefiches(int fiches)
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
    }

}