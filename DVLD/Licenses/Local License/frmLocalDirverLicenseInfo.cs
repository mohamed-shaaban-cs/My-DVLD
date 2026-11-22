using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_License
{
    public partial class frmLocalDirverLicenseInfo : Form
    {
        int _LicenseID;
        public frmLocalDirverLicenseInfo(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
        }
        private void frmLocalDirverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlLocalDriverLicenseInfo1.LoadInfo(_LicenseID);
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
