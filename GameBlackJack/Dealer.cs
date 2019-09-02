// <copyright file="Dealer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack
{
    using HenE.GameBlackJack.Enum;
    using HenE.GameBlackJack.SpelSpullen;

    /// <summary>
    /// Hier controleert de dealer het spel.
    /// </summary>
    public class Dealer : Persoon
    {
        private Kaart kaart;
        private Fiche fiche;
        private readonly StapelKaarten stapelKaarten;
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
        /// <param name="speler">Aan de speler.</param>
        /// <param name="tafel">Op de tafel.</param>
        /// <param name="stapelKaarten">De kaart .</param>
        public void DeelDeBeginKaarten(Speler speler, SpelSpullen.Tafel tafel, StapelKaarten stapelKaarten)
        {
            foreach (Plek plek in tafel.EenPlek())
            {
                this.kaart = stapelKaarten.NeemEenKaart();
                speler.HandVanDeSpeler().VoegKaartIn(this.kaart);
            }
        }

        /// <summary>
        /// De dealer deelt de tweede rondje van de kaarten.
        /// </summary>
        /// <param name="huidigeSpeler">Aan de speler.</param>
        /// <param name="tafel">Op de tafel.</param>
        /// <param name="stapelKaarten">De kaarten.</param>
        public void DeelDeTweedeRondjeVanDeKaarten(Speler huidigeSpeler, SpelSpullen.Tafel tafel, StapelKaarten stapelKaarten)
        {
            foreach (Plek plek in tafel.EenPlek())
            {
                this.kaart = stapelKaarten.NeemEenKaart();
                huidigeSpeler.HandVanDeSpeler().VoegKaartIn(this.kaart);
            }
        }

        /// <summary>
        /// De dealer neemt een kaart van de stapel kaarten en hij geeft de kaart aan de speler.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <returns>Een Kaart.</returns>
        public Kaart GeefEenKaart(Hand hand)
        {
            this.kaart = this.stapelKaarten.NeemEenKaart();
            return this.kaart;
        }

        /// <summary>
        /// De dealer neemt een fiches vanuit de de fiches bak.
        /// </summary>
        /// <param name="hand">Huidige hand.</param>
        /// <param name="huidigeFiches">De fiches die de speler wil kopen.</param>
        /// <returns>Een fiche.</returns>
        public Fiche GeefEenFiche(Hand hand, Fiche huidigeFiches)
        {
            this.fiche = this.fichesBank.NeemEenFiche(huidigeFiches);
            return this.fiche;
        }

        /// <summary>
        /// Deze method gaat een fiche nemen vanuit de fiches bak.
        /// </summary>
        /// <param name="waarde">De waarde.</param>
        /// <returns>een fiche.</returns>
        public Fiche NeemEenFicheGelijkAanBedrag(FichesWaarde waarde)
        {
            this.fiche = this.fichesBank.ZoekEenFiche(waarde);
            return this.fiche;
        }
    }
}
