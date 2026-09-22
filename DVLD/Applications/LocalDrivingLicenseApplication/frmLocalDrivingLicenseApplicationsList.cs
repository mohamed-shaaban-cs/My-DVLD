using DVLD_BusinessLogic;
using ModrenUI_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Applications.LocalDrivingLicenseApplication
{
    public partial class frmLocalDrivingLicenseApplicationsList : Form
    {
        DataTable dtAllLocalDrivingLicenseApplications;

        public frmLocalDrivingLicenseApplicationsList()
        {
            InitializeComponent();
            cbFilters.SelectedIndex = 0;
        }


        private void frmLocalDrivingLicenseApplicationsList_Load(object sender, EventArgs e)
        {
            _RefreshLocalDrivingLicenseApplicationsList();
            DataGridViewInterfacescs.DataGridViewModrenStayle(dgvList);

            dgvList.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "L.D.L.AppID";
            dgvList.Columns["ClassName"].HeaderText = "Driving Class";
            dgvList.Columns["NationalNo"].HeaderText = "National No.";
            dgvList.Columns["FullName"].HeaderText = "Full Name";
            dgvList.Columns["ApplicationDate"].HeaderText = "Application Date";
            dgvList.Columns["PassedTestCount"].HeaderText = "Passed Tests";
            dgvList.Columns["Status"].HeaderText = "Status";


        }

        void _RefreshLocalDrivingLicenseApplicationsList()
        {
            dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvList.DataSource = dtAllLocalDrivingLicenseApplications;
            lblRecordsCount.Text = dgvList.Rows.Count.ToString();
        }
        private void crtlNewLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void tbSeach_TextChanged(object sender, EventArgs e)
        {
            if (tbSeach.Text.Trim() == "" || tbSeach.Text == null)
            {
                dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvList.Rows.Count.ToString();
                return;
            }
            string filterColumn = "";
            switch (cbFilters.SelectedItem.ToString())
            {
                case "L.D.L.App.ID":
                    filterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    filterColumn = "NationalNo";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                case "Status":
                    filterColumn = "Status";
                    break;
                default:
                    filterColumn = cbFilters.SelectedItem.ToString();
                    break;
            }
            if (filterColumn == "LocalDrivingLicenseApplicationID")
                dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, tbSeach.Text.Trim());
            else if (filterColumn == "Status")
                dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", filterColumn, tbSeach.Text.Trim());
            else
                dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filterColumn, tbSeach.Text.Trim());

            lblRecordsCount.Text = dgvList.Rows.Count.ToString();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSeach.Visible = (cbFilters.SelectedItem.ToString() != "None" && cbFilters.SelectedItem.ToString() != "Status");
            cbStatus.Visible = (cbFilters.SelectedItem.ToString() == "Status");

            cbStatus.SelectedIndex = 0;
            tbSeach.Text = "";

            if (tbSeach.Visible)
                tbSeach.Focus();
            else
                cbStatus.Focus();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //chack if the data table is null or has no rows
            if (dtAllLocalDrivingLicenseApplications == null || dtAllLocalDrivingLicenseApplications.Rows.Count == 0)
                return;

            string StatusType = "All";
            switch (cbStatus.SelectedItem.ToString())
            {
                case "New":
                    StatusType = "New";
                    break;
                case "Cancelled":
                    StatusType = "Cancelled";
                    break;
                case "Completed":
                    StatusType = "Completed";
                    break;
                default:
                    StatusType = "All";
                    break;
            }
            if (cbStatus.SelectedItem.ToString() == "All")
                dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            else
                dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", "Status", StatusType);

            lblRecordsCount.Text = dgvList.Rows.Count.ToString();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
