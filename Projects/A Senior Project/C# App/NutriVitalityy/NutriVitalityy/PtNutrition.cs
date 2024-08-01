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
    public partial class PtNutrition : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;");
        public PtNutrition()
        {
            InitializeComponent();
            CustomizeDataGridView();
            PopulateComboBoxes();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                PopulateComboBoxes();
            }
        }
        void ClearAll()
        {
            cbmxPID.SelectedIndex = -1;
            cbmxPlanId.SelectedIndex = -1;
            txtSearch.Text = "";
        }
        private void PopulateComboBoxes()
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

            
            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
            {
                string query = "SELECT PlanName, PlanID FROM nutritionplans";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cbmxPlanId.DisplayMember = "PlanName";
                cbmxPlanId.ValueMember = "PlanID";
                cbmxPlanId.DataSource = dt;
            }
        }



        void viewData()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from patientsnutrition", conn);
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


        private void PtNutrition_Load(object sender, EventArgs e)
        {
            viewData();
            ClearAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();


            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter something to search");
                return;
            }

            string query = "SELECT * FROM patientsnutrition WHERE ";
            query += $"NutritionId LIKE '%{searchText}%' OR ";
            query += $"PatientId LIKE '%{searchText}%' OR ";
            query += $"PlanId LIKE '%{searchText}%' OR ";


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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to insert this data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int patientId = Convert.ToInt32(cbmxPID.SelectedValue);
                int planId = Convert.ToInt32(cbmxPlanId.SelectedValue);
                DateTime startDate = dateTimePickerStartDate.Value;

               
                DateTime endDate;
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
                {
                    string query = "SELECT Duration FROM nutritionplans WHERE PlanID = @PlanID";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PlanID", planId);
                    connection.Open();
                    int duration = Convert.ToInt32(command.ExecuteScalar());
                    endDate = startDate.AddDays(duration);
                }

                
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
                {
                    string query = "INSERT INTO patientsnutrition (PatientId, PlanId, StartDate, EndDate) VALUES (@PatientId, @PlanId, @StartDate, @EndDate)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PatientId", patientId);
                    command.Parameters.AddWithValue("@PlanId", planId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert data.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Insertion cancelled.");
            }
            viewData();
            ClearAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Cells["NutritionId"].Value);
                    DeleteRow(id);
                }


                RefreshDataGridView();
            }
            viewData();
        }
        private void DeleteRow(int id)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                MySqlCommand command = new MySqlCommand("DELETE FROM patientsnutrition WHERE NutritionId = @Nutritionid", conn);
                command.Parameters.AddWithValue("@Nutritionid", id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting row: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void RefreshDataGridView()
        {

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM patientsnutrition";
                MySqlCommand command = new MySqlCommand(query, conn);
                DataTable dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update the selected data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int patientId = Convert.ToInt32(row.Cells["PatientId"].Value);
                        int planId = Convert.ToInt32(row.Cells["PlanId"].Value);
                        DateTime startDate = dateTimePickerStartDate.Value;

                        DateTime endDate;

                      
                        using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
                        {
                            connection.Open();
                            string query = "SELECT Duration FROM nutritionplans WHERE PlanID = @PlanID";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@PlanID", planId);
                            object resultObj = command.ExecuteScalar();
                            if (resultObj != null)
                            {
                                int duration = Convert.ToInt32(resultObj);
                                endDate = startDate.AddDays(duration);
                            }
                            else
                            {
                                MessageBox.Show("No such plan found.");
                                return;
                            }
                        }

                        
                        using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
                        {
                            connection.Open();
                            string updateQuery = "UPDATE patientsnutrition SET StartDate = @StartDate, EndDate = @EndDate WHERE PatientId = @PatientId AND PlanId = @PlanId";
                            MySqlCommand command = new MySqlCommand(updateQuery, connection);
                            command.Parameters.AddWithValue("@StartDate", startDate);
                            command.Parameters.AddWithValue("@EndDate", endDate);
                            command.Parameters.AddWithValue("@PatientId", patientId);
                            command.Parameters.AddWithValue("@PlanId", planId);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show("No data updated. Please make sure the patient and plan exist.");
                            }
                        }
                    }

                   
                    viewData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating the data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Update cancelled.");
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to backup the database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                string path = @"C:\Users\USER\Documents\DataBackup\PtNutritionBackup.sql";


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
                string path = @"C:\Users\USER\Documents\DataBackup\PtNutritionBackup.sql";


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

         private void btnClear_Click(object sender, EventArgs e)
         {
                ClearAll();
         }

         private void btnRefresh_Click(object sender, EventArgs e)
         {
                viewData();
         }
        
    }
}
