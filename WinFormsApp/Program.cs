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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // fiches
            // de hoofdbak met fiches
            Fiches cassiereFiches = FicheFactory.CreateFiches(1000);

            // tafel
            Tafel tafel = Tafel.CreateBlackJackTafel(cassiereFiches.GeefMeFischesTerWaardeVan(500));

            // is   de waarde vban de fiches nu 500?

            // dealer
            // dealer aanmaken en toewijzen aan een tafel
            Dealer dealer = new Dealer("Dealer");
            dealer.GaAanTafelZitten(tafel);

            // spelers, komen binnen en kopen bij het cassiere fiches
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Speler: A");
            Console.ResetColor();
            Console.WriteLine("Leuk je komt Black Jack spelen. Wilt je me je naam vertelen?");
            string naamSpelerA = "Joris";
            Speler spelerA = new Speler(naamSpelerA,new WinFormCommunicator());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Speler: B");
            Console.ResetColor();
            Console.WriteLine("Leuk je komt Black Jack spelen. Wilt je me je naam vertelen?");
            string naamSpelerB = Console.ReadLine();


            // koopt fiches vbij de cassiere
            spelerA.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(90, 20, true));
            spelerB.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(90, 20, true));

            Console.WriteLine();
            Console.WriteLine(spelerA.Naam + "Je hebt gekocht");

            Console.WriteLine();
            Console.WriteLine(spelerB.Naam + "Je hebt gekocht");


            if (!spelerA.GaatAanTafelZitten(tafel, 1))
            {
                throw new ArgumentOutOfRangeException("Het plek is niet meer beschikbaar.");
            }
            else if (!spelerB.GaatAanTafelZitten(tafel, 2))
            {
                throw new ArgumentOutOfRangeException("Het plek is niet meer beschikbaar.");
            }

            BlackjackController blackJackController = new BlackjackController(tafel);
            Application.Run(new BlackJack(tafel, blackJackController));
            Application.Exit();
            Console.ReadLine();
        }
    }
}
