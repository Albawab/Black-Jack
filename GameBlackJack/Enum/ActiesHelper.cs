// <copyright file="ActiesHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.GameBlackJack.Enum
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Ga de enum acties helpen.
    /// </summary>
    public class ActiesHelper
    {
        /// <summary>
        /// Geef de enum als string terug.
        /// </summary>
        /// <param name="actie">De acties.</param>
        /// <returns>Actie als string.</returns>
        public string ZetEnumTotStringOm(Acties actie)
        {
            string actieString = string.Empty;
            switch (actie)
            {
                case Acties.Kopen:
                    actieString = "Kopen";
                    break;
                case Acties.Passen:
                    actieString = "Passen";
                    break;
                case Acties.Splitsen:
                    actieString = "Splitsen";
                    break;
                case Acties.Verdubbelen:
                    actieString = "Verdubbelen";
                    break;
            }

            return actieString;
        }
    }
}
