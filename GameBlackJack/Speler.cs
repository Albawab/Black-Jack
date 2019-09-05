// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HenE.GameBlackJack.HelperEnum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// De klas van de Speler.
    /// </summary>
    public class Speler : Persoon
    {
        /// <summary>
        /// Hier staan de handen van de spelers.
        /// </summary>
        private readonly List<Hand> handen = new List<Hand>();

        /// <summary>
        /// Hoeveel fiches de speler heeft.
        /// </summary>
        private readonly List<Fiche> portemonnee = new List<Fiche>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Speler"/> class.
        /// </summary>
        /// <param name="naam"> De naam van de speler.</param>
        public Speler(string naam)
            : base(naam)
        {
        }

        /// <summary>
        /// Gets Waar de speler wil zitten.
        /// </summary>
        public Plek PlekAanTafel { get; private set; }

        /// <summary>
        /// Gets de hand van de speler.
        /// </summary>
        public Hand Hand { get; private set; }

        /// <summary>
        /// Geef de speler een plek.
        /// </summary>
        /// <param name="plek">op dit plek de speler zit.</param>
        public void NeemtEenPlek(Plek plek)
        {
            this.PlekAanTafel = plek;
        }

        /// <summary>
        /// De speler bepaalt wat hij wil kopen.
        /// </summary>
        /// <param name="hetBedrag">De waarde van de fiches.</param>
        /// <param name="dealer">Huidige dealer.</param>
        /// <param name="fichesBak">Huidige fiches bak.</param>
        public void Koopfiches(int hetBedrag, Dealer dealer, FichesBak fichesBak)
        {
            HelperFiches helperFiches = new HelperFiches();
            Fiche createFiche = helperFiches.OmzettenWaardeDieDeSpelerwil_TotEenFiche(hetBedrag, fichesBak, dealer);
            this.portemonnee.Add(createFiche);
        }

        /// <summary>
        /// Zoek hoe veel fiches de speler heeft in zijn portemonnee.
        /// </summary>
        /// <returns>fiches als string.</returns>
        public string FichesInPortemonnee()
        {
            int i = 0;
            StringBuilder fiches = new StringBuilder();
            foreach (Fiche fiche in this.portemonnee)
            {
                i++;
                string stringFiche = fiche.FicheKleur.ToString();
                fiches.Append("\n" + i.ToString() + "-" + stringFiche + "\n");
            }

            return fiches.ToString();
        }

        /// <summary>
        /// Zoek in de portemonnee voor een fiche.
        /// </summary>
        /// <param name="gekozen">Wat de speler heeft gekozen.</param>
        /// <param name="spel">Dit Spel.</param>
        public void FichesZetten(List<int> gekozen, Spel spel)
        {
            List<Fiche> itemGekozen = new List<Fiche>();
            foreach (int item in gekozen)
            {
                Fiche fiche = this.portemonnee[item - 1];
                Hand hand = new Hand(this);
                spel.VoegEenHandIn(hand);
                this.handen.Add(hand);
                hand.VoegEenFichesIn(fiche);
                itemGekozen.Add(fiche);
            }

            foreach (Fiche fiche1 in itemGekozen)
            {
                this.portemonnee.Remove(fiche1);
            }
        }

        /// <summary>
        /// Geef de hande terug.
        /// </summary>
        /// <returns>De lijst van de handen.</returns>
        public List<Hand> GeefHanden()
        {
            return this.handen;
        }

        /// <summary>
        /// De speler zet de fiches op.
        /// De fiches is gelijk aan de fiches die in de hand is.
        /// Verwijdert de fiches van portemonnee.
        /// </summary>
        /// <param name="fiches">De fiches die in de hand is.</param>
        /// <param name="dealer">Hidige dealer.</param>
        /// <param name="fichesBak">Huidige fiches bak.</param>
        /// <returns>De fiches die de speler wil zitten.</returns>
        public List<Fiche> ZetGelijkFiches(List<Fiche> fiches, Dealer dealer, FichesBak fichesBak)
        {
            List<Fiche> fichesDeSpelerWilZetten = new List<Fiche>();
            if (this.portemonnee.Count != 0)
            {
                foreach (Fiche fiche in fiches)
                {
                    foreach (Fiche ficheInPortemonnee in this.portemonnee)
                    {
                        if (fiche.FicheKleur == ficheInPortemonnee.FicheKleur)
                        {
                            fichesDeSpelerWilZetten.Add(ficheInPortemonnee);
                            this.portemonnee.Remove(ficheInPortemonnee); // ==> deleted boven dan add.
                            if (this.portemonnee.Count == 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Je portemonnee is leeg, Wil je fiches Kopen J of N?");
                ConsoleKeyInfo antwoord = Console.ReadKey();
                while (antwoord.Key != ConsoleKey.J && antwoord.Key != ConsoleKey.N)
                {
                    Console.WriteLine("Graag type \"J\" of \"N\"");
                    antwoord = Console.ReadKey();
                }

                if (antwoord.Key == ConsoleKey.J)
                {
                    int waarde = 0;
                    string waardeAntwoord = Console.ReadLine();
                    while (!int.TryParse(waardeAntwoord, out waarde))
                    {
                        Console.WriteLine("Graag geef een nummer!");
                        waardeAntwoord = Console.ReadLine();
                    }

                    this.Koopfiches(waarde, dealer, fichesBak);
                }
            }

            return fichesDeSpelerWilZetten;
        }

        /// <summary>
        /// Voeg een hand aan de lijst van de handen.
        /// </summary>
        /// <param name="hand">Nieuwe hand.</param>
        public void VoegEenHandIn(Hand hand)
        {
            this.handen.Add(hand);
        }

        /// <summary>
        /// Controleer of de speler mag de waarde zetten.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="huidigeFiche">Huidige fiche.</param>
        public void ZetFiches(SpelSpullen.Tafel tafel, Hand hand, Fiche huidigeFiche)
        {
            /*foreach (Fiches fiche in this.Portemonnee)
            {
                if (fiche == huidigeFiche)
                {
                    this.Portemonnee.Remove(fiche);

                    hand.VoegEenFichesIn(fiche);
                }
            }*/
        }

        /// <summary>
        /// De speler beslist wat wil hij doen.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        /// <param name="wilDoen">Wat wil hij doen.</param>
        /// <param name="dealer">De dealer.</param>
        /// <param name="fiches">De class van de fiches.</param>
        /// <param name="huidigeHand">Huidige hand.</param>
        public void DeSpelerWilDoen(SpelSpullen.Tafel tafel, string wilDoen, Dealer dealer, Fiche fiches, Hand huidigeHand)
        {
            Hand hand1 = null;
            /*foreach (Hand hand in this.Hands)
            {
                if (hand == huidigeHand)
                {
                    hand1 = hand;
                }
            }
            */
            switch (wilDoen)
            {
                // enum
                case "koop":
                    hand1.VoegKaartIn(dealer.GeefEenKaartTerug(hand1));
                    break;

                case "passen":
                    // dealer.NaarVolgendeHand(tafel, this);
                    break;

                case "verdubbelen":
                    // Hand newHand = new Hand();
                    // Fiches fiches1 = null;
                    /*this.Hands.Add(newHand);
                    foreach (Hand hand in this.Hands)
                    {
                        if (hand == huidigeHand)
                        {
                            fiches1 = hand.FichesInHand(hand);
                        }
                    }
                    */
                    // newHand.VoegEenFichesIn(fiches1);
                    // dealer.GeefEenKaart(newHand);
                    break;

                case "Splitsen":
                    /*Hand splitsenHand = new Hand();
                    Fiches splitsenFiches = null;
                    this.Hands.Add(splitsenHand);
                    foreach (Hand hand in this.Hands)
                    {
                        if (hand == huidigeHand)
                        {
                            splitsenFiches = hand.FichesInHand(hand);
                        }
                    }

                    splitsenHand.VoegEenFichesIn(splitsenFiches);
                    dealer.GeefEenKaart(splitsenHand);
                    */
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
        public void VerzamelenDeFiches(Fiche fiches)
        {
            // this.Portemonnee.Add(fiches);
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
            // this.Hands.Remove(huidigeHand);
        }

        /// <summary>
        /// Neem de hands van de speler.
        /// </summary>
        /// <returns>Deze hand.</returns>
        public Hand HandVanDeSpeler()
        {
            /*foreach (Hand hand in this.Hands)
            {
                return hand;
            }
            */
            return null;
        }
    }
}