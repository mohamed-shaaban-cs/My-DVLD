using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Internatinoal_License
{
    public partial class frmInternationalDriverLicenseInfo : Form
    {
        int _InternationalLicenseID;
        public frmInternationalDriverLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = internationalLicenseID;
        }

        private void frmInternationalDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalDriverlicenseInfo1.LoadInfo(_InternationalLicenseID);
            if (ctrlInternationalDriverlicenseInfo1.InternationalLicenseID == -1)
                this.Close();
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
