using DVLD.Global_Classes;
using DVLD_BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
         string UserSttingsFilePath = "user_settings.txt";
 
        public frmLogin()
        {
            InitializeComponent();
           
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {

            string username = "";
            string password = "";
            if(clsGlobal.GetStoredCredential(ref username, ref password))
            {
                txtUserName.Text = username;
                txtPassword.Text = password;
                cbRememberMe.Checked = true;
            }
            else
                cbRememberMe.Checked = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private bool LogIn()
        {
            
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!clsUser.IsUserAndPasswordExist(txtUserName.Text, txtPassword.Text))
            {
                // Login failed
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!clsUser.IsUserActive(txtUserName.Text))
            {
                //Login failed
                MessageBox.Show("Your Account is Deactivated.Please Contact Your Administrator.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //clsGlobal.CurrentUser = clsUser.FindByPersonID(txtUserName.Text);
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            
            if(user != null)
            {
                
                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your Account is Deactivated.Please Contact Your Administrator.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text, txtPassword.Text);
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("","");
                }

                clsGlobal.CurrentUser = user;
                frmDVLDMain frm = new frmDVLDMain(this);
                this.Hide();
                frm.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
