namespace DVLD.Licenses.Local_License
{
    partial class frmIssueDriverLicenseFortheFirstTime
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
            this.ctrlLocalDrivingLicenseApplicationInfo1 = new DVLD.Applications.LocalDrivingLicenseApplication.Controls.ctrlLocalDrivingLicenseApplicationInfo();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnIssue = new System.Windows.Forms.Button();
            this.crtlClose = new System.Windows.Forms.Button();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlLocalDrivingLicenseApplicationInfo1
            // 
            this.ctrlLocalDrivingLicenseApplicationInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ctrlLocalDrivingLicenseApplicationInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlLocalDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrlLocalDrivingLicenseApplicationInfo1.Name = "ctrlLocalDrivingLicenseApplicationInfo1";
            this.ctrlLocalDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(923, 436);
            this.ctrlLocalDrivingLicenseApplicationInfo1.TabIndex = 0;
            // 
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNotes.Location = new System.Drawing.Point(135, 420);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(776, 135);
            this.tbNotes.TabIndex = 82;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Location = new System.Drawing.Point(17, 420);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 20);
            this.label12.TabIndex = 81;
            this.label12.Text = "Notes :";
            // 
            // btnIssue
            // 
            this.btnIssue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(133)))), ((int)(((byte)(198)))));
            this.btnIssue.CausesValidation = false;
            this.btnIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnIssue.Image = global::DVLD.Properties.Resources.New_Driving_License_321;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(769, 564);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(142, 40);
            this.btnIssue.TabIndex = 85;
            this.btnIssue.Text = "Issue";
            this.btnIssue.UseVisualStyleBackColor = false;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // crtlClose
            // 
            this.crtlClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.crtlClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crtlClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crtlClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.crtlClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crtlClose.Location = new System.Drawing.Point(610, 564);
            this.crtlClose.Name = "crtlClose";
            this.crtlClose.Size = new System.Drawing.Size(142, 40);
            this.crtlClose.TabIndex = 84;
            this.crtlClose.Text = "Close";
            this.crtlClose.UseVisualStyleBackColor = false;
            this.crtlClose.Click += new System.EventHandler(this.crtlClose_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD.Properties.Resources.Notes_32;
            this.pictureBox8.Location = new System.Drawing.Point(82, 420);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(34, 34);
            this.pictureBox8.TabIndex = 83;
            this.pictureBox8.TabStop = false;
            // 
            // frm_Issue_Driver_License_For_the_First_Time
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(923, 617);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.crtlClose);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.tbNotes);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ctrlLocalDrivingLicenseApplicationInfo1);
            this.Name = "frm_Issue_Driver_License_For_the_First_Time";
            this.Text = "Issue Driver License For the First Time";
            this.Load += new System.EventHandler(this.frm_Issue_Driver_License_For_the_First_Time_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Applications.LocalDrivingLicenseApplication.Controls.ctrlLocalDrivingLicenseApplicationInfo ctrlLocalDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Button crtlClose;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnIssue;
    }
}