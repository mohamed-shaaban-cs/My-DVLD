namespace DVLD.Licenses.Local_License
{
    partial class frmLocalDirverLicenseInfo
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.crtlClose = new System.Windows.Forms.Button();
            this.ctrlLocalDriverLicenseInfo1 = new DVLD.Licenses.Local_License.Controls.ctrlLocalDriverLicenseInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(353, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 41);
            this.label1.TabIndex = 14;
            this.label1.Text = "Driver License Info";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(133)))), ((int)(((byte)(198)))));
            this.pictureBox1.BackgroundImage = global::DVLD.Properties.Resources.LicenseView_400;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1006, 97);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // crtlClose
            // 
            this.crtlClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.crtlClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crtlClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crtlClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.crtlClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crtlClose.Location = new System.Drawing.Point(866, 603);
            this.crtlClose.Name = "crtlClose";
            this.crtlClose.Size = new System.Drawing.Size(124, 35);
            this.crtlClose.TabIndex = 19;
            this.crtlClose.Text = "Close";
            this.crtlClose.UseVisualStyleBackColor = false;
            this.crtlClose.Click += new System.EventHandler(this.crtlClose_Click);
            // 
            // ctrlLocalDriverLicenseInfo1
            // 
            this.ctrlLocalDriverLicenseInfo1.Location = new System.Drawing.Point(12, 153);
            this.ctrlLocalDriverLicenseInfo1.Name = "ctrlLocalDriverLicenseInfo1";
            this.ctrlLocalDriverLicenseInfo1.Size = new System.Drawing.Size(978, 435);
            this.ctrlLocalDriverLicenseInfo1.TabIndex = 0;
            // 
            // frmLocalDirverLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1006, 650);
            this.Controls.Add(this.crtlClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ctrlLocalDriverLicenseInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLocalDirverLicenseInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmLocalDirverLicenseInfo";
            this.Load += new System.EventHandler(this.frmLocalDirverLicenseInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlLocalDriverLicenseInfo ctrlLocalDriverLicenseInfo1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button crtlClose;
    }
}