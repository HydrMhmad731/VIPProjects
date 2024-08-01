using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FootballInformationSystem
{
    public partial class SignUp_Page : Form
    {
        public SignUp_Page()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstSkills.SelectedIndex != -1) 
            {
                lstSkills2.Items.Add(lstSkills.SelectedItem);
                lstSkills.Items.Remove(lstSkills.SelectedItem);
                lstSkills.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstSkills2.SelectedIndex != -1)
            {
                lstSkills.Items.Add(lstSkills2.SelectedItem);
                lstSkills2.Items.Remove(lstSkills2.SelectedItems);
                lstSkills2.SelectedIndex = 0;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            loginForm loginForm = new loginForm();
            loginForm.Show();
            this.Close();
            MessageBox.Show("Signing Up Was Done Successfully");
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUp_Page_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string age = txtAge.Text;
            string pose = txtWanted.Text;
            string dreamPlayer = txtDream.Text;

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\USER\\Downloads\\waroooo.mdb";
            OleDbCommand cmd = new OleDbCommand("INSERT INTO waroooo (FirstName, LastName, Age, WantedPosition, DreamPlayer) VALUES (@FirstName, @LastName, @Age, @WantedPosition, @DreamPlayer)", conn);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@WantedPosition", pose);
            cmd.Parameters.AddWithValue("@DreamPlayer", dreamPlayer);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        private void lstPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
