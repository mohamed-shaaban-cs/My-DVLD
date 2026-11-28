namespace DVLD.Applications.InternationalDrivingLicenseApplication
{
    partial class frmNewInternationalLicenseApplication
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnIssue = new System.Windows.Forms.Button();
            this.crtlClose = new System.Windows.Forms.Button();
            this.lbllShowLicensesHistory = new System.Windows.Forms.LinkLabel();
            this.llblShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.ctrlInternationalDrivingLicenseApplicationInfo1 = new DVLD.Applications.InternationalDrivingLicenseApplication.Contols.ctrlInternationalDrivingLicenseApplicationInfo();
            this.ctrlLocalDriverLicenseInfoWithFilter1 = new DVLD.Licenses.Local_License.Controls.ctrlLocalDriverLicenseInfoWithFilter();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(366, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 41);
            this.label1.TabIndex = 33;
            this.label1.Text = "Manage Drivers";
            // 
            // btnIssue
            // 
            this.btnIssue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(133)))), ((int)(((byte)(198)))));
            this.btnIssue.CausesValidation = false;
            this.btnIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnIssue.Image = global::DVLD.Properties.Resources.International_321;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(852, 859);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(142, 40);
            this.btnIssue.TabIndex = 87;
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
            this.crtlClose.Location = new System.Drawing.Point(693, 859);
            this.crtlClose.Name = "crtlClose";
            this.crtlClose.Size = new System.Drawing.Size(142, 40);
            this.crtlClose.TabIndex = 86;
            this.crtlClose.Text = "Close";
            this.crtlClose.UseVisualStyleBackColor = false;
            this.crtlClose.Click += new System.EventHandler(this.crtlClose_Click);
            // 
            // lbllShowLicensesHistory
            // 
            this.lbllShowLicensesHistory.AutoSize = true;
            this.lbllShowLicensesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllShowLicensesHistory.Location = new System.Drawing.Point(12, 862);
            this.lbllShowLicensesHistory.Name = "lbllShowLicensesHistory";
            this.lbllShowLicensesHistory.Size = new System.Drawing.Size(190, 20);
            this.lbllShowLicensesHistory.TabIndex = 88;
            this.lbllShowLicensesHistory.TabStop = true;
            this.lbllShowLicensesHistory.Text = "Show Licenses History";
            this.lbllShowLicensesHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbllShowLicenseHistory_LinkClicked);
            // 
            // llblShowLicenseInfo
            // 
            this.llblShowLicenseInfo.AutoSize = true;
            this.llblShowLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblShowLicenseInfo.Location = new System.Drawing.Point(199, 862);
            this.llblShowLicenseInfo.Name = "llblShowLicenseInfo";
            this.llblShowLicenseInfo.Size = new System.Drawing.Size(157, 20);
            this.llblShowLicenseInfo.TabIndex = 89;
            this.llblShowLicenseInfo.TabStop = true;
            this.llblShowLicenseInfo.Text = "Show License Info";
            this.llblShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblShowLicenseInfo_LinkClicked);
            // 
            // ctrlInternationalDrivingLicenseApplicationInfo1
            // 
            this.ctrlInternationalDrivingLicenseApplicationInfo1.ApplicationID = -1;
            this.ctrlInternationalDrivingLicenseApplicationInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ctrlInternationalDrivingLicenseApplicationInfo1.InternationalLicenseID = -1;
            this.ctrlInternationalDrivingLicenseApplicationInfo1.LocalLicenseID = -1;
            this.ctrlInternationalDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(10, 598);
            this.ctrlInternationalDrivingLicenseApplicationInfo1.Name = "ctrlInternationalDrivingLicenseApplicationInfo1";
            this.ctrlInternationalDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(984, 248);
            this.ctrlInternationalDrivingLicenseApplicationInfo1.TabIndex = 34;
            // 
            // ctrlLocalDriverLicenseInfoWithFilter1
            // 
            this.ctrlLocalDriverLicenseInfoWithFilter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ctrlLocalDriverLicenseInfoWithFilter1.FilterEnabled = true;
            this.ctrlLocalDriverLicenseInfoWithFilter1.Location = new System.Drawing.Point(12, 65);
            this.ctrlLocalDriverLicenseInfoWithFilter1.Name = "ctrlLocalDriverLicenseInfoWithFilter1";
            this.ctrlLocalDriverLicenseInfoWithFilter1.Size = new System.Drawing.Size(984, 527);
            this.ctrlLocalDriverLicenseInfoWithFilter1.TabIndex = 0;
            this.ctrlLocalDriverLicenseInfoWithFilter1.OnLicenseSelected += new System.Action<int>(this.ctrlLocalDriverLicenseInfoWithFilter1_OnLicenseSelected);
            // 
            // frmNewInternationalLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1006, 909);
            this.Controls.Add(this.llblShowLicenseInfo);
            this.Controls.Add(this.lbllShowLicensesHistory);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.crtlClose);
            this.Controls.Add(this.ctrlInternationalDrivingLicenseApplicationInfo1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlLocalDriverLicenseInfoWithFilter1);
            this.Name = "frmNewInternationalLicenseApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NewInternationalLicenseApplication";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Licenses.Local_License.Controls.ctrlLocalDriverLicenseInfoWithFilter ctrlLocalDriverLicenseInfoWithFilter1;
        private System.Windows.Forms.Label label1;
        private Contols.ctrlInternationalDrivingLicenseApplicationInfo ctrlInternationalDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button crtlClose;
        private System.Windows.Forms.LinkLabel lbllShowLicensesHistory;
        private System.Windows.Forms.LinkLabel llblShowLicenseInfo;
    }
}