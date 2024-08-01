using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballInformationSystem
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "admin" & txtPassword.Text == "admin") 
            {
                MainPage mainPage = new MainPage();
                mainPage.Show();
                this.Hide();
                MessageBox.Show("Alohaa, welcome to our system😘❤");
            }
            else
            {
                MessageBox.Show("Invalid credentials. Please try again.");
            }




        }

        private void lblSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp_Page signupPage = new SignUp_Page();
            signupPage.Show();
            this.Hide();
            MessageBox.Show("Please Dont Forget Any Box");
        }   
    }
}
