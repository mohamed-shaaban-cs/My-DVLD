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
        int _InternationalLicenseID = -1;
        public int InternationalLicenseID
        {
            set
            {
                _InternationalLicenseID = value;
                if(_InternationalLicenseID == -1)
                    lblI_LicenseID.Text = "[????]";
                else
                    lblI_LicenseID.Text = _InternationalLicenseID.ToString();
            }

            get { return _InternationalLicenseID; }
        }
        
        int _ApplicationID = -1;
        public int ApplicationID
        {
            set
            {
                _ApplicationID = value;
                if(_ApplicationID == -1)
                    lblI_L_ApplicationID.Text = "[????]";
                else
                    lblI_L_ApplicationID.Text = _ApplicationID.ToString();
            }

            get { return _ApplicationID; }
        }

        int _LocalLicenseID = -1;
        public int LocalLicenseID
        {
            set
            {
                _LocalLicenseID = value;
                if(_LocalLicenseID == -1)
                    lblLocalLicenseID.Text = "[????]";
                else
                    lblLocalLicenseID.Text = _LocalLicenseID.ToString();
            }
            get { return _LocalLicenseID; }
        }


        clsApplicationType _ApplicationType;
        public clsApplicationType ApplicationType { get { return _ApplicationType; } }
        public ctrlInternationalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
            _ApplicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense);
            ResetDefultValues();
        }
        public void ResetDefultValues()
        {
            lblI_L_ApplicationID.Text = "[????]";
            lblI_LicenseID.Text = "[????]";
            lblLocalLicenseID.Text = "[????]";

            lblApplciationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplciationDate.Text;
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text = _ApplicationType.ApplicationFees.ToString();
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;
        }

    }
}