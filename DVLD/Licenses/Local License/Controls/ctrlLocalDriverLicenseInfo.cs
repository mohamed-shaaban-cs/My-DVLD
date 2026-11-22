using DVLD_BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_License.Controls
{
    public partial class ctrlLocalDriverLicenseInfo : UserControl
    {
        int _LicenseID;
        clsLicense _LicenseInfo;

        public clsLicense SelectedLicenseInfo
        {
            get
            {
                if (_LicenseInfo == null)
                    _LicenseInfo = new clsLicense();
                return _LicenseInfo;
            }
        }
        int LicenseID { get { return _LicenseID; } }
        public ctrlLocalDriverLicenseInfo()
        {
            InitializeComponent();
        }

        void _LoadPersonImage()
        {
            // Set default image based on gender
            pbPersonImage.Image = (_LicenseInfo.DriverInfo.PersonInfo.Gender == 0) ? Properties.Resources.Male_512 : Properties.Resources.Female_512;

            string ImagePath = _LicenseInfo.DriverInfo.PersonInfo.ImagePath;
            if (!string.IsNullOrEmpty(ImagePath))
                if (File.Exists(ImagePath)) // Check if the image file exists
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show($"Image file not found: {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void _FillLicenseInfo()
        {
            lblClass.Text = _LicenseInfo.LicenseClassInfo.ClassName;
            lblName.Text = _LicenseInfo.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _LicenseInfo.LicenseID.ToString();
            lblNationalNo.Text = _LicenseInfo.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _LicenseInfo.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text = _LicenseInfo.IssueDate.ToShortDateString();

            switch(_LicenseInfo.IssueReason)
            {
                case clsLicense.enIssueReason.FirstTime:
                    lblIssueReason.Text = "First Time";
                    break;
                case clsLicense.enIssueReason.Renew:
                    lblIssueReason.Text = "Renew License";
                    break;
                case clsLicense.enIssueReason.ReplacementForLost:
                    lblIssueReason.Text = "Replacement For Lost";
                    break;
                case clsLicense.enIssueReason.ReplacementForDamaged:
                    lblIssueReason.Text = "Replacement For Damaged";
                    break;
                default:
                    lblIssueReason.Text = "Unknown";
                    break;
            }
            lblNotes.Text = _LicenseInfo.Notes;
            lblIsActive.Text = _LicenseInfo.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _LicenseInfo.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = _LicenseInfo.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = clsDetainedLicense.IsLicenseDetained(_LicenseInfo.LicenseID) ? "Yes" : "No";
            _LoadPersonImage();
        }
        public void ResetLicenseInfo()
        {
            lblClass.Text = "[????]";
            lblName.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "[????]";
            lblIssueReason.Text = "[????]";
            lblNotes.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblIsDetained.Text = "[????]";
            pbPersonImage.ImageLocation = "";
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LicenseInfo = clsLicense.Find(LicenseID);
            if (_LicenseInfo == null)
            {
                ResetLicenseInfo();
                MessageBox.Show($"License with ID {_LicenseID} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLicenseInfo();
        }

    }
}
