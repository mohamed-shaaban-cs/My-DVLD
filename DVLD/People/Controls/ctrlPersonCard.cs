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

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        clsPerson Person = new clsPerson();
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

        }

        public void LoadPersonInfo(int PersonID)
        {
            Person = clsPerson.Find(PersonID);
            if (Person != null)
            {
                lblPersonID.Text = $"ID: {Person.PersonID}" ?? "[????]";
                lblName.Text = $"{Person.FirstName} {Person.SecondName} {Person.ThirdName} {Person.LastName}" ?? "[????]";
                lblNationalNo.Text = Person.NationalNo ?? "[????]";
                lblGendor.Text = (Person.Gendor == 0)? "Male" : (Person.Gendor == 1) ? "Female" : "[????]";
                lblDateOfBirth.Text = Person.DateOfBirth.ToString("dd/MM/yyyy") ?? "[????]";
                lblCountry.Text = (clsCountry.Find(Person.NationalityCountryID)?.CountryName) ?? "[????]" ;
                lblPhone.Text = Person.Phone ?? "[????]";
                lblEmail.Text = Person.Email ?? "[????]";
                lblAddress.Text = Person.Address ?? "[????]";
                // Load Image
                if (!string.IsNullOrEmpty(Person.ImagePath) && System.IO.File.Exists(Person.ImagePath))
                {
                    using(Image img = Image.FromFile(Person.ImagePath))
                    {
                        pbPersonImage.BackgroundImage = new Bitmap(img);
                    }
                }
                else
                    pbPersonImage.BackgroundImage = (lblGendor.Text == "Female") ? Properties.Resources.Female_512 :Properties.Resources.Male_512 ;
            }
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(Person == null)
                return;

            frmAddUpdatePersonInofo frmAddUpdatePerson = new frmAddUpdatePersonInofo(Person.PersonID);
            frmAddUpdatePerson.ShowDialog();
            LoadPersonInfo(Person.PersonID);

        }
    }
}
