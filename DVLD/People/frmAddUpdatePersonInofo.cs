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
        enum enMode { Add=0, Update=1}

        enMode _Mode;
        int _ID { get; set; }

        clsPerson Person = new clsPerson();

        public frmAddUpdatePersonInofo(int ID)
        {
            InitializeComponent();
            _ID = ID;
            _Mode = (ID == -1) ? enMode.Add : enMode.Update;
            


            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            // Fill Country Combobox
            var dtCountries = clsCountry.GetAllCountries();
            if(dtCountries != null && dtCountries.Rows.Count > 0)
                cbCountry.Items.AddRange(dtCountries.AsEnumerable().Select(r => r["CountryName"].ToString() ?? string.Empty).ToArray());

            // Set Default Country to Egypt
            cbCountry.SelectedIndex = 50;

            // Set Default Image
            setDefultImage();

            


        }
        private void frmAddUpdatePersonInofo_Load(object sender, EventArgs e)
        {
            if(_Mode == enMode.Add)
            {
                lblTitle.Text = "Add New Person";
                lblRemove.Visible = false;
                lblPersonID.Text = "-1";
            }
            else if(_Mode == enMode.Update)
            {
                lblTitle.Text = "Update Person Info";
                


                // Get Person Info
                Person = clsPerson.Find(_ID);
                if(Person == null)
                {
                    MessageBox.Show("Person not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Fill Data to Controls

                lblPersonID.Text = Person.PersonID.ToString();
                tbNationalNo.Text = Person.NationalNo.ToString();
                tbFristName.Text = Person.FirstName.ToString();
                tbSecondName.Text = Person.SecondName.ToString();
                tbThirdName.Text = Person.ThirdName.ToString();
                tbLastName.Text = Person.LastName.ToString();
                dtpDateOfBirth.Value = Person.DateOfBirth;
                if (Person.Gender == 0)
                    rbtnMale.Checked = true;
                else if (Person.Gender == 1)
                    rbtnFemale.Checked = true;

                rtbAddress.Text = Person.Address.ToString();
                tbPhone.Text = Person.Phone.ToString();
                tbEmail.Text = Person.Email.ToString();
                cbCountry.SelectedIndex = cbCountry.FindStringExact(clsCountry.Find(Person.NationalityCountryID)?.CountryName ?? string.Empty);

                // Set Person Image
                if (!string.IsNullOrEmpty(Person.ImagePath) && File.Exists(Person.ImagePath))
                {
                    using (Image tempImg = Image.FromFile(Person.ImagePath))
                    {
                        pbPersonImage.BackgroundImage = new Bitmap(tempImg);
                    }
                    lblRemove.Visible = true;
                }

            }
        }

        void setDefultImage()
        {
            if (!string.IsNullOrEmpty(Person.ImagePath))
                return;

            if (rbtnMale.Checked)
            {
                pbPersonImage.BackgroundImage = Properties.Resources.Male_512;
            }
            else if(rbtnFemale.Checked)
            {
                pbPersonImage.BackgroundImage = Properties.Resources.Female_512;
            }

            lblRemove.Visible = false;
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

        private void inputControl_Validating_IsNullOrEmpty(object sender, CancelEventArgs e)
        {
            WinFormValidation.inputControl_Validating_IsNullOrEmpty(errorProvider1, sender, e);
        }



        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNationalNo.Text))  // Check if the field is empty
            {
                errorProvider1.SetError(tbNationalNo, "This field is required.");
            }
            else if (clsPerson.IsPersonExist(tbNationalNo.Text) && Person.NationalNo != tbNationalNo.Text) // Check if the national number already exists for another person outside NationalNo of the current person being updated
            {
                errorProvider1.SetError(tbNationalNo, "Person with this national number already exists.");
            }
            else
            {
                errorProvider1.SetError(tbNationalNo, ""); // Clear the error if validation passes
            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            WinFormValidation.ValidatingEmailInTextBox(errorProvider1, tbEmail);
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
                if (Person.ImagePath != null && File.Exists(Person.ImagePath))
                    File.Delete(Person.ImagePath);

                // Update Person.ImagePath with the new image path
                Person.ImagePath = destFileName;


                // Display the selected image in the PictureBox
                using (Image tempImg = Image.FromFile(destFileName))
                {
                    pbPersonImage.BackgroundImage = new Bitmap(tempImg);
                }
                lblRemove.Visible = true;

            }

        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            

            if (Person.ImagePath != null && File.Exists(Person.ImagePath))
                File.Delete(Person.ImagePath);
            
            Person.ImagePath = "";

            setDefultImage();
        }

        private void btnSavePersonData_Click(object sender, EventArgs e)
        {
            ValidateChildren();
            if (WinFormValidation.HasValidationErrors(this, errorProvider1))
            {
                MessageBox.Show("Please fix validation errors before saving.", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Fill Person Object with Data from Controls
            Person.NationalNo = tbNationalNo.Text;
            Person.FirstName = tbFristName.Text;
            Person.SecondName = tbSecondName.Text;
            Person.ThirdName = tbThirdName.Text;
            Person.LastName = tbLastName.Text;
            Person.DateOfBirth = dtpDateOfBirth.Value;
            Person.Email = tbEmail.Text;
            Person.Address = rtbAddress.Text;
            Person.Phone = tbPhone.Text;
            Person.Gender =  (byte) (rbtnMale.Checked ? 0 : 1 );
            Person.NationalityCountryID = clsCountry.Find(cbCountry.SelectedItem.ToString()).CountryID;

            if (MessageBox.Show("Are you sure you want to save the person data?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (Person.Save())
                {
                    MessageBox.Show("Person data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Mode = enMode.Update;
                    lblTitle.Text = "Update Person Info";
                    _ID = Person.PersonID;
                    lblPersonID.Text = _ID.ToString();

                }
                else
                {
                    MessageBox.Show("Failed to save person data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
