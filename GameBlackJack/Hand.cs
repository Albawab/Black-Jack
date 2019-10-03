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
    /// Heeft de kaarten en de fiches van de speler en ook heeft eigen situatie.
    /// </summary>
    public class Hand
    {
        /// <summary>
        /// Een lijst van kaarten die bij de hand zijn.
        /// </summary>
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

            this.Persoon = persoon;
            this.Inzet = new Fiches();
            this.Status = HandStatussen.NogNietGestart;
        }

        /// <summary>
        /// Gets de spelers.
        /// </summary>
        public Persoon Persoon { get; private set; }

        /// <summary>
        /// Gets de kaarten.
        /// </summary>
        public List<Kaart> Kaarten
        {
            // todo copy lijst teruggeven
            get
            {
               return this.kaarten;
            }
        }

        /// <summary>
        /// Gets de status van de hand.
        /// </summary>
        public HandStatussen Status { get; private set; }

        /// <summary>
        /// Gets or sets De fiches die in de hand zijn.
        /// </summary>
        public Fiches Inzet { get; set; }

        /// <summary>
        /// Geef de huidige speler terug.
        /// </summary>
        /// <returns>Huidige speler.</returns>
        public Speler HuidigeSpeler()
        {
            return this.Persoon as Speler;
        }

        /// <summary>
        /// Add een kaart aan de hand.
        /// </summary>
        /// <param name="kaart">Nieuwe kaart.</param>
        public void AddKaart(Kaart kaart)
        {
            this.kaarten.Add(kaart);
            this.Status = HandStatussen.InSpel;
        }

        /// <summary>
        /// Als de hand klaar is, veraandert de status van de hand.
        /// </summary>
        public void Close()
        {
            this.Status = HandStatussen.Gestopt;

            // en gooi alle kaarten weg.
            this.kaarten.Clear();
            this.ChangeStatus(HandStatussen.Gestopt);
        }

        /// <summary>
        /// Splits de hand.
        /// </summary>
        /// <returns>De hand die wordt gesplitst.</returns>
        public Hand Splits()
        {
            // todo, wat zijn de voorwaarden om te splitsen?
            if (this.kaarten.Count == 2)
            {
                if (true/*this.kaarten[0] == this.kaarten[1]*/)
                {
                    // kaarten moeten gelijk zijn
                    // kaarten moeten een even aantal zijn (== twee).
                    // welke controle moet ik doen
                    Hand nieuweHand = new Hand(this.Persoon);

                    for (int index = 0; index < this.kaarten.Count; index++)
                    {
                        nieuweHand.kaarten.Add(this.Kaarten[index]);
                    }

                    return nieuweHand;
                }
            }

            return null;

            // wat betekent dit?
            // dat ik een nieuwe hand moet maken

            // en dat ik de kaarten van deze hand moet delen en verplaatsen naar de nieuwe hand
        }

        /// <summary>
        /// Heef fiches bij de hand van de speler.
        /// </summary>
        public void GeefFichesBijHand()
        {
            foreach (Fiche fiche in this.Inzet.ReadOnlyFiches)
            {
                this.HuidigeSpeler().ZetFichesBijHandIn(this, this.Inzet.WaardeVanDeFiches);
            }
        }

        /// <summary>
        /// Zoek op in hand die wordt gesplits voor de fiches.
        /// Geef de nieuwe hand de zelfde fiches.
        /// </summary>
        /// <param name="handWordtGesplits">De hand die gesplits wordt.</param>
        public void GeefFichesBijHandDieWordtGesplits(Hand handWordtGesplits)
        {
            foreach (Fiche fiche in handWordtGesplits.Inzet.ReadOnlyFiches)
            {
                this.HuidigeSpeler().ZetFichesBijHandIn(this, fiche.Waarde);
            }
        }

        /// <summary>
        /// Check of de hand heeft meer dan 21 score.
        /// </summary>
        /// <param name="waardeVanKaarten">De waarde die bij de hand is.</param>
        /// <returns>Meer dan 21 score of minder.</returns>
        public bool IsDood(int waardeVanKaarten) => waardeVanKaarten > 21;

        /// <summary>
        /// Verandert de status van de hand.
        /// </summary>
        /// <param name="status">Nieuwe status.</param>
        public void ChangeStatus(HandStatussen status)
        {
            this.Status = status;
        }
    }
}