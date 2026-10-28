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
                    filterColumn = "none";
                    break;
            }
            if (tbSeach.Text.Trim() == "" || filterColumn == "none")
            {
                dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvList.Rows.Count.ToString();
                return;
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
            int localDrivingLicenseApplicationID = (int)dgvList.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            frmLocalDrivingLicenseApplicationInfo frmInfo = new frmLocalDrivingLicenseApplicationInfo(localDrivingLicenseApplicationID);
            frmInfo.ShowDialog();   
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvList.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication(localDrivingLicenseApplicationID);
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int L_D_L_App_ID = (int)dgvList.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            if (MessageBox.Show("Are you sure you want to delete this Local Driving License Application?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(L_D_L_App_ID))
            {
                MessageBox.Show("Local Driving License Application deleted successfully.");
                _RefreshLocalDrivingLicenseApplicationsList();
            }
                
            else
                MessageBox.Show("Local Driving License Application deletion failed. Please try again.");
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID((int)dgvList.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value);

            int TotalPassedTests = clsLocalDrivingLicenseApplication.countPassedTests(localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);

            bool LicenseExists = false; // You need to implement a method to check if the license has been issued for this application.

            showDetailsToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = !LicenseExists && localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;  

            //We only allow to Delete or Cancel the Application if the Application status is New.
            deleteToolStripMenuItem.Enabled = localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;
            CancelApplicationToolStripMenuItem.Enabled = localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            SchduleTestsToolStripMenuItem.Enabled = !LicenseExists;

            IssueDrivingLicenseFirstTime.Enabled = !LicenseExists && TotalPassedTests == 3;
            ShowLicense.Enabled = LicenseExists;
            ShowPersonLicenseHistory.Enabled = true;

            bool PassedVisionTest = false; //not implemented yet, you need to implement a method to check if the applicant has passed the vision test for this application.
            bool PassedWrittenTest = false; //not implemented yet, you need to implement a method to check if the applicant has passed the written test for this application.
            bool PassedStreetTest = false; //not implemented yet, you need to implement a method to check if the applicant has passed the street test for this application.

            scheduleStreetTestToolStripMenuItem.Enabled = ( !PassedStreetTest || PassedVisionTest || PassedWrittenTest);
            if(SchduleTestsToolStripMenuItem.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = !PassedWrittenTest;
                scheduleStreetTestToolStripMenuItem.Enabled = !PassedStreetTest;
            }



        }

        private void CancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvList.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if(localDrivingLicenseApplication == null)
            {
                MessageBox.Show("Failed to find the application. Please try again.");
                return;
            }
            if (MessageBox.Show("Are you sure you want to cancel this application?", "Cancel Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (localDrivingLicenseApplication.Cancel())
            {
                MessageBox.Show("Application cancelled successfully.");
                _RefreshLocalDrivingLicenseApplicationsList();
            }
            else
                MessageBox.Show("Failed to cancel application. Please try again.");
        }
    }
}
