using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLogic;

namespace DVLD.Applications.Applicatino_Types
{
    public partial class frmUpdateApplicationType : Form
    {
        int _ID;
        clsApplicationType _applicationType;
        public frmUpdateApplicationType(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }
        
        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            lblID.Text = _ID.ToString();
            _applicationType = clsApplicationType.Find(_ID);

            if (_applicationType == null)
            {
                MessageBox.Show("Application Type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {
                tbTitle.Text = _applicationType.ApplicationTypeTitle;
                tbFees.Text = _applicationType.ApplicationFees.ToString("F2");
            }
        }

        private void btnSavePersonData_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are Invalid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _applicationType.ApplicationTypeTitle = tbTitle.Text;
            _applicationType.ApplicationFees = decimal.Parse(tbFees.Text);

            if(MessageBox.Show("Are you sure you want to update the Application Type?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            
            if (_applicationType.Save())
            {
                MessageBox.Show("Application Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update Application Type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbTitle, "Title cannot be empty.");
            }
            else
            {

                errorProvider1.SetError(tbTitle, "");
            }
        }

        private void tbFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbFees, "Fees cannot be empty.");
            }
            else
            {
                errorProvider1.SetError(tbFees, "");
            }

            if (clsValidation.IsNumber(tbFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbFees, "Invalid fees format.");
            }
            else
            {
                
                errorProvider1.SetError(tbFees, "");
            }
        }
    }
}
