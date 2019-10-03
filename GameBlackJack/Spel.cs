﻿// <copyright file="Spel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Settings;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Behandel het spel.
    /// </summary>
    public class Spel
    {
        /// <summary>
        /// De lijst van de spelers die gaan spelen.
        /// </summary>
        private readonly List<Speler> spelers = new List<Speler>();
        private readonly BlackJackPointsCalculator blackJackPointsCalculator = new BlackJackPointsCalculator();

        /// <summary>
        /// Initializes a new instance of the <see cref="Spel"/> class.
        /// </summary>
        public Spel()
        {
            this.HuidigeHand = null;
            this.Handen = new List<Hand>();
        }

        /// <summary>
        ///  Gets geeft de huidige hand van het spel.
        /// </summary>
        public Hand HuidigeHand { get; private set; }

        /// <summary>
        /// Gets de handen.
        /// </summary>
        /// <remarks>Geeft een collectie met unieke handen terug.</remarks>
        public List<Hand> Handen { get; private set; }

        /// <summary>
        /// set de huidige hand naar de volgende speelbare hand, als er nog geen hand is gezet, dan de eerste pakken.
        /// als ik de laatste ben, weer terug naar het begin.
        /// </summary>
        /// <returns>de huidige hand of null indien niet gevonden.</returns>
        public Hand GaNaarDeVolgendeSpeelbareHand()
        {
            int indexHuidigehand = 0;

            if (this.HuidigeHand != null)
            {
                for (int index = 0; index < this.Handen.Count; index++)
                {
                    if (this.Handen[index] == this.HuidigeHand)
                    {
                        indexHuidigehand = index;
                    }
                }
            }

            // als ik null ben, geef me dan de eerste speelbase hand;
            for (int i = indexHuidigehand; i < this.Handen.Count; i++)
            {
                if (this.Handen[i].Status == HandStatussen.InSpel)
                {
                    this.HuidigeHand = this.Handen[i];
                    return this.HuidigeHand;
                }
            }

            return null;
        }

        /// <summary>
        /// Voegt een hand toe aan de collectie.
        /// </summary>
        /// <param name="hand">De hand die toegevoegd moet worden.</param>
        public void VoegEenHandToe(Hand hand)
        {
            // null check
            if (hand == null)
            {
                throw new ArgumentNullException("Hand mag niet nuul zijn.");
            }

            // of de hand niet al bestaat in de collectie
            foreach (Hand hand1 in this.Handen)
            {
                if (hand1 == hand)
                {
                    throw new ArgumentException("Die hand bestaat al.");
                }
            }

            hand.ChangeStatus(HandStatussen.InSpel);

            // zit de hand wel in dit spel
            this.Handen.Add(hand);
        }

        /// <summary>
        /// Voegt een hand in in de collectie.
        /// </summary>
        /// <param name="positie">de plaats waar de hand ingevoegd moet worden.</param>
        /// <param name="hand">De hand die ingevoegd moet worden.</param>
        public void VoegEenHandIn(int positie, Hand hand)
        {
            // TODO welke controle moet ik hier doen?
            if (hand == null)
            {
                throw new ArgumentNullException("Hand mag niet nuul zijn.");
            }

            // grootte van de list moet kleiner zijn dan de positie.
            if (this.Handen.Count < positie)
            {
                throw new ArgumentOutOfRangeException("De positie moet kleiner dan list");
            }

            hand.ChangeStatus(HandStatussen.InSpel);
            this.Handen.Insert(positie, hand);
        }

        /// <summary>
        /// Voeg een speler aan het spel toe.
        /// </summary>
        /// <param name="spelerDieWilDeelnemenAanHetSpel">De speler die wordt ingevoegd.</param>
        /// <returns>De hand van de speler.</returns>
        public Hand SpelerToevoegen(Speler spelerDieWilDeelnemenAanHetSpel)
        {
            // een hand wordt aangemaakt,
            Hand hand = new Hand(spelerDieWilDeelnemenAanHetSpel);

            // aan de collectie toegevoegd.
            this.spelers.Add(spelerDieWilDeelnemenAanHetSpel);
            this.VoegEenHandToe(hand);

            // en de hand wordt teruggegeven
            return hand;
        }

        /// <summary>
        /// Voeg een dealer aan het spel toe.
        /// </summary>
        /// <param name="dealer">De dealer die wordt toegevoegd.</param>
        /// <returns>De hand van de dealer.</returns>
        public Hand DealerToevoegen(Dealer dealer)
        {
            // een hand wordt aangemaakt,
            Hand hand = new Hand(dealer);

            // aan de collectie toegevoegd.
            this.VoegEenHandToe(hand);

            // en de hand wordt teruggegeven
            return hand;
        }

        /// <summary>
        /// Splits de hand.
        /// geef kaarten . en ook geef ficehs aan de hand .
        /// De fiches zijn gelijk op de fiches die bij de hand die wordet gesplist.
        /// De Kaarten zijn gelijk op de Kaarten die bij de hand die wordet gesplist.
        /// </summary>
        /// <param name="handDieGesplitstMoetWorden">De hand die wordt gesplitst.</param>
        /// <returns>De hand.</returns>
        public Hand SplitsHand(Hand handDieGesplitstMoetWorden)
        {
            Hand nieuweHand = null;

            // zoek de postitie va de handDieGesplitstMoetWorden
            for (int index = 0; index < this.Handen.Count; index++)
            {
                if (this.Handen[index] == handDieGesplitstMoetWorden)
                {
                    // clone de oudehand
                    nieuweHand = handDieGesplitstMoetWorden.Splits();

                    if (index == this.Handen.Count)
                    {
                        // dan moet ik toevoegen
                        this.VoegEenHandToe(nieuweHand);
                    }
                    else
                    {
                        // TODO voeg je de hand in op de postite van de oude + 1
                        this.VoegEenHandIn(index + 1, nieuweHand);
                    }
                }
            }

            nieuweHand.GeefFichesBijHandDieWordtGesplits(handDieGesplitstMoetWorden);
            return nieuweHand;
        }

        /// <summary>
        /// Maak de lei schoon.
        /// </summary>
        /// <param name="dealer">De Dealer.</param>
        /// <param name="spelers">De spelers die willen spelen.</param>
        public void InitialiseerHetSpel(Dealer dealer, List<Speler> spelers)
        {
            // wat willen we dan doen.
            // in ieder geval beginnen met een schone lei.
            // dus lege collectie van handen.
            this.Handen.Clear();

            foreach (Speler speler in spelers)
            {
                this.SpelerToevoegen(speler);
            }

            // als laatste de dealer toevoegen
            this.DealerToevoegen(dealer);

            // we beginnenn een nieuw spel, dus even weer resetten.
            this.HuidigeHand = null;
        }

        /// <summary>
        /// Geef een kaart aan de hand.
        /// </summary>
        /// <param name="hand">Huidige habd.</param>
        /// <param name="stapelKaarten">De stapel waarin leggen de kaarten.</param>
        public void GeefDeHandEenKaart(Hand hand, StapelKaarten stapelKaarten)
        {
            // controleer hand
            if (hand == null)
            {
                throw new ArgumentNullException("Er is geen hand.");
            }

            Kaart kaart = stapelKaarten.NeemEenKaart();

            // status van de hand
            Console.WriteLine();
            Console.WriteLine($"{hand.Persoon.Naam} Je krijgt een kaart {kaart.Kleur} van {kaart.Teken}.");
            hand.AddKaart(kaart);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{hand.Persoon.Naam} Je hebt {this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten)} points bij je hand.");
            Console.ResetColor();
        }

        /// <summary>
        /// dit kan alleen de eerste keer.
        /// </summary>
        /// <param name="stapelKaarten">De sptapel kaarten van het spel.</param>
        public void GeefIedereHandEersteKaart(StapelKaarten stapelKaarten)
        {
            foreach (Hand hand in this.Handen)
            {
                this.GeefDeHandEenKaart(hand, stapelKaarten);
            }
        }

        /// <summary>
        /// Geef elke hand van de speler een kaart.
        /// </summary>
        /// <param name="stapelKaarten">De stapel van de kaarten.</param>
        public void GeefIedereHandTweedeKaart(StapelKaarten stapelKaarten)
        {
            foreach (Hand hand in this.Handen)
            {
                if (hand.Persoon == hand.HuidigeSpeler())
                {
                    this.GeefDeHandEenKaart(hand, stapelKaarten);
                }
            }
        }

        /// <summary>
        /// Verdubbel de hand.
        /// </summary>
        /// <param name="hand">De hand die verdubbelt wordt.</param>
        /// <param name="stapelKaarten">De stapel kaarten van het spel.</param>
        public void Verdubbelen(Hand hand, StapelKaarten stapelKaarten)
        {
            /*            Console.WriteLine(hand.HuidigeSpeler().Naam + " : Je mag je inzet Verdubbelen. Wil ja dat doen J of N?");
                        if (this.speler.CheckAntwoord())
                        {
                            if (!this.speler.HeeftSpelerNogFiches())
                            {
                                this.speler.Koopfiches();
                            }

                            hand.VerdubbelenHand();
                        }*/
            hand.GeefFichesBijHand();
            this.GeefDeHandEenKaart(hand, stapelKaarten);
        }

        /// <summary>
        /// Geef een kaart uit.
        /// </summary>
        /// <param name="hand">De hand die een kaart krijgt.</param>
        /// <param name="stapelKaarten">De stapel kaarten van het sperl.</param>
        public void Kopen(Hand hand, StapelKaarten stapelKaarten)
        {
            this.GeefDeHandEenKaart(hand, stapelKaarten);
        }
    }
}
