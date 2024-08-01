using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballInformationSystem
{
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\USER\\Downloads\\waroooo.mdb";
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from MuFcSystem",conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "t0");
            dataGridView1.DataSource = ds.Tables["t0"];
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void cbmxFirstName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\USER\\Downloads\\waroooo.mdb";
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from MuFcSystem where FirstName = '" + cbmxFirstName.Text + "'", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "t0");
            dataGridView1.DataSource = ds.Tables["t0"];
            conn.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\USER\\Downloads\\waroooo.mdb";
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from MuFcSystem where LastName = '" + cbmxLast.Text + "'", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "t0");
            dataGridView1.DataSource = ds.Tables["t0"];
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            {
                
            }
        }
    }
}
