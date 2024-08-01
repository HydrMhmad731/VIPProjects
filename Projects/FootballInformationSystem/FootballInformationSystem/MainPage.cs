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
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View view = new View();
            view.Show();
            this.Close();
            MessageBox.Show("Just View, And Don't Touch👊");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginForm loginForm = new loginForm();
            loginForm.Show();
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaareftMarket maareftMarket = new MaareftMarket();
            maareftMarket.Show();
            this.Close();
            MessageBox.Show("Spend as much as you want okk🤳");
        }

        private void ourCoachesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Coaches coaches = new Coaches();
            coaches.Show();
            this.Close();
        }
    }
}
