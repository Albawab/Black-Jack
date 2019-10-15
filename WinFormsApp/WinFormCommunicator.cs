using HenE.GameBlackJack;
using HenE.GameBlackJack.Enum;
using HenE.GameBlackJack.Interface;
using HenE.GameBlackJack.SpelSpullen;
using HenE.WinFormsApp.Dialogs;
using System;
using System.Windows.Forms;

namespace HenE.WinFormsApp
{
    /// <summary>
    /// Stuur een bericht en krijg antwoord van de speler.
    /// </summary>
    public class WinFormCommunicator : ICommunicate
    {
        public WinFormCommunicator()
        {
        }

        /// <summary>
        /// Stuur een bericht met een vraag aan de speler en krijg antwoord op die vraag.
        /// </summary>
        /// <param name="ask">De vraag dit gesteld gaat worden.</param>
        /// <returns>Het antwoord van de vraag.</returns>
        public string Ask(Speler speler, Vragen vraag, string info = null)
        {
            switch (vraag)
            {
                case Vragen.Inzetten:
                    return StelInzettenVraag(speler, info);
                default:
                    throw new NotImplementedException("Deze vraag wordt niet ondersteund.");
            }
        }

        public bool AskFichesInzetten(SpelerHand hand, int minWaarde)
        {
            throw new NotImplementedException();
        }

        public bool AskFichesInzetten(SpelerHand hand, out int minWaarde)
        {
            throw new NotImplementedException();
        }

        public bool AskFichesKopen(SpelerHand hand)
        {
            throw new NotImplementedException();
        }

        public bool AskFichesKopen(SpelerHand hand, Vragen vragen)
        {
            throw new NotImplementedException();
        }

        public bool AskWhichAction(SpelerHand hand)
        {
            throw new NotImplementedException();
        }

        public bool AskWhichAction(SpelerHand hand, Vragen vragen)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Geef een bericht of informatie aan de speler.
        /// </summary>
        /// <param name="message">De melding die verstuurt gaat worden.</param>
        public void Tell(Speler speler, Meldingen melding, string message = null)
        {
            switch (melding)
            {
                case Meldingen.GeenActie:
                    MessageBox.Show(message, "", MessageBoxButtons.OK);
                    break;
            }
        }

        public void TellHand(SpelerHand hand, Meldingen melding)
        {
            throw new NotImplementedException();
        }

        public void TellPlayer(Speler speler, Meldingen melding)
        {
            throw new NotImplementedException();
        }

        protected string StelInzettenVraag(Speler speler, string info)
        {
            DlgInzetten dlg = new DlgInzetten(speler);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // kan ik dit inzetten? 
                // nu moet ik het anwtoord teruggeven of 
            }

            return string.Empty;
        }
    }
}
