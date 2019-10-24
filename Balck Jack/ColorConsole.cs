// <copyright file="ColorConsole.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenEBalck_Jack
{
    using System;

    /// <summary>
    /// Geef de text een keleur.
    /// </summary>
    public static class ColorConsole
    {
        /// <summary>
        /// Change de kleur van een text.
        /// Write op een nieuwe lijn.
        /// </summary>
        /// <param name="color">De kleur die gaat passen.</param>
        /// <param name="message">De message die de kleur wordt veranderd.</param>
        public static void WriteLine(ConsoleColor color, string message)
        {
            // Geef de text een color.
            Console.ForegroundColor = color;
            Console.WriteLine(message);

            // Reset color to basic color.
            Console.ResetColor();
        }

        /// <summary>
        /// Change de kleur van een text.
        /// Write op de zelfe lijn.
        /// </summary>
        /// <param name="color">De kleur die gaat passen.</param>
        /// <param name="message">De message die de kleur wordt veranderd.</param>
        public static void Write(ConsoleColor color, string message)
        {
            // Geef de text een color.
            Console.ForegroundColor = color;
            Console.Write(message);

            // Reset color to basic color.
            Console.ResetColor();
        }
    }
}