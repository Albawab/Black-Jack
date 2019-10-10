using HenE.GameBlackJack.Interface;
using System;
using System.Windows.Forms;

namespace HenE.WinFormsApp
{
    /// <summary>
    /// Stuur een bericht en krijg antwoord van de speler.
    /// </summary>
    public class WinFormCommunicator : ICommunicate
    {
        private TextBox vraag;
        private TextBox antwoord ;

        public WinFormCommunicator(TextBox vraag, TextBox antwoord)
        {
            this.vraag = vraag;
            this.antwoord = antwoord;
        }

        /// <summary>
        /// Stuur een bericht met een vraag aan de speler en krijg antwoord op die vraag.
        /// </summary>
        /// <param name="vraag">De vraag dit gesteld gaat worden.</param>
        /// <returns>Het antwoord van de vraag.</returns>
        public string Ask(string vraag)
        {
         MessageBox.Show(vraag,"", MessageBoxButtons.OK);
            return Console.ReadLine();
        }

        /// <summary>
        /// Geef een bericht of informatie aan de speler.
        /// </summary>
        /// <param name="message">De melding die verstuurt gaat worden.</param>
        public void Tell(string message)
        {
            Console.WriteLine(message);
        }
    }
}
