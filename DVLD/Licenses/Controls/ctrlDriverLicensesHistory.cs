using DVLD.Licenses.Local_License;
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

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicensesHistory : UserControl
    {
        int _DriverID =-1;
        clsDriver _Driver;
        public clsDriver Driver { get { return _Driver; } }
        DataTable _dtLocalLicensesHistory;
        DataTable _dtInternationalLicensesHistory;
        public ctrlDriverLicensesHistory()
        {
            InitializeComponent();
        }
        void _LoadLocalLicensesInfo()
        {
            _dtLocalLicensesHistory = clsLicense.GetLicensesHistoryWithDriverID(_DriverID);
            dgvLocalList.DataSource = _dtLocalLicensesHistory;

            lblLocalRecords.Text = _dtLocalLicensesHistory.Rows.Count.ToString();


            DataGridViewInterfacescs.DataGridViewModernStayle(dgvLocalList);
            if (dgvLocalList.Rows.Count > 0)
            {

                dgvLocalList.Columns["LicenseID"].HeaderText = "lic.ID";
                dgvLocalList.Columns["LicenseID"].Width = 100;
                dgvLocalList.Columns["ApplicationID"].HeaderText = "App.ID";
                dgvLocalList.Columns["ApplicationID"].Width = 100;
                dgvLocalList.Columns["ClassName"].HeaderText = "Class Name";
                dgvLocalList.Columns["ClassName"].Width = 200;
                dgvLocalList.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvLocalList.Columns["IssueDate"].Width = 150;
                dgvLocalList.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvLocalList.Columns["ExpirationDate"].Width = 150;
                dgvLocalList.Columns["IsActive"].HeaderText = "Is Active";
                dgvLocalList.Columns["IsActive"].Width = 100;
            }

        }
        void _LoadInternationalLicensesInfo()
        {
            _dtInternationalLicensesHistory = clsInternationalLicense.GetInternationalLicensesHistoryWithDriverID(_DriverID);
            dgvInternationalList.DataSource = _dtInternationalLicensesHistory;

            lblInternationalRecords.Text = _dtInternationalLicensesHistory.Rows.Count.ToString();


            DataGridViewInterfacescs.DataGridViewModernStayle(dgvInternationalList);
            if (dgvInternationalList.Rows.Count > 0)
            {
                dgvInternationalList.Columns["InternationalLicenseID"].HeaderText = "Int.License ID";
                dgvInternationalList.Columns["InternationalLicenseID"].Width = 100;
                dgvInternationalList.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvInternationalList.Columns["ApplicationID"].Width = 100;
                dgvInternationalList.Columns["LicenseID"].HeaderText = "L.License ID";
                dgvInternationalList.Columns["LicenseID"].Width = 100;
                dgvInternationalList.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvInternationalList.Columns["IssueDate"].Width = 150;
                dgvInternationalList.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvInternationalList.Columns["ExpirationDate"].Width = 150;
                dgvInternationalList.Columns["IsActive"].HeaderText = "Is Active";
                dgvInternationalList.Columns["IsActive"].Width = 100;
            }


        }

        public void Clear()
        {
            dgvInternationalList.DataSource = null;
            dgvLocalList.DataSource = null;
        }
        public void LoadInfoByPersonID(int PersonID)
        {
             _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("There is no driver Linked With Person With Id = " + PersonID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            _DriverID = _Driver.DriverID;

            _LoadLocalLicensesInfo();
            _LoadInternationalLicensesInfo();
            
        }
        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.Find(DriverID);

            if (_Driver != null)
            {
                _LoadLocalLicensesInfo();
                _LoadInternationalLicensesInfo();
                return;
            }
            else
                MessageBox.Show("There is no driver Linked With Person With Id = " + DriverID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void ShowLicense_Click(object sender, EventArgs e)
        {
                frmLocalDirverLicenseInfo frm = new frmLocalDirverLicenseInfo((int)dgvLocalList.SelectedRows[0].Cells["LicenseID"].Value);
                frm.ShowDialog();
        }
    }
}
