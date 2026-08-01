using DVLD_BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        //Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        //Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int personID)
        {
            Action<int> Handler = OnPersonSelected;
            if(Handler != null)
            {
                Handler(personID); // Raise the event with the personID parameter
            }
        }

        private bool _ShowAddPersonButton = true;
        // Property to control the visibility of the Add Person button
        public bool ShowAddPersonButton
        {
            get { return _ShowAddPersonButton; }
            set
            {
                _ShowAddPersonButton = value;
                btnAddNewPerson.Visible = value;
            }
        }
        private bool _FilterEnabled = true;
        // Property to control the enabled state of the filter group box
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = value;
            }
        }

        private int _PersonID = -1;
        public int PersonID { get { return ctrlPersonCard1.PersonID; } }

        public clsPerson SelectedPersonInfo { get { return ctrlPersonCard1.SelectedPersonInfo; } }


        public void LoadPersonInfo(int personID)
        {
            cbFindBy.SelectedIndex = 1; // Set to "Person ID"
            txtFilterValue.Text = personID.ToString();
            FindNow();
        }
        private void crtlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void FindNow()
        {
            switch (cbFindBy.SelectedIndex)
            {
                case 0:
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                case 1:
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                default:
                    MessageBox.Show("Please select a valid filter option.");
                    break;
            }
            if (OnPersonSelected != null && _FilterEnabled)
                OnPersonSelected(ctrlPersonCard1.PersonID); // Raise the event with the selected person ID
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before searching.");
                return;
            }
            FindNow();

        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            Control Temp = sender as Control;
            if (string.IsNullOrWhiteSpace(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This Field is Required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersonInofo frmAddUpdatePersonInofo1 = new frmAddUpdatePersonInofo();
            frmAddUpdatePersonInofo1.DataBack += frm_DataBackNewPersonID;
            frmAddUpdatePersonInofo1.ShowDialog();

            
        }
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void frm_DataBackNewPersonID(object sender, int personID)
        {
            cbFindBy.SelectedIndex = 1; // Set to "Person ID"
            txtFilterValue.Text = personID.ToString(); 
            ctrlPersonCard1.LoadPersonInfo(personID);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check if the user pressed Enter key
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnFind.PerformClick();
                //e.Handled = true; // Mark the event as handled
            }
            if (cbFindBy.SelectedIndex == 1) // If "Person ID" is selected
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // Allow only digits and control characters

        }
    }
}
