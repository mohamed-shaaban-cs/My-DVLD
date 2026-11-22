using DVLD.Applications.LocalDrivingLicenseApplication.Controls;
using DVLD.Global_Classes;
using DVLD.People.Controls;
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
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplicationInfo == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_LocalDrivingLicenseApplicationInfo.PassedAllTests())
            {
                MessageBox.Show("Person Should Pass All Tests First",
                                "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            int LicenseID = _LocalDrivingLicenseApplicationInfo.GetActiveLicenseID();
            if (LicenseID != -1)
            {
                MessageBox.Show($"This person already Has an active license Before with License ID {LicenseID}", "Active License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = ctrlLocalDrivingLicenseApplicationInfo1.LocalDrivingLicenseApplication.IssueDriverLicenseFirstTime(tbNotes.Text, clsGlobal.CurrentUser.UserID);
            
            if(LicenseID == -1)
                MessageBox.Show("Failed to issue the license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("License issued successfully With License ID: " + LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
        }
    }
}        

