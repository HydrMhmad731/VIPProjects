using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballInformationSystem
{
    public partial class MaareftMarket : Form
    {
        public MaareftMarket()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            rtbPurchase.AppendText(" Excellent Choice Indeed, you have such great taste. Thank you very much for bying from us, and have a lovely Nice day.");

        }
    }
}
