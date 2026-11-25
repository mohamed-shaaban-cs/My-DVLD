namespace DVLD.Licenses.Controls
{
    partial class ctrlDriverLicensesHistory
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tcLicenseHistory = new System.Windows.Forms.TabControl();
            this.tpLocal = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLocalRecords = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvLocalList = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tpInternational = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInternationalRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvInternationalList = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.tcLicenseHistory.SuspendLayout();
            this.tpLocal.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tpInternational.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.groupBox1.Controls.Add(this.tcLicenseHistory);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Emoji", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(248)))), ((int)(((byte)(253)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1219, 354);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // tcLicenseHistory
            // 
            this.tcLicenseHistory.Controls.Add(this.tpLocal);
            this.tcLicenseHistory.Controls.Add(this.tpInternational);
            this.tcLicenseHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcLicenseHistory.Font = new System.Drawing.Font("Segoe UI Emoji", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcLicenseHistory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tcLicenseHistory.Location = new System.Drawing.Point(3, 29);
            this.tcLicenseHistory.Name = "tcLicenseHistory";
            this.tcLicenseHistory.SelectedIndex = 0;
            this.tcLicenseHistory.Size = new System.Drawing.Size(1213, 322);
            this.tcLicenseHistory.TabIndex = 0;
            // 
            // tpLocal
            // 
            this.tpLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.tpLocal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tpLocal.Controls.Add(this.label1);
            this.tpLocal.Controls.Add(this.lblLocalRecords);
            this.tpLocal.Controls.Add(this.label5);
            this.tpLocal.Controls.Add(this.panel2);
            this.tpLocal.Location = new System.Drawing.Point(4, 29);
            this.tpLocal.Name = "tpLocal";
            this.tpLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocal.Size = new System.Drawing.Size(1205, 289);
            this.tpLocal.TabIndex = 0;
            this.tpLocal.Text = "Local";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "Local Licenses History :";
            // 
            // lblLocalRecords
            // 
            this.lblLocalRecords.AutoSize = true;
            this.lblLocalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalRecords.ForeColor = System.Drawing.Color.White;
            this.lblLocalRecords.Location = new System.Drawing.Point(139, 261);
            this.lblLocalRecords.Name = "lblLocalRecords";
            this.lblLocalRecords.Size = new System.Drawing.Size(20, 24);
            this.lblLocalRecords.TabIndex = 28;
            this.lblLocalRecords.Text = "4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(19, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 24);
            this.label5.TabIndex = 27;
            this.label5.Text = "# Records :  ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvLocalList);
            this.panel2.Location = new System.Drawing.Point(14, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1176, 216);
            this.panel2.TabIndex = 26;
            // 
            // dgvLocalList
            // 
            this.dgvLocalList.AllowUserToAddRows = false;
            this.dgvLocalList.AllowUserToDeleteRows = false;
            this.dgvLocalList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvLocalList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLocalList.Location = new System.Drawing.Point(0, 0);
            this.dgvLocalList.Name = "dgvLocalList";
            this.dgvLocalList.ReadOnly = true;
            this.dgvLocalList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalList.Size = new System.Drawing.Size(1176, 216);
            this.dgvLocalList.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.LightSlateGray;
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowLicense});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 42);
            // 
            // ShowLicense
            // 
            this.ShowLicense.Image = global::DVLD.Properties.Resources.License_View_32;
            this.ShowLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowLicense.Name = "ShowLicense";
            this.ShowLicense.Size = new System.Drawing.Size(182, 38);
            this.ShowLicense.Text = "Show License";
            this.ShowLicense.Click += new System.EventHandler(this.ShowLicense_Click);
            // 
            // tpInternational
            // 
            this.tpInternational.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(40)))));
            this.tpInternational.Controls.Add(this.label2);
            this.tpInternational.Controls.Add(this.lblInternationalRecords);
            this.tpInternational.Controls.Add(this.label3);
            this.tpInternational.Controls.Add(this.panel1);
            this.tpInternational.Location = new System.Drawing.Point(4, 29);
            this.tpInternational.Name = "tpInternational";
            this.tpInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternational.Size = new System.Drawing.Size(1205, 289);
            this.tpInternational.TabIndex = 1;
            this.tpInternational.Text = "International";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(17, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 24);
            this.label2.TabIndex = 25;
            this.label2.Text = "International Licenses History :";
            // 
            // lblInternationalRecords
            // 
            this.lblInternationalRecords.AutoSize = true;
            this.lblInternationalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternationalRecords.ForeColor = System.Drawing.Color.White;
            this.lblInternationalRecords.Location = new System.Drawing.Point(137, 262);
            this.lblInternationalRecords.Name = "lblInternationalRecords";
            this.lblInternationalRecords.Size = new System.Drawing.Size(20, 24);
            this.lblInternationalRecords.TabIndex = 24;
            this.lblInternationalRecords.Text = "4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(17, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 24);
            this.label3.TabIndex = 23;
            this.label3.Text = "# Records :  ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvInternationalList);
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 216);
            this.panel1.TabIndex = 22;
            // 
            // dgvInternationalList
            // 
            this.dgvInternationalList.AllowUserToAddRows = false;
            this.dgvInternationalList.AllowUserToDeleteRows = false;
            this.dgvInternationalList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvInternationalList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInternationalList.Location = new System.Drawing.Point(0, 0);
            this.dgvInternationalList.Name = "dgvInternationalList";
            this.dgvInternationalList.ReadOnly = true;
            this.dgvInternationalList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInternationalList.Size = new System.Drawing.Size(1178, 216);
            this.dgvInternationalList.TabIndex = 2;
            // 
            // ctrlDriverLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlDriverLicensesHistory";
            this.Size = new System.Drawing.Size(1219, 354);
            this.groupBox1.ResumeLayout(false);
            this.tcLicenseHistory.ResumeLayout(false);
            this.tpLocal.ResumeLayout(false);
            this.tpLocal.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tpInternational.ResumeLayout(false);
            this.tpInternational.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tcLicenseHistory;
        private System.Windows.Forms.TabPage tpLocal;
        private System.Windows.Forms.TabPage tpInternational;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvInternationalList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLocalRecords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvLocalList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInternationalRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ShowLicense;
    }
}
