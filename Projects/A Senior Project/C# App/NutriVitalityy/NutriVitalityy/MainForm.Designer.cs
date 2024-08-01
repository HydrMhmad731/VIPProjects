namespace NutriVitalityy
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.timerSettingButton = new System.Windows.Forms.Timer(this.components);
            this.PanelUsers = new System.Windows.Forms.Panel();
            this.panelPatients = new System.Windows.Forms.Panel();
            this.PanelAppointment = new System.Windows.Forms.Panel();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblPatients = new System.Windows.Forms.Label();
            this.lblUsers = new System.Windows.Forms.Label();
            this.lblAppiontment = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PanelUsers.SuspendLayout();
            this.panelPatients.SuspendLayout();
            this.PanelAppointment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(637, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // timerSettingButton
            // 
            this.timerSettingButton.Interval = 1000;
            this.timerSettingButton.Tick += new System.EventHandler(this.timerSettingButton_Tick);
            // 
            // PanelUsers
            // 
            this.PanelUsers.BackColor = System.Drawing.Color.Beige;
            this.PanelUsers.Controls.Add(this.lblUsers);
            this.PanelUsers.Location = new System.Drawing.Point(327, 172);
            this.PanelUsers.Name = "PanelUsers";
            this.PanelUsers.Size = new System.Drawing.Size(294, 140);
            this.PanelUsers.TabIndex = 2;
            // 
            // panelPatients
            // 
            this.panelPatients.BackColor = System.Drawing.Color.Beige;
            this.panelPatients.Controls.Add(this.lblPatients);
            this.panelPatients.Location = new System.Drawing.Point(12, 172);
            this.panelPatients.Name = "panelPatients";
            this.panelPatients.Size = new System.Drawing.Size(273, 140);
            this.panelPatients.TabIndex = 3;
            // 
            // PanelAppointment
            // 
            this.PanelAppointment.BackColor = System.Drawing.Color.Beige;
            this.PanelAppointment.Controls.Add(this.lblAppiontment);
            this.PanelAppointment.Location = new System.Drawing.Point(12, 362);
            this.PanelAppointment.Name = "PanelAppointment";
            this.PanelAppointment.Size = new System.Drawing.Size(273, 159);
            this.PanelAppointment.TabIndex = 3;
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblClock.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClock.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblClock.Location = new System.Drawing.Point(741, 134);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(15, 21);
            this.lblClock.TabIndex = 4;
            this.lblClock.Text = ".";
            // 
            // lblPatients
            // 
            this.lblPatients.AutoSize = true;
            this.lblPatients.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatients.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblPatients.Location = new System.Drawing.Point(68, 41);
            this.lblPatients.Name = "lblPatients";
            this.lblPatients.Size = new System.Drawing.Size(62, 25);
            this.lblPatients.TabIndex = 0;
            this.lblPatients.Text = "label1";
            // 
            // lblUsers
            // 
            this.lblUsers.AutoSize = true;
            this.lblUsers.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblUsers.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblUsers.Location = new System.Drawing.Point(40, 41);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.Size = new System.Drawing.Size(65, 25);
            this.lblUsers.TabIndex = 1;
            this.lblUsers.Text = "label2";
            // 
            // lblAppiontment
            // 
            this.lblAppiontment.AutoSize = true;
            this.lblAppiontment.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblAppiontment.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblAppiontment.Location = new System.Drawing.Point(23, 36);
            this.lblAppiontment.Name = "lblAppiontment";
            this.lblAppiontment.Size = new System.Drawing.Size(65, 25);
            this.lblAppiontment.TabIndex = 2;
            this.lblAppiontment.Text = "label3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1084, 121);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Script MT Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.YellowGreen;
            this.label1.Location = new System.Drawing.Point(425, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 44);
            this.label1.TabIndex = 1;
            this.label1.Text = "👨‍💼";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Script MT Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.YellowGreen;
            this.label2.Location = new System.Drawing.Point(107, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 44);
            this.label2.TabIndex = 5;
            this.label2.Text = "👨‍⚕️";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Script MT Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.YellowGreen;
            this.label3.Location = new System.Drawing.Point(122, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 44);
            this.label3.TabIndex = 6;
            this.label3.Text = "🕑";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 711);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblClock);
            this.Controls.Add(this.PanelAppointment);
            this.Controls.Add(this.panelPatients);
            this.Controls.Add(this.PanelUsers);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.PanelUsers.ResumeLayout(false);
            this.PanelUsers.PerformLayout();
            this.panelPatients.ResumeLayout(false);
            this.panelPatients.PerformLayout();
            this.PanelAppointment.ResumeLayout(false);
            this.PanelAppointment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timerSettingButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel PanelUsers;
        private System.Windows.Forms.Panel panelPatients;
        private System.Windows.Forms.Panel PanelAppointment;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.Label lblPatients;
        private System.Windows.Forms.Label lblAppiontment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}