// <copyright file="Meldingen.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Enum
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Die gaat bepaal wat soort van melding is dat.
    /// </summary>
    public enum Meldingen
    {
        /// <summary>
        /// Als de speler geen actie heeft.
        /// </summary>
        GeenActie,

        /// <summary>
        /// Die toont inzet.
        /// </summary>
        ToonInzet,

        /// <summary>
        /// Die toont de hand.
        /// </summary>
        ToonHand,

        /// <summary>
        /// Als de dealer dood is, dan laat de anders dat weten.
        /// </summary>
        DealerDied,

        /// <summary>
        /// Als de persoon dood is dan laat hem dat weten.
        /// </summary>
        YouDied,

        /// <summary>
        /// De waarde die bij de hand is.
        /// </summary>
        WaardeVanDeHand,

        /// <summary>
        /// De kaarten die bij de hand zij.
        /// </summary>
        KaartenVanDeHand,

        /// <summary>
        /// Als de inzet is niet geldig.
        /// </summary>
        OngeldigeInzet,

        /// <summary>
        /// Als de speler heeft iets verdienen.
        /// </summary>
        Verdienen,

        /// <summary>
        /// Als de speler verliezen is, dan laat hem dat weten.
        /// </summary>
        Verliezen,

        /// <summary>
        /// De acties die de speler mag doen.
        /// </summary>
        Acties,

        /// <summary>
        /// Als er een fout is.
        /// </summary>
        Fout,

        /// <summary>
        /// Als de speler heeft waarde die gelijk aan de waarde van de dealer, dus hij moet  wachten.
        /// </summary>
        Hold,

        /// <summary>
        /// Als de speler is gewonnen dan laat hem dat weten.
        /// </summary>
        Gewonnen,

        /// <summary>
        /// Actie die de speler heeft gedaan.
        /// </summary>
        ActieGekozen,
    }
}
