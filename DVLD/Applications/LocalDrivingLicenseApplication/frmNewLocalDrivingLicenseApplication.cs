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

namespace DVLD.Applications.LocalDrivingLicenseApplication
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        enum enMode { AddNew, Update }
        enMode _Mode;
        int _LocalDrivingLicenseApplicationID;
        int _SelectedPersonID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
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

        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillLicenseClassComboBox();
            _ResetDefaultValues();
            
            if(_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void _FillLicenseClassComboBox()
        {
            DataTable licenseClasses = clsLicenseClass.GetAllLicenseClasses();
            foreach(DataRow row in licenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"].ToString());
            }

        }
        void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblApplicationDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                cbLicenseClass.SelectedIndex = 2;
                lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewLocalDrivingLicense).ApplicationFees.ToString("C");
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
                tabApplicationInfo.Enabled = true;
            }
            else if (_Mode == enMode.Update)
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                tabApplicationInfo.Enabled = true;
                tcAdd_UpdateLocalDrivingLicenseApplication.SelectTab(tabApplicationInfo);
            }
        }
        
        void _LoadData()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication != null)
            {
                lblDLApplicationID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
                lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString("yyyy-MM-dd");
                cbLicenseClass.SelectedIndex = _LocalDrivingLicenseApplication.LicenseClassID + 1;
                lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString("C");
                lblCreatedByUser.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;
                ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
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
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;
            int PersonAge = DateTime.Now.Year - ctrlPersonCardWithFilter1.SelectedPersonInfo.DateOfBirth.Year;
            PersonAge = (DateTime.Now.DayOfYear < ctrlPersonCardWithFilter1.SelectedPersonInfo.DateOfBirth.DayOfYear) ? PersonAge - 1 : PersonAge;

            if (clsLicenseClass.Find(cbLicenseClass.Text).MinimumAllowedAge > PersonAge)
            {
                MessageBox.Show("Person is not old enough for the selected license class.", "Age Restriction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsLocalDrivingLicenseApplication.DoesPersonHaveActiveApplicationForLicenseClass(ctrlPersonCardWithFilter1.PersonID,(clsApplication.enApplicationType) clsApplication.enApplicationType.NewLocalDrivingLicense, LicenseClassID))
            {
                MessageBox.Show("This person already has an active application.", "Active Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(clsLicense.IsActiveLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("This person already has an active license for the selected class.", "Active License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewLocalDrivingLicense;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
            _LocalDrivingLicenseApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewLocalDrivingLicense).ApplicationFees;
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(_LocalDrivingLicenseApplication.Save())
            {
                MessageBox.Show("Application saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";
                lblDLApplicationID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
                ctrlPersonCardWithFilter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Failed to save application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int PersonID)
        {
            _SelectedPersonID = PersonID;
        }

        private void frmNewLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void cbLicenseClass_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
