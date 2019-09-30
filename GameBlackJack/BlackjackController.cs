// <copyright file="BlackjackController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using System;
    using System.Collections.Generic;
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.Kaarten;
    using HenE.GameBlackJack.Settings;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Controller op het spel.Is het spelr gestart.Vraagt de dealer om iets te doen. vraagt ook de spelet om iets te doen.
    /// </summary>
    public class BlackjackController
    {
        private readonly ICalculatePoints pointsCalculator = new BlackJackPointsCalculator();
        private readonly BlackJackPointsCalculator blackJackPointsCalculator = new BlackJackPointsCalculator();
        private readonly Tafel tafel;
        private readonly KaartenExtensions kaartenHelper = new KaartenExtensions();
        private readonly Spel spel = new Spel();
        private Speler speler = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackjackController"/> class.
        /// </summary>
        /// <param name="tafel">Huidige tafel.</param>
        public BlackjackController(Tafel tafel)
        {
            this.tafel = tafel;
        }

        /// <summary>
        /// Check of het spel is klaar om te starten.
        /// </summary>
        /// <returns>Klaar of nog niet.</returns>
        public bool Start()
        {
            // check of ik alles heb
            if (this.tafel.Dealer == null)
            {
                throw new ArgumentNullException("Er is geen dealer.");
            }

            this.StartRonde();
            return true;
        }

        /// <summary>
        /// Staart een rondje.
        /// </summary>
        private void StartRonde()
        {
            if (this.tafel.Plekken == null)
            {
                throw new ArgumentNullException("Er zijn geen plekken met spelers.");
            }

            this.BepaalWaarElkeSpelerGaatZitten();

            this.spel.Start(this.tafel.Dealer, this.tafel.Spelers);
            this.spel.GeefIedereHandEersteKaart(this.tafel.StapelKaarten);
            this.spel.GeefIedereHandTweedeKaart(this.tafel.StapelKaarten);

            while (this.spel.GaNaarDeVolgendeSpeelbareHand() != null)
            {
                Hand huidigeHand = this.spel.HuidigeHand;
                List<Acties> mogelijkActies = this.ControleerHand(huidigeHand);

                if (mogelijkActies.Count == 0)
                {
                    // dan kan ik niks en ga ik naar de volgende hand. Bijv, omdat de hand is gesloten
                    Console.WriteLine($"{huidigeHand.HuidigeSpeler().Naam} je mag geen actie doen, dan word je gestopt.");
                    continue;
                }

                this.spel.GaNaarDeVolgendeSpeelbareHand();
                while (mogelijkActies.Count > 0)
                {
                    if (mogelijkActies.Count == 1)
                    {
                        this.spel.ProcessActie(this.spel.HuidigeHand, mogelijkActies[0]);
                    }
                    else
                    {
                        // er zijn meerdere acties mogelijk, vraag aan de speler wat hij/zij wil
                        Acties gekozenActie = this.AskActie(mogelijkActies, this.spel.HuidigeHand);
                        this.spel.ProcessActie(this.spel.HuidigeHand, gekozenActie);
                    }

                    // zijn  er nog acties mogelijk>?
                    mogelijkActies = this.ControleerHand(huidigeHand);
                }

/*                else
                {
                    this.BehandelDeDealer(huidigeHand);
                }*/

                    // wat zijn de mogelijkheden?
                //huidigeHand = this.spel.NextHand;
            }
        }

        /// <summary>
        /// Bepaal wat de speler mag doen.En welke actie mag returen.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        private List<Acties> ControleerHand(Hand hand)
        {
            List<Acties> acties = new List<Acties>();

            while (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 9 && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 11 && hand.Kaarten.Count == 2)
            {
                acties.Add(Acties.Verdubbelen);
                break;
            }

            while (this.kaartenHelper.MagSplitsen(hand))
            {
                acties.Add(Acties.Splitsen);
                break;
            }

            while (this.MagDeSpelerKopen(hand))
            {
                acties.Add(Acties.Kopen);
                break;
            }

            while (this.MagDeSpelerPassen(hand))
            {
                acties.Add(Acties.Passen);
                break;
            }

            return acties;
        }

        /// <summary>
        /// Verdubblen de hand.Fiches bij de hand zetten.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        private void VerdubbelenHand(Hand hand)
        {
            this.speler.ZetFichesBijHandIn(hand);
        }

/*        private void BeoordeelHand(Hand hand)
        {
            if (this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) > 21)
            {
                hand.ChangeStatus(HandStatussen.IsDood);
                this.CloseHand(hand);
            }
        }*/

        /// <summary>
        /// Beaal aan de Hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="wordtBetaal">De waarde die worde gebetaald.</param>
        private void KeerUit(Hand hand, double wordtBetaal)
        {
            if (wordtBetaal == 1.5)
            {
                int moetBetalenAanHand = hand.Inzet.WaardeVanDeFiches * (int)1.5;
                this.FichesVerdienen(hand, moetBetalenAanHand);
            }
            else if (wordtBetaal == 1.0)
            {
                this.FichesVerdienen(hand, hand.Inzet.WaardeVanDeFiches);
            }
        }

        /// <summary>
        /// Neem de status van de hand en bepaal Hoeveel keer aan de speler moet terug betalen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Hoeveel keer moet aan de speler moet betalen.</returns>
        private double BepaalFactorInzet(Hand hand)
        {
            // betaal uit
            switch (hand.Status)
            {
                case HandStatussen.BlackJeck:
                    return 1.5;

                case HandStatussen.Gewonnen:
                    return 1.0;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// Vraag de speler wat hij wil doen.
        /// </summary>
        /// <param name="mogelijkActies">Lijst van de acties die de speler mag van uit het mag kiezen is.</param>
        /// <param name="huidigeHand">De huidige hand.</param>
        private Acties AskActie(List<Acties> mogelijkActies, Hand huidigeHand)
        {
            return huidigeHand.HuidigeSpeler().AskActie(mogelijkActies, huidigeHand);
        }

        /// <summary>
        /// Als de hand klaar is dan doe hij dicht.
        /// </summary>
        /// <param name="hand">De hand die klaar is.</param>
        private void CloseHand(Hand hand)
        {
            if (hand.Status == HandStatussen.BlackJeck)
            {
                this.KeerUit(hand, this.BepaalFactorInzet(hand));
            }
            else if (hand.Status == HandStatussen.OnHold)
            {
            }
            else if (hand.Status == HandStatussen.Gewonnen)
            {
                this.KeerUit(hand, this.BepaalFactorInzet(hand));
            }
            else
            {
                this.VerzameelDeFiches(hand); // ==========================> verlies
            }

            hand.Close();
        }

        /// <summary>
        /// Controleert of de speler mag kopen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Of de speler kan kopen of niet.</returns>
        private bool MagDeSpelerKopen(Hand hand)
        {
            if (hand.Status == HandStatussen.InSpel)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// controleert of de speler mag passen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Mag de speler passen of niet.</returns>
        private bool MagDeSpelerPassen(Hand hand)
        {
            if (hand.Status == HandStatussen.InSpel)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Controleer of de dealer mag kopen.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns>Mag de dealer kopen of mag niet.</returns>
        private bool MoetDeDealerKopen(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) < 17;
        }

        /// <summary>
        /// Controleer of de dealer moet passen.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns> mag passen of niet.</returns>
        private bool MoetDeDealerPassen(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) >= 17 && this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) <= 21;
        }

        /// <summary>
        /// Check als de dealer meer dan 21 punten heeft dan maak de situatie van de hand Dood.
        /// </summary>
        /// <param name="hand">De hand van de dealer.</param>
        /// <returns>Heeft de dealer meer dan 21 punten of niet.</returns>
        private bool IsDealerDood(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten) > 21;
        }

        /// <summary>
        /// Controleer aan de hand van de dealer.
        /// </summary>
        /// <param name="dealerHand">De hand van de dealer.</param>
        private void BehandelDeDealer(Hand dealerHand)
        {
            while (this.MoetDeDealerKopen(dealerHand))
            {
                this.spel.GeefDeHandEenKaart(dealerHand, this.tafel.StapelKaarten);
            }

            if (this.MoetDeDealerPassen(dealerHand))
            {
                dealerHand.ChangeStatus(HandStatussen.Gepassed);
                Console.WriteLine("Je heeft meer dan 17 punten dus je moet passen.");
            }
            else if (this.IsDealerDood(dealerHand))
            {
                dealerHand.ChangeStatus(HandStatussen.IsDood);
            }
        }

        /// <summary>
        /// Controleer de punten.
        /// Geef de winnaar fiches.
        /// Neem van de losser fiches.
        /// beeind het spel.
        /// </summary>
        /// <param name="handen">De handen als lijst.</param>
        private void BeeindHetSpel(List<Hand> handen)
        {
            int waardeVanDeDealerHand = this.WaardeVanDeDealerHand(handen);
            foreach (Hand hand in handen)
            {
                if (hand.HuidigeSpeler() != null)
                {
                    if (hand.Status != HandStatussen.BlackJeck && hand.Status != HandStatussen.IsDood && hand.Status != HandStatussen.Gestopt)
                    {
                        if (waardeVanDeDealerHand <= 21)
                        {
                            this.DefinieerResultaten(hand, waardeVanDeDealerHand);
                        }
                        else
                        {
                            hand.ChangeStatus(HandStatussen.Gewonnen);
                        }
                    }

                    this.CloseHand(hand);
                }
            }
        }

        /// <summary>
        /// Geef de waarde van de hand van de dealer.
        /// </summary>
        /// <param name="handen">Handen van het spel.</param>
        /// <returns>De waarde van de dealers hand.</returns>
        private int WaardeVanDeDealerHand(List<Hand> handen)
        {
            foreach (Hand hand in handen)
            {
                if (hand.Persoon != hand.HuidigeSpeler())
                {
                    return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten);
                }
            }

            return 0;
        }

        /// <summary>
        /// Calculate Hoe veel score er in de hand van de speler staat.
        /// </summary>
        /// <param name="hand">De hand van de speler.</param>
        /// <returns>De score van de kaarten die op de hand van de speler staat. </returns>
        private int WaardeVanDeSpelerHand(Hand hand)
        {
            return this.blackJackPointsCalculator.CalculatePoints(hand.Kaarten);
        }

        /// <summary>
        /// betaal aan de hand de fiches die de speler heeft gwonnen.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="betaalAanHand">Het bedrag die moet betalen worden.</param>
        private void FichesVerdienen(Hand hand, double betaalAanHand)
        {
            if (betaalAanHand == 1.5)
            {
                double keerEnHalfUit = hand.Inzet.WaardeVanDeFiches * 1.5;
                int keerEnHalfUitWordtBetaald = (int)keerEnHalfUit;
                hand.Inzet.GeefMeFischesTerWaardeVan(keerEnHalfUitWordtBetaald, 2, true);
            }

            hand.Inzet.Add(this.tafel.Fiches.GeefMeFischesTerWaardeVan((int)betaalAanHand));
        }

        /// <summary>
        /// Neem de fiches van de hand uit.
        /// </summary>
        /// <param name="hand">De hand.</param>
        private void VerzameelDeFiches(Hand hand)
        {
            this.tafel.Fiches.Add(hand.Inzet.GeefMeFischesTerWaardeVan(hand.Inzet.WaardeVanDeFiches));
        }

        /// <summary>
        /// Contreel of de speler is gewonnen of niet. Change de situatie van de hand.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="waardeVanDeDealerHand">De waarde van de hand van de dealer.</param>
        private void DefinieerResultaten(Hand hand, int waardeVanDeDealerHand)
        {
            int waardeVanDeSpeler = this.WaardeVanDeSpelerHand(hand);

            if (waardeVanDeSpeler < waardeVanDeDealerHand)
            {
                hand.ChangeStatus(HandStatussen.Verloren);
                Console.WriteLine($"Helaas, je bent verloren! \n{hand.Persoon.Naam} je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
            }
            else if (waardeVanDeSpeler == waardeVanDeDealerHand)
            {
                hand.ChangeStatus(HandStatussen.OnHold);
                Console.WriteLine($" Je mag wachten totdat het volgend rondje start. \n{hand.Persoon.Naam} je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
            }
            else if (waardeVanDeSpeler > waardeVanDeDealerHand && waardeVanDeSpeler <= 21)
            {
                hand.ChangeStatus(HandStatussen.Gewonnen);
                Console.WriteLine($"Wat leuk, Je bent gewonnen. {hand.Persoon.Naam} je heeft {waardeVanDeSpeler} punten en de dealer heeft {waardeVanDeDealerHand}");
            }
        }

        /// <summary>
        /// Geef elke persoon een plek.
        /// </summary>
        private void BepaalWaarElkeSpelerGaatZitten()
        {
            for (int i = 0; i < this.tafel.Plekken.Length; i++)
            {
                if (this.tafel.Plekken[i].Speler != null)
                {
                    Console.WriteLine();
                    int plek = i + 1;
                    Console.WriteLine(" Je gaat op de plek " + plek + " aan de tafel zitten");
                }
            }
        }

        /// <summary>
        /// Check of de fiches mag inzitten of de speler moet opnieuw te evalueren.
        /// </summary>
        /// <param name="ficheWaarde">De waarde van de fiches.</param>
        private void IsJusteWaarde(int ficheWaarde)
        {
            while (!this.tafel.BepaaltOfDeWaardetussenMaxInzetEnMinInzet(ficheWaarde))
            {
                Console.WriteLine("Een onjuiste waarde.");
                ficheWaarde = this.speler.FicheWaardeDeSpelerWilZetten();
            }
        }
     }
}
