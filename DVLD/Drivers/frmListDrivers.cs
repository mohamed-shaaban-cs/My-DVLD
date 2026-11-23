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

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        DataTable _dtDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
            cbFilters.SelectedIndex = 0;
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _dtDrivers = clsDriver.GetAllDrivers();
            dgvList.DataSource = _dtDrivers;
            lblRecordsCount.Text = dgvList.RowCount.ToString();
            ModrenUI_Interface.DataGridViewInterfacescs.DataGridViewModernStayle(dgvList);

            dgvList.Columns["DriverID"].HeaderText = "Driver ID";
            dgvList.Columns["PersonID"].HeaderText = "Person ID";
            dgvList.Columns["NationalNo"].HeaderText = "National No.";
            dgvList.Columns["FullName"].HeaderText = "Full Name";
            dgvList.Columns["Date"].HeaderText = "Date";
            dgvList.Columns["ActiveLicenses"].HeaderText = "Active Licenses";

        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSeach_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilters.SelectedItem.ToString())
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if (FilterColumn == "None" || tbSeach.Text == "")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvList.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "PersonID" || FilterColumn == "DriverID")
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, tbSeach.Text);
            }
            else
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", FilterColumn, tbSeach.Text);


             lblRecordsCount.Text = dgvList.Rows.Count.ToString();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilters.SelectedIndex == 0)
            {
                tbSeach.Visible = false;
                tbSeach.Text = "";
            }
            else
            {
                tbSeach.Visible = true;
                tbSeach.Text = "";
            }
        }

        private void tbSeach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedItem.ToString() == "Person ID" || cbFilters.SelectedItem.ToString() == "Driver ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }
    }
}
