using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp;
using HenE.GameBlackJack;
using static HenE.GameBlackJack.Fiche;
using HenE.GameBlackJack.SpelSpullen;

namespace HenE.WinFormsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [Obsolete]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Application.Run(new BlackJack());
            Application.Exit();
            Console.ReadLine();
        }
    }
}
