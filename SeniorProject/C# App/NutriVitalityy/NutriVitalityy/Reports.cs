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
    public partial class Reports : UserControl
    {
        MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;");
        private PrintDocument printDocument = new PrintDocument();
        private PrintDialog printDialog = new PrintDialog();
        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        public Reports()
        {
            InitializeComponent();
            CustomizeDataGridView();
            PopulatePatientComboBox();
            InitializeDatabaseConnection();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                PopulatePatientComboBox();
            }
        }
        private void InitializeDatabaseConnection()
        {
            string connectionString = "SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;";
            conn = new MySqlConnection(connectionString);
        }
        private void PopulatePatientComboBox()
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
        void clearAll()
        {
            cbmxPID.SelectedIndex = -1;
            cbmxReportTitle.SelectedIndex = -1;
            richReportContent.Text = "";
        }
        void viewDataGrid()
        {
            string connectionString = "SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM reports";
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

        private void Reports_Load(object sender, EventArgs e)
        {
            viewDataGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();


            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter something to search");
                return;
            }

            string query = "SELECT * FROM reports WHERE ";
            query += $"Reportid LIKE '%{searchText}%' OR ";
            query += $"PatientId LIKE '%{searchText}%' OR ";
            query += $"ReportTitle LIKE '%{searchText}%' ";


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

            try
            {

                DialogResult result = MessageBox.Show("Are you sure you want to create a backup of patient data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                string path = @"C:\Users\USER\Documents\DataBackup\ReportsBackup.sql";

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

                string path = @"C:\Users\USER\Documents\DataBackup\ReportsBackup.sql";

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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int patientId = Convert.ToInt32(cbmxPID.SelectedValue);
            string reportTitle = cbmxReportTitle.Text;
            string reportContent = richReportContent.Text;
            DateTime reportDate = DateTime.Now;

            string query = "INSERT INTO reports (PatientId, ReportTitle, ReportContent, ReportDate) VALUES (@PatientId, @ReportTitle, @ReportContent, @ReportDate)";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Parameters.AddWithValue("@ReportTitle", reportTitle);
            cmd.Parameters.AddWithValue("@ReportContent", reportContent);
            cmd.Parameters.AddWithValue("@ReportDate", reportDate);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Report inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            viewDataGrid();
            clearAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select one or more rows to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbmxPID.SelectedItem == null)
            {
                MessageBox.Show("Please select a patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedPatientId = cbmxPID.SelectedValue.ToString();
            string selectedReportTitle = cbmxReportTitle.Text;
            string selectedReportContent = richReportContent.Text;

            using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
            {
                conn.Open();

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int reportId = Convert.ToInt32(row.Cells["ReportId"].Value);


                    string query = "UPDATE reports SET ";
                    bool hasChanges = false;

                    if (!string.IsNullOrEmpty(selectedPatientId))
                    {
                        query += "PatientId = @PatientId, ";
                        hasChanges = true;
                    }

                    if (!string.IsNullOrEmpty(selectedReportTitle))
                    {
                        query += "ReportTitle = @ReportTitle, ";
                        hasChanges = true;
                    }

                    if (!string.IsNullOrEmpty(selectedReportContent))
                    {
                        query += "ReportContent = @ReportContent, ";
                        hasChanges = true;
                    }

                    
                    query = query.TrimEnd(',', ' ');

                    query += " WHERE ReportId = @ReportId";

                    if (hasChanges)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        if (!string.IsNullOrEmpty(selectedPatientId))
                        {
                            cmd.Parameters.AddWithValue("@PatientId", selectedPatientId);
                        }

                        if (!string.IsNullOrEmpty(selectedReportTitle))
                        {
                            cmd.Parameters.AddWithValue("@ReportTitle", selectedReportTitle);
                        }

                        if (!string.IsNullOrEmpty(selectedReportContent))
                        {
                            cmd.Parameters.AddWithValue("@ReportContent", selectedReportContent);
                        }

                        cmd.Parameters.AddWithValue("@ReportId", reportId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Report updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No changes to update for Report ID: " + reportId, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            viewDataGrid();
            clearAll();

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
                using (MySqlConnection conn = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int reportId = Convert.ToInt32(row.Cells["ReportId"].Value);
                        string query = "DELETE FROM reports WHERE ReportId = @ReportId";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ReportId", reportId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Report deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

               
                viewDataGrid();
                clearAll();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Arial", 24, FontStyle.Bold);
            Font contentFont = new Font("Arial", 16);
            Brush brush = new SolidBrush(Color.Black);
            float lineHeight = titleFont.GetHeight(g);

            float printableWidth = e.PageSettings.PrintableArea.Width;
            float printableHeight = e.PageSettings.PrintableArea.Height;

            string reportTitle = "Report";
            SizeF titleSize = g.MeasureString(reportTitle, titleFont);
            float titleX = (printableWidth - titleSize.Width) / 2;
            g.DrawString(reportTitle, titleFont, brush, titleX, 50);

            string reportDate = DateTime.Now.ToString("yyyy-MM-dd");
            SizeF dateSize = g.MeasureString(reportDate, contentFont);
            float dateX = printableWidth - dateSize.Width - 50;
            g.DrawString(reportDate, contentFont, brush, dateX, 50);

            float contentTop = 100 + lineHeight;
            string patientName = "";
            if (cbmxPID.SelectedValue != null)
            {
                using (MySqlConnection connection = new MySqlConnection("SERVER=LOCALHOST ;DATABASE=account ;UID=root ;PASSWORD=gardian731 ;"))
                {
                    string query = "SELECT CONCAT(FirstName, ' ', LastName) AS FullName FROM patients WHERE PatientId = @PatientId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PatientId", cbmxPID.SelectedValue);
                    connection.Open();
                    patientName = command.ExecuteScalar()?.ToString() ?? "N/A";
                }
            }
            else
            {
                patientName = "N/A";
            }

            string reportContent = richReportContent.Text;

            
            string[] lines = reportContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

           
            StringFormat format = new StringFormat(StringFormatFlags.LineLimit);
            format.Trimming = StringTrimming.Word;

            
            g.DrawString("Patient Name: " + patientName, contentFont, brush, new PointF(50, contentTop), format);
            contentTop += contentFont.GetHeight(g);
            g.DrawString("Report Title: " + cbmxReportTitle.Text, contentFont, brush, new PointF(50, contentTop), format);
            contentTop += contentFont.GetHeight(g);
            foreach (string line in lines)
            {
                g.DrawString(line, contentFont, brush, new PointF(50, contentTop), format);
                contentTop += contentFont.GetHeight(g);
            }

            float contentHeight = contentFont.GetHeight(g) * lines.Length;
            if (contentTop + contentHeight > printableHeight)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            viewDataGrid();
        }
    }
}
