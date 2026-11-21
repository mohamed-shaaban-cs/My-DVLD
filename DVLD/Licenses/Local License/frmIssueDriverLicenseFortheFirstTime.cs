using DVLD.Applications.LocalDrivingLicenseApplication.Controls;
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

namespace DVLD.Licenses.Local_License
{
    public partial class frmIssueDriverLicenseFortheFirstTime : Form
    {
        int _LocalDrivingLicenseApplicationID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        public frmIssueDriverLicenseFortheFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }
        private void frm_Issue_Driver_License_For_the_First_Time_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplicationInfo = ctrlLocalDrivingLicenseApplicationInfo1.LocalDrivingLicenseApplication;
            clsDriver Driver = new clsDriver();
            Driver.PersonID = _LocalDrivingLicenseApplicationInfo.ApplicantPersonID;
            Driver.CreatedByUserID = Global_Classes.clsGlobal.CurrentUser.UserID;
            if (!Driver.Save())
            {
                MessageBox.Show("Failed to save driver information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsLicense License = new clsLicense();
            License.ApplicationID = _LocalDrivingLicenseApplicationInfo.ApplicationID;
            License.DriverID = Driver.DriverID;
            License.LicenseClassID = _LocalDrivingLicenseApplicationInfo._LicenseClassID;
            License.ExpirationDate = DateTime.Now.AddYears(_LocalDrivingLicenseApplicationInfo.LicenseClassInfo.DefaultValidityLength); // Example: 10-year expiration
            License.Notes = tbNotes.Text ;
            License.PaidFees = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassFees;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = Global_Classes.clsGlobal.CurrentUser.UserID;
            if(!License.Save())
            {
                MessageBox.Show("Failed to issue the license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("License issued successfully With License ID: " + License.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _LocalDrivingLicenseApplicationInfo.ApplicationStatus = clsLocalDrivingLicenseApplication.enApplicationStatus.Completed;
                _LocalDrivingLicenseApplicationInfo.LastStatusDate = DateTime.Now;
                _LocalDrivingLicenseApplicationInfo.Save();
                this.Close();
            }
        }
    }
}        

