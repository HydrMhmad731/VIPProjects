using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NutriVitalityy
{
    public partial class AccountCreation : Form
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public AccountCreation()
        {
            InitializeComponent();
            txtPassword.TextChanged += PasswordTextBox_TextChanged;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Authentication authentication = new Authentication();
            authentication.Show();
            this.Hide();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(cbmxRole.Text))
                {
                    MessageBox.Show("Please fill in all the Textbox fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string gender;
                if (rdbMale.Checked)
                {
                    gender = "Male";
                }
                else if (rdbFemale.Checked)
                {
                    gender = "Female";
                }
                else
                {
                    MessageBox.Show("Please Select :A Male Or Female.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MySqlDataAdapter da = new MySqlDataAdapter("insert into users(Firstname,Lastname,Username,Password,Role,Email,Age,Sex) values('" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + cbmxRole.Text + "','" + txtEmail.Text + "','" + txtAge.Text + "','" + gender + "')", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MessageBox.Show("The Account has Been Created👍");
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
            Authentication authentication = new Authentication();
            authentication.Show();
            this.Hide();
        }
        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            int strength = CheckPasswordStrength(password);
            progressBar1.Value = strength;
            UpdateStrengthLabel(strength);
        }
        private int CheckPasswordStrength(string password)
        {
            int strength = 0;

            if (password.Length >= 8)
                strength++;
            if (ContainsCharacterType(password, "abcdefghijklmnopqrstuvwxyz"))
                strength++;
            if (ContainsCharacterType(password, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"))
                strength++;
            if (ContainsCharacterType(password, "1234567890"))
                strength++;
            if (ContainsCharacterType(password, "!@#$%^&*()_+}{:;'?/><.,[]"))
                strength++;

            return strength;
        }
        private bool ContainsCharacterType(string input, string characterSet)
        {
            foreach (char c in input)
            {
                if (characterSet.Contains(c))
                    return true;
            }
            return false;
        }
         private void UpdateStrengthLabel(int strength)
        {
            if (strength <= 2)
                label8.Text = "Weak";
            else if (strength <= 3)
                label8.Text = "Moderate";
            else
                label8.Text = "Strong";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
