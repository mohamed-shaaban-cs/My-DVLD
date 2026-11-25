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
            this.ctrlLocalDriverLicenseInfoWithFilter1 = new DVLD.Licenses.Local_License.Controls.ctrlLocalDriverLicenseInfoWithFilter();
            this.SuspendLayout();
            // 
            // ctrlLocalDriverLicenseInfoWithFilter1
            // 
            this.ctrlLocalDriverLicenseInfoWithFilter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ctrlLocalDriverLicenseInfoWithFilter1.Location = new System.Drawing.Point(12, 12);
            this.ctrlLocalDriverLicenseInfoWithFilter1.Name = "ctrlLocalDriverLicenseInfoWithFilter1";
            this.ctrlLocalDriverLicenseInfoWithFilter1.Size = new System.Drawing.Size(984, 527);
            this.ctrlLocalDriverLicenseInfoWithFilter1.TabIndex = 0;
            // 
            // frmNewInternationalLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 600);
            this.Controls.Add(this.ctrlLocalDriverLicenseInfoWithFilter1);
            this.Name = "frmNewInternationalLicenseApplication";
            this.Text = "NewInternationalLicenseApplication";
            this.ResumeLayout(false);

        }

        #endregion

        private Licenses.Local_License.Controls.ctrlLocalDriverLicenseInfoWithFilter ctrlLocalDriverLicenseInfoWithFilter1;
    }
}