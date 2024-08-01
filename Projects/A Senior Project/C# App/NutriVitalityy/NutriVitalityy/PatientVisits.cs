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
    public partial class PatientVisits : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;");

        public PatientVisits()
        {
            InitializeComponent();
            CustomizeDataGridView();
            PopulateCombos();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                PopulateCombos();
            }
        }
        void clearAll()
        {
            cbmxPID.SelectedIndex = -1;
            cbmxAppointment.SelectedIndex = -1;
            richNotes.Text = "";
        }
        private void PopulateCombos()
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
            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
            {
                string query = "SELECT CONCAT('Appointment ID: ', AppointmentId, ' - ', 'Date: ', AppointmentDate) AS DisplayText, AppointmentId FROM appointments";
                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cbmxAppointment.DisplayMember = "DisplayText";
                cbmxAppointment.ValueMember = "AppointmentId";
                cbmxAppointment.DataSource = dt;
            }

        }

        void viewData()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from patientvisits", conn);
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

            string query = "SELECT * FROM patientvisits WHERE ";
            query += $"VisitID LIKE '%{searchText}%' ";
    


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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected visit(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    
                    List<int> selectedVisitIDs = new List<int>();
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        selectedVisitIDs.Add((int)row.Cells["VisitID"].Value);
                    }

                    
                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
                    {
                        connection.Open();
                        foreach (int visitID in selectedVisitIDs)
                        {
                            string query = "DELETE FROM patientvisits WHERE VisitID = @VisitID";
                            MySqlCommand command = new MySqlCommand(query, connection);

                            command.Parameters.AddWithValue("@VisitID", visitID);

                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Selected visit(s) have been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    
                    viewData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting visit(s): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PatientVisits_Load(object sender, EventArgs e)
        {
            viewData();
            clearAll();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to insert this visit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
              
                int patientID = (int)cbmxPID.SelectedValue;
                int appointmentID = (int)cbmxAppointment.SelectedValue;

               
                DateTime visitDateTime = datePickerVisit.Value;
                string visitNotes = richNotes.Text;

                
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
                {
                    string query = "INSERT INTO patientvisits (PatientID, AppointmentID, VisitDateTime, VisitNotes) VALUES (@PatientID, @AppointmentID, @VisitDateTime, @VisitNotes)";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@PatientID", patientID);
                    command.Parameters.AddWithValue("@AppointmentID", appointmentID);
                    command.Parameters.AddWithValue("@VisitDateTime", visitDateTime);
                    command.Parameters.AddWithValue("@VisitNotes", visitNotes);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Visit data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        viewData();
                        clearAll();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while inserting visit data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update the selected visit(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                  
                    List<int> selectedVisitIDs = new List<int>();
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        selectedVisitIDs.Add((int)row.Cells["VisitID"].Value);
                    }

                   
                    

                    
                    int updatedPatientID = (int)cbmxPID.SelectedValue;
                    int updatedAppointmentID = (int)cbmxAppointment.SelectedValue;
                    DateTime updatedVisitDateTime = datePickerVisit.Value;
                    string updatedVisitNotes = richNotes.Text;

                    
                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
                    {
                        connection.Open();
                        foreach (int visitID in selectedVisitIDs)
                        {
                            string query = "UPDATE patientvisits SET PatientID = @PatientID, AppointmentID = @AppointmentID, VisitDateTime = @VisitDateTime, VisitNotes = @VisitNotes WHERE VisitID = @VisitID";
                            MySqlCommand command = new MySqlCommand(query, connection);

                            command.Parameters.AddWithValue("@PatientID", updatedPatientID);
                            command.Parameters.AddWithValue("@AppointmentID", updatedAppointmentID);
                            command.Parameters.AddWithValue("@VisitDateTime", updatedVisitDateTime);
                            command.Parameters.AddWithValue("@VisitNotes", updatedVisitNotes);
                            command.Parameters.AddWithValue("@VisitID", visitID);

                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Selected visit(s) have been updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    viewData();
                    clearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating visit data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to backup the database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                string path = @"C:\Users\USER\Documents\DataBackup\PatientVisitsBackup.sql";


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
                string path = @"C:\Users\USER\Documents\DataBackup\PatientVisitsBackup.sql";


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
    }
}
