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
using System.IO;

namespace DVLD.Licenses.Internatinoal_License
{
    public partial class ctrlInternationalDriverlicenseInfo : UserControl
    {
        int _InternationalLicenseID;
        public int InternationalLicenseID { get { return _InternationalLicenseID; } }

        clsInternationalLicense _InternationalLicense;
        public clsInternationalLicense InternationalLicense { get { return _InternationalLicense; } }
        public ctrlInternationalDriverlicenseInfo()
        {
            InitializeComponent();
        }
        void _LoadPersonImage()
        {
            // Set default image based on gender
            pbPersonImage.Image = (_InternationalLicense.DriverInfo.PersonInfo.Gender == 0) ? Properties.Resources.Male_512 : Properties.Resources.Female_512;

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;
            if (!string.IsNullOrEmpty(ImagePath))
            {
                if (File.Exists(ImagePath)) // Check if the image file exists
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show($"Image file not found: {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ResetLicenseInfo()
        {
            _InternationalLicense = null;
            _InternationalLicenseID = -1;
            lblInt_LicenseID.Text = "[????]";
            lblName.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "[????]";

            lblApplicationID.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            pbPersonImage.ImageLocation = "";
        }
        public void LoadInfo(int internationalLicenseID)
        {
            _InternationalLicenseID = internationalLicenseID;
            _InternationalLicense = clsInternationalLicense.Find(internationalLicenseID);
            if(_InternationalLicense == null)
            {
                MessageBox.Show($"International License is Not Exisit with License ID {internationalLicenseID}", "Not Exsist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetLicenseInfo();
                return;
            }
            lblInt_LicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.PersonID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = (_InternationalLicense.DriverInfo.PersonInfo.Gender == (short)clsPerson.enGender.Male)? "Male":"Female";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToString("yyyy-MMM-dd");

            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToString("yyyy-MMM-dd");
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToString("yyyy-MMM-dd");
            _LoadPersonImage();
        }

    }
}
