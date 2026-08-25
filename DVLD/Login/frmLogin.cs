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
            string rawData = CryptoHelper.DecryptFromFile(UserSttingsFilePath);
            if (!string.IsNullOrEmpty(rawData))
            {
                string[] parts = rawData.Split(new string[] { "#//#" }, StringSplitOptions.None);
                if (parts.Length == 3)
                {
                    txtUserName.Text = parts[0];
                    txtPassword.Text = parts[1];
                    cbRememberMe.Checked = bool.Parse(parts[2]);
                }
            }
            
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
            clsGlobal.CurrentUser = clsUser.Find(txtUserName.Text);
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if(LogIn())
            {
                if (cbRememberMe.Checked)
                {
                    string rawData = $"{txtUserName.Text}#//#{txtPassword.Text}#//#{cbRememberMe.Checked}";
                    CryptoHelper.EncryptAndSaveToFile(rawData, UserSttingsFilePath);
                }
                else
                {
                    if (File.Exists(UserSttingsFilePath))
                    {
                        File.WriteAllText(UserSttingsFilePath, string.Empty);
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
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
