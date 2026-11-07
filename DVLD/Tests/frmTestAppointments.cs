using DVLD.Properties;
using DVLD_BusinessLogic;
using ModrenUI_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmTestAppointments : Form
    {
        int _LocalDrivingLicenseApplicationID;
        clsTestType.enTestType _TestType;
        clsTestType _TestTypeInfo;
        clsTestAppointment _TestAppointment;
        DataTable _dtTestAppointments;


        public frmTestAppointments(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
            _TestTypeInfo = clsTestType.Find(TestType);
            
        }

        void _RefreshTestAppointmentsList()
        {
            _dtTestAppointments = clsTestAppointment.GetAllTestAppointmentsByTestType(_TestType, _LocalDrivingLicenseApplicationID);
            dgvTestAppointmentsList.DataSource = _dtTestAppointments;
            lblRecordsCount.Text = $"{_dtTestAppointments.Rows.Count}";
        }

        private void frmTestAppiontments_Load(object sender, EventArgs e)
        {
            this.Text = $"{_TestTypeInfo.TestTypeTitle} Appointments";
            label1.Text = this.Text;

            pictureBox1.Image = (_TestType == clsTestType.enTestType.StreetTest) ? Properties.Resources.driving_test_512 : (_TestType == clsTestType.enTestType.VisionTest) ? Resources.Vision_512 : Resources.Written_Test_512;

            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            _RefreshTestAppointmentsList();
            DataGridViewInterfacescs.DataGridViewModernStayle(dgvTestAppointmentsList);

            if(dgvTestAppointmentsList.Rows.Count > 0)
            {
                dgvTestAppointmentsList.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                dgvTestAppointmentsList.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                dgvTestAppointmentsList.Columns["PaidFees"].HeaderText = "Paid Fees";
                dgvTestAppointmentsList.Columns["IsLocked"].HeaderText = "Is Locked";
            }
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void crtlNewLocalLicenseAppication_Click(object sender, EventArgs e)
        {
            if(clsLocalDrivingLicenseApplication.IsHaveActiveTestAppointment(_LocalDrivingLicenseApplicationID, _TestType))
            {
                MessageBox.Show($"You already have an active {_TestTypeInfo.TestTypeTitle} appointment. You cannot schedule a new appointment until the existing one is completed.", "Active Appointment Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(clsLocalDrivingLicenseApplication.IsHavePassedTestPerTestType(_LocalDrivingLicenseApplicationID, _TestType))
            {
                MessageBox.Show($"You have already passed the {_TestTypeInfo.TestTypeTitle}. You cannot schedule a new appointment.", "Test Already Passed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
            frmScheduleTest.ShowDialog();
            _RefreshTestAppointmentsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frmScheduleTest = new frmScheduleTest((int)dgvTestAppointmentsList.CurrentRow.Cells["TestAppointmentID"].Value);
            frmScheduleTest.ShowDialog();
            _RefreshTestAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduledTest frmScheduledTest = new frmScheduledTest((int)dgvTestAppointmentsList.CurrentRow.Cells["TestAppointmentID"].Value);
            frmScheduledTest.ShowDialog();
            _RefreshTestAppointmentsList();
        }
    }
}
