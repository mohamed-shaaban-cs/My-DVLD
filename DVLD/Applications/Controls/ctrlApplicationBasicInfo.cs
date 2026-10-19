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

namespace DVLD
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        //Private Fields
        int _ApplicationID;
        clsApplication _Application;


        // Properties to Get Private Fields
        public int ApplicationID
        {
            get { return _ApplicationID; }
        }
        
        public clsApplication SelectedApplicationInfo
        {
            get { return _Application; }
        }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void _FillApplicationInfo()
        {
            lblAppicationID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.ApplicationStatus.ToString();
            lblFees.Text = _Application.PaidFees.ToString("C");
            lblType.Text = _Application.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text = _Application.ApplicantFullName;
            lblDate.Text = _Application.ApplicationDate.ToString("yyyy-MMM-dd");
            lblStatusDate.Text = _Application.LastStatusDate.ToString("yyyy-MMM-dd");
            lblCreatedByUser.Text = clsUser.FindByUserID(_Application.CreatedByUserID).UserName;
        }

        public void LoadApplicationInfo(int applicationID)
        {
            _ApplicationID = applicationID;
            _Application = clsApplication.FindBaseApplication(_ApplicationID);
            if(_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillApplicationInfo();
        }



        public void ResetApplicationInfo()
        {
            lblAppicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblFees.Text = "[????]";
            lblType.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
        }

        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails(_Application.ApplicantPersonID);
            frmPersonDetails.Show();
        }
    }
}
