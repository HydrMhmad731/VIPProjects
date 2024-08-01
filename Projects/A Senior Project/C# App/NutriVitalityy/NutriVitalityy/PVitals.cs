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
    public partial class PVitals : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        MySqlDataAdapter da = new MySqlDataAdapter();
        MySqlCommand cmd = new MySqlCommand();
        public PVitals()
        {
            InitializeComponent();
            FillPatientIDComboBox();
        }
        void viewDataGrid()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("select * from patientvitals", conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
        }
        void ClearCombos()
        {

            cbmxID.SelectedIndex = -1;
            cbmxWeight.SelectedIndex = -1;
            cbmxHeight.SelectedIndex = -1;
            cbmxSystolic.SelectedIndex = -1;
            cbmxDiastolic.SelectedIndex = -1;
            cbmxHeart.SelectedIndex = -1;
            cbmxBodyTemp.SelectedIndex = -1;
            cbmxCholesterol.SelectedIndex = -1;
            cbmxGlucose.SelectedIndex = -1;
            cbmxRespiration.SelectedIndex = -1;

        }
        private void FillPatientIDComboBox()
        {
            using (conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
            {
                string query = "SELECT Patientid FROM patients";
                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cbmxID.Items.Add(reader["Patientid"].ToString());
                }
                conn.Close();
            }
        }

        private void PVitals_Load(object sender, EventArgs e)
        {

        }

        private void cbmxSystolic_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private bool IsPatientIDExists(string patientID)
        {
            bool exists = false;
            string query = "SELECT COUNT(*) FROM patientvitals WHERE PatientId = @patientID";

            using (conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@patientID", patientID);

                try
                {
                    conn.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    exists = (count > 0);
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

            return exists;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string patientID = cbmxID.SelectedItem.ToString();
            string weight = cbmxWeight.SelectedItem.ToString();
            string height = cbmxHeight.SelectedItem.ToString();
            string bloodPressure = cbmxSystolic.SelectedItem.ToString() + "/" + cbmxDiastolic.SelectedItem.ToString();
            string heartRate = cbmxHeart.SelectedItem.ToString();
            string temperature = cbmxBodyTemp.SelectedItem.ToString();
            string cholesterol = cbmxCholesterol.SelectedItem.ToString();
            string glucose = cbmxGlucose.SelectedItem.ToString();
            string respiratoryRate = cbmxRespiration.SelectedItem.ToString();

            if (IsPatientIDExists(patientID))
            {
                MessageBox.Show("Patient ID already exists in patientvitals table!");
                return;
            }

            string query = "INSERT INTO patientvitals (PatientId, Weight, Height, BloodPressure, HeartRate, BodyTemperature, Cholesterol, Glucose, RespiratoryRate, DateRecorded) VALUES (@PatientID, @Weight, @Height, @BloodPressure, @HeartRate, @BodyTemperature, @Cholesterol, @Glucose, @RespiratoryRate, NOW())";

            using (conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@patientID", patientID);
                command.Parameters.AddWithValue("@Weight", weight);
                command.Parameters.AddWithValue("@Height", height);
                command.Parameters.AddWithValue("@bloodPressure", bloodPressure);
                command.Parameters.AddWithValue("@heartRate", heartRate);
                command.Parameters.AddWithValue("@temperature", temperature);
                command.Parameters.AddWithValue("@cholesterol", cholesterol);
                command.Parameters.AddWithValue("@glucose", glucose);
                command.Parameters.AddWithValue("@respiratoryRate", respiratoryRate);

                try
                {
                    conn.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show("Data inserted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                viewDataGrid();
                ClearCombos();
            }
        }
    }
}
