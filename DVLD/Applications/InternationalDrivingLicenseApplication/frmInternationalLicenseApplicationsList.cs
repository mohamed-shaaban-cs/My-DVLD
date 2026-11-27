using DVLD.Licenses;
using DVLD.Licenses.Internatinoal_License;
using DVLD.People;
using DVLD_BusinessLogic;
using ModrenUI_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.InternationalDrivingLicenseApplication
{
    public partial class frmInternationalLicenseApplicationsList : Form
    {
        DataTable _dtInternationalLicenseApplications;

        public frmInternationalLicenseApplicationsList()
        {
            InitializeComponent();
            cbFilters.SelectedIndex = 0;
        }
        
        void _RefreashList()
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            dgvList.DataSource = _dtInternationalLicenseApplications;
            lblRecordsCount.Text = dgvList.Rows.Count.ToString();
        }
        private void frmInternationalLicenseApplicationsList_Load(object sender, EventArgs e)
        {
            //InternationalLicensesID,ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            dgvList.DataSource = _dtInternationalLicenseApplications;
            lblRecordsCount.Text = dgvList.Rows.Count.ToString();

            DataGridViewInterfacescs.DataGridViewModernStayle(dgvList);
            if(dgvList.Rows.Count > 0)
            {
                if (dgvList.Rows.Count > 0)
                {
                    dgvList.Columns["InternationalLicensesID"].HeaderText = "Int.License ID";
                    dgvList.Columns["ApplicationID"].HeaderText = "Application ID";
                    dgvList.Columns["DriverID"].HeaderText = "Driver ID";
                    dgvList.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";
                    dgvList.Columns["IssueDate"].HeaderText = "Issue Date";
                    dgvList.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                    dgvList.Columns["IsActive"].HeaderText = "Is Active";
                }
            }


        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void crtlNewAppication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
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

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.Find((int)dgvList.SelectedRows[0].Cells["DriverID"].Value).PersonID;
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void ShowLicense_Click(object sender, EventArgs e)
        {
            frmInternationalDriverLicenseInfo frm = new frmInternationalDriverLicenseInfo((int)dgvList.SelectedRows[0].Cells["InternationalLicensesID"].Value);
            frm.ShowDialog();

        }

        private void ShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.Find((int)dgvList.SelectedRows[0].Cells["DriverID"].Value).PersonID;
            frmLicenseDriverHistory frm = new frmLicenseDriverHistory(PersonID);
            frm.ShowDialog();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            //chack if the data table is null or has no rows
            if (_dtInternationalLicenseApplications == null || _dtInternationalLicenseApplications.Rows.Count == 0)
                return;

            switch (cbIsActive.SelectedItem.ToString())
            {
                case "Yes":
                    _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", "IsActive", true );
                    break;
                case "No":
                    _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", "IsActive",false );
                    break;
                default:
                    _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                    break;
            }

            lblRecordsCount.Text = dgvList.Rows.Count.ToString();
        }

        private void tbSeach_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";
            switch (cbFilters.SelectedItem.ToString())
            {
                case "InternationalLicenses ID":
                    filterColumn = "InternationalLicensesID";
                    break;
                case "Application ID":
                    filterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    filterColumn = "DriverID";
                    break;
                case "Local License ID":
                    filterColumn = "IssuedUsingLocalLicenseID";
                    break;
                default:
                    filterColumn = "none";
                    break;
            }
            if (tbSeach.Text.Trim() == "" || filterColumn == "none")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvList.Rows.Count.ToString();
                return; 
            }
            else
            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, tbSeach.Text.Trim());


            lblRecordsCount.Text = dgvList.Rows.Count.ToString();
        }

        private void tbSeach_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }
    }
}
