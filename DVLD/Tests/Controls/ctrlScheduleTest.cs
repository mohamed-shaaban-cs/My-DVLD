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

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public event EventHandler _OnTestAppointmentSaved;
        // private fields
        public enum enMode { AddNew = 0, Update = 1 };
        enum enRetakeTestMode { NoRetake = 0, Retake = 1 };
        enMode _Mode;
        enRetakeTestMode _RetakeTestMode;

        int _TestAppointmentID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        clsTestAppointment _TestAppointment;
        clsTestType _TestTypeInfo;

        clsApplication _ApplicationRetakeInfo;

        
        // Public properties
        public clsTestAppointment TestAppointment
        {
            get { return _TestAppointment; }
        }
        public clsTestType TestTypeInfo
        {
            get { return _TestTypeInfo; }
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        void _SetDefaultValues()
        {
            lblAppointmentLoackedMassage.Visible = false;
            btnSave.Enabled = true;
            dtpDate.Enabled = true;
            
            switch (_TestTypeInfo.TestTypeID)
            {
                case clsTestType.enTestType.WrittenTest:
                    gbScheduleTest.Text = "Schedule Written Test";
                    picTitleImage.Image = Properties.Resources.Written_Test_512;
                    break;
                case clsTestType.enTestType.VisionTest:
                    gbScheduleTest.Text = "Schedule Vision Test";
                    picTitleImage.Image = Properties.Resources.Vision_512;
                    break;
                case clsTestType.enTestType.StreetTest:
                    gbScheduleTest.Text = "Schedule Street Test";
                    picTitleImage.Image = Properties.Resources.driving_test_512;
                    break;
            }

            if(_RetakeTestMode == enRetakeTestMode.Retake)
            {
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
            }
            else
            {
                lblTitle.Text = "Schedule Test";
                gbRetakeTestInfo.Enabled = false;
            }
        }
        void _FillScheduleTestInfo()
        {
            _SetDefaultValues();

            lblD_L_Application_ID.Text = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();
            lblD_Class.Text = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblPersonName.Text = _LocalDrivingLicenseApplicationInfo.ApplicantFullName;
            lblTrial.Text = clsLocalDrivingLicenseApplication.TotalTrialsPerTestType(_LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID, _TestTypeInfo.TestTypeID).ToString();
            lblTestTypeFees.Text = _TestTypeInfo.TestTypeFees.ToString("C2");

            if(_RetakeTestMode == enRetakeTestMode.Retake)
            {
                lblR_App_Fees.Text = _ApplicationRetakeInfo.ApplicationTypeInfo.ApplicationFees.ToString("C2");
                lblTotalFees.Text = (_TestTypeInfo.TestTypeFees + _ApplicationRetakeInfo.ApplicationTypeInfo.ApplicationFees).ToString("C2");
            }
            else
            {
                lblR_App_Fees.Text = "0";
                lblTotalFees.Text = _TestTypeInfo.TestTypeFees.ToString("C2");
            }

            if (_Mode == enMode.AddNew)
            {
                dtpDate.MinDate = DateTime.Now;
                dtpDate.Value = DateTime.Now;
            }
            else if (_Mode == enMode.Update)
            {
                
                dtpDate.Value = _TestAppointment.AppointmentDate;

                if(_TestAppointment.IsLocked)
                {
                    dtpDate.Enabled = false;
                    btnSave.Enabled = false;
                    lblAppointmentLoackedMassage.Visible = true;
                }
            }

        }

        public void LoadNewScheduleTestInfo(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            _Mode = enMode.AddNew;
            _TestAppointment = new clsTestAppointment();
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            _TestTypeInfo = clsTestType.Find(TestType);
            if (clsLocalDrivingLicenseApplication.TotalTrialsPerTestType(_LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID, _TestTypeInfo.TestTypeID) > 0) //check if the user has already taken the test before
            {
                _RetakeTestMode = enRetakeTestMode.Retake;
                
                _ApplicationRetakeInfo = new clsApplication();
                _ApplicationRetakeInfo.ApplicantPersonID = _LocalDrivingLicenseApplicationInfo.ApplicantPersonID;
                _ApplicationRetakeInfo.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                
                _ApplicationRetakeInfo.CreatedByUserID = Global_Classes.clsGlobal.CurrentUser.UserID;

            }else
            {
                _RetakeTestMode = enRetakeTestMode.NoRetake;
                _ApplicationRetakeInfo = null;
            }
            //_SetDefaultTitleByTestTypeAndRetakeMode();
            _FillScheduleTestInfo();

        }
        public void LoadExistingScheduleTestInfo(int TestAppointmentID)
        {
            _Mode = enMode.Update;
            _TestAppointment= clsTestAppointment.Find(TestAppointmentID);
            _LocalDrivingLicenseApplicationInfo = _TestAppointment.LocalDrivingLicenseApplicationInfo;
            _TestTypeInfo = _TestAppointment.TestTypeInfo;
            
            if (_TestAppointment.ApplicationRetakeInfo != null) //check if the user has already taken the test before
            {
                _RetakeTestMode = enRetakeTestMode.Retake;
                _ApplicationRetakeInfo = _TestAppointment.ApplicationRetakeInfo;

            }else
            {
                _RetakeTestMode = enRetakeTestMode.NoRetake;
            }
            //_SetDefaultTitleByTestTypeAndRetakeMode();
            _FillScheduleTestInfo();

        }

        private void btnSaveUserData_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNew)
            {
                if(_RetakeTestMode == enRetakeTestMode.Retake)
                {
                    _ApplicationRetakeInfo.PaidFees = _ApplicationRetakeInfo.ApplicationTypeInfo.ApplicationFees;
                    if (!_ApplicationRetakeInfo.Save())
                    {
                        MessageBox.Show("Failed to save Retake Test Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _TestAppointment.RetakeTestApplicationID = _ApplicationRetakeInfo.ApplicationID;
                    lblR_Test_App_ID.Text = _ApplicationRetakeInfo.ApplicationID.ToString();
                }
                _TestAppointment = new clsTestAppointment();
                _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID;
                _TestAppointment.TestTypeID = (int)_TestTypeInfo.TestTypeID;
                _TestAppointment.AppointmentDate = dtpDate.Value;
                _TestAppointment.PaidFees = _TestTypeInfo.TestTypeFees;
                _TestAppointment.CreatedByUserID = Global_Classes.clsGlobal.CurrentUser.UserID;

               if(_TestAppointment.Save())
                {
                    MessageBox.Show("Test Appointment saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _OnTestAppointmentSaved?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Failed to save Test Appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else if (_Mode == enMode.Update)
            {
                _TestAppointment.AppointmentDate = dtpDate.Value;
                if (_TestAppointment.Save())
                {
                    MessageBox.Show("Test Appointment updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _OnTestAppointmentSaved?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Failed to update Test Appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
