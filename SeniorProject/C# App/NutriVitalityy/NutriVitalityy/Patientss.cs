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
    public partial class Patientss : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public Patientss()
        {
            InitializeComponent();
            CustomizeDataGridView();
        }
        void clearAll()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtSearch.Text = "";
            txtContact.Text = "";
            txtAge.Text = "";
            txtAddress.Text = "";
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
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

        void clearTextBoxes()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAge.Text = "";
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
            txtAddress.Text = "";
            txtContact.Text = "";
        }
        void viewDataGrid()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from patients", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtAge.Text) || string.IsNullOrWhiteSpace(txtContact.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
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
                    MessageBox.Show("Please Select a Male Or Female.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string dob = DOB.Value.ToString("yyyy-MM-dd");

                
                DialogResult result = MessageBox.Show("Are you sure you want to create this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    conn.Open();

                    string insertQuery = "INSERT INTO patients (Firstname, Lastname, Age, DateOfBirth, Sex, Address, Email) VALUES (@FirstName, @LastName, @Age, @DOB, @Gender, @Address, @Email)";
                    MySqlCommand command = new MySqlCommand(insertQuery, conn);
                    command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    command.Parameters.AddWithValue("@Age", txtAge.Text);
                    command.Parameters.AddWithValue("@DOB", dob);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Address", txtAddress.Text);
                    command.Parameters.AddWithValue("@Email", txtContact.Text);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("The Account has Been Created👍");

                viewDataGrid();
                clearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a row to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int patientId = Convert.ToInt32(row.Cells["PatientId"].Value);

                    string updateQuery = "UPDATE patients SET ";

                    if (!string.IsNullOrEmpty(txtFirstName.Text))
                        updateQuery += "Firstname = @FirstName, ";
                    if (!string.IsNullOrEmpty(txtLastName.Text))
                        updateQuery += "Lastname = @LastName, ";
                    if (!string.IsNullOrEmpty(txtAddress.Text))
                        updateQuery += "Address = @Address, ";
                    if (!string.IsNullOrEmpty(txtContact.Text))
                        updateQuery += "Email = @Email, ";
                    if (!string.IsNullOrEmpty(txtAge.Text))
                        updateQuery += "Age = @Age, ";

                    
                    updateQuery = updateQuery.TrimEnd(',', ' ');

                    
                    updateQuery += " WHERE Patientid = @PatientId";

                    using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                    {
                        conn.Open();

                        MySqlCommand command = new MySqlCommand(updateQuery, conn);
                        command.Parameters.AddWithValue("@PatientId", patientId);
                        command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        command.Parameters.AddWithValue("@Address", txtAddress.Text);
                        command.Parameters.AddWithValue("@Email", txtContact.Text);
                        command.Parameters.AddWithValue("@Age", txtAge.Text);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Patient info has been updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                viewDataGrid();
                clearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            
                string patientID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            
                var relatedTables = new Dictionary<string, string>
                {
                    {"appointments", "Patientid"},
                    {"healthcarerecords", "Patientid"},
                    {"patientregistration", "PatientID"},
                    {"patientsexercise", "patientId"},
                    {"patientsnutrition", "PatientId"},
                    {"patientvitals", "PatientId"},
                    {"reports", "PatientId"}
                };

                
                DialogResult result = MessageBox.Show("Are you sure you want to delete this patient and associated records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

            
                foreach (var kvp in relatedTables)
                {
                    string deleteRelatedRecords = $"DELETE FROM {kvp.Key} WHERE {kvp.Value} = {patientID}";
                    ExecuteNonQuery(deleteRelatedRecords);
                }

                
                string deletePatient = $"DELETE FROM patients WHERE Patientid = {patientID}";
                ExecuteNonQuery(deletePatient);

                MessageBox.Show("Patient and associated records have been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                viewDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExecuteNonQuery(string query)
        {
            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        private void Patientss_Load(object sender, EventArgs e)
        {
            viewDataGrid();
            clearAll();
           

        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
            
                DialogResult result = MessageBox.Show("Are you sure you want to create a backup of patient data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                string path = @"C:\Users\USER\Documents\DataBackup\PatientsBackup.sql";

                MySqlCommand cmd = new MySqlCommand();
                MySqlBackup bkp = new MySqlBackup(cmd);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    bkp.ExportToFile(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                MessageBox.Show("Backup of patient data has been created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                
                DialogResult result = MessageBox.Show("Are you sure you want to restore patient data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                string path = @"C:\Users\USER\Documents\DataBackup\PatientsBackup.sql";

                MySqlCommand cmd = new MySqlCommand();
                MySqlBackup bkp = new MySqlBackup(cmd);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    bkp.ImportFromFile(path);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                MessageBox.Show("Patient data has been restored successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            viewDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter a search query.");
                return;
            }

            string query = "SELECT * FROM patients WHERE ";
            query += $"Patientid LIKE '%{searchText}%' OR ";
            query += $"Firstname LIKE '%{searchText}%' OR ";
            query += $"Lastname LIKE '%{searchText}%' OR ";
            query += $"Age LIKE '%{searchText}%' OR ";
            query += $"Address LIKE '%{searchText}%' OR ";
            query += $"Email LIKE '%{searchText}%'";

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
        }

        private void panelDataGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbmxId_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewDataGrid();
        }
    }
}
