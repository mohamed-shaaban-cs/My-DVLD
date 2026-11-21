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
    public partial class ScheduledTest : UserControl
    {
        public clsTestAppointment TestAppointment { set; get; }
        int _TestID = -1;
        public int TestID
        {
            get { return _TestID; }
            set
            {
                _TestID = value;
                lblTestID.Text = _TestID.ToString();
            }
        }

        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public clsTestType.enTestType TestTypeID 
        { 
            get { return _TestTypeID; }
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
        public ScheduledTest()
        {
            InitializeComponent();
        }
       void _FillTestAppointmentInfo()
        {
            
            lblD_L_Application_ID.Text = TestAppointment.LocalDrivingLicenseApplicationID.ToString();
                lblD_Class.Text = TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName.ToString();
                lblPersonName.Text = TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicantFullName;
                lblTrial.Text = TestAppointment.LocalDrivingLicenseApplicationInfo.TotalTrialsPerTestType(TestTypeID).ToString();
                lblDate.Text = TestAppointment.AppointmentDate.ToString("dd/MM/yyyy");
                lblTestTypeFees.Text = TestAppointment.PaidFees.ToString("C2");
                lblTestID.Text =(_TestID == -1)? "Not Taken Yet" : TestAppointment.TestID.ToString();
        }
        public void LoadTestAppointmentInfo(int appointmentId)
        {
            TestAppointment = clsTestAppointment.Find(appointmentId);
           
            if(TestAppointment == null)
            {
                MessageBox.Show($"Test Appointment with ID {appointmentId} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(TestAppointment.LocalDrivingLicenseApplicationInfo == null)
            {
                MessageBox.Show($"Local Driving License Application with ID {appointmentId} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

             _TestID = TestAppointment.TestID;
            _FillTestAppointmentInfo();
        }
    }
}
