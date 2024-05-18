using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using MySql.Data.MySqlClient;

namespace NutriVitalityy
{
    public partial class Authentication : Form
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        private bool isPasswordVisible = false;
        public Authentication()
        {
            InitializeComponent();
            checkPass.CheckedChanged += checkBox1_CheckedChanged;
        }

        private void Authentication_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
         

        }

        private void lblCreateAcc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AccountCreation accountCreation = new AccountCreation();
            accountCreation.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !checkPass.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Missing Username Or Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string username = txtUser.Text;
            string password = txtPass.Text;

            string query = "SELECT Password FROM users WHERE Username = @Username";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    object result = command.ExecuteScalar();
                    conn.Close();

                    if (result != null)
                    {
                        string storedHash = result.ToString();
                        if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                        {
                            Home home = new Home();
                            home.Show();
                            this.Hide();
                            MessageBox.Show("Welcome to NutriVitality👋");
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password. Please try again.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
