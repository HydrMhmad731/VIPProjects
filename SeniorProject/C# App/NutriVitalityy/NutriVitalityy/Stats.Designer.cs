namespace NutriVitalityy
{
    partial class Stats
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAppointments = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelAppointment = new System.Windows.Forms.Panel();
            this.lblAppiontment = new System.Windows.Forms.Label();
            this.panelPatients = new System.Windows.Forms.Panel();
            this.lblPatients = new System.Windows.Forms.Label();
            this.PanelUsers = new System.Windows.Forms.Panel();
            this.lblUsers = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.PanelAppointment.SuspendLayout();
            this.panelPatients.SuspendLayout();
            this.PanelUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.lblAppointments);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PanelAppointment);
            this.panel1.Controls.Add(this.panelPatients);
            this.panel1.Controls.Add(this.PanelUsers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1093, 543);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblAppointments
            // 
            this.lblAppointments.AutoSize = true;
            this.lblAppointments.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointments.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblAppointments.Location = new System.Drawing.Point(396, 441);
            this.lblAppointments.Name = "lblAppointments";
            this.lblAppointments.Size = new System.Drawing.Size(15, 23);
            this.lblAppointments.TabIndex = 23;
            this.lblAppointments.Text = ".";
            this.lblAppointments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Script MT Bold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Orange;
            this.label3.Location = new System.Drawing.Point(901, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 35);
            this.label3.TabIndex = 21;
            this.label3.Text = "🕑";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Script MT Bold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.YellowGreen;
            this.label2.Location = new System.Drawing.Point(158, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 35);
            this.label2.TabIndex = 20;
            this.label2.Text = "👨‍⚕️";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Script MT Bold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.label1.Location = new System.Drawing.Point(521, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 35);
            this.label1.TabIndex = 16;
            this.label1.Text = "👨‍💼";
            // 
            // PanelAppointment
            // 
            this.PanelAppointment.BackColor = System.Drawing.Color.Orange;
            this.PanelAppointment.Controls.Add(this.lblAppiontment);
            this.PanelAppointment.Location = new System.Drawing.Point(772, 98);
            this.PanelAppointment.Name = "PanelAppointment";
            this.PanelAppointment.Size = new System.Drawing.Size(290, 90);
            this.PanelAppointment.TabIndex = 18;
            // 
            // lblAppiontment
            // 
            this.lblAppiontment.AutoSize = true;
            this.lblAppiontment.Font = new System.Drawing.Font("Bell MT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppiontment.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblAppiontment.Location = new System.Drawing.Point(3, 21);
            this.lblAppiontment.Name = "lblAppiontment";
            this.lblAppiontment.Size = new System.Drawing.Size(67, 25);
            this.lblAppiontment.TabIndex = 2;
            this.lblAppiontment.Text = "label3";
            // 
            // panelPatients
            // 
            this.panelPatients.BackColor = System.Drawing.Color.YellowGreen;
            this.panelPatients.Controls.Add(this.lblPatients);
            this.panelPatients.Location = new System.Drawing.Point(36, 98);
            this.panelPatients.Name = "panelPatients";
            this.panelPatients.Size = new System.Drawing.Size(291, 90);
            this.panelPatients.TabIndex = 19;
            // 
            // lblPatients
            // 
            this.lblPatients.AutoSize = true;
            this.lblPatients.Font = new System.Drawing.Font("Bell MT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatients.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblPatients.Location = new System.Drawing.Point(1, 19);
            this.lblPatients.Name = "lblPatients";
            this.lblPatients.Size = new System.Drawing.Size(67, 25);
            this.lblPatients.TabIndex = 0;
            this.lblPatients.Text = "label1";
            // 
            // PanelUsers
            // 
            this.PanelUsers.BackColor = System.Drawing.Color.MediumAquamarine;
            this.PanelUsers.Controls.Add(this.lblUsers);
            this.PanelUsers.Location = new System.Drawing.Point(410, 98);
            this.PanelUsers.Name = "PanelUsers";
            this.PanelUsers.Size = new System.Drawing.Size(284, 90);
            this.PanelUsers.TabIndex = 17;
            // 
            // lblUsers
            // 
            this.lblUsers.AutoSize = true;
            this.lblUsers.Font = new System.Drawing.Font("Bell MT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsers.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblUsers.Location = new System.Drawing.Point(3, 23);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.Size = new System.Drawing.Size(67, 25);
            this.lblUsers.TabIndex = 1;
            this.lblUsers.Text = "label2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(93, 194);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(910, 244);
            this.dataGridView1.TabIndex = 124;
            // 
            // Stats
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Name = "Stats";
            this.Size = new System.Drawing.Size(1093, 543);
            this.Load += new System.EventHandler(this.Stats_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PanelAppointment.ResumeLayout(false);
            this.PanelAppointment.PerformLayout();
            this.panelPatients.ResumeLayout(false);
            this.panelPatients.PerformLayout();
            this.PanelUsers.ResumeLayout(false);
            this.PanelUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAppointments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PanelAppointment;
        private System.Windows.Forms.Label lblAppiontment;
        private System.Windows.Forms.Panel panelPatients;
        private System.Windows.Forms.Label lblPatients;
        private System.Windows.Forms.Panel PanelUsers;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
