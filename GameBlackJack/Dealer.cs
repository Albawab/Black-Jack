// <copyright file="Dealer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Hier controleert de dealer het spel.
    /// </summary>
    public class Dealer : Persoon
    {
        private Kaart kaart;
        private Fiche fiche;
        private readonly FichesBak fichesBank;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dealer"/> class.
        /// </summary>
        /// <param name="naam">De naam van de dealer.</param>
        public Dealer(string naam)
            : base(naam)
        {
        }

        /// <summary>
        /// Gets or Sets De plek waar de dealer zit aan de tafel.
        /// </summary>
        private Plek PlekAanTafel { get; set; }

        /// <summary>
        /// Gets or Sets de hand van de dealer.
        /// </summary>
        private Hand Hand { get; set; }

        /// <summary>
        /// De dealer deelt de begin kaarten.
        /// </summary>
        /// <param name="spel">Huidig spel.</param>
        /// <param name="tafel">huidige tafel.</param>
        public void DeelDeBeginKaarten(Spel spel, Tafel tafel)
        {
            StapelKaarten kaarten = tafel.StapelKaarten;
            Hand hand = new Hand(this);
            this.Hand = hand;
            spel.VoegEenHandIn(hand);
            List<Hand> handen = spel.NeemHand();
            foreach (Hand hand1 in handen)
            {
                this.kaart = kaarten.NeemEenKaart();
                hand1.VoegKaartIn(this.kaart);
            }
        }

        /// <summary>
        /// De dealer deelt de tweede rondje van de kaarten.
        /// </summary>
        /// <param name="spel">Het Spel.</param>
        /// <param name="tafel">Op de tafel.</param>
        public void DeelDeTweedeRondjeVanDeKaarten(Spel spel, Tafel tafel)
        {
            StapelKaarten kaarten = tafel.StapelKaarten;
            List<Hand> handen = spel.NeemHand();
            foreach (Hand hudigeHand in handen)
            {
                if (hudigeHand == this.Hand)
                {
                    this.kaart = null;
                }
                else
                {
                    this.kaart = kaarten.NeemEenKaart();
                    hudigeHand.VoegKaartIn(this.kaart);
                    this.CheckDeHand(handen);
                    if (hudigeHand.BlackJeck())
                    {
                        hudigeHand.PutBlackJack();
                    }
                }
            }
        }

        /// <summary>
        /// De dealer neemt een kaart van de stapel kaarten en hij geeft de kaart aan de speler.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Een Kaart.</returns>
        public Kaart GeefEenKaart(Hand hand)
        {
            // this.kaart = this.stapelKaarten.NeemEenKaart();
            return this.kaart;
        }

        /// <summary>
        /// De dealer neemt een fiches vanuit de de fiches bak.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="huidigeFiches">De fiches die de dealer aan de speler wil geven.</param>
        /// <returns>Een fiche.</returns>
        public Fiche GeefEenFiche(Hand hand, Fiche huidigeFiches)
        {
            this.fiche = this.fichesBank.NeemEenFiche(huidigeFiches);
            return this.fiche;
        }

        /*
        /// <summary>
        /// Deze method gaat een fiche nemen vanuit de fiches bak.
        /// </summary>
        /// <param name="waarde">De waarde.</param>
        /// <returns>een fiche.</returns>
        public Fiche NeemEenFicheGelijkAanBedrag(FichesWaarde waarde)
        {
            this.fiche = this.fichesBank.ZoekEenFiche(waarde, this);
            return this.fiche;
        }
        */

        /// <summary>
        /// Geeft een waarde aan de juste fiche.
        /// </summary>
        /// <param name="waarde">waarde die de speler wil kopen.</param>
        /// <param name="fiches">list van de fiches die in de fiches bak staat.</param>
        /// <returns>een fiche.</returns>
        public Fiche GeefWaardeAanFiche(FichesWaarde waarde, List<Fiche> fiches)
        {
            Fiche fiche1 = null;
            foreach (Fiche fiche in fiches)
            {
                switch (waarde)
                {
                    case FichesWaarde.Tien:
                        if (fiche.FicheKleur == FichesKleur.Blue)
                        {
                            fiche1 = fiche;
                            fiches.Remove(fiche);
                        }

                        break;
                    case FichesWaarde.Twintig:
                        if (fiche.FicheKleur == FichesKleur.Geel)
                        {
                            fiche1 = fiche;
                            fiches.Remove(fiche);
                        }

                        break;
                    case FichesWaarde.Vijfentwintig:
                        if (fiche.FicheKleur == FichesKleur.Groen)
                        {
                            fiche1 = fiche;
                            fiches.Remove(fiche);
                        }

                        break;
                    case FichesWaarde.Vijftien:
                        if (fiche.FicheKleur == FichesKleur.Rood)
                        {
                            fiche1 = fiche;
                            fiches.Remove(fiche);
                        }

                        break;
                }

                if (fiche1 != null)
                {
                    break;
                }
            }

            return fiche1;
        }

        /// <summary>
        /// De dealer controleert wat de hand heeft.
        /// </summary>
        /// <param name="handen">Lijst of handen.</param>
        private void CheckDeHand(List<Hand> handen)
        {
            List<Kaart> kaarten;
            foreach (Hand hand in handen)
            {
                kaarten = hand.NeemKaarten();
                foreach (Kaart kaart in kaarten)
                {
                    switch (kaart.Teken)
                    {
                        case KaartTeken.Aas:
                            hand.AddEenPunten(10);
                            break;
                        case KaartTeken.Twee:
                            hand.AddEenPunten(2);
                            break;
                        case KaartTeken.Drie:
                            hand.AddEenPunten(3);
                            break;
                        case KaartTeken.Vier:
                            hand.AddEenPunten(4);
                            break;
                        case KaartTeken.Vijf:
                            hand.AddEenPunten(5);
                            break;
                        case KaartTeken.Zes:
                            hand.AddEenPunten(6);
                            break;
                        case KaartTeken.Zeven:
                            hand.AddEenPunten(7);
                            break;
                        case KaartTeken.Acht:
                            hand.AddEenPunten(8);
                            break;
                        case KaartTeken.Negen:
                            hand.AddEenPunten(9);
                            break;
                        case KaartTeken.Tien:
                            hand.AddEenPunten(10);
                            break;
                        case KaartTeken.Heer:
                            hand.AddEenPunten(10);
                            break;
                        case KaartTeken.Vrouw:
                            hand.AddEenPunten(10);
                            break;
                        case KaartTeken.Boer:
                            hand.AddEenPunten(10);
                            break;
                    }
                }
            }
        }
    }
}
