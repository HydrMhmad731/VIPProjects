using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NutriVitalityy
{
    public partial class Appointments : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public Appointments()
        {
            InitializeComponent();
            CustomizeDataGridView();
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
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                PopulatePatientsComboBox();
            }
        }
        void clearAll()
        {
            cbmxPatients.SelectedIndex = -1;
            cbmxUser.SelectedIndex = -1;
            cbmxAppointment.SelectedIndex = -1;
            richTextNote.Text = "";
        }


        void viewData()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from appointments", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }
        private void PopulatePatientsComboBox()
        {
            cbmxPatients.Items.Clear();

            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                connection.Open();
                string qquery = "SELECT Firstname, Lastname FROM patients";
                using (MySqlCommand command = new MySqlCommand(qquery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Firstname = reader["Firstname"].ToString();
                            string Lastname = reader["Lastname"].ToString();
                            cbmxPatients.Items.Add($"{Firstname} {Lastname}");
                        }
                    }
                }
            }
        }
        private void PopulateUsersComboBox()
        {
            cbmxUser.Items.Clear();

            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                connection.Open();
                string qquery = "SELECT Firstname, Lastname FROM users";
                using (MySqlCommand command = new MySqlCommand(qquery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Firstname = reader["Firstname"].ToString();
                            string Lastname = reader["Lastname"].ToString();
                            cbmxUser.Items.Add($"{Firstname} {Lastname}");
                        }
                    }
                }
            }
        }
       


       
        private int GetPatientId(string Firstname, string Lastname)
        {
            int patientId = 0;
            string query = "SELECT Patientid FROM patients WHERE Firstname = @Firstname AND Lastname = @Lastname";

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                conn.Open();
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Firstname", Firstname);
                    command.Parameters.AddWithValue("@Lastname", Lastname);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        patientId = Convert.ToInt32(result);
                    }
                }
            }

            return patientId;
        }

        private int GetUserId(string Firstname, string Lastname)
        {
            int userId = 0;
            string query = "SELECT UID FROM users WHERE Firstname = @Firstname AND Lastname = @Lastname";

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                conn.Open();
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Firstname", Firstname);
                    command.Parameters.AddWithValue("@Lastname", Lastname);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }

            return userId;
        }



        private void Appointments_Load(object sender, EventArgs e)
        {
            PopulatePatientsComboBox();
            PopulateUsersComboBox();
       
            viewData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbmxPatients.SelectedItem == null || cbmxUser.SelectedItem == null || cbmxAppointment.SelectedItem == null)
            {
                MessageBox.Show("Please select patient, user, and appointment type.");
                return;
            }

            string selectedPatientName = cbmxPatients.SelectedItem.ToString();
            string selectedUserName = cbmxUser.SelectedItem.ToString();
            string selectedAppointmentType = cbmxAppointment.SelectedItem.ToString();

            Console.WriteLine($"Selected Patient Name: {selectedPatientName}");
            Console.WriteLine($"Selected User Name: {selectedUserName}");
            Console.WriteLine($"Selected Appointment Type: {selectedAppointmentType}");

            int patientId = GetSelectedPatientId(selectedPatientName);
            int userId = GetSelectedUserId(selectedUserName);
            string appointment = selectedAppointmentType;
            DateTime appointmentDate = dateTimePicker1.Value;
            string notes = richTextNote.Text;

            Console.WriteLine($"Retrieved Patient ID: {patientId}");
            Console.WriteLine($"Retrieved User ID: {userId}");


            if (patientId == 0 || userId == 0)
            {
                MessageBox.Show("Invalid patient or user ID. Please check your selection.");
                return;
            }


            DialogResult result = MessageBox.Show("Do you want to insert?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string query = "INSERT INTO appointments (Patientid, Doctorid, AppointmentDate, AppointmentType, Notes) VALUES (@Patientid, @Doctorid, @AppointmentDate, @AppointmentType, @Notes)";

                using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Patientid", patientId);
                        command.Parameters.AddWithValue("@Doctorid", userId);
                        command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                        command.Parameters.AddWithValue("@AppointmentType", appointment);
                        command.Parameters.AddWithValue("@Notes", notes);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Data inserted successfully!");
                    }
                }
                viewData();
                clearAll();
            }
        }
        private int GetSelectedPatientId(string selectedPatientName)
        {
            string[] names = selectedPatientName.Split(' ');
            string Firstname = names[0];
            string Lastname = names[1];

            return GetPatientId(Firstname, Lastname);
        }

        private int GetSelectedUserId(string selectedUserName)
        {
            string[] names = selectedUserName.Split(' ');
            string Firstname = names[0];
            string Lastname = names[1];

            return GetUserId(Firstname, Lastname);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            bool isUpdating = !string.IsNullOrEmpty(cbmxPatients.Text) || !string.IsNullOrEmpty(cbmxUser.Text) || !string.IsNullOrEmpty(cbmxAppointment.Text) || !string.IsNullOrEmpty(richTextNote.Text);

            if (!isUpdating)
            {
                int appointmentId = Convert.ToInt32(selectedRow.Cells["AppointmentID"].Value);
                string patientName = GetPatientName(Convert.ToInt32(selectedRow.Cells["PatientID"].Value));
                string userName = GetUserName(Convert.ToInt32(selectedRow.Cells["DoctorID"].Value));
                DateTime appointmentDate = Convert.ToDateTime(selectedRow.Cells["AppointmentDate"].Value);
                string appointmentType = selectedRow.Cells["AppointmentType"].Value.ToString();
                string notes = selectedRow.Cells["Notes"].Value.ToString();

                cbmxPatients.SelectedItem = patientName;
                cbmxUser.SelectedItem = userName;
                cbmxAppointment.SelectedItem = appointmentType;
                dateTimePicker1.Value = appointmentDate;
                richTextNote.Text = notes;

                btnUpdate.Text = "Confirm Update";
            }
            else
            {
                int appointmentId = Convert.ToInt32(selectedRow.Cells["AppointmentID"].Value);
                int? patientId = null;
                int? userId = null;
                DateTime? appointmentDate = null;
                string appointmentType = null;
                string notes = null;

                if (cbmxPatients.SelectedItem != null)
                {
                    string selectedPatientName = cbmxPatients.SelectedItem.ToString();
                    patientId = GetSelectedPatientId(selectedPatientName);
                }

                if (cbmxUser.SelectedItem != null)
                {
                    string selectedUserName = cbmxUser.SelectedItem.ToString();
                    userId = GetSelectedUserId(selectedUserName);
                }

                if (!string.IsNullOrEmpty(cbmxAppointment.Text))
                {
                    appointmentType = cbmxAppointment.SelectedItem.ToString();
                }

                if (dateTimePicker1.Value != null)
                {
                    appointmentDate = dateTimePicker1.Value;
                }

                if (!string.IsNullOrEmpty(richTextNote.Text))
                {
                    notes = richTextNote.Text;
                }

                using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        List<string> setClauses = new List<string>();

                        if (patientId.HasValue)
                        {
                            setClauses.Add("Patientid = @Patientid");
                            command.Parameters.AddWithValue("@Patientid", patientId.Value);
                        }

                        if (userId.HasValue)
                        {
                            setClauses.Add("Doctorid = @Doctorid");
                            command.Parameters.AddWithValue("@Doctorid", userId.Value);
                        }

                        if (appointmentDate.HasValue)
                        {
                            setClauses.Add("AppointmentDate = @AppointmentDate");
                            command.Parameters.AddWithValue("@AppointmentDate", appointmentDate.Value);
                        }

                        if (!string.IsNullOrEmpty(appointmentType))
                        {
                            setClauses.Add("AppointmentType = @AppointmentType");
                            command.Parameters.AddWithValue("@AppointmentType", appointmentType);
                        }

                        if (!string.IsNullOrEmpty(notes))
                        {
                            setClauses.Add("Notes = @Notes");
                            command.Parameters.AddWithValue("@Notes", notes);
                        }

                        if (setClauses.Count > 0)
                        {
                            string query = $"UPDATE appointments SET {string.Join(", ", setClauses)} WHERE AppointmentID = @AppointmentID";
                            command.CommandText = query;
                            command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Appointment updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No fields were changed. Please update at least one field.");
                        }
                    }
                }

                viewData();
                clearAll();

                btnUpdate.Text = "Update";
            }
        }
        private string GetPatientName(int patientId)
        {
            string query = "SELECT CONCAT(Firstname, ' ', Lastname) AS FullName FROM patients WHERE Patientid = @Patientid";

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                conn.Open();
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Patientid", patientId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }

            return string.Empty;
        }

        private string GetUserName(int userId)
        {
           
            string query = "SELECT CONCAT(Firstname, ' ', Lastname) AS FullName FROM users WHERE UID = @UserId";

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                conn.Open();
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }

            return string.Empty;
        }

    

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            int appointmentId = Convert.ToInt32(selectedRow.Cells["AppointmentID"].Value);

            
            DialogResult result = MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                
                DeleteAppointment(appointmentId);
            }
        }

        private void DeleteAppointment(int appointmentId)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM appointments WHERE AppointmentID = @AppointmentID";

                using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Appointment deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete appointment. Please try again.");
                        }
                    }
                }

                viewData();
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

            string query = "SELECT * FROM appointments WHERE ";
            query += $"AppointmentId LIKE '%{searchText}%'";
          

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

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\USER\Documents\DataBackup\AppointmentsBackup.sql";
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
            string path = @"C:\Users\USER\Documents\DataBackup\AppointmentsBackup.sql";
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
                    viewData(); 
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
            viewData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
           
        }
    }
}
