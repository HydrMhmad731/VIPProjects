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
    public partial class PtExercise : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public PtExercise()
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

            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
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


            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
            {
                string query = "SELECT Planname, ExPlanId FROM exerciseplans";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cbmxPlanId.DisplayMember = "Planname";
                cbmxPlanId.ValueMember = "ExPlanId";
                cbmxPlanId.DataSource = dt;
            }
        }



        void viewData()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from patientsexercise", conn);
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

            string query = "SELECT * FROM patientsexercise WHERE ";
            query += $"ExerciseId LIKE '%{searchText}%' OR ";
            query += $"patientId LIKE '%{searchText}%' OR ";
            query += $"ExPlanid LIKE '%{searchText}%' OR ";


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
                    int id = Convert.ToInt32(row.Cells["ExerciseId"].Value);
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

                MySqlCommand command = new MySqlCommand("DELETE FROM patientsexercise WHERE ExerciseId = @Exerciseid", conn);
                command.Parameters.AddWithValue("@Exerciseid", id);
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

                string query = "SELECT * FROM patientsexercise";
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void PtExercise_Load(object sender, EventArgs e)
        {
            ClearAll();
            viewData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to insert this data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int patientId = Convert.ToInt32(cbmxPID.SelectedValue);
                int ExplanId = Convert.ToInt32(cbmxPlanId.SelectedValue);
                DateTime startDate = dateTimePickerStartDate.Value;


                DateTime endDate;
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    string query = "SELECT Duration FROM exerciseplans WHERE ExPlanId = @ExPlanid";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ExPlanid", ExplanId);
                    connection.Open();
                    int duration = Convert.ToInt32(command.ExecuteScalar());
                    endDate = startDate.AddDays(duration);
                }


                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    string query = "INSERT INTO patientsexercise (patientId, ExPlanid, StartDate, EndDate) VALUES (@patientId, @ExplanId, @StartDate, @EndDate)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@patientId", patientId);
                    command.Parameters.AddWithValue("@ExplanId", ExplanId);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update the selected data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int patientId = Convert.ToInt32(row.Cells["patientId"].Value);
                    int ExplanId = Convert.ToInt32(row.Cells["ExPlanid"].Value);
                    DateTime startDate = dateTimePickerStartDate.Value;

                    DateTime endDate;
                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                    {
                        string query = "SELECT Duration FROM exerciseplans WHERE ExPlanId = @ExPlanid";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ExPlanid", ExplanId);
                        connection.Open();
                        int duration = Convert.ToInt32(command.ExecuteScalar());
                        endDate = startDate.AddDays(duration);
                    }

                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                    {
                        string query = "UPDATE patientsexercise SET StartDate = @StartDate, EndDate = @EndDate WHERE patientId = @patientid AND ExPlanid = @ExPlanId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@patientid", patientId);
                        command.Parameters.AddWithValue("@ExPlanId", ExplanId);
                        connection.Open();
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
                ClearAll();
            }
            else
            {
                MessageBox.Show("Update cancelled.");
            }

        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\USER\Documents\DataBackup\patientsexerciseBackup.sql";

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
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\USER\Documents\DataBackup\patientsexerciseBackup.sql";

            MySqlCommand cmd = new MySqlCommand();
            MySqlBackup bkp = new MySqlBackup(cmd);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                bkp.ImportFromFile(path);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewData();
        }
    }
}
