// <copyright file="Helper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Helper
{
    using HenE.GameBlackJack.Enum;

    /// <summary>
    /// change een enum tot string.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// change de actie tot stiring en geef hem terug.
        /// </summary>
        /// <param name="acties">De actie die wordt geomgezetten tot string.</param>
        /// <returns>actie als string.</returns>
        public static string ChangeEnumToString(Acties acties)
        {
            switch (acties)
            {
                case Acties.IsDefined:
                    return "IsDefined";
                case Acties.Kopen:
                    return "Kopen";
                case Acties.Passen:
                    return "Passen";
                case Acties.Verdubbelen:
                    return "Verdubbelen";
                case Acties.Splitsen:
                    return "Splitsen";
                default:
                    return null;
            }
        }
    }
}
