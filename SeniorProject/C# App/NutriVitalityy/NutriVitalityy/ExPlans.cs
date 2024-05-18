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
    public partial class ExPlans : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public ExPlans()
        {
            InitializeComponent();
            CustomizeDataGridView();
        }
        void clearAll()
        {
            txtPlanName.Text = "";
            cbmxDuration.SelectedIndex = -1;
            cbmxIntensity.SelectedIndex = -1;
            cbmxPrice.SelectedIndex = -1;
        }
        void viewData()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from exerciseplans", conn);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();


            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter something to search");
                return;
            }

            string query = "SELECT * FROM exerciseplans WHERE ";
            query += $"ExPlanId LIKE '%{searchText}%' OR ";
            query += $"Planname LIKE '%{searchText}%' ";
            


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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select row(s) to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmation message box
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If user confirms
            if (result == DialogResult.Yes)
            {
                // Create MySQL connection
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    try
                    {
                        connection.Open();

                        // Begin a transaction
                        using (MySqlTransaction transaction = connection.BeginTransaction())
                        {
                            foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                            {
                                // Get the ExPlanId of the selected row
                                int exPlanId = Convert.ToInt32(selectedRow.Cells["ExPlanId"].Value);

                                // Delete row from exerciseplans table
                                string deleteExercisePlansQuery = "DELETE FROM exerciseplans WHERE ExPlanId = @ExPlanId";
                                using (MySqlCommand cmd = new MySqlCommand(deleteExercisePlansQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ExPlanId", exPlanId);
                                    cmd.ExecuteNonQuery();
                                }

                                // Delete related rows from patientsexercise table
                                string deletePatientExerciseQuery = "DELETE FROM patientsexercise WHERE ExPlanid = @ExPlanId";
                                using (MySqlCommand cmd = new MySqlCommand(deletePatientExerciseQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ExPlanId", exPlanId);
                                    cmd.ExecuteNonQuery();
                                }

                                // Delete related rows from exerciseinfo table
                                string deleteExerciseInfoQuery = "DELETE FROM exerciseinfo WHERE ExPlanid = @ExPlanId";
                                using (MySqlCommand cmd = new MySqlCommand(deleteExerciseInfoQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ExPlanId", exPlanId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // Commit the transaction
                            transaction.Commit();

                            MessageBox.Show("Selected row(s) deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            viewData();
            clearAll();
        }
       
        private void btnBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to backup the database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
                string path = @"C:\Users\USER\Documents\DataBackup\exerciseplansBackup.sql";

               
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
                string path = @"C:\Users\USER\Documents\DataBackup\exerciseplansBackup.sql";

                
                MySqlCommand cmd = new MySqlCommand();
                MySqlBackup bkp = new MySqlBackup(cmd);

                
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    bkp.ImportFromFile(path);
                    MessageBox.Show("Restore succeeded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            clearAll();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlanName.Text) ||
        string.IsNullOrWhiteSpace(cbmxDuration.Text) ||
        string.IsNullOrWhiteSpace(cbmxIntensity.Text) ||
        string.IsNullOrWhiteSpace(cbmxPrice.Text))
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            DialogResult result = MessageBox.Show("Are you sure you want to insert this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
                
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    try
                    {
                        connection.Open();

                        
                        string query = "INSERT INTO exerciseplans (Planname, Duration, Intensity, Price) VALUES (@Planname, @Duration, @Intensity, @Price)";

                        
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            
                            cmd.Parameters.AddWithValue("@Planname", txtPlanName.Text);
                            cmd.Parameters.AddWithValue("@Duration", cbmxDuration.Text);
                            cmd.Parameters.AddWithValue("@Intensity", cbmxIntensity.Text);
                            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(cbmxPrice.Text));

                            
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Record inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                            txtPlanName.Clear();
                            cbmxDuration.SelectedIndex = -1;
                            cbmxIntensity.SelectedIndex = -1;
                            cbmxPrice.SelectedIndex = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            viewData();
            clearAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected row
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Get the ExPlanId of the selected row
            int exPlanId = Convert.ToInt32(selectedRow.Cells["ExPlanId"].Value);

            // Confirmation message box
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If user confirms
            if (result == DialogResult.Yes)
            {
                // Create MySQL connection
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    try
                    {
                        connection.Open();
                        // Begin a transaction
                        using (MySqlTransaction transaction = connection.BeginTransaction())
                        {
                            // SQL update query for exerciseplans table
                            string updateExercisePlansQuery = "UPDATE exerciseplans SET ";

                            // Check and add fields to update query
                            if (!string.IsNullOrWhiteSpace(txtPlanName.Text))
                            {
                                updateExercisePlansQuery += "Planname = @Planname, ";
                            }
                            if (!string.IsNullOrWhiteSpace(cbmxDuration.Text))
                            {
                                updateExercisePlansQuery += "Duration = @Duration, ";
                            }
                            if (!string.IsNullOrWhiteSpace(cbmxIntensity.Text))
                            {
                                updateExercisePlansQuery += "Intensity = @Intensity, ";
                            }
                            if (!string.IsNullOrWhiteSpace(cbmxPrice.Text))
                            {
                                updateExercisePlansQuery += "Price = @Price, ";
                            }

                            // Remove trailing comma and space
                            updateExercisePlansQuery = updateExercisePlansQuery.TrimEnd(',', ' ');

                            // Add WHERE clause
                            updateExercisePlansQuery += " WHERE ExPlanId = @ExPlanId";

                            // Create command for exerciseplans table
                            using (MySqlCommand cmd = new MySqlCommand(updateExercisePlansQuery, connection, transaction))
                            {
                                // Add parameters for exerciseplans table
                                cmd.Parameters.AddWithValue("@ExPlanId", exPlanId);

                                if (!string.IsNullOrWhiteSpace(txtPlanName.Text))
                                {
                                    cmd.Parameters.AddWithValue("@Planname", txtPlanName.Text);
                                }
                                if (!string.IsNullOrWhiteSpace(cbmxDuration.Text))
                                {
                                    cmd.Parameters.AddWithValue("@Duration", cbmxDuration.Text);
                                }
                                if (!string.IsNullOrWhiteSpace(cbmxIntensity.Text))
                                {
                                    cmd.Parameters.AddWithValue("@Intensity", cbmxIntensity.Text);
                                }
                                if (!string.IsNullOrWhiteSpace(cbmxPrice.Text))
                                {
                                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(cbmxPrice.Text));
                                }

                                // Execute command for exerciseplans table
                                cmd.ExecuteNonQuery();
                            }

                            // Update EndDate in patientsexercise table based on the updated duration
                            if (!string.IsNullOrWhiteSpace(cbmxDuration.Text))
                            {
                                // SQL update query for patientsexercise table
                                string updatePatientExerciseQuery = "UPDATE patientsexercise SET EndDate = DATE_ADD(StartDate, INTERVAL @Duration DAY) WHERE ExPlanId = @ExPlanId";

                                // Create command for patientsexercise table
                                using (MySqlCommand cmd = new MySqlCommand(updatePatientExerciseQuery, connection, transaction))
                                {
                                    // Add parameters for patientsexercise table
                                    cmd.Parameters.AddWithValue("@ExPlanId", exPlanId);
                                    cmd.Parameters.AddWithValue("@Duration", Convert.ToInt32(cbmxDuration.Text));

                                    // Execute command for patientsexercise table
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // Commit the transaction
                            transaction.Commit();

                            MessageBox.Show("Record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            viewData();
            clearAll();
        }

        private void ExPlans_Load(object sender, EventArgs e)
        {
            viewData();
            clearAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewData();
        }
    }
}
