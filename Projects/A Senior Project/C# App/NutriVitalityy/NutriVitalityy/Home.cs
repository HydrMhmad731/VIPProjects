using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NutriVitalityy
{
    public partial class Home : Form
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;");
        private bool sidebarExpand = false;
        private bool isCollapsed = false;
        private Panel contentPanel;
        
        public Home()
        {
            InitializeComponent();
            this.FormClosing += Home_FormClosing;

        }
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public void loadform(object Form)
        {
            
        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void timerSidebar_Tick(object sender, EventArgs e)
        {

           
          
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
           
        }

        private void timerSideButtons_Tick(object sender, EventArgs e)
        {
            const int step = 10;
            if (isCollapsed)
            {
                if (panelProfiles.Height < panelProfiles.MaximumSize.Height)
                {
                    panelProfiles.Height += step;
                }
                else
                {
                    timerSideButtons.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                if (panelProfiles.Height > panelProfiles.MinimumSize.Height)
                {
                    panelProfiles.Height -= step;
                }
                else
                {
                    timerSideButtons.Stop();
                    isCollapsed = true;
                }
            }
           
        }

        private void btnProfiles_Click(object sender, EventArgs e)
        {
       
        }

        private void MenuButton_Click_1(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
       
        }

        private void timerSettingButton_Tick(object sender, EventArgs e)
        {
            
        }

        private void btnEditAcc_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
          
        }

        private void Home_Load(object sender, EventArgs e)
        {
            timer1.Start();
            panelProfiles.Height = panelProfiles.MinimumSize.Height;
            panelSuppliers.Height = panelSuppliers.MinimumSize.Height;
            panelExercise.Height = panelExercise.MinimumSize.Height;
            panelAppointment.Height = panelAppointment.MinimumSize.Height;
            grbPassword.Visible = false;

            stats1.Show();
            patientVisits1.Hide();
            patientss1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
        }


        private void btnPatients_Click(object sender, EventArgs e)
        {
            if (timerSideButtons.Enabled)
            {
                timerSideButtons.Stop();
            }
            else
            {
                panelSuppliers.Height = panelSuppliers.MinimumSize.Height;
                timerSideButtons.Start();
            }
           
        }

        private void lblpatients_Click(object sender, EventArgs e)
        {

        }

        private void lblSuppliers_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            patientss1.Show();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();

        }

        private void btnVitals_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Show();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnHRecords_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Show();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Show();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stats1.Show();
            stats1.BringToFront();
            patientss1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            timerPlanBtn.Start();
            panelProfiles.Height = panelProfiles.MinimumSize.Height;
            
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            timerAppointments.Start();
        }

        private void timerPlanBtn_Tick(object sender, EventArgs e)
        {
            const int step = 10;
            if (isCollapsed)
            {
                if (panelSuppliers.Height < panelSuppliers.MaximumSize.Height)
                {
                    panelSuppliers.Height += step;
                }
                else
                {
                    timerPlanBtn.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                if (panelSuppliers.Height > panelSuppliers.MinimumSize.Height)
                {
                    panelSuppliers.Height -= step;
                }
                else
                {
                    timerPlanBtn.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("dd-MM-yy hh:mm tt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Authentication authentication = new Authentication();
            authentication.Show();
            this.Hide();
        }

        private void btnGroupSet_Click(object sender, EventArgs e)
        {
            grbPassword.Visible = true;
        }

        private void btnNotify_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("mailto:") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to email" + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            grbSettings.Visible = false;
        }

        private void lblClock_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Show();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnPNutrition_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Show();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void timerExercise_Tick(object sender, EventArgs e)
        {
            const int step = 10;
            if (isCollapsed)
            {
                if (panelExercise.Height < panelExercise.MaximumSize.Height)
                {
                    panelExercise.Height += step;
                }
                else
                {
                    timerExercise.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                if (panelExercise.Height > panelExercise.MinimumSize.Height)
                {
                    panelExercise.Height -= step;
                }
                else
                {
                    timerExercise.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void btnExercise_Click(object sender, EventArgs e)
        {
            timerExercise.Start();
            panelProfiles.Height = panelProfiles.MinimumSize.Height;
            panelSuppliers.Height = panelSuppliers.MinimumSize.Height;
            panelAppointment.Height = panelAppointment.MinimumSize.Height;
        }

        private void btnPExercise_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Show();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnExPlans_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Show();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to backup the database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

           
            if (result == DialogResult.Yes)
            {
                try
                {
                    string path = @"C:\Users\USER\Documents\DataBackup\SystemBackUp.sql";

                    
                    MySqlCommand cmd = new MySqlCommand();
                    MySqlBackup bkp = new MySqlBackup(cmd);

                    
                    cmd.Connection = conn;
                    conn.Open();

          
                    bkp.ExportToFile(path);
                    conn.Close();

                    MessageBox.Show("Backup succeeded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to restore the database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
                try
                {
                    string path = @"C:\Users\USER\Documents\DataBackup\SystemBackUp.sql";

                   
                    MySqlCommand cmd = new MySqlCommand();
                    MySqlBackup bkp = new MySqlBackup(cmd);

             
                    cmd.Connection = conn;
                    conn.Open();

  
                    bkp.ImportFromFile(path);
                    conn.Close();

                    MessageBox.Show("Restore succeeded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExInfo_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Show();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Show();
            reports1.Hide();
            grbSettings.Visible = false;
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Show();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientVisits1.Hide();
            patientRegistration1.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAppontments_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Show();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Hide();
        }

        private void btnVisits_Click(object sender, EventArgs e)
        {
            patientss1.Hide();
            stats1.Hide();
            vitals1.Hide();
            healthRecords1.Hide();
            nutritionPlan1.Hide();
            nutritionInfo1.Hide();
            ptNutrition1.Hide();
            appointments1.Hide();
            ptExercise1.Hide();
            exPlans1.Hide();
            exerciseInfo1.Hide();
            userAccount1.Hide();
            reports1.Hide();
            patientRegistration1.Hide();
            patientVisits1.Show();
        }

        private void timerAppointments_Tick(object sender, EventArgs e)
        {
            const int step = 10;
            if (isCollapsed)
            {
                if (panelAppointment.Height < panelAppointment.MaximumSize.Height)
                {
                    panelAppointment.Height += step;
                }
                else
                {
                    timerAppointments.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                if (panelAppointment.Height > panelAppointment.MinimumSize.Height)
                {
                    panelAppointment.Height -= step;
                }
                else
                {
                    timerAppointments.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnExxit_Click(object sender, EventArgs e)
        {
            grbPassword.Visible = false;
        }

        private void btnEnterSettings_Click(object sender, EventArgs e)
        {
            string enteredPassword = txtPassword.Text;
            if (VerifyPasswordAndRole(enteredPassword))
            {
                MessageBox.Show("Login successful! Access granted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                grbPassword.Visible = false;
                grbSettings.Visible = true;
            }
            else
            {
                MessageBox.Show("Login failed! Insufficient privileges.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool VerifyPasswordAndRole(string enteredPassword)
        {
            string connectionString = "SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;";
            string query = "SELECT Password, Role FROM users WHERE Role = 'Administrator' LIMIT 1"; 

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHashedPassword = reader["Password"].ToString();
                            string role = reader["Role"].ToString();

                            
                            if (BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword) && role == "Administrator")
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

    }
}
