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

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        int _UserID= -1;
        clsUser _User= null;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            if (!ctrlUserCard1.LoadInfo(_UserID))
                this.Close();
            _User = clsUser.Find(_UserID);
        }  
        
        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_User.Password != tbCurrentPassword.Text.Trim())
            {
                errorProvider1.SetError(tbCurrentPassword, "Current Password is incorrect");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(tbCurrentPassword, "");
            }
        }


        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            string Password = tbNewPassword.Text.Trim();
            if (string.IsNullOrEmpty(Password))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNewPassword, "Password Cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(tbNewPassword, "");
            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            string ConfirmPassword = tbConfirmPassword.Text.Trim();
            if (ConfirmPassword != tbNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "Passwords do not match");
                return;
            }
            else
            {
                errorProvider1.SetError(tbConfirmPassword, "");
            }
        }

        private void btnSavePersonData_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.Password = tbNewPassword.Text.Trim();
            if(_User.Save())
            {
                MessageBox.Show("Password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
