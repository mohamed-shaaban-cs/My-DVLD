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

namespace DVLD.Licenses.Local_License.Controls
{
    public partial class ctrlLocalDriverLicenseInfoWithFilter : UserControl
    {
        public event EventHandler FindClick;
        int _LocalLicenseID;
        public int LocalLicenseID { get { return _LocalLicenseID; } }
        clsLicense _LocalLicense;
        public clsLicense LocalLicense { get { return _LocalLicense; } }
        public ctrlLocalDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            ctrlLocalDriverLicenseInfo1.LoadInfo(Convert.ToInt32(txtSearchValue.Text));
            _LocalLicense = ctrlLocalDriverLicenseInfo1.SelectedLicenseInfo;
            _LocalLicenseID = ctrlLocalDriverLicenseInfo1.LicenseID;
            FindClick?.Invoke(this, EventArgs.Empty);
        }

        private void txtSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }
    }
}
