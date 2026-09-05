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
        clsApplicationType _applicationType;
        public frmUpdateApplicationType(int ID)
        {
            InitializeComponent();
            _applicationType = clsApplicationType.Find(ID);
            if( _applicationType == null )
            {
                MessageBox.Show("Application Type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }
        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            lblID.Text = _applicationType.ApplicationTypeID.ToString();
            tbTitle.Text = _applicationType.ApplicationTypeTitle;
            tbFees.Text = _applicationType.ApplicationFees.ToString("F2");
        }

        private void btnSavePersonData_Click(object sender, EventArgs e)
        {
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

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.')); // Allow only digits and control characters
        }

        
    }
}
