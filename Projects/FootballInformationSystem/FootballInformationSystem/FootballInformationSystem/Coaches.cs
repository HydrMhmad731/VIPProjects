using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FootballInformationSystem
{
    public partial class Coaches : Form
    {
        public Coaches()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Coaches_Load(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string coachname = txtFirst.Text;
            string lastName = txtLast.Text;
            string gender = txtGender.Text;
            string nationality = txtNationality.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string working = txtWork.Text;


            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\USER\\Downloads\\coaches.mdb";
            OleDbCommand cmd = new OleDbCommand("INSERT INTO coaches (CoachName,LastName,Gender,Nationality,Phone,Address,WorkingYears) VALUES (@CoachName,@LastName,@Gender,@Nationality,@Phone,@Address,@WorkingYears)", conn);
            cmd.Parameters.AddWithValue("@CoachName,", coachname);
            cmd.Parameters.AddWithValue("@LastName,", lastName);
            cmd.Parameters.AddWithValue("@Gender,", gender);
            cmd.Parameters.AddWithValue("@Nationality,", nationality);
            cmd.Parameters.AddWithValue("@Phone,", phone);
            cmd.Parameters.AddWithValue("@Address,", address);
            cmd.Parameters.AddWithValue("@WorkingHours,", working);
            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainPage mainpage = new MainPage();
            mainpage.Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string coachname = txtFirst.Text;
            string lastName = txtLast.Text;
            string gender = txtGender.Text;
            string nationality = txtNationality.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string working = txtWork.Text;


            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\USER\\Downloads\\coaches.mdb";
            OleDbCommand cmd = new OleDbCommand("UPDATE coaches SET CoachName = @CoachName, LastName = @LastName, Gender = @Gender, Nationality = @Nationality, Phone = @Phone, Address = @Address, WorkingYears = @WorkingYears", conn);
            cmd.Parameters.AddWithValue("@CoachName,", coachname);
            cmd.Parameters.AddWithValue("@LastName,", lastName);
            cmd.Parameters.AddWithValue("@Gender,", gender);
            cmd.Parameters.AddWithValue("@Nationality,", nationality);
            cmd.Parameters.AddWithValue("@Phone,", phone);
            cmd.Parameters.AddWithValue("@Address,", address);
            cmd.Parameters.AddWithValue("@WorkingHours,", working);
            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string coachname = txtFirst.Text;
            string lastName = txtLast.Text;
            string gender = txtGender.Text;
            string nationality = txtNationality.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string working = txtWork.Text;


            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\USER\\Downloads\\coaches.mdb";
            OleDbCommand cmd = new OleDbCommand("DELETE FROM coaches WHERE CoachName = @CoachName AND LastName = @LastName AND Gender = @Gender AND Nationality = @Nationality AND Phone = @Phone AND Address = @Address AND WorkingYears = @WorkingYears", conn);
            cmd.Parameters.AddWithValue("@CoachName,", coachname);
            cmd.Parameters.AddWithValue("@LastName,", lastName);
            cmd.Parameters.AddWithValue("@Gender,", gender);
            cmd.Parameters.AddWithValue("@Nationality,", nationality);
            cmd.Parameters.AddWithValue("@Phone,", phone);
            cmd.Parameters.AddWithValue("@Address,", address);
            cmd.Parameters.AddWithValue("@WorkingHours,", working);
            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\USER\\Downloads\\coaches.mdb";
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from coaches", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "t0");
            dataGridView1.DataSource = ds.Tables["t0"];
            conn.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\USER\\Downloads\\coaches.mdb";
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from coaches where CoachName = '" + cbmxSearch.Text + "'", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "t0");
            dataGridView1.DataSource = ds.Tables["t0"];
            conn.Close();
        }
    }
    }
