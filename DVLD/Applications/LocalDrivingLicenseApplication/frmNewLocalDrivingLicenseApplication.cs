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

namespace DVLD.Applications.LocalDrivingLicenseApplication
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        enum enMode { AddNew, Update }
        enMode _Mode;
        int _LocalDrivingLicenseApplicationID;
        clsLocalDrivingLicenseApplication localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        clsApplicationType applicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.NewLocalDrivingLicense);
        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            tabApplicationInfo.Enabled = false;
        }
        public frmNewLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _Mode = enMode.Update;
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            tabApplicationInfo.Enabled = true;
            tcAdd_UpdateLocalDrivingLicenseApplication.SelectTab(tabApplicationInfo);
        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillLicenseClassComboBox();
            _ResetDefaultValues();
            
            if(_Mode == enMode.Update)
            {
                _LoadData();
                lblTitle.Text = "Update Local Driving License Application";
            }
        }

        private void _FillLicenseClassComboBox()
        {
            var licenseClasses = clsLicenseClass.GetAllLicenseClasses();
            if(licenseClasses != null && licenseClasses.Rows.Count > 0)
            cbLicenseClass.Items.AddRange(licenseClasses.AsEnumerable().Select(r => r["ClassName"].ToString()).ToArray());

        }
        void _ResetDefaultValues()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbLicenseClass.SelectedIndex = 2;
            lblApplicationFees.Text = applicationType.ApplicationFees.ToString("C");
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }
        
        void _LoadData()
        {
            localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (localDrivingLicenseApplication != null)
            {
                lblDLApplicationID.Text = localDrivingLicenseApplication.ApplicationID.ToString();
                lblApplicationDate.Text = localDrivingLicenseApplication.ApplicationDate.ToString("yyyy-MM-dd");
                cbLicenseClass.SelectedIndex = localDrivingLicenseApplication.LicenseClassID + 1;
                lblApplicationFees.Text = localDrivingLicenseApplication.PaidFees.ToString("C");
                lblCreatedByUser.Text = clsUser.FindByUserID(localDrivingLicenseApplication.CreatedByUserID).UserName;
                ctrlPersonCardWithFilter1.LoadPersonInfo(localDrivingLicenseApplication.ApplicantPersonID);
            }
            else
            {
                MessageBox.Show($"No application found with ID = {_LocalDrivingLicenseApplicationID}", "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.Update)
            {
                tabApplicationInfo.Enabled = true;
                tcAdd_UpdateLocalDrivingLicenseApplication.SelectTab(tabApplicationInfo);
                return;
            }


            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please select a person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                btnSaveUserData.Enabled = true;
                tabApplicationInfo.Enabled = true;
                tcAdd_UpdateLocalDrivingLicenseApplication.SelectTab(tabApplicationInfo);
            }

        }

        private void btnSaveUserData_Click(object sender, EventArgs e)
        {
            if(clsLocalDrivingLicenseApplication.DoesPersonHaveActiveApplicationForLicenseClass(ctrlPersonCardWithFilter1.PersonID,(clsApplication.enApplicationType) applicationType.ApplicationTypeID,(cbLicenseClass.SelectedIndex - 1)))
            {
                MessageBox.Show("This person already has an active application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            localDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            localDrivingLicenseApplication.ApplicationTypeID = applicationType.ApplicationTypeID;
            localDrivingLicenseApplication.LicenseClassID = cbLicenseClass.SelectedIndex - 1;
            localDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            localDrivingLicenseApplication.PaidFees = applicationType.ApplicationFees;
            localDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(localDrivingLicenseApplication.Save())
            {
                MessageBox.Show("Application saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";
                lblDLApplicationID.Text = localDrivingLicenseApplication.ApplicationID.ToString();
                ctrlPersonCardWithFilter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Failed to save application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
