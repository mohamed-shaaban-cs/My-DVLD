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
        clsLocalDrivingLicenseApplication localDrivingLicenseApplication ;
        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        void _LoadDrivingLicenseApplicationInfo()
        {
            lblDLApplicationID.Text = localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text= clsLicenseClass.Find(localDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = $"{clsLocalDrivingLicenseApplication.countPassedTests(localDrivingLicenseApplication.LocalDrivingLicenseApplicationID)}/3";

            llblShowLicenseInfo.Enabled = localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.Completed;
        }
        void _LoadApplicantInfo()
        {
            lblAppicationID.Text = localDrivingLicenseApplication.ApplicationID.ToString();
            lblStatus.Text = localDrivingLicenseApplication.ApplicationStatus.ToString();
            lblFees.Text = localDrivingLicenseApplication.PaidFees.ToString("C");
            lblType.Text = localDrivingLicenseApplication.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text = localDrivingLicenseApplication.PersonInfo.FullName;
            lblDate.Text = localDrivingLicenseApplication.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = localDrivingLicenseApplication.LastStatusDate.ToShortDateString();
            lblCreatedByUser.Text = clsUser.FindByUserID(localDrivingLicenseApplication.CreatedByUserID).UserName;

        }
        private void ctrlLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
           
        }
        public void LoadLocalDrivingLicenseApplicationInfo(int localDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (localDrivingLicenseApplication == null)
                return;
            
            _LoadDrivingLicenseApplicationInfo();
            _LoadApplicantInfo();
        }
        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails(localDrivingLicenseApplication.ApplicantPersonID);
            frmPersonDetails.ShowDialog();
        }
    }
}
