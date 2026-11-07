using DVLD.Tests.Controls;
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

namespace DVLD.Tests
{
    public partial class frmScheduledTest : Form
    {
        clsTestAppointment _TestAppointmentIfNo;
        clsTest _Test;
        public frmScheduledTest(int TestAppointmentID)
        {
            InitializeComponent();

            _RestDefultValues();

            _TestAppointmentIfNo = clsTestAppointment.Find(TestAppointmentID);
            if(_TestAppointmentIfNo == null)
            {
                MessageBox.Show($"Test Appointment with ID {TestAppointmentID} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            scheduledTest1.LoadTestAppointmentInfo(_TestAppointmentIfNo);

            if(_TestAppointmentIfNo.IsLocked)
                LoadTestData();
            
        }
        void _RestDefultValues()
        {
            _Test = new clsTest();

            tbNotes.Text = "";

            rbPass.Enabled = true;
            rbFail.Enabled = true;
            tbNotes.Enabled = true;
            btnSave.Enabled = true;

            lblAppointmentLoackedMassage.Visible = false;
        }

        void LoadTestData()
        {
            clsTest Test = clsTest.FindByTestAppointmentID(_TestAppointmentIfNo.TestAppointmentID);

            //if Test Appointment not Have a Test
            if (Test == null)
                return;

            scheduledTest1.SetTestID(Test.TestID);
            if (Test.TestResult)
                rbPass.Checked = true;
            else
                rbFail.Checked = true;

            tbNotes.Text = Test.Notes;

            rbPass.Enabled = false;
            rbFail.Enabled = false;
            tbNotes.Enabled = false;
            btnSave.Enabled = false;

            lblAppointmentLoackedMassage.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Test.TestAppointmentID = _TestAppointmentIfNo.TestAppointmentID;
            _Test.TestResult = (rbPass.Checked == true)? true : false;
            _Test.Notes = tbNotes.Text.Trim();
            _Test.CreatedByUserID = Global_Classes.clsGlobal.CurrentUser.UserID;
            //lock the Test Appointment after saving the Test Result
            _TestAppointmentIfNo.IsLocked = true;

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want Save Test Result", "Save Result", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                if(_Test.Save() && _TestAppointmentIfNo.Save())
                {
                    MessageBox.Show("your Test Result Has been Saved Successfully ","Save Result",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Faild to Save Test Result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
