using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NutriVitalityy
{
    public partial class ExerciseInfo : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public ExerciseInfo()
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
        private void PopulateComboBoxes()
        {
            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                string query = "SELECT Planname, ExPlanId FROM exerciseplans";
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cbmxPlanId.DisplayMember = "Planname";
                    cbmxPlanId.ValueMember = "ExPlanId";
                    cbmxPlanId.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        void clearAll()
        {
            cbmxPlanId.SelectedIndex = -1;
            cbmxMuscleGroup.SelectedIndex = -1;
            cbmxExName.SelectedIndex = -1;
            cbmxEquipment.SelectedIndex = -1;
        }

        void viewData()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from exerciseinfo", conn);
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

            string query = "SELECT * FROM exerciseinfo WHERE ";
            query += $"ExPlanid LIKE '%{searchText}%' OR ";
            query += $"exerciseId LIKE '%{searchText}%' ";



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

           
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
                    {
                        connection.Open();

                        foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                        {
                            int exerciseId = Convert.ToInt32(selectedRow.Cells["exerciseId"].Value);

                            
                            string query = "DELETE FROM exerciseinfo WHERE exerciseId = @ExerciseId";

                            
                            MySqlCommand command = new MySqlCommand(query, connection);

                            
                            command.Parameters.AddWithValue("@ExerciseId", exerciseId);

                            
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Selected row(s) deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string path = @"C:\Users\USER\Documents\DataBackup\ExerciseInfoBackup.sql";


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
                string path = @"C:\Users\USER\Documents\DataBackup\ExerciseInfoBackup.sql";


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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to insert this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

           
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
                    {
                        string query = "INSERT INTO exerciseinfo (ExPlanid, ExName, MuscleGroup, ReqEquipment) VALUES (@ExPlanid, @ExName, @MuscleGroup, @ReqEquipment)";
                        MySqlCommand command = new MySqlCommand(query, connection);

                       
                        int exPlanId = Convert.ToInt32(cbmxPlanId.SelectedValue);
                        string exName = cbmxExName.Text;
                        string muscleGroup = cbmxMuscleGroup.Text;
                        string reqEquipment = cbmxEquipment.Text;

                        
                        command.Parameters.AddWithValue("@ExPlanid", exPlanId);
                        command.Parameters.AddWithValue("@ExName", exName);
                        command.Parameters.AddWithValue("@MuscleGroup", muscleGroup);
                        command.Parameters.AddWithValue("@ReqEquipment", reqEquipment);

                        
                        connection.Open();

                     
                        command.ExecuteNonQuery();

                        
                        connection.Close();

                        MessageBox.Show("Record inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            viewData();
            clearAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select cell(s) to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to update the selected cell(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
                    {
                        connection.Open();

                        foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
                        {
                            int rowIndex = selectedCell.RowIndex;
                            int columnIndex = selectedCell.ColumnIndex;
                            int exerciseId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["exerciseId"].Value);

                            string columnName = dataGridView1.Columns[columnIndex].Name;
                            string newValue = "New Value"; 

                            string query = $"UPDATE exerciseinfo SET {columnName} = @NewValue WHERE exerciseId = @ExerciseId";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@NewValue", newValue);
                            command.Parameters.AddWithValue("@ExerciseId", exerciseId);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Selected cell(s) updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            viewData();
            clearAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void ExerciseInfo_Load(object sender, EventArgs e)
        {
            viewData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewData();
        }
    }
}
