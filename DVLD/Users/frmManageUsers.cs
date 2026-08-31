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
    public partial class frmManageUsers : Form
    {
        DataTable _dtUsers = new DataTable();
        public frmManageUsers()
        {
            InitializeComponent();
            cbFilters.SelectedIndex = 0;
            cbIsActive.SelectedIndex = 0;

        }
        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            ModrenUI_Interface.DataGridViewInterfacescs.DataGridViewModrenStayle(dgvUsers);
            if(dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;
                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 110;
                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 110;
                dgvUsers.Columns[3].HeaderText = "Username";
                dgvUsers.Columns[3].Width = 110;
                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 110;
            }
        }

        void _RefreshUsersList()
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtUsers;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tbSeach_TextChanged(object sender, EventArgs e)
        {
            string filterField = "";

            switch (cbFilters.SelectedItem.ToString())
            {
                case "User ID":
                    filterField = "UserID";
                    break;
                case "Person ID":
                    filterField = "PersonID";
                    break;
                case "Full Name":
                    filterField = "FullName";
                    break;
                case "User Name":
                    filterField = "UserName";
                    break;              
                default:
                    filterField = "None";
                    break;
            }

            if(tbSeach.Text.Trim() == "" || filterField == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if (filterField== "UserID" || filterField == "PersonID")
            _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterField, tbSeach.Text.Trim());
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filterField, tbSeach.Text.Trim());

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSeach.Visible = (cbFilters.SelectedItem.ToString() != "None" && cbFilters.SelectedItem.ToString() != "Is Active");
            cbIsActive.Visible = (cbFilters.SelectedItem.ToString() == "Is Active");

            cbIsActive.SelectedIndex = 0;
            tbSeach.Text = "";

            if (tbSeach.Visible)
                tbSeach.Focus();
            else 
                cbIsActive.Focus();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool IsActive = false;

            switch (cbIsActive.Text)
            {
                case "Yes":
                    IsActive = true;
                    break;
                case "No":
                    IsActive = false;
                    break;
            }
            if (cbIsActive.SelectedItem.ToString() == "All")
                _dtUsers.DefaultView.RowFilter = "";
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", IsActive);

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void crtlAddNewUser_Click(object sender, EventArgs e)
        {
            frmAdd_UpdateUser frmAdd_UpdateUser = new frmAdd_UpdateUser();
            frmAdd_UpdateUser.DataBackEvent += FrmAdd_UpdateUser_DataBackEvent;
            frmAdd_UpdateUser.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crtlAddNewUser_Click (sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dgvUsers.SelectedRows[0].Cells["UserID"].Value.ToString());
            frmAdd_UpdateUser frmAdd_UpdateUser = new frmAdd_UpdateUser(UserID);
            frmAdd_UpdateUser.DataBackEvent += FrmAdd_UpdateUser_DataBackEvent;
            frmAdd_UpdateUser.ShowDialog();
        }

        void FrmAdd_UpdateUser_DataBackEvent(object sender,int PersonID)
        {
            frmManageUsers_Load(null, null);
        }

        private void tbSeach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedItem.ToString() == "User ID" || cbFilters.SelectedItem.ToString() == "Person ID")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            int UserID = int.Parse(dgvUsers.SelectedRows[0].Cells["UserID"].Value.ToString());
            clsUser.DeleteUser(UserID);
            _RefreshUsersList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dgvUsers.SelectedRows[0].Cells["UserID"].Value.ToString());
            frmUserDetails frmUserDetails = new frmUserDetails(UserID);
            frmUserDetails.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dgvUsers.SelectedRows[0].Cells["UserID"].Value.ToString());
            frmChangePassword frmChangePassword = new frmChangePassword(UserID);
            frmChangePassword.ShowDialog();
        }
    }
}
