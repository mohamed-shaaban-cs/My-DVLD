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
        int _LocalDrivingLicenseApplicationID = -1;
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        int _ApponintmentID = -1;
        public frmScheduleTest(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID,int AppointmentID=-1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _ApponintmentID = AppointmentID;
        }
        private void frmScheduleTest_Load_1(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestTypeID;
            ctrlScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID,_ApponintmentID);
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ctrlScheduleTest1__OnTestAppointmentSaved(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
