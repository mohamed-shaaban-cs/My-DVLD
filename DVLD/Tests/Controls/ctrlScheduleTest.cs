using DVLD.Global_Classes;
using DVLD_BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        enMode _Mode;
        enum enRetakeTestMode { FristTimeSchedule = 0, Retake = 1 };
        enRetakeTestMode _RetakeTestMode;
        int _LocalDrivingLicenseApplicationID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        int _TestAppointmentID;
        clsTestAppointment _TestAppointment;
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public clsTestType.enTestType TestTypeID 
        { 
            get 
            { 
                return _TestTypeID;
            }
            set
            { 
                _TestTypeID = value;

                switch (_TestTypeID)
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
            } 
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int TestAppointmentID = -1)
        {
            if (TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _TestAppointmentID = TestAppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                MessageBox.Show("Local Driving License Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }


            if (_LocalDrivingLicenseApplicationInfo.DoseAttendTestType(_TestTypeID)) //check if the user has already taken the test 
                _RetakeTestMode = enRetakeTestMode.Retake;
            else
                _RetakeTestMode = enRetakeTestMode.FristTimeSchedule;



            if (_RetakeTestMode == enRetakeTestMode.Retake)
            {
                lblR_App_Fees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString("C2");
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblR_Test_App_ID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblR_App_Fees.Text = "0";
                lblR_Test_App_ID.Text = "N/A";
            }

            lblL_D_L_Application_ID.Text = _LocalDrivingLicenseApplicationInfo._LocalDrivingLicenseApplicationID.ToString();
            lblD_Class.Text = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblPersonName.Text = _LocalDrivingLicenseApplicationInfo.ApplicantFullName;

            lblTrial.Text = clsLocalDrivingLicenseApplication.TotalTrialsPerTestType(_LocalDrivingLicenseApplicationInfo._LocalDrivingLicenseApplicationID, _TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                clsTestType TestTypeInfo = clsTestType.Find(_TestTypeID);
                lblTestTypeFees.Text = TestTypeInfo.TestTypeFees.ToString();
                dtpDate.MinDate = DateTime.Now;

                _TestAppointment = new clsTestAppointment();

            }
            else
            {
                if (!_LoadExistingTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (Convert.ToSingle(lblTestTypeFees.Text) + Convert.ToSingle(lblR_App_Fees.Text)).ToString("C2");

            if (!_HandleActiveTestAppointmentConstraint())
                return;
            if (!_HandleAppointmentLockedConstraint())
                return;
            if (!_HandlePrviousTestConstraint())
                return;

        }
        private bool _LoadExistingTestAppointmentData()
        {

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("no Appointment with ID " + _TestAppointmentID.ToString() + " found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblTestTypeFees.Text = _TestAppointment.PaidFees.ToString();

            //set the min date for the date picker to the appointment date if the appointment date is in the past, otherwise set it to today
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) > 0)
                dtpDate.MinDate = _TestAppointment.AppointmentDate;
            else
                dtpDate.MinDate = DateTime.Now;

            dtpDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblR_App_Fees.Text = "0";
                lblR_Test_App_ID.Text = "N/A";
            }
            else
            {
                lblR_App_Fees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                lblR_Test_App_ID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;

            }
            return true;

        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsHaveActiveTestAppointment(_LocalDrivingLicenseApplicationInfo._LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                lblUserMassage.Text = "Person Already have an active appointment for this test";
                lblUserMassage.Visible = true;
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if (_Mode == enMode.Update && _TestAppointment.IsLocked)
            {
                lblUserMassage.Text = "Person Already sat for the Test,appointment loacked.";
                lblUserMassage.Visible = true;
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMassage.Visible = false;
                    return true;

                case clsTestType.enTestType.WrittenTest:

                    if (!_LocalDrivingLicenseApplicationInfo.DosePassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMassage.Text = "Cannot Sechule, Vision Test should be passed First";
                        lblUserMassage.Visible = true;
                        dtpDate.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMassage.Visible = false;
                        dtpDate.Enabled = true;
                        btnSave.Enabled = true;
                    }
                    return true;
                case clsTestType.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplicationInfo.DosePassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMassage.Text = "Cannot Sechule, Written Test should be passed First";
                        lblUserMassage.Visible = true;
                        dtpDate.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMassage.Visible = false;
                        dtpDate.Enabled = true;
                        btnSave.Enabled = true;
                    }
                    return true;
            }
            return true;
        }

        private bool _HandleRetakeApplication()
        {
            if(_Mode == enMode.AddNew && _RetakeTestMode == enRetakeTestMode.Retake)
            {
                clsApplication Application = new clsApplication();
                Application.ApplicantPersonID = _LocalDrivingLicenseApplicationInfo.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = Global_Classes.clsGlobal.CurrentUser.UserID;
                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Failed to save Retake Test Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }
            return true;
        }

        private void btnSaveUserData_Click(object sender, EventArgs e)
        {

            if (!_HandleRetakeApplication())
                return;  


                _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationInfo._LocalDrivingLicenseApplicationID;
                _TestAppointment.TestTypeID = _TestTypeID;
                _TestAppointment.AppointmentDate = dtpDate.Value;
                _TestAppointment.PaidFees = Convert.ToDecimal(lblTestTypeFees.Text);
                _TestAppointment.CreatedByUserID = Global_Classes.clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                MessageBox.Show("Test Appointment saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _OnTestAppointmentSaved?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Failed to save Test Appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

    }
}
