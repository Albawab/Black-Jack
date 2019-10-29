// <copyright file="StapelKaarten.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.SpelSpullen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// De kaarten van het spel die we nodig hebben voor het spel.
    /// Staat de kaarten.
    /// </summary>
    public class StapelKaarten
    {
        private readonly List<KaartTeken> mogelijkeKaartTekens;
        private readonly int aantalPakken = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="StapelKaarten"/> class.
        /// Creert een stapel kaarten, van het aantal pakken en de soorten kaarten.
        /// </summary>
        /// <param name="aantalPakken">Hoe veel pakken in de stapel staan.</param>
        /// <param name="mogelijkeKaartTekens">Het teken van de kaarten die we willen gebruiken.</param>
        public StapelKaarten(int aantalPakken, List<KaartTeken> mogelijkeKaartTekens)
        {
            this.mogelijkeKaartTekens = mogelijkeKaartTekens;
            this.aantalPakken = aantalPakken;
            this.Reset();
        }

        /// <summary>
        /// Gets de kaarten.
        /// </summary>
        public List<Kaart> Kaarten { get; private set; }

        /// <summary>
        /// Neem een kaart van de stapel kaarten.
        /// </summary>
        /// <returns>De eerste kaart van de stapel of null als er geen kaarten zijn.</returns>
        public Kaart NeemEenKaart()
        {
            if (this.Kaarten.Count == 0)
            {
                return null;
            }

            Kaart kaart = this.Kaarten.First();
            this.Kaarten.Remove(kaart);
            return kaart;
        }

        /// <summary>
        /// functie om de stapel kaarten te resetten. voor elke stapel kaarten wordt een reeks kaarten toegevoegd.
        /// </summary>
        public void Reset()
        {
            this.Kaarten = new List<Kaart>();
            for (int i = 0; i < this.aantalPakken; i++)
            {
                foreach (KaartTeken tekenVanKaart in this.mogelijkeKaartTekens)
                {
                    this.Kaarten.Add(new Kaart(KaartKleur.Harten, tekenVanKaart, this.GeefWaardeAanKaart()));
                }

                foreach (KaartTeken tekenVanKaart in this.mogelijkeKaartTekens)
                {
                    this.Kaarten.Add(new Kaart(KaartKleur.Klaveren, tekenVanKaart, this.GeefWaardeAanKaart()));
                }

                foreach (KaartTeken tekenVanKaart in this.mogelijkeKaartTekens)
                {
                    this.Kaarten.Add(new Kaart(KaartKleur.Ruiten, tekenVanKaart, this.GeefWaardeAanKaart()));
                }

                foreach (KaartTeken tekenVanKaart in this.mogelijkeKaartTekens)
                {
                    this.Kaarten.Add(new Kaart(KaartKleur.Schoppen, tekenVanKaart, this.GeefWaardeAanKaart()));
                }
            }
        }

        /// <summary>
        /// functie om alle kaarten te schudden.
        /// </summary>
        /// <param name="kerenDatWeDeStapelSchudden">Hoe veel keer de kaarten shuffle shuifelen.</param>
        public void Shuffle(int kerenDatWeDeStapelSchudden)
        {
            int i = 0;
            while (i < kerenDatWeDeStapelSchudden--)
            {
               // this.RandomShuffleDeckOnce();
            }
        }

        /// <summary>
        /// Loop door de kaarten heen, en swap de kaarten via functie SwapKaart.
        /// </summary>
        private void RandomShuffleDeckOnce()
        {
            // beginnen bij 1, zodat als we maar 1 kaart hebben, we niet gaan swappen.
            for (int i = 1; i < this.Kaarten.Count; i++)
            {
                // maak een nieuwe plek voor de kaart en swap de kaarten dan;
                this.SwapKaart(i, this.GetRandomIndex(0, this.Kaarten.Count));
            }
        }

        /// <summary>
        /// verandert de plaats van de kaart tot andere plaats van een andere kaart.
        /// </summary>
        /// <param name="index1">De huidige plaats van de huidige kaart.</param>
        /// <param name="index2">De nieuwe plaats waar de kaart gaat verplaatsen.</param>
        private void SwapKaart(int index1, int index2)
        {
            if (index2 > this.Kaarten.Count)
            {
                throw new ArgumentOutOfRangeException("Mag de nummer niet hooger dan de maxnummer van de stapel kaart.");
            }

            // todo indexen controleren aan de grootte vande stapel
            Kaart kaart1 = this.Kaarten[index1];
            Kaart kaart2 = this.Kaarten[index2];
            this.Kaarten[index1] = kaart2;
            this.Kaarten[index2] = kaart1;
        }

        /// <summary>
        /// Geef een random nummer tussen null en de maxnummer van de stapelkaarten.
        /// </summary>
        /// <param name="minNumber">Waar moet de random starten.</param>
        /// <param name="maxNumber">waar moet de random stopen.</param>
        /// <returns>Een nummer tussen minnummer en maxnummer.</returns>
        private int GetRandomIndex(int minNumber, int maxNumber)
        {
            return new Random().Next(minNumber, maxNumber);
        }

        /// <summary>
        /// Geef de juiste waarde aan een kaart.
        /// </summary>
        private int GeefWaardeAanKaart()
        {
            int waarde = 0;
            foreach (Kaart kaart in this.Kaarten)
            {
                switch (kaart.Teken)
                {
                    case KaartTeken.Aas:
                        waarde = 1;
                        break;
                    case KaartTeken.Twee:
                        waarde = 2;
                        break;
                    case KaartTeken.Drie:
                        waarde = 3;
                        break;
                    case KaartTeken.Vier:
                        waarde = 4;
                        break;
                    case KaartTeken.Vijf:
                        waarde = 5;
                        break;
                    case KaartTeken.Zes:
                        waarde = 6;
                        break;
                    case KaartTeken.Zeven:
                        waarde = 7;
                        break;
                    case KaartTeken.Acht:
                        waarde = 8;
                        break;
                    case KaartTeken.Negen:
                        waarde = 9;
                        break;
                    case KaartTeken.Tien:
                        waarde = 10;
                        break;
                    case KaartTeken.Heer:
                        waarde = 10;
                        break;
                    case KaartTeken.Vrouw:
                        waarde = 10;
                        break;
                    case KaartTeken.Boer:
                        waarde = 10;
                        break;
                    default:
                        return 0;
                }
            }

            return waarde;
        }
    }
}