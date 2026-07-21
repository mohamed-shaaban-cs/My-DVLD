using DVLD.Properties;
using DVLD_BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmAddUpdatePersonInofo : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int personID);

        //Declare an event uding the delegate
        public event DataBackEventHandler DataBack;

        enum enMode { Add=0, Update=1}
        public enum enGendor { Male = 0, Female = 1 };
        enMode _Mode;
        int _PersonID = -1;
        clsPerson _Person = new clsPerson();

        public frmAddUpdatePersonInofo()
        {
            InitializeComponent();
            _Mode = enMode.Add;
        }
        public frmAddUpdatePersonInofo(int ID)
        {
            InitializeComponent();
            _PersonID = ID;
            _Mode = enMode.Update;
        }

        void _FillCountriesInComoboBox()
        {
            // Fill Country Combobox
            var dtCountries = clsCountry.GetAllCountries();
            if (dtCountries != null && dtCountries.Rows.Count > 0)
                cbCountry.Items.AddRange(dtCountries.AsEnumerable().Select(r => r["CountryName"].ToString() ?? string.Empty).ToArray());
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillCountriesInComoboBox();

            if (_Mode == enMode.Add)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            //set default image for the person.
            if (rbtnMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            //hide/show the remove linke incase there is no image for the person.
            llblRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to egypt.
            cbCountry.SelectedIndex = cbCountry.FindString("Egypt");

            tbFristName.Text = "";
            tbSecondName.Text = "";
            tbThirdName.Text = "";
            tbLastName.Text = "";
            tbNationalNo.Text = "";
            rbtnMale.Checked = true;
            tbPhone.Text = "";
            tbEmail.Text = "";
            rtbAddress.Text = "";


        }

        void _LoadData()
        {
            // Get Person Info
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Person not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found

            lblPersonID.Text = _Person.PersonID.ToString();
            tbNationalNo.Text = _Person.NationalNo;
            tbFristName.Text = _Person.FirstName;
            tbSecondName.Text = _Person.SecondName;
            tbThirdName.Text = _Person.ThirdName;
            tbLastName.Text = _Person.LastName;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gender == 0)
                rbtnMale.Checked = true;
            else
                rbtnFemale.Checked = true;

            rtbAddress.Text = _Person.Address;
            tbPhone.Text = _Person.Phone;
            tbEmail.Text = _Person.Email;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);

            // Load Person Image
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }

            // Show or hide the remove image link based on whether an image path exists
            llblRemoveImage.Visible = !string.IsNullOrEmpty(_Person.ImagePath);
        }

        private void frmAddUpdatePersonInofo_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        void setDefultImage()
        {
            if (!string.IsNullOrEmpty(_Person.ImagePath))
                return;

            if (rbtnMale.Checked)
            {
                pbPersonImage.Image = Properties.Resources.Male_512;
            }
            else if(rbtnFemale.Checked)
            {
                pbPersonImage.Image = Properties.Resources.Female_512;
            }

            llblRemoveImage.Visible = false;
        }


        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            setDefultImage();
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            setDefultImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
           Control Temp = sender as Control ;
            if(string.IsNullOrWhiteSpace(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This Field is Required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }



        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            string NationalNo = tbNationalNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(NationalNo))  // Check if the field is empty
            {
                e.Cancel= true;
                errorProvider1.SetError(tbNationalNo, "This field is required.");
                return;
            }

            if (clsPerson.IsPersonExist(NationalNo) && _Person.NationalNo != NationalNo) // Check if the national number already exists for another person outside NationalNo of the current person being updated
            {
                e.Cancel= true;
                errorProvider1.SetError(tbNationalNo, "National Number is used for another person!");
            }
            else
            {
                errorProvider1.SetError(tbNationalNo, null); // Clear the error if validation passes
            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase  it's empty
            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                errorProvider1.SetError(tbEmail, null);
                return;
            }
                
            if(!clsValidation.ValidateEmail(tbEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbEmail, "Invalid Email Format!");
            }
            else
            {
                errorProvider1.SetError(tbEmail, null);
            }


        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "Select Person Image";
            openFileDialog1.DefaultExt = "Image Files | *.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Filter = "Image Files | *.jpg;*.jpeg;*.png;*.bmp;*.gif";

            string sourceFileName = "";
            string destFileName = "";

            // Get the selected file path
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceFileName = openFileDialog1.FileName;

                string FolderImages = "C:\\DVLD-People-Images";
                // Create directory if not exists
                if (!Directory.Exists(FolderImages))
                    Directory.CreateDirectory(FolderImages);

                // Generate a unique file name to avoid conflicts
                destFileName = Path.Combine(FolderImages, Guid.NewGuid().ToString() + Path.GetExtension(sourceFileName)); 
                //Copy Image To FolderImages
                File.Copy(sourceFileName, destFileName);

                // Set Default Image if the current image is not the default one to avoid keeping unused images in the folder
                
                   

                // Delete Old Image if exists and update Person.ImagePath
                if (_Person.ImagePath != null && File.Exists(_Person.ImagePath))
                    File.Delete(_Person.ImagePath);

                // Update Person.ImagePath with the new image path
                _Person.ImagePath = destFileName;


                // Display the selected image in the PictureBox
                using (Image tempImg = Image.FromFile(destFileName))
                {
                    pbPersonImage.Image = new Bitmap(tempImg);
                }
                llblRemoveImage.Visible = true;

            }

        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            

            if (_Person.ImagePath != null && File.Exists(_Person.ImagePath))
                File.Delete(_Person.ImagePath);
            
            _Person.ImagePath = "";

            setDefultImage();
        }

        private bool _HandlePersonImage()
        {
            // Implementation for handling person image
            return true;
        }

        private void btnSavePersonData_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we Don't Contniue because the form is not valid.
                MessageBox.Show("Please fix validation errors before saving.", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(!_HandlePersonImage())
            {
                MessageBox.Show("Failed to handle person image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Fill Person Object with Data from Controls
            _Person.NationalNo = tbNationalNo.Text.Trim();
            _Person.FirstName = tbFristName.Text.Trim();
            _Person.SecondName = tbSecondName.Text.Trim();
            _Person.ThirdName = tbThirdName.Text.Trim();
            _Person.LastName = tbLastName.Text.Trim();
            _Person.Email = tbEmail.Text.Trim();
            _Person.Address = rtbAddress.Text.Trim();
            _Person.Phone = tbPhone.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            _Person.Gender =  (short) (rbtnMale.Checked ? enGendor.Male : enGendor.Female );

            _Person.NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";
            

            if (MessageBox.Show("Are you sure you want to save the person data?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (_Person.Save())
                {
                    MessageBox.Show("Person data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Mode = enMode.Update;
                    lblTitle.Text = "Update Person Info";
                    _PersonID = _Person.PersonID;
                    lblPersonID.Text = _PersonID.ToString();

                    //Trigger the event to send data back to the caller form.
                    DataBack?.Invoke(this, _PersonID);

                }
                else
                {
                    MessageBox.Show("Failed to save person data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
