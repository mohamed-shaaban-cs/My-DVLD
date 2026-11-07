namespace DVLD.Tests
{
    partial class frmScheduledTest
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
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.rbPass = new System.Windows.Forms.RadioButton();
            this.rbFail = new System.Windows.Forms.RadioButton();
            this.lable6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblAppointmentLoackedMassage = new System.Windows.Forms.Label();
            this.crtlClose = new System.Windows.Forms.Button();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.scheduledTest1 = new DVLD.Tests.Controls.ScheduledTest();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNotes
            // 
            this.tbNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNotes.Location = new System.Drawing.Point(154, 626);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(399, 103);
            this.tbNotes.TabIndex = 78;
            // 
            // rbPass
            // 
            this.rbPass.AutoSize = true;
            this.rbPass.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbPass.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.rbPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbPass.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rbPass.Location = new System.Drawing.Point(154, 582);
            this.rbPass.Name = "rbPass";
            this.rbPass.Size = new System.Drawing.Size(62, 24);
            this.rbPass.TabIndex = 77;
            this.rbPass.Text = "Pass";
            this.rbPass.UseVisualStyleBackColor = true;
            // 
            // rbFail
            // 
            this.rbFail.AutoSize = true;
            this.rbFail.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbFail.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.rbFail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbFail.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rbFail.Location = new System.Drawing.Point(222, 582);
            this.rbFail.Name = "rbFail";
            this.rbFail.Size = new System.Drawing.Size(52, 24);
            this.rbFail.TabIndex = 76;
            this.rbFail.Text = "Fail";
            this.rbFail.UseVisualStyleBackColor = true;
            // 
            // lable6
            // 
            this.lable6.AutoSize = true;
            this.lable6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lable6.Location = new System.Drawing.Point(38, 584);
            this.lable6.Name = "lable6";
            this.lable6.Size = new System.Drawing.Size(63, 20);
            this.lable6.TabIndex = 72;
            this.lable6.Text = "Result :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Location = new System.Drawing.Point(42, 626);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 20);
            this.label12.TabIndex = 73;
            this.label12.Text = "Notes :";
            // 
            // lblAppointmentLoackedMassage
            // 
            this.lblAppointmentLoackedMassage.AutoSize = true;
            this.lblAppointmentLoackedMassage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentLoackedMassage.ForeColor = System.Drawing.Color.Brown;
            this.lblAppointmentLoackedMassage.Location = new System.Drawing.Point(310, 584);
            this.lblAppointmentLoackedMassage.Name = "lblAppointmentLoackedMassage";
            this.lblAppointmentLoackedMassage.Size = new System.Drawing.Size(243, 20);
            this.lblAppointmentLoackedMassage.TabIndex = 71;
            this.lblAppointmentLoackedMassage.Text = "You Cannot Change The Results";
            // 
            // crtlClose
            // 
            this.crtlClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.crtlClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crtlClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crtlClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.crtlClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.crtlClose.Location = new System.Drawing.Point(251, 746);
            this.crtlClose.Name = "crtlClose";
            this.crtlClose.Size = new System.Drawing.Size(142, 40);
            this.crtlClose.TabIndex = 80;
            this.crtlClose.Text = "Close";
            this.crtlClose.UseVisualStyleBackColor = false;
            this.crtlClose.Click += new System.EventHandler(this.crtlClose_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD.Properties.Resources.Notes_32;
            this.pictureBox8.Location = new System.Drawing.Point(107, 626);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(34, 33);
            this.pictureBox8.TabIndex = 79;
            this.pictureBox8.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(133)))), ((int)(((byte)(198)))));
            this.btnSave.CausesValidation = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSave.Image = global::DVLD.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(411, 746);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(142, 40);
            this.btnSave.TabIndex = 75;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::DVLD.Properties.Resources.Number_32;
            this.pictureBox10.Location = new System.Drawing.Point(107, 580);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(34, 33);
            this.pictureBox10.TabIndex = 74;
            this.pictureBox10.TabStop = false;
            // 
            // scheduledTest1
            // 
            this.scheduledTest1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.scheduledTest1.Location = new System.Drawing.Point(4, 11);
            this.scheduledTest1.Name = "scheduledTest1";
            this.scheduledTest1.Size = new System.Drawing.Size(565, 560);
            this.scheduledTest1.TabIndex = 81;
            // 
            // frmScheduledTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(574, 802);
            this.Controls.Add(this.scheduledTest1);
            this.Controls.Add(this.crtlClose);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.tbNotes);
            this.Controls.Add(this.rbPass);
            this.Controls.Add(this.rbFail);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.lable6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblAppointmentLoackedMassage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmScheduledTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Take Test";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button crtlClose;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.RadioButton rbPass;
        private System.Windows.Forms.RadioButton rbFail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label lable6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblAppointmentLoackedMassage;
        private Controls.ScheduledTest scheduledTest1;
    }
}