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
        int _TestAppointmentID = -1;
        clsTestType.enTestType _TestTypeID;
        clsTestAppointment _TestAppointmentIfNo;
        int _TestID = -1;
        clsTest _Test;
        public frmScheduledTest(int TestAppointmentID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
            _TestAppointmentID = TestAppointmentID;
            
        }
        private void frmScheduledTest_Load(object sender, EventArgs e)
        {

            tbNotes.Text = "";

            rbPass.Enabled = true;
            rbFail.Enabled = true;
            tbNotes.Enabled = true;
            btnSave.Enabled = true;

            lblAppointmentLoackedMassage.Visible = false;


            ctrlscheduledTest1.TestTypeID = _TestTypeID;

            ctrlscheduledTest1.LoadTestAppointmentInfo(_TestAppointmentID);
            if (ctrlscheduledTest1.TestAppointment == null)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            int TestID = ctrlscheduledTest1.TestID;
            //if Test Appointment Have a Test
            if (TestID != -1)
            {             
                _Test = clsTest.FindByTestAppointmentID(TestID); 
                LoadTestData();
                return;
            }
            else
            _Test = new clsTest();
                
        }

        void LoadTestData()
        {
            if (_Test.TestResult)
                rbPass.Checked = true;
            else
                rbFail.Checked = true;

            tbNotes.Text = _Test.Notes;

            rbPass.Enabled = false;
            rbFail.Enabled = false;
            tbNotes.Enabled = false;
            btnSave.Enabled = false;

            lblAppointmentLoackedMassage.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Are you sure you want Save Test Result", "Save Result",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                return;

            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = (rbPass.Checked == true) ? true : false;
            _Test.Notes = tbNotes.Text.Trim();
            _Test.CreatedByUserID = Global_Classes.clsGlobal.CurrentUser.UserID;
            
            if (_Test.Save())
            {
                MessageBox.Show("your Test Result Has been Saved Successfully ", "Save Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Faild to Save Test Result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
