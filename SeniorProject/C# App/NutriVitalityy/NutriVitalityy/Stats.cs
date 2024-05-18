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
    public partial class Stats : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        public Stats()
        {
            InitializeComponent();
            CustomizeDataGridView();
        }

            private void ViewAllAppointments()
            {
                try
                {
                    string query = "SELECT * FROM appointments";

                    DataTable dt = new DataTable();

                    using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
                    {
                        conn.Open();

                        using (MySqlCommand command = new MySqlCommand(query, conn))
                        {
                            MySqlDataAdapter da = new MySqlDataAdapter(command);
                            da.Fill(dt); // Fill the DataTable directly
                            dataGridView1.DataSource = dt;
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        lblAppointments.Text = $"There are {dt.Rows.Count} appointment(s) in total.";
                    }
                    else
                    {
                        lblAppointments.Text = "There are no appointments.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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


        private void panelPatients_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timerSettingButton_Tick(object sender, EventArgs e)
        {
            
        }

        private void Stats_Load(object sender, EventArgs e)
        {
            
            try
            {
                int patientCount = GetPatientCountFromDatabase();


                lblPatients.Text = "Number of Patients: " + patientCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                int userCount = GetUsersCountFromDatabase();


                lblUsers.Text = "Number of Users: " + userCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {

                int appointmentCount = GetAppointmentCountFromDatabase();

                lblAppiontment.Text = "Number of Appointments: " + appointmentCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ViewAllAppointments();
        }
        private int GetPatientCountFromDatabase()
        {
            int count = 0;

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM patients";
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }

        private int GetUsersCountFromDatabase()
        {
            int count = 0;

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM users";
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }

        private int GetAppointmentCountFromDatabase()
        {
            int count = 0;

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=gardian731;"))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM appointments";
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return count;
        }

        private void lblAppointments_Click(object sender, EventArgs e)
        {

        }

        private void PanelUsers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
