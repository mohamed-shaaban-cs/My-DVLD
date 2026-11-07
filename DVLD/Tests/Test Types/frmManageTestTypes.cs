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

namespace DVLD.Applications.Test_Types
{
    public partial class frmManageTestTypes : Form
    {
        DataTable _dtTestTypes;
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            
            _dtTestTypes = clsTestType.GetAllTestTypes();
            _dgvTestTypes.DataSource = _dtTestTypes;
            lblRecordsCount.Text = _dgvTestTypes.Rows.Count.ToString();

            ModrenUI_Interface.DataGridViewInterfacescs.DataGridViewModernStayle(_dgvTestTypes);

            if (_dgvTestTypes.Rows.Count > 0)
            {
                _dgvTestTypes.Columns[0].HeaderText = "ID";
                _dgvTestTypes.Columns[0].Width = 100;
                _dgvTestTypes.Columns[1].HeaderText = "Title";
                _dgvTestTypes.Columns[1].Width = 150;
                _dgvTestTypes.Columns[2].HeaderText = "Description";
                _dgvTestTypes.Columns[2].Width = 200;
                _dgvTestTypes.Columns[3].HeaderText = "Fees";
                _dgvTestTypes.Columns[3].Width = 100;
            }

        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frmUpdate = new frmUpdateTestType(int.Parse(_dgvTestTypes.CurrentRow.Cells[0].Value.ToString()));
            frmUpdate.ShowDialog();
            frmManageTestTypes_Load(null, null);
        }
    }
}
