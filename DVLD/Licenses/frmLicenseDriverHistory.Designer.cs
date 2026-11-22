namespace DVLD.Licenses
{
    partial class frmLicenseDriverHistory
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
            this.ctrlDriverLicensesHistory1 = new DVLD.Licenses.Controls.ctrlDriverLicensesHistory();
            this.ctrlPersonCardWithFilter1 = new DVLD.People.Controls.ctrlPersonCardWithFilter();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(526, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 41);
            this.label1.TabIndex = 14;
            this.label1.Text = "License History";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::DVLD.Properties.Resources.PersonLicenseHistory_512;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(12, 226);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(207, 297);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // crtlClose
            // 
            this.crtlClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.crtlClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crtlClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crtlClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.crtlClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crtlClose.Location = new System.Drawing.Point(1085, 913);
            this.crtlClose.Name = "crtlClose";
            this.crtlClose.Size = new System.Drawing.Size(139, 44);
            this.crtlClose.TabIndex = 19;
            this.crtlClose.Text = "Close";
            this.crtlClose.UseVisualStyleBackColor = false;
            this.crtlClose.Click += new System.EventHandler(this.crtlClose_Click);
            // 
            // ctrlDriverLicensesHistory1
            // 
            this.ctrlDriverLicensesHistory1.Location = new System.Drawing.Point(12, 550);
            this.ctrlDriverLicensesHistory1.Name = "ctrlDriverLicensesHistory1";
            this.ctrlDriverLicensesHistory1.Size = new System.Drawing.Size(1213, 354);
            this.ctrlDriverLicensesHistory1.TabIndex = 17;
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ctrlPersonCardWithFilter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(225, 71);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.ShowAddPersonButton = true;
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(1000, 474);
            this.ctrlPersonCardWithFilter1.TabIndex = 16;
            this.ctrlPersonCardWithFilter1.OnPersonSelected += new System.Action<int>(this.ctrlPersonCardWithFilter1_OnPersonSelected);
            // 
            // frmLicenseDriverHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1236, 965);
            this.Controls.Add(this.crtlClose);
            this.Controls.Add(this.ctrlDriverLicensesHistory1);
            this.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLicenseDriverHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmLicenseDriverHistory";
            this.Load += new System.EventHandler(this.frmLicenseDriverHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private People.Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private Controls.ctrlDriverLicensesHistory ctrlDriverLicensesHistory1;
        private System.Windows.Forms.Button crtlClose;
    }
}