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
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;
               
            }
        }
        clsTestType _TestTypeInfo;
        clsTestAppointment _TestAppointment;
        DataTable _dtTestAppointments;


        public frmTestAppointments(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestType;
            
        }

        void _LoadTestTypeImageAndTitle()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.WrittenTest:
                    this.Text = "Written Test Appointments";
                    lblTitle.Text = this.Text;
                    pictureBox1.Image = Properties.Resources.Written_Test_512;
                    break;
                case clsTestType.enTestType.VisionTest:
                    this.Text = "Vision Test Appointments";
                    lblTitle.Text = this.Text;
                    pictureBox1.Image = Properties.Resources.Vision_512;
                    break;
                case clsTestType.enTestType.StreetTest:
                    this.Text = "Street Test Appointments";
                    lblTitle.Text = this.Text;
                    pictureBox1.Image = Properties.Resources.driving_test_512;
                    break;
            }
        }
        private void frmTestAppiontments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

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
        void _RefreshTestAppointmentsList()
        {
            _dtTestAppointments = clsTestAppointment.GetAllTestAppointmentsByTestType(_TestTypeID, _LocalDrivingLicenseApplicationID);
            dgvTestAppointmentsList.DataSource = _dtTestAppointments;
            lblRecordsCount.Text = $"{_dtTestAppointments.Rows.Count}";
        }


        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void crtlNewLocalLicenseAppication_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if(localDrivingLicenseApplication.IsHaveActiveTestAppointment(_TestTypeID))
            {
                MessageBox.Show($"You already have an active Test Appointment. You cannot schedule a new appointment until the existing one is completed.", "Active Appointment Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if(clsLocalDrivingLicenseApplication.IsHavePassedTestPerTestType(_LocalDrivingLicenseApplicationID, _TestTypeID))
            //{
            //    MessageBox.Show($"You have already passed the {_TestTypeInfo.TestTypeTitle}. You cannot schedule a new appointment.", "Test Already Passed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestTypeID(_TestTypeID);
            if(LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
                frm1.ShowDialog();
                _RefreshTestAppointmentsList();
                return;
            }
            if(LastTest.TestResult == true)
            {
                MessageBox.Show($"You have already passed in this Test . You cannot schedule a new appointment.", "Test Already Passed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicenseApplicationID,_TestTypeID);
            frmScheduleTest.ShowDialog();
            _RefreshTestAppointmentsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalDrivingLicenseApplicationID,_TestTypeID, (int)dgvTestAppointmentsList.CurrentRow.Cells["TestAppointmentID"].Value);
            frmScheduleTest.ShowDialog();
            _RefreshTestAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduledTest frmScheduledTest = new frmScheduledTest((int)dgvTestAppointmentsList.CurrentRow.Cells["TestAppointmentID"].Value,_TestTypeID);
            frmScheduledTest.ShowDialog();
            _RefreshTestAppointmentsList();
        }
    }
}
