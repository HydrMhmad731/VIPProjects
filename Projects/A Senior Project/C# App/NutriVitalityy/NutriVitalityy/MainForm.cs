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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void timerSettingButton_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("dd-MM-yy hh:mm tt");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timerSettingButton.Start();
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
    }
}
