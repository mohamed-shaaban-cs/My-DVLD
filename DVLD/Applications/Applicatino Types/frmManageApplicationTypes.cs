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

namespace DVLD.Applications.Applicatino_Types
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }
        DataTable _dtApplicationTypes;
        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {

            _RefreshList();
            ModrenUI_Interface.DataGridViewInterfacescs.DataGridViewModrenStayle(_dgvApplicationTypes);
            if (_dgvApplicationTypes.Rows.Count > 0)
            {
                _dgvApplicationTypes.Columns[0].HeaderText = "ID";
                _dgvApplicationTypes.Columns[0].Width = 100;
                _dgvApplicationTypes.Columns[1].HeaderText = "Title";
                _dgvApplicationTypes.Columns[1].Width = 250;
                _dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                _dgvApplicationTypes.Columns[2].Width = 100;
            }
        }

        void _RefreshList()
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            _dgvApplicationTypes.DataSource = _dtApplicationTypes;
            lblRecordsCount.Text = _dgvApplicationTypes.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frmUpdate = new frmUpdateApplicationType(int.Parse(_dgvApplicationTypes.CurrentRow.Cells[0].Value.ToString()));
            frmUpdate.ShowDialog();
            frmManageApplicationTypes_Load(null, null);
        }
    }
}
