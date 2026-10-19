using DVLD.People;
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

namespace DVLD.Applications.LocalDrivingLicenseApplication.Controls
{
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        int _LocalDrivingLicenseApplicationID;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
            
        }

        int _LicenseID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication ;
        

        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }


        void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;

            lblDLApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text= clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = $"{clsLocalDrivingLicenseApplication.countPassedTests(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID)}/3";
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);

            // Enable the link label only if the application status is Completed and have license class assigned
            llblShowLicenseInfo.Enabled = _LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.Completed;
        }

        public void ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            _LocalDrivingLicenseApplication = null;

            lblDLApplicationID.Text = "[????]";
            lblAppliedForLicense.Text = "[????]";
            lblPassedTests.Text = "0/3";
            llblShowLicenseInfo.Enabled = false; // Disable the link label when no application is loaded
        }
        public void LoadApplicationInfoByLocalDrivingLicenseAppID(int localDrivingLicenseApplicationID)
        {
            
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show($"Local Driving License Application with ID {_LocalDrivingLicenseApplicationID} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApplicationInfo(); 
        }
        public void LoadApplicationInfoByApplicationID(int applicationID)
        {
            
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(applicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show($"Application with ID {applicationID} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApplicationInfo(); 
        }

        private void ctrlLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
