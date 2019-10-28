// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Balck_Jack
{
    using HenE.GameBlackJack;
    using HenE.GameBlackJack.Settings;
    using HenE.GameBlackJack.SpelSpullen;
    using HenEBalck_Jack;

    /// <summary>
    /// Program van het spel.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">args.</param>
        public static void Main(string[] args)
        {
            InitialiseerDeTafel initialiseerDeTafel = new InitialiseerDeTafel(new ConsoleCommunicatorBehandelen());
            initialiseerDeTafel.Initialiseer();
            BlackjackController blackJackController = new BlackjackController(initialiseerDeTafel.HuidigeTafel(), new ConsoleCommunicatorBehandelen());
            blackJackController.Start();
        }
    }
}
