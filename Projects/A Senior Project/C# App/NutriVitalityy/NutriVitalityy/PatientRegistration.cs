using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace NutriVitalityy
{
    public partial class PatientRegistration : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;");
        private PrintDocument printDocument = new PrintDocument();
        private PrintDialog printDialog = new PrintDialog();
        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        public PatientRegistration()
        {
            InitializeComponent();
            PopulatePatientComboBox();
            CustomizeDataGridView();
            cbmxPID.SelectedIndexChanged += cbmxPID_SelectedIndexChanged;
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                PopulatePatientComboBox();
            }
        }
        private void PopulatePatientComboBox()
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
        }
        void viewDataGrid()
        {
            string connectionString = "SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM patientregistration";
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
        void clearAll()
        {
            cbmxPID.SelectedIndex = -1;
            txtPayments.Text = "";
            txtNutrition.Text = "";
            txtExercise.Text = "";
            
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

            string query = "SELECT * FROM patientregistration WHERE ";
            query += $"RegistrationID LIKE '%{searchText}%' OR ";
            query += $"PatientID LIKE '%{searchText}%' ";
            


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
            viewDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select one or more rows to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int RegisrationtId = Convert.ToInt32(row.Cells["RegistrationID"].Value);
                        string query = "DELETE FROM patientregistration WHERE RegistrationID = @Registrationid";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Registrationid", RegisrationtId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }


                viewDataGrid();
                clearAll();
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result = MessageBox.Show("Are you sure you want to create a backup of patient data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                string path = @"C:\Users\USER\Documents\DataBackup\Pts_RegistrationBackup.sql";

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

                MessageBox.Show("Backup of patient data has been created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result = MessageBox.Show("Are you sure you want to restore patient data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                string path = @"C:\Users\USER\Documents\DataBackup\Pts_RegistrationBackup.sql";

                MySqlCommand cmd = new MySqlCommand();
                MySqlBackup bkp = new MySqlBackup(cmd);
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    bkp.ImportFromFile(path);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }

                MessageBox.Show("Patient data has been restored successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            viewDataGrid();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int patientId = Convert.ToInt32(cbmxPID.SelectedValue);

            decimal totalSum = 0;

            
            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
            {
                connection.Open();
                string query = "SELECT np.Price FROM patientsnutrition pn JOIN nutritionplans np ON pn.PlanId = np.PlanId WHERE pn.PatientId = @PatientId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientId", patientId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        totalSum += reader.GetDecimal("Price");
                    }
                }
            }

            
            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah;"))
            {
                connection.Open();
                string query = "SELECT ep.Price FROM patientsexercise pe JOIN exerciseplans ep ON pe.ExPlanid = ep.ExPlanId WHERE pe.patientId = @PatientId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientId", patientId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        totalSum += reader.GetDecimal("Price");
                    }
                }
            }

           
            txtPayments.Text = totalSum.ToString();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                int patientId = Convert.ToInt32(cbmxPID.SelectedValue);
                decimal paymentAmount = decimal.Parse(txtPayments.Text);

                DateTime registrationDate = DateTime.Today;
                DateTime paymentDate = registrationDate.AddDays(30);

                DialogResult result = MessageBox.Show("Are you sure you want to insert the registration data?", "Insert Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
                    {
                        connection.Open();
                        string query = "INSERT INTO patientregistration (PatientID, RegistrationDate, PaymentDate, Payment) VALUES (@PatientID, @RegistrationDate, @PaymentDate, @Payment)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@PatientID", patientId);
                        command.Parameters.AddWithValue("@RegistrationDate", registrationDate);
                        command.Parameters.AddWithValue("@PaymentDate", paymentDate);
                        command.Parameters.AddWithValue("@Payment", paymentAmount);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                viewDataGrid();
                clearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select one or more rows to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            DialogResult result = MessageBox.Show("Are you sure you want to update the selected rows?", "Update Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
                
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int registrationId = Convert.ToInt32(row.Cells["RegistrationID"].Value);

                    
                    int? newPatientId = null;
                    decimal? newPayment = null;

                    if (cbmxPID.SelectedValue != null)
                    {
                        newPatientId = Convert.ToInt32(cbmxPID.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(txtPayments.Text))
                    {
                        newPayment = decimal.Parse(txtPayments.Text);
                    }

                    
                    if (newPatientId != null && newPayment == null)
                    {
                       
                        using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
                        {
                            connection.Open();
                            string query = "UPDATE patientregistration SET PatientID = @NewPatientID WHERE RegistrationID = @RegistrationID";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@NewPatientID", newPatientId);
                            command.Parameters.AddWithValue("@RegistrationID", registrationId);
                            command.ExecuteNonQuery();
                        }
                    }
                    else if (newPatientId == null && newPayment != null)
                    {
                        
                        using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
                        {
                            connection.Open();
                            string query = "UPDATE patientregistration SET Payment = @NewPayment WHERE RegistrationID = @RegistrationID";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@NewPayment", newPayment);
                            command.Parameters.AddWithValue("@RegistrationID", registrationId);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please specify either PatientID or Payment to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

               
                viewDataGrid();

                MessageBox.Show("Selected rows updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to print the report?", "Print Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void PatientRegistration_Load(object sender, EventArgs e)
        {
            viewDataGrid();
            clearAll();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Arial", 24, FontStyle.Bold);
            Font contentFont = new Font("Arial", 16);
            Font tableHeaderFont = new Font("Arial", 14, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);
            float lineHeight = titleFont.GetHeight(g);

            float printableWidth = e.PageSettings.PrintableArea.Width;
            float printableHeight = e.PageSettings.PrintableArea.Height;

            string reportTitle = "Nutrition and Exercise Plan Report";
            SizeF titleSize = g.MeasureString(reportTitle, titleFont);
            float titleX = (printableWidth - titleSize.Width) / 2;
            g.DrawString(reportTitle, titleFont, brush, titleX, 50);

            string reportDate = DateTime.Now.ToString("yyyy-MM-dd");
            SizeF dateSize = g.MeasureString(reportDate, contentFont);
            float dateX = printableWidth - dateSize.Width - 50;
            g.DrawString(reportDate, contentFont, brush, dateX, 50);

            float contentTop = 100 + lineHeight;

            string patientName = "N/A";
            string nutritionPlanName = "N/A";
            string exercisePlanName = "N/A";
            string nutritionalDetails = "N/A";
            string exerciseIntensity = "N/A";
            int nutritionDuration = 0;
            decimal nutritionPrice = 0.0m;
            int exerciseDuration = 0;
            decimal exercisePrice = 0.0m;
            List<string[]> nutritionInfo = new List<string[]>();
            List<string[]> exerciseInfo = new List<string[]>();

            if (cbmxPID.SelectedValue != null)
            {
                int patientId = Convert.ToInt32(cbmxPID.SelectedValue);

                try
                {
                    using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST;DATABASE=account;UID=root;PASSWORD=142089mfzah;"))
                    {
                        connection.Open();

                        
                        string patientQuery = "SELECT CONCAT(FirstName, ' ', LastName) AS FullName FROM patients WHERE PatientId = @PatientId";
                        MySqlCommand patientCommand = new MySqlCommand(patientQuery, connection);
                        patientCommand.Parameters.AddWithValue("@PatientId", patientId);
                        patientName = patientCommand.ExecuteScalar()?.ToString() ?? "N/A";

                       
                        string nutritionPlanQuery = "SELECT np.PlanName, np.NutritionalDetails, np.Duration, np.Price FROM nutritionplans np JOIN patientsnutrition pn ON np.PlanID = pn.PlanID WHERE pn.PatientID = @PatientID";
                        MySqlCommand nutritionPlanCommand = new MySqlCommand(nutritionPlanQuery, connection);
                        nutritionPlanCommand.Parameters.AddWithValue("@PatientID", patientId);
                        using (MySqlDataReader reader = nutritionPlanCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nutritionPlanName = reader["PlanName"].ToString();
                                nutritionalDetails = reader["NutritionalDetails"].ToString();
                                nutritionDuration = Convert.ToInt32(reader["Duration"]);
                                nutritionPrice = Convert.ToDecimal(reader["Price"]);
                            }
                        }


                        string exercisePlanQuery = "SELECT ep.PlanName, ep.Duration, ep.Intensity, ep.Price FROM exerciseplans ep JOIN patientsexercise pe ON ep.ExPlanId = pe.ExPlanID WHERE pe.PatientID = @PatientID";
                        MySqlCommand exercisePlanCommand = new MySqlCommand(exercisePlanQuery, connection);
                        exercisePlanCommand.Parameters.AddWithValue("@PatientID", patientId);
                        using (MySqlDataReader reader = exercisePlanCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                exercisePlanName = reader["PlanName"].ToString();
                                exerciseDuration = Convert.ToInt32(reader["Duration"]);
                                exerciseIntensity = reader["Intensity"].ToString();
                                exercisePrice = Convert.ToDecimal(reader["Price"]);
                            }
                        }

                        string nutritionInfoQuery = "SELECT FoodName, Sugar, Calories, Protein, Carbohydrates, Fat, Fiber, ServingSize, Description FROM nutritionsinfo WHERE PlanId = (SELECT PlanID FROM patientsnutrition WHERE PatientID = @PatientID)";
                        MySqlCommand nutritionInfoCommand = new MySqlCommand(nutritionInfoQuery, connection);
                        nutritionInfoCommand.Parameters.AddWithValue("@PatientID", patientId);
                        using (MySqlDataReader reader = nutritionInfoCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nutritionInfo.Add(new string[] {
                            reader["FoodName"].ToString(),
                            reader["Sugar"].ToString(),
                            reader["Calories"].ToString(),
                            reader["Protein"].ToString(),
                            reader["Carbohydrates"].ToString(),
                            reader["Fat"].ToString(),
                            reader["Fiber"].ToString(),
                            reader["ServingSize"].ToString(),
                            reader["Description"].ToString()
                        });
                            }
                        }

                        
                        string exerciseInfoQuery = "SELECT ExName, MuscleGroup, ReqEquipment FROM exerciseinfo WHERE ExPlanId = (SELECT ExPlanID FROM patientsexercise WHERE PatientID = @PatientID)";
                        MySqlCommand exerciseInfoCommand = new MySqlCommand(exerciseInfoQuery, connection);
                        exerciseInfoCommand.Parameters.AddWithValue("@PatientID", patientId);
                        using (MySqlDataReader reader = exerciseInfoCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                exerciseInfo.Add(new string[] {
                            reader["ExName"].ToString(),
                            reader["MuscleGroup"].ToString(),
                            reader["ReqEquipment"].ToString()
                        });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message);
                    return;
                }
            }

            
            StringFormat format = new StringFormat(StringFormatFlags.LineLimit);
            format.Trimming = StringTrimming.Word;

            g.DrawString("Patient Name: " + patientName, contentFont, brush, new PointF(50, contentTop), format);
            contentTop += contentFont.GetHeight(g);

            if (nutritionPlanName != "N/A")
            {
                g.DrawString("Nutrition Plan Name: " + nutritionPlanName, contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g);
                g.DrawString("Duration: " + nutritionDuration + " days", contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g);
                g.DrawString("Price: $" + nutritionPrice.ToString("F2"), contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g);
                g.DrawString("Nutritional Details: " + nutritionalDetails, contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g) + 20;
            }

            if (exercisePlanName != "N/A")
            {
                g.DrawString("Exercise Plan Name: " + exercisePlanName, contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g);
                g.DrawString("Duration: " + exerciseDuration + " days", contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g);
                g.DrawString("Intensity: " + exerciseIntensity, contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g);
                g.DrawString("Price: $" + exercisePrice.ToString("F2"), contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g) + 20;
            }

            
            if (nutritionInfo.Count > 0)
            {
                float tableTop = contentTop;
                float[] columnWidths = { 150, 80, 100, 100, 120, 80, 80, 120, 200 };
                string[] columnHeaders = { "Food Name", "Sugar (g)", "Calories (kcal)", "Protein (g)", "Carbohydrates (g)", "Fat (g)", "Fiber (g)", "Serving Size", "Description" };

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    g.DrawString(columnHeaders[i], tableHeaderFont, brush, new RectangleF(50 + columnWidths.Take(i).Sum(), tableTop, columnWidths[i], contentFont.GetHeight(g)), format);
                }
                tableTop += tableHeaderFont.GetHeight(g) + 5;

                foreach (string[] row in nutritionInfo)
                {
                    for (int i = 0; i < row.Length; i++)
                    {
                        g.DrawString(row[i], contentFont, brush, new RectangleF(50 + columnWidths.Take(i).Sum(), tableTop, columnWidths[i], contentFont.GetHeight(g)), format);
                    }
                    tableTop += contentFont.GetHeight(g);
                }

                contentTop = tableTop + 20;
            }

            
            if (exerciseInfo.Count > 0)
            {
                float tableTop = contentTop;
                float[] columnWidths = { 150, 150, 50 };
                string[] columnHeaders = { "Exercise Name", "Muscle Group", "Requires Equipment" };

                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    g.DrawString(columnHeaders[i], tableHeaderFont, brush, new RectangleF(50 + columnWidths.Take(i).Sum(), tableTop, columnWidths[i], contentFont.GetHeight(g)), format);
                }
                tableTop += tableHeaderFont.GetHeight(g) + 5;

                foreach (string[] row in exerciseInfo)
                {
                    for (int i = 0; i < row.Length; i++)
                    {
                        g.DrawString(row[i], contentFont, brush, new RectangleF(50 + columnWidths.Take(i).Sum(), tableTop, columnWidths[i], contentFont.GetHeight(g)), format);
                    }
                    tableTop += contentFont.GetHeight(g);
                }

                contentTop = tableTop;
            }

            float contentHeight = contentFont.GetHeight(g) * (nutritionInfo.Count + exerciseInfo.Count);
            if (contentTop + contentHeight > printableHeight)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cbmxPID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int patientId = Convert.ToInt32(cbmxPID.SelectedValue);
            if (patientId > 0)
            {
                FetchAndDisplayPlans(patientId);
            }
        }
        private void FetchAndDisplayPlans(int patientId)
        {
            string nutritionPlanName = "N/A";
            string exercisePlanName = "N/A";

            using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=142089mfzah ;"))
            {
                connection.Open();

                
                string nutritionQuery = @"
                    SELECT np.PlanName
                    FROM patientsnutrition pn
                    JOIN nutritionplans np ON pn.PlanId = np.PlanId
                    WHERE pn.PatientId = @PatientId";
                MySqlCommand nutritionCommand = new MySqlCommand(nutritionQuery, connection);
                nutritionCommand.Parameters.AddWithValue("@PatientId", patientId);
                nutritionPlanName = nutritionCommand.ExecuteScalar()?.ToString() ?? "N/A";

                
                string exerciseQuery = @"
                    SELECT ep.PlanName
                    FROM patientsexercise pe
                    JOIN exerciseplans ep ON pe.ExPlanId = ep.ExPlanId
                    WHERE pe.PatientId = @PatientId";
                MySqlCommand exerciseCommand = new MySqlCommand(exerciseQuery, connection);
                exerciseCommand.Parameters.AddWithValue("@PatientId", patientId);
                exercisePlanName = exerciseCommand.ExecuteScalar()?.ToString() ?? "N/A";
            }

            txtNutrition.Text = nutritionPlanName;
            txtExercise.Text = exercisePlanName;
        }
    }
}
