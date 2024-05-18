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
    public partial class HealthRecords : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        private Dictionary<string, List<string>> diagnosisTreatments = new Dictionary<string, List<string>>
        {
            {"Hypertension (High Blood Pressure)", new List<string>{"Lifestyle changes (e.g., diet, exercise)", "Medications (e.g., ACE inhibitors, diuretics)"}},
            {"Type 2 Diabetes Mellitus", new List<string>{"Lifestyle modifications (e.g., diet, exercise)", "Oral medications (e.g., metformin)", "Insulin therapy"}},
            {"Obesity", new List<string>{"Dietary changes (e.g., calorie restriction)", "Physical activity", "Behavioral therapy", "Medications (e.g., orlistat)", "Bariatric surgery"}},
            {"Hyperlipidemia (High Cholesterol)", new List<string>{"Lifestyle modifications (e.g., diet, exercise)", "Statins", "Fibrates", "Bile acid sequestrants", "Ezetimibe"}},
            {"Coronary Artery Disease", new List<string>{"Lifestyle modifications (e.g., diet, exercise)", "Medications (e.g., aspirin, beta-blockers)", "Angioplasty", "Coronary artery bypass grafting (CABG)"}},
            {"Gastroesophageal Reflux Disease (GERD)", new List<string>{"Lifestyle modifications (e.g., diet, weight loss)", "Antacids", "H2 blockers", "Proton pump inhibitors (PPIs)"}},
            {"Irritable Bowel Syndrome (IBS)", new List<string>{"Dietary modifications (e.g., low-FODMAP diet)", "Fiber supplements", "Antispasmodic medications", "Probiotics"}},
            {"Celiac Disease", new List<string>{"Strict gluten-free diet", "Vitamin and mineral supplements"}},
            {"Crohn's Disease", new List<string>{"Anti-inflammatory medications (e.g., corticosteroids)", "Immunosuppressants", "Biologic therapies", "Nutritional therapy"}},
            {"Ulcerative Colitis", new List<string>{"Anti-inflammatory medications (e.g., 5-aminosalicylates)", "Immunosuppressants", "Biologic therapies", "Surgery (colectomy)"}},
            {"Osteoarthritis", new List<string>{"Lifestyle modifications (e.g., weight loss, exercise)", "Pain relievers (e.g., acetaminophen, NSAIDs)", "Physical therapy", "Joint injections", "Surgery (joint replacement)"}},
            {"Rheumatoid Arthritis", new List<string>{"Disease-modifying antirheumatic drugs (DMARDs)", "Biologic response modifiers", "Corticosteroids", "Nonsteroidal anti-inflammatory drugs (NSAIDs)", "Physical therapy"}},
            {"Asthma", new List<string>{"Bronchodilators (e.g., albuterol)", "Inhaled corticosteroids", "Leukotriene modifiers", "Biologic therapies"}},
            {"Chronic Obstructive Pulmonary Disease (COPD)", new List<string>{"Bronchodilators (e.g., albuterol)", "Inhaled corticosteroids", "Long-acting anticholinergics", "Oxygen therapy", "Pulmonary rehabilitation"}},
            {"Depression", new List<string>{"Antidepressant medications (e.g., SSRIs, SNRIs)", "Psychotherapy (e.g., cognitive-behavioral therapy)", "Electroconvulsive therapy (ECT)", "Transcranial magnetic stimulation (TMS)"}},
            {"Anxiety Disorder", new List<string>{"Selective serotonin reuptake inhibitors (SSRIs)", "Serotonin-norepinephrine reuptake inhibitors (SNRIs)", "Benzodiazepines", "Psychotherapy"}},
            {"Bipolar Disorder", new List<string>{"Mood stabilizers (e.g., lithium, valproate)", "Atypical antipsychotics", "Antidepressants", "Psychotherapy"}},
            {"Attention Deficit Hyperactivity Disorder (ADHD)", new List<string>{"Stimulant medications (e.g., methylphenidate, amphetamine salts)", "Non-stimulant medications (e.g., atomoxetine, guanfacine)", "Behavioral therapy"}},
            {"Autism Spectrum Disorder (ASD)", new List<string>{"Behavioral therapy (e.g., applied behavior analysis)", "Educational interventions", "Speech therapy", "Occupational therapy"}},
            {"Hypothyroidism", new List<string>{"Levothyroxine (thyroid hormone replacement therapy)"}},
            {"Hyperthyroidism", new List<string>{"Antithyroid medications (e.g., methimazole, propylthiouracil)", "Radioactive iodine therapy", "Surgery (thyroidectomy)"}},
            {"Polycystic Ovary Syndrome (PCOS)", new List<string>{"Lifestyle modifications (e.g., diet, exercise)", "Oral contraceptives", "Anti-androgen medications", "Insulin-sensitizing medications"}},
            {"Endometriosis", new List<string>{"Pain relievers (e.g., NSAIDs)", "Hormonal therapy (e.g., birth control pills, GnRH agonists)", "Surgery (laparoscopy, laparotomy)"}},
            {"Chronic Kidney Disease (CKD)", new List<string>{"Blood pressure control (e.g., ACE inhibitors, ARBs)", "Diabetes management", "Dietary modifications (e.g., low-protein diet)", "Dialysis", "Kidney transplant"}},
            {"Gout", new List<string>{"Nonsteroidal anti-inflammatory drugs (NSAIDs)", "Colchicine", "Corticosteroids", "Xanthine oxidase inhibitors (e.g., allopurinol)", "Probenecid"}},
            {"Migraine Headaches", new List<string>{"Pain relievers (e.g., acetaminophen, NSAIDs)", "Triptans", "Preventive medications (e.g., beta-blockers, antidepressants)", "Lifestyle modifications"}},
            {"Osteoporosis", new List<string>{"Calcium supplements", "Vitamin D supplements", "Bisphosphonates", "Denosumab", "Teriparatide", "Raloxifene"}},
            {"Anemia", new List<string>{"Iron supplements", "Vitamin B12 injections", "Folic acid supplements", "Erythropoiesis-stimulating agents"}},
            {"Peptic Ulcer Disease", new List<string>{"Proton pump inhibitors (PPIs)", "H2 blockers", "Antibiotics (for H. pylori eradication)", "Antacids"}},
            {"Sleep Apnea", new List<string>{"Continuous positive airway pressure (CPAP) therapy", "Oral appliances", "Lifestyle modifications (e.g., weight loss, positional therapy)"}}
        };

       

        public HealthRecords()
        {
            InitializeComponent();
            PopulateComboBox();
            CustomizeDataGridView();
            PopulateDiagnosisComboBox();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                PopulateComboBox();
            }
        }
        void ClearAll()
        {

            cbmxPID.SelectedIndex = -1;
            cbmxDiagnosis.SelectedIndex = -1;
            cbmxTreatment.SelectedIndex = -1;
            richDescription.Text = "";
            richTestResult.Text = "";


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

        void viewDataGrid()
        {
            string connectionString = "SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM healthcarerecords";
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
        private void PopulateDiagnosisComboBox()
        {

            cbmxDiagnosis.Items.AddRange(diagnosisTreatments.Keys.ToArray());
        }

        private void PopulateComboBox()
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
        }
        private void HealthRecords_Load(object sender, EventArgs e)
        {
            viewDataGrid();
        }

        private void richDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to insert the record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO healthcarerecords (Patientid, RecordedDate, Description, Diagnosis, Treatment, TestResults) " +
                                   "VALUES (@Patientid, @RecordedDate, @Description, @Diagnosis, @Treatment, @TestResults)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Patientid", cbmxPID.SelectedValue);
                    command.Parameters.AddWithValue("@RecordedDate", DateTime.Today);
                    command.Parameters.AddWithValue("@Description", richDescription.Text);
                    command.Parameters.AddWithValue("@Diagnosis", cbmxDiagnosis.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Treatment", cbmxTreatment.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@TestResults", richTestResult.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Record inserted successfully!");
                    ClearAll();
                    viewDataGrid();
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
        }

        private void cbmxDiagnosis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbmxDiagnosis.SelectedItem != null)
            {
                string selectedDiagnosis = cbmxDiagnosis.SelectedItem.ToString();

                cbmxTreatment.Items.Clear();

                if (diagnosisTreatments.ContainsKey(selectedDiagnosis))
                {
                    foreach (string treatment in diagnosisTreatments[selectedDiagnosis])
                    {
                        cbmxTreatment.Items.Add(treatment);
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to update the record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a record to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int recordId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RecordID"].Value);

                try
                {
                    conn.Open();
                    StringBuilder queryBuilder = new StringBuilder("UPDATE healthcarerecords SET ");

                    if (!string.IsNullOrEmpty(richDescription.Text))
                    {
                        queryBuilder.Append("Description = @Description, ");
                    }

                    if (cbmxDiagnosis.SelectedItem != null)
                    {
                        queryBuilder.Append("Diagnosis = @Diagnosis, ");
                    }

                    if (cbmxTreatment.SelectedItem != null)
                    {
                        queryBuilder.Append("Treatment = @Treatment, ");
                    }

                    if (!string.IsNullOrEmpty(richTestResult.Text))
                    {
                        queryBuilder.Append("TestResults = @TestResults, ");
                    }

                    queryBuilder.Length -= 2;
                    queryBuilder.Append(" WHERE RecordID = @RecordID");

                    MySqlCommand command = new MySqlCommand(queryBuilder.ToString(), conn);
                    command.Parameters.AddWithValue("@RecordID", recordId);

                    if (!string.IsNullOrEmpty(richDescription.Text))
                    {
                        command.Parameters.AddWithValue("@Description", richDescription.Text);
                    }

                    if (cbmxDiagnosis.SelectedItem != null)
                    {
                        command.Parameters.AddWithValue("@Diagnosis", cbmxDiagnosis.SelectedItem.ToString());
                    }

                    if (cbmxTreatment.SelectedItem != null)
                    {
                        command.Parameters.AddWithValue("@Treatment", cbmxTreatment.SelectedItem.ToString());
                    }

                    if (!string.IsNullOrEmpty(richTestResult.Text))
                    {
                        command.Parameters.AddWithValue("@TestResults", richTestResult.Text);
                    }

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully!");
                        ClearAll();
                        viewDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("No records were updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Cells["RecordID"].Value);
                    DeleteRow(id);
                }


                RefreshDataGridView();
            }

        }
        private void DeleteRow(int id)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                MySqlCommand command = new MySqlCommand("DELETE FROM healthcarerecords WHERE RecordID = @Recordid", conn);
                command.Parameters.AddWithValue("@Recordid", id);
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

                string query = "SELECT * FROM healthcarerecords";
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();


            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter something to search");
                return;
            }

            string query = "SELECT * FROM healthcarerecords WHERE ";
            query += $"RecordID LIKE '%{searchText}%' OR ";
            query += $"Patientid LIKE '%{searchText}%' OR ";
            query += $"Diagnosis LIKE '%{searchText}%' OR ";
            
           

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

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\USER\Documents\DataBackup\HealthRecordsBackup.sql";
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
            string path = @"C:\Users\USER\Documents\DataBackup\HealthRecordsBackup.sql";
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
                    viewDataGrid();
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
    }
}
