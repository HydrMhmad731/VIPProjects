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
    public partial class NutritionPlan : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        private DataGridViewRow selectedRow;
        public NutritionPlan()
        {
            InitializeComponent();
            CustomizeDataGridView();
        }
        void clearAll()
        {
            txtPlanName.Text= "";
            richNDetails.Text = "";
            cbmxDuration.SelectedIndex = -1;
            cbmxPrice.SelectedIndex = -1;

        }
        void viewDataGrid()
        {
            string connectionString = "SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM nutritionplans";
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

        private void NutritionPlan_Load(object sender, EventArgs e)
        {
            viewDataGrid();
        }


        private void btnInsert_Click(object sender, EventArgs e)
        {
           

        }

        private void cbmxPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridPlan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridPlan_SelectionChanged(object sender, EventArgs e)
        {
           
        }

   

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();


            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter something to search");
                return;
            }

            string query = "SELECT * FROM nutritionplans WHERE ";
            query += $"PlanID LIKE '%{searchText}%' OR ";
            query += $"PlanName LIKE '%{searchText}%'";
           

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
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Cells["PlanID"].Value);
                    DeleteRow(id);
                }


                RefreshDataGridView();
            }
            viewDataGrid();
        }
        private void DeleteRow(int id)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                MySqlCommand command = new MySqlCommand("DELETE FROM nutritionplans WHERE PlanID = @Planid", conn);
                command.Parameters.AddWithValue("@Planid", id);
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

                string query = "SELECT * FROM nutritionplans";
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

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\USER\Documents\DataBackup\NutritionPlansBackup.sql";
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
            string path = @"C:\Users\USER\Documents\DataBackup\NutritionPlansBackup.sql";
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

        private void btnInsert_Click_1(object sender, EventArgs e)
        {
            string planName = txtPlanName.Text;
            string nutritionalDetails = richNDetails.Text;
            int duration = Convert.ToInt32(cbmxDuration.SelectedItem);
            decimal price = Convert.ToDecimal(cbmxPrice.SelectedItem);

           
            DialogResult result = MessageBox.Show("Are you sure you want to insert this data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
               
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand();
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO nutritionplans (PlanName, NutritionalDetails, Duration, Price) VALUES (@PlanName, @NutritionalDetails, @Duration, @Price)";
                        command.Parameters.AddWithValue("@PlanName", planName);
                        command.Parameters.AddWithValue("@NutritionalDetails", nutritionalDetails);
                        command.Parameters.AddWithValue("@Duration", duration);
                        command.Parameters.AddWithValue("@Price", price);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Data inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            viewDataGrid();
            clearAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                int planID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PlanID"].Value);

                
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand();
                        command.Connection = connection;

                        
                        string updateQuery = "UPDATE nutritionplans SET ";
                        bool isFirstParameter = true;

                        
                        if (!string.IsNullOrEmpty(txtPlanName.Text))
                        {
                            updateQuery += isFirstParameter ? "" : ", ";
                            updateQuery += "PlanName = @PlanName";
                            command.Parameters.AddWithValue("@PlanName", txtPlanName.Text);
                            isFirstParameter = false;
                        }

                        if (!string.IsNullOrEmpty(richNDetails.Text))
                        {
                            updateQuery += isFirstParameter ? "" : ", ";
                            updateQuery += "NutritionalDetails = @NutritionalDetails";
                            command.Parameters.AddWithValue("@NutritionalDetails", richNDetails.Text);
                            isFirstParameter = false;
                        }

                        if (cbmxDuration.SelectedItem != null)
                        {
                            updateQuery += isFirstParameter ? "" : ", ";
                            updateQuery += "Duration = @Duration";
                            command.Parameters.AddWithValue("@Duration", Convert.ToInt32(cbmxDuration.SelectedItem));
                            isFirstParameter = false;
                        }

                        if (cbmxPrice.SelectedItem != null)
                        {
                            updateQuery += isFirstParameter ? "" : ", ";
                            updateQuery += "Price = @Price";
                            command.Parameters.AddWithValue("@Price", Convert.ToDecimal(cbmxPrice.SelectedItem));
                            isFirstParameter = false;
                        }

                        updateQuery += " WHERE PlanID = @PlanID";
                        command.Parameters.AddWithValue("@PlanID", planID);

                        command.CommandText = updateQuery;
                        command.ExecuteNonQuery();

                        MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            viewDataGrid();
            clearAll();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

               
                txtPlanName.Text = selectedRow.Cells["PlanName"].Value.ToString();
                richNDetails.Text = selectedRow.Cells["NutritionalDetails"].Value.ToString();
                cbmxDuration.SelectedItem = selectedRow.Cells["Duration"].Value;
                cbmxPrice.SelectedItem = selectedRow.Cells["Price"].Value;
            }
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
