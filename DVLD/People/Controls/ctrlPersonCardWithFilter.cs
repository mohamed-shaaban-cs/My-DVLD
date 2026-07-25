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

        private void crtlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedIndex = 0;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            if(cbFindBy.SelectedIndex == 0)
            {
                ctrlPersonCard1.LoadPersonInfo(mtbSearchBox.Text);
            }
            else if (cbFindBy.SelectedIndex == 1)
            {
                ctrlPersonCard1.LoadPersonInfo(int.Parse(mtbSearchBox.Text));
            }
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFindBy.SelectedText == "Person ID")
            {
                mtbSearchBox.Mask = "000000000"; 
            }
            else
            {
                mtbSearchBox.Mask = "";
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersonInofo frmAddUpdatePersonInofo1 = new frmAddUpdatePersonInofo();
            frmAddUpdatePersonInofo1.DataBack += frm_DataBackNewPersonID;
            frmAddUpdatePersonInofo1.ShowDialog();

            
        }

        private void frm_DataBackNewPersonID(object sender, int personID)
        {
            ctrlPersonCard1.LoadPersonInfo(personID);
        }
    }
}
