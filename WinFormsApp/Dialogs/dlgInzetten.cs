using HenE.GameBlackJack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HenE.WinFormsApp.Dialogs
{
    public partial class DlgInzetten : Form
    {
        private readonly Speler speler;
        public DlgInzetten(Speler speler)
        {
            this.speler = speler;
            InitializeComponent();
        }
    }
}
