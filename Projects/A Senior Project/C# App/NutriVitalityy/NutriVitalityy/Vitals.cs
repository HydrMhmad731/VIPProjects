using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace NutriVitalityy
{
    public partial class Vitals : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah;");
        MySqlDataAdapter da = new MySqlDataAdapter();
        MySqlCommand cmd = new MySqlCommand();
        public Vitals()
        {
            InitializeComponent();
            CustomizeDataGridView();
            FillPatientIDComboBox();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                FillPatientIDComboBox();
            }
        }

        void ClearCombos()
        {

            cbmxPID.SelectedIndex = -1;
            cbmxWeight.SelectedIndex = -1;
            cbmxHeight.SelectedIndex = -1;
            cbmxSystolic.SelectedIndex = -1;
            cbmxDiastolic.SelectedIndex = -1;
            cbmxHeart.SelectedIndex = -1;
            cbmxBody.SelectedIndex = -1;
            cbmxCholesterol.SelectedIndex = -1;
            cbmxGlucose.SelectedIndex = -1;
            cbmxRespiration.SelectedIndex = -1;

        }
        private void FillPatientIDComboBox()
        {
            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
            {
                string query = "SELECT CONCAT(FirstName, ' ', LastName) AS FullName, PatientId FROM patients";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cbmxPID.DisplayMember = "FullName";
                cbmxPID.ValueMember = "PatientId";
                cbmxPID.DataSource = dt;
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

        void viewDataGrid()
        {
            string connectionString = "SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM patientvitals";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Vitals_Load(object sender, EventArgs e)
        {
            ClearCombos();
            viewDataGrid();
        }



        private void txtHeartRate_TextChanged(object sender, EventArgs e)
        {

        }




        private void btnClear_Click(object sender, EventArgs e)
        {
            
        }
       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
        }

        private void grbInfo_Enter(object sender, EventArgs e)
        {

        }


        private void cbmxID_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }
    

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (cbmxPID.SelectedItem == null || cbmxWeight.SelectedItem == null || cbmxHeight.SelectedItem == null ||
       cbmxSystolic.SelectedItem == null || cbmxDiastolic.SelectedItem == null || cbmxHeart.SelectedItem == null ||
       cbmxBody.SelectedItem == null || cbmxCholesterol.SelectedItem == null || cbmxGlucose.SelectedItem == null ||
       cbmxRespiration.SelectedItem == null)
            {
                MessageBox.Show("Please select all fields.");
                return;
            }

            string patientID = cbmxPID.SelectedValue.ToString(); // Use SelectedValue instead of SelectedItem
            string weight = cbmxWeight.SelectedItem.ToString();
            string height = cbmxHeight.SelectedItem.ToString();
            string bloodPressure = cbmxSystolic.SelectedItem.ToString() + "/" + cbmxDiastolic.SelectedItem.ToString();
            string heartRate = cbmxHeart.SelectedItem.ToString();
            string temperature = cbmxBody.SelectedItem.ToString();
            string cholesterol = cbmxCholesterol.SelectedItem.ToString();
            string glucose = cbmxGlucose.SelectedItem.ToString();
            string respiratoryRate = cbmxRespiration.SelectedItem.ToString();

            if (string.IsNullOrEmpty(patientID) || string.IsNullOrEmpty(weight) || string.IsNullOrEmpty(height) ||
                string.IsNullOrEmpty(bloodPressure) || string.IsNullOrEmpty(heartRate) || string.IsNullOrEmpty(temperature) ||
                string.IsNullOrEmpty(cholesterol) || string.IsNullOrEmpty(glucose) || string.IsNullOrEmpty(respiratoryRate))
            {
                MessageBox.Show("Please select all fields.");
                return;
            }

            DialogResult result = MessageBox.Show("Do you wish to insert?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
               

                string query = @"
            INSERT INTO patientvitals (
                PatientId, Weight, Height, BloodPressure, HeartRate, BodyTemperature, Cholesterol, Glucoselevel, RespiratoryRate, DateRecorded
            ) VALUES (
                @PatientID, @Weight, @Height, @BloodPressure, @HeartRate, @BodyTemperature, @Cholesterol, @Glucoselevel, @RespiratoryRate, NOW()
            )";

                using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
                {
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@Height", height);
                    command.Parameters.AddWithValue("@BloodPressure", bloodPressure);
                    command.Parameters.AddWithValue("@HeartRate", heartRate);
                    command.Parameters.AddWithValue("@BodyTemperature", temperature);
                    command.Parameters.AddWithValue("@Cholesterol", cholesterol);
                    command.Parameters.AddWithValue("@Glucoselevel", glucose);
                    command.Parameters.AddWithValue("@RespiratoryRate", respiratoryRate);

                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data inserted successfully!");
                        ClearCombos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

                viewDataGrid();
               
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

            if (cbmxPID.SelectedItem == null)
            {
                MessageBox.Show("Please select a Patient ID.");
                return;
            }

            string patientID = cbmxPID.SelectedValue.ToString(); 
            string weight = cbmxWeight.SelectedItem?.ToString();
            string height = cbmxHeight.SelectedItem?.ToString();
            string systolic = cbmxSystolic.SelectedItem?.ToString();
            string diastolic = cbmxDiastolic.SelectedItem?.ToString();
            string heartRate = cbmxHeart.SelectedItem?.ToString();
            string temperature = cbmxBody.SelectedItem?.ToString();
            string cholesterol = cbmxCholesterol.SelectedItem?.ToString();
            string glucose = cbmxGlucose.SelectedItem?.ToString();
            string respiratoryRate = cbmxRespiration.SelectedItem?.ToString();

            List<string> updates = new List<string>();
            if (!string.IsNullOrEmpty(weight))
                updates.Add("Weight = @Weight");
            if (!string.IsNullOrEmpty(height))
                updates.Add("Height = @Height");
            if (!string.IsNullOrEmpty(systolic) && !string.IsNullOrEmpty(diastolic))
                updates.Add("BloodPressure = CONCAT(@Systolic, '/', @Diastolic)");
            if (!string.IsNullOrEmpty(heartRate))
                updates.Add("HeartRate = @HeartRate");
            if (!string.IsNullOrEmpty(temperature))
                updates.Add("BodyTemperature = @BodyTemperature");
            if (!string.IsNullOrEmpty(cholesterol))
                updates.Add("Cholesterol = @Cholesterol");
            if (!string.IsNullOrEmpty(glucose))
                updates.Add("Glucoselevel = @Glucoselevel");
            if (!string.IsNullOrEmpty(respiratoryRate))
                updates.Add("RespiratoryRate = @RespiratoryRate");

            if (updates.Count == 0)
            {
                MessageBox.Show("Please select at least one field to update.");
                return;
            }

            DialogResult result = MessageBox.Show("Do you wish to update?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string updateFields = string.Join(", ", updates);
                string query = $"UPDATE patientvitals SET {updateFields}, DateRecorded = NOW() WHERE PatientId = @PatientID";

                using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
                {
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@PatientID", patientID);
                    if (!string.IsNullOrEmpty(weight))
                        command.Parameters.AddWithValue("@Weight", weight);
                    if (!string.IsNullOrEmpty(height))
                        command.Parameters.AddWithValue("@Height", height);
                    if (!string.IsNullOrEmpty(systolic) && !string.IsNullOrEmpty(diastolic))
                    {
                        command.Parameters.AddWithValue("@Systolic", systolic);
                        command.Parameters.AddWithValue("@Diastolic", diastolic);
                    }
                    if (!string.IsNullOrEmpty(heartRate))
                        command.Parameters.AddWithValue("@HeartRate", heartRate);
                    if (!string.IsNullOrEmpty(temperature))
                        command.Parameters.AddWithValue("@BodyTemperature", temperature);
                    if (!string.IsNullOrEmpty(cholesterol))
                        command.Parameters.AddWithValue("@Cholesterol", cholesterol);
                    if (!string.IsNullOrEmpty(glucose))
                        command.Parameters.AddWithValue("@Glucoselevel", glucose);
                    if (!string.IsNullOrEmpty(respiratoryRate))
                        command.Parameters.AddWithValue("@RespiratoryRate", respiratoryRate);

                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data updated successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

                viewDataGrid();
                ClearCombos();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select one or more rows to delete.", "Delete Rows", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Do you want to delete the selected rows?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string patientID = row.Cells["PatientId"].Value.ToString();

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
                        {
                            conn.Open();
                            string query = "DELETE FROM patientvitals WHERE PatientId = @PatientID";
                            MySqlCommand command = new MySqlCommand(query, conn);
                            command.Parameters.AddWithValue("@PatientID", patientID);
                            int rowsAffected = command.ExecuteNonQuery();
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting row with Patient ID: " + patientID + "\nError: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                MessageBox.Show("Selected rows deleted successfully.", "Delete Rows", MessageBoxButtons.OK, MessageBoxIcon.Information);
                viewDataGrid(); 
            }
            else
            {
                MessageBox.Show("Deletion canceled.", "Delete Rows", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearCombos();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchCriteria = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchCriteria))
            {
                MessageBox.Show("Please enter a Vital ID or a Date (yyyy) to search.");
                return;
            }

            bool isDateSearch = false;
            DateTime searchDate;
            if (DateTime.TryParseExact(searchCriteria, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out searchDate))
            {
                isDateSearch = true;
            }

            string query = isDateSearch ?
                "SELECT * FROM patientvitals WHERE YEAR(DateRecorded) = @Year" :
                "SELECT * FROM patientvitals WHERE Vitalid = @VitalID";

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                if (isDateSearch)
                {
                    command.Parameters.AddWithValue("@Year", searchDate.Year);
                }
                else
                {
                    command.Parameters.AddWithValue("@VitalID", searchCriteria);
                }

                try
                {
                    conn.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    
                    dataGridView1.DataSource = null;

                    if (reader.HasRows)
                    {
                        
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("No records found with the provided search criteria.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\USER\Documents\DataBackup\VitalsBackup.sql";
            MySqlCommand cmd = new MySqlCommand();
            MySqlBackup bkp = new MySqlBackup(cmd);
            cmd.Connection = conn;

            DialogResult result = MessageBox.Show("Do you want to backup the database?", "Backup Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    bkp.ExportToFile(path);
                    MessageBox.Show("Backup completed successfully!", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                MessageBox.Show("Backup canceled.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\USER\Documents\DataBackup\VitalsBackup.sql";
            MySqlCommand cmd = new MySqlCommand();
            MySqlBackup bkp = new MySqlBackup(cmd);
            cmd.Connection = conn;

            DialogResult result = MessageBox.Show("Do you want to restore the database?", "Restore Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    bkp.ImportFromFile(path);
                    viewDataGrid();
                    MessageBox.Show("Restore completed successfully!", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                MessageBox.Show("Restore canceled.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewDataGrid();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
