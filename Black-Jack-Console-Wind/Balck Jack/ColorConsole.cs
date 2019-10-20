// <copyright file="ColorConsole.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Geef de text een keleur.
    /// </summary>
    public static class ColorConsole
    {
        /// <summary>
        /// Change de kleur van een text.
        /// </summary>
        /// <param name="color">De kleur die gaat passen.</param>
        /// <param name="message">De message die de kleur wordt veranderd.</param>
        public static void WriteLine(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}