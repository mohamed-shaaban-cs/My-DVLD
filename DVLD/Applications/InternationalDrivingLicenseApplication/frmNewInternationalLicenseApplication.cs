using DVLD.Global_Classes;
using DVLD.Licenses;
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
using static DVLD_BusinessLogic.clsApplication;

namespace DVLD.Applications.InternationalDrivingLicenseApplication
{
    public partial class frmNewInternationalLicenseApplication : Form
    {

        clsInternationalLicense _InternationalLicense;
        clsApplication _Application;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        bool _HandleLicenseClass()
        {
            if (ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.LicenseClassID != 3)
            {
                MessageBox.Show($"Selected license should be class 3 ", "Invalid License Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return false;
            }
            else
            {
                btnIssue.Enabled = true;
                return true;
            }
        }
        bool _HandleIsLicenseActive()
        {
            if (!ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.IsActive)
            {
                MessageBox.Show($"this License Is Not Active ", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return false;
            }
            else
            {
                btnIssue.Enabled = true;
                return true;
            }
        }
        bool _HandleIsLicenseDetained()
        {
            if (ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.IsDetained)
            {
                MessageBox.Show($"this License Is Detained ", "Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return false;
            }
            else
            {
                btnIssue.Enabled = true;
                return true;
            }
        }

        bool _HandleIsLicenseExpired()
        {
            if (ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("This license is expired. Please renew it before proceeding.", "License Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIssue.Enabled = false;
                return false;
            }
            else
            {
                btnIssue.Enabled = true;
                return true;
            }
        }

        private void ctrlLocalDriverLicenseInfoWithFilter1_FindClick(object sender, EventArgs e)
        {
            if(ctrlLocalDriverLicenseInfoWithFilter1.LocalLicenseID == -1)
            {
                btnIssue.Enabled = false;
                lbllShowLicensesHistory.Enabled = false;
                llblShowLicenseInfo.Enabled = false;
                ctrlInternationalDrivingLicenseApplicationInfo1.ResetDefultValues();
                return;
            }
            ctrlInternationalDrivingLicenseApplicationInfo1.LocalLicenseID = ctrlLocalDriverLicenseInfoWithFilter1.LocalLicenseID;

            _InternationalLicense = clsInternationalLicense.FindByDriverID(ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.DriverID);
            if(_InternationalLicense != null)
            {
                MessageBox.Show("This person was already issued an international license.", "Already Issued", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                llblShowLicenseInfo.Enabled = true;
                lbllShowLicensesHistory.Enabled = true;
                return;
            }
            else
            {
                btnIssue.Enabled = true;
                llblShowLicenseInfo.Enabled = false;
            }

            if (!_HandleLicenseClass())
                return;

            if (!_HandleIsLicenseActive())
                return;

            if (!_HandleIsLicenseExpired())
                return;

            if (!_HandleIsLicenseDetained())
                return;

        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbllShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense == null)
            {
                MessageBox.Show("Error: Select Local License First.", "Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmLicenseDriverHistory frm = new frmLicenseDriverHistory(ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense == null)
            {
                MessageBox.Show("Error: Select Local License First.", "Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DialogResult.No == MessageBox.Show("are you sure you want to issue License ?", "Issue Licenes", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            _Application = new clsApplication();

            _Application.ApplicantPersonID = ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.DriverInfo.PersonID;
            _Application.ApplicationDate = DateTime.Now;
            _Application.ApplicationTypeID = ctrlInternationalDrivingLicenseApplicationInfo1.ApplicationType.ApplicationTypeID;
            _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _Application.LastStatusDate = _Application.ApplicationDate;
            _Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (!_Application.Save())
            {
                MessageBox.Show("Error: Application was not saved.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlInternationalDrivingLicenseApplicationInfo1.ApplicationID = _Application.ApplicationID;

            _InternationalLicense = new clsInternationalLicense();

            _InternationalLicense.ApplicationID = _Application.ApplicationID;
            _InternationalLicense.DriverID = ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.DriverID;
            _InternationalLicense.IssuedUsingLocalLicenseID = ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense.LicenseID;
            _InternationalLicense.IssueDate = DateTime.Now;
            _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _InternationalLicense.IsActive = true;
            _InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!_InternationalLicense.Save())
            {
                MessageBox.Show("Failed to issue the International License.", "Issue Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlInternationalDrivingLicenseApplicationInfo1.InternationalLicenseID = _InternationalLicense.InternationalLicenseID;

            btnIssue.Enabled = false;
            llblShowLicenseInfo.Enabled = true;
            lbllShowLicensesHistory.Enabled = true;
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlLocalDriverLicenseInfoWithFilter1.LocalLicense == null)
            {
                MessageBox.Show("Error: Select Local License First.", "Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
