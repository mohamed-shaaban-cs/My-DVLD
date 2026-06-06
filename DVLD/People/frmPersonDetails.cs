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
    public partial class frmPersonDetails : Form
    {
        
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
