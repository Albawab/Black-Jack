// <copyright file="Spel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Behandel het spel.
    /// </summary>
    public class Spel
    {
        private readonly ActiesHelper actiesHelper = new ActiesHelper();

        /// <summary>
        /// De lijst van de spelers die gaan spelen.
        /// </summary>
        private List<Speler> spelers = new List<Speler>();

        /// <summary>
        ///  Gets geeft de huidige hand van het spel.
        /// </summary>
        public Hand HuidigeHand { get; private set; }

        /// <summary>
        /// Huidige speler.
        /// </summary>
        private Speler speler = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spel"/> class.
        /// </summary>
        public Spel()
        {
            HuidigeHand = null;
            this.Handen = new List<Hand>();
        }

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
        /// Geeft de eerste hand terug waarvan de status inspel is.
        /// </summary>
        public Hand EersteHandDieNogNietGespeeldIs
        {
            get
            {
                int index = 0;

                // geef de eerste hand terug waarvan de status inspel is
                while (index < this.Handen.Count)
                {
                    if (this.Handen[index].Status == HandStatussen.InSpel)
                    {
                        return this.Handen[index];
                    }

                    index++;
                }

                return null;
            }
        }

        /// <summary>
        /// Voegt een hand toe aan de collectie.
        /// </summary>
        /// <param name="hand">De hand die toegevoegd moet worden.</param>
        public void VoegEenHandToe(Hand hand)
        {
            // TODO welke controle moet ik hier doen?
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
        /// Vraag de speler om een fiche te zetten bij de hand.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <returns>De waarde van de fiches die wordt gezetten.</returns>
        public int VraagSpelerOmFichesTeZetten(Hand hand)
        {
            if (hand == null)
            {
                throw new ArgumentNullException("Er zijn geen hand.");
            }

            Speler speler = hand.HuidigeSpeler();
            int ficheWaarde = speler.FicheWaardeDeSpelerWilZetten();
            speler.ZetFichesBijHandIn(hand, ficheWaarde);
            Console.WriteLine();
            Console.WriteLine($"{speler.Naam} Je zet bij je hand {hand.Inzet.WaardeVanDeFiches} in.");
            return ficheWaarde;
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

            nieuweHand.GeefFichesBijHand(handDieGesplitstMoetWorden);
            return nieuweHand;
        }

        /// <summary>
        /// Maak de lei schoon.
        /// </summary>
        /// <param name="dealer">De Dealer.</param>
        /// <param name="spelers">De spelers die willen spelen.</param>
        public void Start(Dealer dealer, List<Speler> spelers)
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
        /// Doet wat de speler wil doen.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="actie">Wat de speler wil doen.</param>
        public void VoerActieVanDeSpeleruit(Hand hand, Acties actie)
        {
            if (actie == null)
            {
                throw new ArgumentNullException("Actie moet niet null zijn.");
            }

            this.speler = hand.HuidigeSpeler();
            switch (actie)
            {
                case Acties.Splitsen:
                    this.SplitsHand(hand);
                    break;
                case Acties.Verdubbelen:
                    this.Verdubbelen(hand);

                    break;
                case Acties.Kopen:
                    break;
            }
        }

        /// <summary>
        /// Doet wat de speler wil doen.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <param name="acties">De lijst van de acties die de speler mag doen.</param>
        /// <param name="magKaartDelen">Vraag of mag de hand een kaart krijgen.</param>
        public void ProcessActie(Hand hand, Acties actie)
        {
            Console.Write($"{hand.HuidigeSpeler().Naam} je mag");
            this.VoerActieUit(hand, actie);
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
        }

        /// <summary>
        /// dit kan alleen de eerste keer.
        /// </summary>
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

        /*     /// <summary>
             /// Gaat de hand splitsen.
             /// </summary>
             /// <param name="hand">De hand die gesplitst worde.</param>
             private void Splitsen(Hand hand)
             {
                 Console.WriteLine(hand.HuidigeSpeler().Naam + " : Je mag splitsen. Wil je dat doen J of N?");
                 if (this.speler.CheckAntwoord())
                 {
                     if (!this.speler.HeeftSpelerNogFiches())
                     {
                         this.speler.Koopfiches(this.tafel);
                     }

                     this.SplitHand(hand);
                 }
             }
        /// <summary>
                /// Geef een nieuwe hand met kaarten en fiches.
                /// </summary>
                /// <param name="hand">Huidige hand.</param>
                private void SplitHand(Hand hand)
                {
                    Hand nieuweHand = new Hand(hand.Persoon);
                    this.VoegEenHandIn(this.huidigeIndex + 1, nieuweHand);
                    this.NeemKaartVanHand(hand);
                }*/

        /*        /// <summary>
                /// Vraag of de speler wil kopen.
                /// </summary>
                /// <param name="hand">De hand die een kaart wil kopen.</param>
                private void Kopen(Hand hand)
                {
                    Console.WriteLine(hand.HuidigeSpeler().Naam + " : Je mag Een kaart Kopen. Wil ja dat doen J of N?");
                    if (this.speler.CheckAntwoord())
                    {
                        this.GeefDeHandEenKaart(hand);
                        this.BeoordeelHand(hand);
                    }
                    else
                    {
                        this.PassenDeHand(hand);
                    }
                }
        */

        /// <summary>
        /// Verdubbel de hand.
        /// </summary>
        /// <param name="hand">De hand die verdubbelt wordt.</param>
        private void Verdubbelen(Hand hand)
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
        }

        public void Close()
        {
            // oke, roepe close tegen alle handen
            // geen idee iof de hand als gesloten is, maar dat boiet mij niet
            // ik doe gewoon mijn ding
            // todo
        }

        /// <summary>
        /// Voer de actie die de speler heeft gekozen uit.
        /// </summary>
        /// <param name="hand">De huidige hand.</param>
        /// <param name="deActie">De actie die de speler wil doen.</param>
        /// <param name="magKaartDelen">Of de speler mag nog een nieuwe kaart nemen.</param>
        private void VoerActieUit(Hand hand, Acties deActie)
        {
            this.speler = hand.HuidigeSpeler();
            switch (deActie)
            {
                case Acties.Splitsen:
                    this.SplitsHand(hand);
                    break;
                case Acties.Verdubbelen:
                    this.Verdubbelen(hand);

                    break;
                case Acties.Kopen:
                    break;
                case Acties.Passen:
                    break;
            }
        }
    }
}
