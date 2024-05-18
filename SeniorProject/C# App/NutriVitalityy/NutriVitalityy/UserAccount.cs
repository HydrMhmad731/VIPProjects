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
using Org.BouncyCastle.Utilities.Encoders;

namespace NutriVitalityy
{
    public partial class UserAccount : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public UserAccount()
        {
            InitializeComponent();
            CustomizeDataGridView();
        }

        public void UpdateExistingPasswords()
        {
            using (var connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
            {
                connection.Open();
                string selectQuery = "SELECT UID, Password FROM users";
                using (var selectCommand = new MySqlCommand(selectQuery, connection))
                {
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string plaintextPassword = reader.GetString(1);
                            string passwordHash = BCrypt.Net.BCrypt.HashPassword(plaintextPassword);

                            UpdateUserPassword(userId, passwordHash);
                        }
                    }
                }
            }

            MessageBox.Show("All passwords have been hashed and updated.");
        }

        private void UpdateUserPassword(int userId, string passwordHash)
        {
            using (var connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
            {
                connection.Open();
                string updateQuery = "UPDATE users SET Password = @Password WHERE UID = @userId";
                using (var updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Password", passwordHash);
                    updateCommand.Parameters.AddWithValue("@userId", userId);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

         void clearALL()
         {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAge.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            cbmxRole.SelectedIndex = -1;
            txtSearch.Text = "";
         }
        void viewData()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from users", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }
        private void CustomizeDataGridView()
        {
            dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridView1.GridColor = System.Drawing.Color.OliveDrab;
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Berlin Sans FB", 15);
            dataGridView1.DefaultCellStyle.ForeColor = System.Drawing.Color.YellowGreen;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.YellowGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Berlin Sans FB", 16);

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.Padding = new Padding(0);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void UserAccount_Load(object sender, EventArgs e)
        {
            viewData();
            clearALL();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();


            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter something to search");
                return;
            }

            string query = "SELECT * FROM users WHERE ";
            query += $"UID LIKE '%{searchText}%' OR ";
            query += $"Firstname LIKE '%{searchText}%' OR ";
            query += $"Lastname LIKE '%{searchText}%' ";



            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            try
            {
                conn.Open();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            clearALL();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete selected rows?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int uid = Convert.ToInt32(row.Cells["UID"].Value); 
                    DeleteUser(uid);
                    DeleteAppointments(uid);
                    dataGridView1.Rows.Remove(row);
                }
                MessageBox.Show("Account deleted Successfully");
            }
        }
        private void DeleteUser(int uid)
        {
            string query = "DELETE FROM users WHERE UID = @UID";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@UID", uid);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void DeleteAppointments(int uid)
        {
            string query = "DELETE FROM appointments WHERE Doctorid = @UID";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@UID", uid);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting appointments: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtAge.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Please fill in all the Textbox fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string gender = "";
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
                    MessageBox.Show("Please Select a Male Or Female.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string dob = DOB.Value.ToString("yyyy-MM-dd");
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);

                DialogResult result = MessageBox.Show("Are you sure you want to insert the data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                    {
                        conn.Open();

                        string insertQuery = "INSERT INTO users (Firstname, Lastname, Age, DateOfBirth, Sex, Username, Password, Role, Email) VALUES (@FirstName, @LastName, @Age, @DOB, @Gender, @Username, @Password, @Role, @Email)";
                        MySqlCommand command = new MySqlCommand(insertQuery, conn);
                        command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        command.Parameters.AddWithValue("@Age", txtAge.Text);
                        command.Parameters.AddWithValue("@DOB", dob);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Username", txtUsername.Text);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@Role", cbmxRole.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("The Account has Been Created👍");
                }
                else
                {
                    MessageBox.Show("Operation Cancelled", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            viewData();
            clearALL();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select at least one row to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to update the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
                    {
                        conn.Open();
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            string updateQuery = "UPDATE users SET ";
                            List<string> updateFields = new List<string>();

                            if (!string.IsNullOrWhiteSpace(txtFirstName.Text))
                                updateFields.Add("Firstname = @FirstName");
                            if (!string.IsNullOrWhiteSpace(txtLastName.Text))
                                updateFields.Add("Lastname = @LastName");
                            if (!string.IsNullOrWhiteSpace(txtAge.Text))
                                updateFields.Add("Age = @Age");
                            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                                updateFields.Add("Email = @Email");
                            if (!string.IsNullOrWhiteSpace(txtUsername.Text))
                                updateFields.Add("Username = @Username");
                            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                            {
                                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);
                                updateFields.Add("Password = @Password");
                            }
                            if (cbmxRole.SelectedItem != null)
                                updateFields.Add("Role = @Role");

                            if (updateFields.Count == 0)
                            {
                                MessageBox.Show("Please fill in at least one field to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            updateQuery += string.Join(", ", updateFields);
                            updateQuery += " WHERE UID = @UID";

                            using (MySqlCommand command = new MySqlCommand(updateQuery, conn))
                            {
                                command.Parameters.AddWithValue("@UID", row.Cells["UID"].Value);
                                command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                                command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                                command.Parameters.AddWithValue("@Age", txtAge.Text);
                                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                                command.Parameters.AddWithValue("@Username", txtUsername.Text);
                                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                                {
                                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);
                                    command.Parameters.AddWithValue("@Password", hashedPassword);
                                }
                                command.Parameters.AddWithValue("@Role", cbmxRole.SelectedItem.ToString());

                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Selected row(s) have been updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Operation Cancelled", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            viewData();
            clearALL();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearALL();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to backup the database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                string path = @"C:\Users\USER\Documents\DataBackup\UserAccountsBackup.sql";


                MySqlCommand cmd = new MySqlCommand();
                MySqlBackup bkp = new MySqlBackup(cmd);


                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    bkp.ExportToFile(path);
                    MessageBox.Show("Backup succeeded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to restore the database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                string path = @"C:\Users\USER\Documents\DataBackup\UserAccountsBackup.sql";


                MySqlCommand cmd = new MySqlCommand();
                MySqlBackup bkp = new MySqlBackup(cmd);


                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    bkp.ImportFromFile(path);
                    MessageBox.Show("Restore succeeded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    viewData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void DOB_ValueChanged(object sender, EventArgs e)
        {
            DateTime dob = DOB.Value;
            int age = CalculateAge(dob, DateTime.Today);
            txtAge.Text = age.ToString();
        }
        private int CalculateAge(DateTime dateOfBirth, DateTime currentDate)
        {
            int age = currentDate.Year - dateOfBirth.Year;

            if (currentDate.Month < dateOfBirth.Month || (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewData();
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            UpdateExistingPasswords();
        }
    }
}
