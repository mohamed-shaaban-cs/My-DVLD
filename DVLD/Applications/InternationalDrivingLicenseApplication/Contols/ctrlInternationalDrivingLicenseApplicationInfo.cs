using DVLD.Global_Classes;
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

namespace DVLD.Applications.InternationalDrivingLicenseApplication.Contols
{
    public partial class ctrlInternationalDrivingLicenseApplicationInfo : UserControl
    {
        int InternationalLicenseID;
        clsInternationalLicense _InternationalLicense;
        public clsInternationalLicense InternationalLicense
        {
            get { return _InternationalLicense; }
        }
        public clsApplication _Application;
        clsApplicationType _ApplicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense);
        int _LocalLicenseID = -1;
        public int LocalLicenseID  {get { return _LocalLicenseID; }}
        clsLicense _LocalLicenseInfo;
        public ctrlInternationalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        public void _RestDefultValues()
        {
            lblI_L_ApplicationID.Text = "[????]";
            lblI_LicenseID.Text = "[????]";
            lblLocalLicenseID.Text = "[????]";

            lblApplciationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplciationDate.Text;
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text =_ApplicationType.ApplicationFees.ToString();
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;
        }

        public void LoadLocalLicenseInfo(int LocalLicenseID)
        {
            _LocalLicenseInfo = clsLicense.Find(LocalLicenseID);
            if (_LocalLicenseInfo == null)
            {
                MessageBox.Show($"Invalid Local License ID With{LocalLicenseID}.", "Invalid License ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLocalLicenseID.Text = "[????]";
                return;   
            }
            _LocalLicenseID = LocalLicenseID;
            lblLocalLicenseID.Text = _LocalLicenseID.ToString();


            _InternationalLicense = clsInternationalLicense.FindByDriverID(_LocalLicenseInfo.DriverID);
            if (_InternationalLicense != null)
            {
                lblI_LicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
                lblI_L_ApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            }
        }

        bool _HandleLocalLicense()
        {

            if (_LocalLicenseInfo.LicenseClassID != 3) 
            {
                MessageBox.Show($"Invalid License Class ", "Invalid License Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            if(!_LocalLicenseInfo.IsActive)
            {
                MessageBox.Show($"this License Is Not Active ", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(_LocalLicenseInfo.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("This license is expired. Please renew it before proceeding.","License Expired",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
                
            }
            return true;
        }

        public bool IssueLicense()
        {
            if (_InternationalLicense == null)
            {
                MessageBox.Show("This person was already issued an international license.", "Already Issued", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                
            if (!_HandleLocalLicense())
                return false;

            
            _Application = new clsApplication();

            _Application.ApplicantPersonID = _LocalLicenseInfo.DriverInfo.PersonID;
            _Application.ApplicationDate = DateTime.Parse(lblApplciationDate.Text);
            _Application.ApplicationTypeID = _ApplicationType.ApplicationTypeID;
            _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _Application.LastStatusDate = _Application.ApplicationDate;
            _Application.PaidFees = _ApplicationType.ApplicationFees;
            _Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if(!_Application.Save())
            {
                MessageBox.Show("Error: Application was not saved.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            lblI_L_ApplicationID.Text = _Application.ApplicationID.ToString();

            _InternationalLicense = new clsInternationalLicense();

            _InternationalLicense.ApplicationID = _Application.ApplicationID;
            _InternationalLicense.DriverID = _LocalLicenseInfo.DriverID;
            _InternationalLicense.IssuedUsingLocalLicenseID = _LocalLicenseInfo.LicenseID;
            _InternationalLicense.IssueDate = _Application.ApplicationDate;
            _InternationalLicense.ExpirationDate = DateTime.Parse(lblExpirationDate.Text);
            _InternationalLicense.IsActive = true;
            _InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!_InternationalLicense.Save())
            {
                MessageBox.Show("Failed to issue the International License.","Issue Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            lblI_LicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            return true;
        }
    }
}