using DVLD_BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Test_Types
{
    public partial class frmUpdateTestType : Form
    {
        int _ID;
        clsTestType _clsTestType;
        public frmUpdateTestType(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            lblID.Text = _ID.ToString();
            _clsTestType = clsTestType.Find((clsTestType.enTestType)_ID);

            if (_clsTestType == null)
            {
                MessageBox.Show("Test Type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {
                tbTitle.Text = _clsTestType.TestTypeTitle;
                tbDescription.Text = _clsTestType.TestTypeDescription;
                tbFees.Text = _clsTestType.TestTypeFees.ToString("F2");
            }
        }
        private void tbTitle_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(tbTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbTitle, "Title cannot be empty.");
            }
            else
            {
                errorProvider1.SetError(tbTitle, "");
            }
        }

        private void tbDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbDescription, "Description cannot be empty.");
            }
            else
            {
                errorProvider1.SetError(tbDescription, "");
            }
        }

        private void tbFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(tbFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbFees, "Fees must be a valid non-negative number.");
            }
            else
            {
                errorProvider1.SetError(tbFees, "");
            }

            if(!clsValidation.IsNumber(tbFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbFees, "Fees must be a valid non-negative number.");
            }
            else
            {
                errorProvider1.SetError(tbFees, "");
            }
    }

        private void btnSavePersonData_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are Invalid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _clsTestType.TestTypeTitle = tbTitle.Text;
            _clsTestType.TestTypeDescription = tbDescription.Text;
            _clsTestType.TestTypeFees = decimal.Parse(tbFees.Text);
            if (MessageBox.Show("Are you sure you want to update the Test Type?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (_clsTestType.Save())
            {
                MessageBox.Show("Test Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update Test Type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
