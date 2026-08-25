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
    public partial class frmAdd_UpdateUser : Form
    {
        enum enMode { AddNew, Update }
        enMode _Mode;
        int _UserID;
        clsUser _User = new clsUser();

       //Declare a delegate
       public delegate void DataBackEventHandler(Object sender,int PersonID);

       //Declare an event using the delegate 
       public event DataBackEventHandler DataBackEvent;


        public frmAdd_UpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }
        public frmAdd_UpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
        }

        void _RestDefultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                tabLoginInfo.Enabled = false;

            }
            else if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                tbPassword.Enabled = false;
                tbConfirmPassword.Enabled = false;
            }
        }

        void _LoadData()
        {
            _User = clsUser.Find(_UserID);
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            tbUserName.Text = _User.UserName;
            tbPassword.Text = _User.Password;
            tbConfirmPassword.Text = _User.Password;
            chbIsActive.Checked = _User.IsActive;
        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void frmAdd_UpdateUser_Load(object sender, EventArgs e)
        {
            _RestDefultValues();
            if(_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(ctrlPersonCardWithFilter1.PersonID == -1 || ctrlPersonCardWithFilter1.PersonID == null)
            {
                MessageBox.Show("Please select a person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsUser.IsPersonHasAccount(ctrlPersonCardWithFilter1.PersonID) && _Mode == enMode.AddNew)
            {
                MessageBox.Show("This person is already a user. Please select another person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                tabLoginInfo.Enabled = true;
                tcAdd_UpdateUser.SelectTab(tabLoginInfo);
            }

            
        }

        private void btnSavePersonData_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = tbUserName.Text.Trim();
            _User.Password = tbPassword.Text.Trim();
            _User.IsActive = chbIsActive.Checked;

            if (MessageBox.Show("Are you sure you want to save the User data?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_User.Save())
                {
                    MessageBox.Show("User saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Mode = enMode.Update;
                    lblUserID.Text = _User.UserID.ToString();
                    _RestDefultValues();
                    DataBackEvent?.Invoke(this,_User.UserID);
                }
                else
                {
                    MessageBox.Show("Failed to save user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            string username = tbUserName.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbUserName, "Username Cannot be blank");
                return;
            }

            if (clsUser.IsUserExist(username) && _User.UserName != username)
            {
                e.Cancel = true;
                errorProvider1.SetError(tbUserName, "Username already exists. Please choose another username.");
                return;
            }
            else
            {
                errorProvider1.SetError(tbUserName, "");
            }
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            string Password = tbPassword.Text.Trim();
            if (string.IsNullOrEmpty(Password))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbPassword, "Password Cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(tbPassword, "");
            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            string ConfirmPassword = tbConfirmPassword.Text.Trim();
            if (ConfirmPassword != tbPassword.Text.Trim())
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

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
