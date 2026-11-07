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
    public partial class frmScheduleTest : Form
    {
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            ctrlScheduleTest1.LoadNewScheduleTestInfo(LocalDrivingLicenseApplicationID, TestType);
        }
        public frmScheduleTest(int TestAppointmentID)
        {
            InitializeComponent();
            ctrlScheduleTest1.LoadExistingScheduleTestInfo(TestAppointmentID);

        }
        

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {

        }

        private void ctrlScheduleTest1__OnTestAppointmentSaved(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
