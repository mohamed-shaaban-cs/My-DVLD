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
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if(handler != null)
            {
                handler(LicenseID);
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        

        public int LocalLicenseID { get { return ctrlLocalDriverLicenseInfo1.LicenseID; } }
        public clsLicense LocalLicense { get { return ctrlLocalDriverLicenseInfo1.SelectedLicenseInfo; } }

        public ctrlLocalDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtSearch.Text = LicenseID.ToString();
            btnFind.PerformClick();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("some Fileds are not valide!", "Filds Not Valide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearch.Focus();
                return;
            }

            ctrlLocalDriverLicenseInfo1.LoadInfo(int.Parse(txtSearch.Text));
            int _LocalLicenseID = ctrlLocalDriverLicenseInfo1.LicenseID;

            if(OnLicenseSelected != null && FilterEnabled)
            OnLicenseSelected(_LocalLicenseID);
        }

        private void txtSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));

            //check If the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
                btnFind.PerformClick();
        }
        private void txtSearch_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                errorProvider1.SetError(txtSearch, "Field cannot be empty.");
            }
            else
                errorProvider1.SetError(txtSearch, "");
        }
        public void txtSearchFoucs()
        {
                txtSearch.Focus();
        }

    }
}
