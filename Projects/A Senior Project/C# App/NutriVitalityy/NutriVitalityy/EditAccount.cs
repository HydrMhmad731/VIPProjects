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

namespace NutriVitalityy
{
    public partial class EditAccount : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public EditAccount()
        {
            InitializeComponent();
            txtPassword.TextChanged += PasswordTextBox_TextChanged;
        }

        private void dataGridUpdate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        void viewDataGrid()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from users", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridUpdate.DataSource = ds.Tables[0];
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                string update = " update users set Firstname = '" + txtFirstName.Text + "', Lastname = '" + txtLastName.Text + "', Username = '" + txtUsername.Text + "', Password = '" + txtPassword.Text + "', Role = '" + cbmxRole.Text + "', Email = '" + txtEmail.Text + "', Age = '" + txtAge.Text + "' where UID= " + dataGridUpdate.SelectedRows[0].Cells[0].Value +" ";


                MySqlDataAdapter da = new MySqlDataAdapter(update, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MessageBox.Show("The Account has Been Updated👍");

                viewDataGrid();
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }
    }
}
