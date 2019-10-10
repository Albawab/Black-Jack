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
        TextBox[] textBoxesSpelers = new TextBox[5];
        private Tafel Tafel { get; set; }
        private BlackjackController blackjackController;

        public BlackJack(Tafel tafel, BlackjackController blackjackController)
        {
            this.NewGame();

            this.Tafel = tafel;
            this.blackjackController = blackjackController;
            InitializeComponent();
        }

        private void NewGame()
        {
            // fiches
            // de hoofdbak met fiches
            Fiches cassiereFiches = FicheFactory.CreateFiches(1000);

            // tafel
            this.Tafel = Tafel.CreateBlackJackTafel(cassiereFiches.GeefMeFischesTerWaardeVan(500));

            // is   de waarde vban de fiches nu 500?

            // dealer
            // dealer aanmaken en toewijzen aan een tafel
            Dealer dealer = new Dealer("Dealer");
            dealer.GaAanTafelZitten(tafel);

            new WinFormCommunicator(this.vraag, this.antwoord)
        }

        private void BlackJack_Load(object sender, EventArgs e)
        {
            this.AddSpelerBoxToArray();
            for (int index = 0; index < Tafel.Plekken.Length-1; index++)
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, EventArgs e)
        {
            Start.ForeColor = Color.Red;
            Thread.Sleep(1000);
            Start.Visible = false;
            Thread.Sleep(1000);
            Hand1.Visible = true;
            blackjackController.Start();

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

/*        public string Ask(string a)
        {

        }*/
    }
}
