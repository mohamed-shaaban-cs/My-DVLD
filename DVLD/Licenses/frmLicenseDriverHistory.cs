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
        int _PersonID;

        public frmLicenseDriverHistory(int personID )
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void frmLicenseDriverHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false; // Disable filtering in the person card
            ctrlDriverLicensesHistory1.LoadLicensesHistory(_PersonID);

        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
