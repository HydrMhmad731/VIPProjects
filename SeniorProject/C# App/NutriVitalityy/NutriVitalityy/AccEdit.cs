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
    public partial class AccEdit : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        private bool sidebarExpand = false;
        public AccEdit()
        {
            InitializeComponent();
            txtPassword.TextChanged += PasswordTextBox_TextChanged;
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

                clearTextBoxes();
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }

        private void AccEdit_Load(object sender, EventArgs e)
        {
            viewDataGrid();
        }
        void clearTextBoxes()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAge.Text = "";
            cbmxRole.Text = "";
            txtEmail.Text = "";
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
            txtUsername.Text = "";
            txtPassword.Text = "";
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

                string update = "UPDATE users SET ";

                if (!string.IsNullOrEmpty(txtFirstName.Text))
                    update += "Firstname = '" + txtFirstName.Text + "', ";
                if (!string.IsNullOrEmpty(txtLastName.Text))
                    update += "Lastname = '" + txtLastName.Text + "', ";
                if (!string.IsNullOrEmpty(txtUsername.Text))
                    update += "Username = '" + txtUsername.Text + "', ";
                if (!string.IsNullOrEmpty(txtPassword.Text))
                    update += "Password = '" + txtPassword.Text + "', ";
                if (!string.IsNullOrEmpty(cbmxRole.Text))
                    update += "Role = '" + cbmxRole.Text + "', ";
                if (!string.IsNullOrEmpty(txtEmail.Text))
                    update += "Email = '" + txtEmail.Text + "', ";
                if (!string.IsNullOrEmpty(txtAge.Text))
                    update += "Age = '" + txtAge.Text + "', ";

                update = update.TrimEnd(',', ' ');
                update += " WHERE UID = " + dataGridUpdate.SelectedRows[0].Cells[0].Value;

                MySqlDataAdapter da = new MySqlDataAdapter(update, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MessageBox.Show("The Account has Been Updated👍");

                viewDataGrid();
                clearTextBoxes();
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                string update = " delete from users where UID= " + dataGridUpdate.SelectedRows[0].Cells[0].Value + " ";


                MySqlDataAdapter da = new MySqlDataAdapter(update, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MessageBox.Show("The Account has Been deleted👍");

                viewDataGrid();
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }
    }
}
