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
using System.IO;
using DVLD.Properties;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        clsPerson _Person = new clsPerson();
        //
        int _PersonID = -1;

        // Property to only get the PersonID
        public int PersonID { get { return _PersonID; } }

        // Property to only get the Person object
        public clsPerson SelectedPersonInfo { get { return _Person; } }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

        }

        public  void ResetPersonInfo()
        {
            
            _PersonID = -1;
            lblEditPersonInfo.Enabled = false; // Disable the edit link when no person is loaded
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblPhone.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            // Set default image
            pbPersonImage.Image = Resources.Male_512;

        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show($"Person with National No {NationalNo} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _PersonID = PersonID;
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show($"Person with ID {PersonID} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePersonInofo frmAddUpdatePerson = new frmAddUpdatePersonInofo(_PersonID);
            frmAddUpdatePerson.ShowDialog();

            //refresh the person info after editing
            LoadPersonInfo(_PersonID);
        }

        void _FillPersonInfo()
        {
            lblEditPersonInfo.Enabled = true; // Enable the edit link when a person is loaded
            _PersonID = _Person.PersonID;  // Update the _PersonID with the current person's ID

            lblPersonID.Text = $"ID: {_Person.PersonID}";
            lblName.Text = $"{_Person.FirstName} {_Person.SecondName} {_Person.ThirdName} {_Person.LastName}";
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = (_Person.Gender == 0) ? "Male" : "Female";
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblCountry.Text = (clsCountry.Find(_Person.NationalityCountryID)?.CountryName);
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;

            _LoadPersonImage();
        }

        void _LoadPersonImage()
        {
            // Set default image based on gender
            pbPersonImage.Image = (_Person.Gender == 0) ? Properties.Resources.Male_512 : Properties.Resources.Female_512;

            string ImagePath = _Person.ImagePath;

            if (!string.IsNullOrEmpty(ImagePath))
                if(File.Exists(ImagePath)) // Check if the image file exists
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show($"Image file not found: {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        
    }
}
