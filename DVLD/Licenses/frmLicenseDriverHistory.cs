using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmLicenseDriverHistory : Form
    {
        int _PersonID = -1;

        public frmLicenseDriverHistory(int personID )
        {
            InitializeComponent();
            _PersonID = personID;
        }
        public frmLicenseDriverHistory()
        {
            InitializeComponent();
        }

        private void frmLicenseDriverHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false; // Disable filtering in the person card
                ctrlDriverLicensesHistory1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.FilterEnabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }

        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            int PersonID = obj;
            if(PersonID == -1)
                ctrlDriverLicensesHistory1.Clear();
            else
            ctrlDriverLicensesHistory1.LoadInfoByPersonID(PersonID);
        }
    }
}
