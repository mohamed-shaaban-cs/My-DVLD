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
        clsTestAppointment _TestAppointment;
        int _TestID;
        public int TestID
        {
            get { return _TestID; } 
        }
        public ScheduledTest()
        {
            InitializeComponent();
        }
       void _FillTestAppointmentInfo()
        {
            switch (_TestAppointment.TestTypeInfo.TestTypeID)
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
            lblD_L_Application_ID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
                lblD_Class.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName.ToString();
                lblPersonName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicantFullName;
                lblTrial.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.countPassedTests().ToString();
                lblDate.Text = _TestAppointment.AppointmentDate.ToString("dd/MM/yyyy");
                lblTestTypeFees.Text = _TestAppointment.TestTypeInfo.TestTypeFees.ToString("C2");
                lblTestID.Text = "Not Taken Yet";
        }
        public void LoadTestAppointmentInfo(int appointmentId)
        {
             _TestAppointment = clsTestAppointment.Find(appointmentId);
            if(_TestAppointment == null)
            {
                MessageBox.Show($"Test Appointment with ID {appointmentId} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillTestAppointmentInfo();
        }
        public void LoadTestAppointmentInfo(clsTestAppointment testAppointment)
        {
            _TestAppointment = testAppointment;
            if(_TestAppointment == null)
            {
                MessageBox.Show($"Test Appointment is Empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillTestAppointmentInfo();
        }

        public void SetTestID(int testID)
        {
            _TestID = testID;
            lblTestID.Text = testID.ToString();
        }
    }
}
