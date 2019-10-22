using HenE.GameBlackJack;
using HenE.GameBlackJack.SpelSpullen;
using HenE.WinFormsApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HenE.GameBlackJack.Fiche;

namespace WinFormsApp
{
    public partial class BlackJack : Form
    {
        WinFormCommunicator communicator = new WinFormCommunicator();
        TextBox[] textBoxesSpelers = new TextBox[5];
        private Tafel Tafel { get; set; }
        private BlackjackController blackjackController;

        [Obsolete]
        public BlackJack()
        {
            InitializeComponent();
            this.NewGame();
        }

        /// <summary>
        /// Het creat nieuw spel.
        /// </summary>
        private void NewGame()
        {
            // fiches
            // de hoofdbak met fiches
            Fiches cassiereFiches = FicheFactory.CreateFiches(1000);

            // tafel
            this.Tafel = Tafel.CreateBlackJackTafel(cassiereFiches.GeefMeFischesTerWaardeVan(500, 50, true));
            blackjackController = new BlackjackController(this.Tafel, new WinFormCommunicator());
            // is   de waarde vban de fiches nu 500?

            // dealer
            // dealer aanmaken en toewijzen aan een tafel
            Dealer dealer = new Dealer("Dealer");
            dealer.GaAanTafelZitten(Tafel);

            // spelers, komen binnen en kopen bij het cassiere fiches
            Console.WriteLine("Speler: A");
            string naamSpelerA = "Speler1";
            Speler spelerA = new Speler(naamSpelerA);
            if (!spelerA.GaatAanTafelZitten(this.Tafel, 1))
            {
                throw new ArgumentOutOfRangeException("Het plek is niet meer beschikbaar.");
            }
            spelerA.Fiches.Add(cassiereFiches.GeefMeFischesTerWaardeVan(90, 100, true));
            this.AddSpelerBoxToArray();
        }

        /// <summary>
        /// die voeg de speler in een array toe.
        /// </summary>
        /// <param name="sender">Een object.</param>
        /// <param name="e">Een event.</param>
        private void BlackJack_Load(object sender, EventArgs e)
        {


            for (int index = 0; index < Tafel.Plekken.Length - 1; index++)
            {
                if (Tafel.Plekken[index].Speler != null)
                {
                    textBoxesSpelers[index].Text = Tafel.Plekken[index].Speler.Naam;
                }
            }
            /*            blackjackController.Start();*/
        }

        /// <summary>
        /// start het spel.
        /// </summary>
        /// <param name="sender">Een object.</param>
        /// <param name="e">Een event.</param>
        private void Start_Click(object sender, EventArgs e)
        {
            Hand1.Text = "RR";
            Hand1.BackColor = Color.Red;
            //doe de  text box van vraag en antwoord onzichtbaar
            Start.ForeColor = Color.Red;
            //Thread.Sleep(1000);
            Start.Visible = false;
            //Thread.Sleep(1000);
            Hand1.Visible = true;
            Hand1.Text = communicator.Ask(Tafel.Plekken[0].Speler, HenE.GameBlackJack.Enum.Vragen.Inzetten);

            /*            blackjackController.Start();*/
        }

        /// <summary>
        /// Voeg de speler in de array in.
        /// </summary>
        private void AddSpelerBoxToArray()
        {
            textBoxesSpelers[0] = speler1;
            textBoxesSpelers[1] = speler2;
            textBoxesSpelers[2] = speler3;
            textBoxesSpelers[3] = speler4;
            textBoxesSpelers[4] = speler5;
        }

        /// <summary>
        /// Change de naam van de speler.
        /// </summary>
        /// <param name="sender">Een object.</param>
        /// <param name="e">Een event.</param
        private void Speler1_TextChanged(object sender, EventArgs e)
        {
            speler1.Text = speler1.Text;
        }


        /*        public string Ask(string a)
                {

                }*/
    }
}
