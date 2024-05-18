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
    public partial class NutritionInfo : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        
        public NutritionInfo()
        {
            InitializeComponent();
            CustomizeDataGridView();
            FillPlanIDComboBox();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                FillPlanIDComboBox();
            }
        }
        private void FillPlanIDComboBox()
        {
            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
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
        void ClearAll()
        {
            cbmxPlanId.SelectedIndex = -1;
            cbmxServing.SelectedIndex = -1;
            txtFoodname.Text = "";
            txtCalories.Text = "";
            txtProtein.Text = "";
            txtCarbohydrates.Text = "";
            txtSugar.Text = "";
            txtFat.Text = "";
            txtFiber.Text = "";
            richDescription.Text = "";
        }
        void viewDataGrid()
        {
            string connectionString = "SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM nutritionsinfo";
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Cells["FoodId"].Value);
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

                MySqlCommand command = new MySqlCommand("DELETE FROM nutritionsinfo WHERE FoodId = @Foodid", conn);
                command.Parameters.AddWithValue("@Foodid", id);
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

                string query = "SELECT * FROM nutritionsinfo";
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

        private void NutritionInfo_Load(object sender, EventArgs e)
        {
            viewDataGrid();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to insert?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (cbmxPlanId.SelectedItem == null || string.IsNullOrEmpty(txtFoodname.Text) || string.IsNullOrEmpty(txtCalories.Text)
                    || string.IsNullOrEmpty(txtProtein.Text) || string.IsNullOrEmpty(txtCarbohydrates.Text) || string.IsNullOrEmpty(txtFat.Text)
                    || string.IsNullOrEmpty(txtFiber.Text) || string.IsNullOrEmpty(txtSugar.Text)
                    || cbmxServing.SelectedItem == null || string.IsNullOrEmpty(richDescription.Text))
                {
                    MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    try
                    {
                        connection.Open();

                        
                        DataRowView selectedPlan = (DataRowView)cbmxPlanId.SelectedItem;
                        int planId = Convert.ToInt32(selectedPlan["PlanId"]);

                        
                        string foodName = txtFoodname.Text;
                        decimal calories = Convert.ToDecimal(txtCalories.Text);
                        decimal protein = Convert.ToDecimal(txtProtein.Text);
                        decimal carbohydrates = Convert.ToDecimal(txtCarbohydrates.Text);
                        decimal fat = Convert.ToDecimal(txtFat.Text);
                        decimal fiber = Convert.ToDecimal(txtFiber.Text);
                        decimal sugar = Convert.ToDecimal(txtSugar.Text);
                        string servingSize = cbmxServing.SelectedItem.ToString();
                        string description = richDescription.Text;

                        
                        MySqlCommand command = new MySqlCommand();
                        command.Connection = connection;

                        command.CommandText = "INSERT INTO nutritionsinfo (PlanId, FoodName, Calories, Protein, Carbohydrates, Fat, Fiber, Sugar, ServingSize, Description) VALUES (@planId, @foodName, @calories, @protein, @carbohydrates, @fat, @fiber, @sugar, @servingSize, @description)";
                        command.Parameters.AddWithValue("@planId", planId);
                        command.Parameters.AddWithValue("@foodName", foodName);
                        command.Parameters.AddWithValue("@calories", calories);
                        command.Parameters.AddWithValue("@protein", protein);
                        command.Parameters.AddWithValue("@carbohydrates", carbohydrates);
                        command.Parameters.AddWithValue("@fat", fat);
                        command.Parameters.AddWithValue("@fiber", fiber);
                        command.Parameters.AddWithValue("@sugar", sugar);
                        command.Parameters.AddWithValue("@servingSize", servingSize);
                        command.Parameters.AddWithValue("@description", description);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Data inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAll();
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

                viewDataGrid();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    try
                    {
                        connection.Open();

                        int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                        int foodId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["FoodId"].Value);

                        string updateSql = "UPDATE nutritionsinfo SET ";
                        List<MySqlParameter> parameters = new List<MySqlParameter>();

                        if (!string.IsNullOrWhiteSpace(txtFoodname.Text))
                        {
                            updateSql += "FoodName = @foodName, ";
                            parameters.Add(new MySqlParameter("@foodName", txtFoodname.Text));
                        }

                        if (!string.IsNullOrWhiteSpace(txtCalories.Text))
                        {
                            updateSql += "Calories = @calories, ";
                            parameters.Add(new MySqlParameter("@calories", Convert.ToDecimal(txtCalories.Text)));
                        }

                        if (!string.IsNullOrWhiteSpace(txtProtein.Text))
                        {
                            updateSql += "Protein = @protein, ";
                            parameters.Add(new MySqlParameter("@protein", Convert.ToDecimal(txtProtein.Text)));
                        }

                        if (!string.IsNullOrWhiteSpace(txtCarbohydrates.Text))
                        {
                            updateSql += "Carbohydrates = @carbohydrates, ";
                            parameters.Add(new MySqlParameter("@carbohydrates", Convert.ToDecimal(txtCarbohydrates.Text)));
                        }

                        if (!string.IsNullOrWhiteSpace(txtFat.Text))
                        {
                            updateSql += "Fat = @fat, ";
                            parameters.Add(new MySqlParameter("@fat", Convert.ToDecimal(txtFat.Text)));
                        }

                        if (!string.IsNullOrWhiteSpace(txtFiber.Text))
                        {
                            updateSql += "Fiber = @fiber, ";
                            parameters.Add(new MySqlParameter("@fiber", Convert.ToDecimal(txtFiber.Text)));
                        }

                        if (!string.IsNullOrWhiteSpace(txtSugar.Text))
                        {
                            updateSql += "Sugar = @sugar, ";
                            parameters.Add(new MySqlParameter("@sugar", Convert.ToDecimal(txtSugar.Text)));
                        }

                        if (cbmxServing.SelectedItem != null)
                        {
                            updateSql += "ServingSize = @servingSize, ";
                            parameters.Add(new MySqlParameter("@servingSize", cbmxServing.SelectedItem.ToString()));
                        }

                        if (!string.IsNullOrWhiteSpace(richDescription.Text))
                        {
                            updateSql += "Description = @description, ";
                            parameters.Add(new MySqlParameter("@description", richDescription.Text));
                        }


                     

                        updateSql = updateSql.TrimEnd(',', ' ');

                        updateSql += " WHERE FoodId = @foodId";
                        parameters.Add(new MySqlParameter("@foodId", foodId));

                        MySqlCommand command = new MySqlCommand();
                        command.Connection = connection;

                        command.CommandText = updateSql;
                        command.Parameters.AddRange(parameters.ToArray());
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

                viewDataGrid();
                ClearAll();

            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to perform a backup?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string path = @"C:\Users\USER\Documents\DataBackup\nutritionsinfoBackup.sql";

                MySqlCommand cmd = new MySqlCommand();
                MySqlBackup bkp = new MySqlBackup(cmd);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    bkp.ExportToFile(path);
                    MessageBox.Show("Backup completed successfully!", "Backup Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DialogResult result = MessageBox.Show("Are you sure you want to perform a restore?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string path = @"C:\Users\USER\Documents\DataBackup\nutritionsinfoBackup.sql";

                MySqlCommand cmd = new MySqlCommand();
                MySqlBackup bkp = new MySqlBackup(cmd);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    bkp.ImportFromFile(path);
                    MessageBox.Show("Restore completed successfully!", "Restore Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    viewDataGrid();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();


            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter something to search");
                return;
            }

            string query = "SELECT * FROM nutritionsinfo WHERE ";
            query += $"FoodId LIKE '%{searchText}%' OR ";
            query += $"PlanId LIKE '%{searchText}%'";


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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewDataGrid();
        }
    }
}
