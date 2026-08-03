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

namespace DVLD.People
{
    public partial class frmFindPerson : Form
    {
        public frmFindPerson()
        {
            InitializeComponent();
        }
        // Declare a delegate
        public delegate void DatabackEventHandler(object sender,int PersonID,clsPerson PersonData);
        //Declare an event using the delegate
        public event DatabackEventHandler DataBack;

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID, ctrlPersonCardWithFilter1.SelectedPersonInfo);
            this.Close();
        }

        private void frmFindPerson_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataBack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID, ctrlPersonCardWithFilter1.SelectedPersonInfo);
        }
    }
}
