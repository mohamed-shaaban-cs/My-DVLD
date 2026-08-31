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

namespace DVLD.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        int _UserID = -1;
        clsUser _User = new clsUser();

        public int UserID { get { return _UserID; } }
        public clsUser User {  get { return _User; } }
        public ctrlUserCard()
        {
            InitializeComponent();
        }
        void _ResetUserInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            _UserID = -1;
            lblUserID.Text = "[????]";
            lblUsername.Text = "[????]";
            lblIsActive.Text = "[????]";
        }

        void _FillUserInfo()
        {

            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text =_User.UserID.ToString();
            lblUsername.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";
        }



        public bool LoadInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            _FillUserInfo();
            return true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
