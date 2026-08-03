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
    public partial class frmManagePeople : Form
    {

        private static DataTable dtAllPeople;

        private static DataTable dtPeople;
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
            ModrenUI_Interface.DataGridViewInterfacescs.DataGridViewModrenStayle(dgvPeople);
            cbFilters.SelectedIndex = 0;

            dgvPeople.Columns["PersonID"].HeaderText = "Person ID";
            dgvPeople.Columns["NationalNo"].HeaderText = "National No.";
            dgvPeople.Columns["FirstName"].HeaderText = "First Name";
            dgvPeople.Columns["SecondName"].HeaderText = "Second Name";
            dgvPeople.Columns["ThirdName"].HeaderText = "Third Name";
            dgvPeople.Columns["LastName"].HeaderText = "Last Name";
            dgvPeople.Columns["GenderCaption"].HeaderText = "Gender";
            dgvPeople.Columns["DateOfBirth"].HeaderText = "Date Of Birth";
            dgvPeople.Columns["Phone"].HeaderText = "Phone";
            dgvPeople.Columns["CountryName"].HeaderText = "Nationality";

        }

        void _RefreshPeopleList()
        {
            dtAllPeople = clsPerson.GetAllPersons();

            dtPeople = dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "GenderCaption", "DateOfBirth", "CountryName", "Phone", "Email");

            dgvPeople.DataSource = dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            tbSeach.Visible = (cbFilters.SelectedIndex != 0);

            if(tbSeach.Visible )
            {
                tbSeach.Text = "";
                tbSeach.Focus();
            }
        }

        private void tbSeach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedItem.ToString() == "Person ID" || cbFilters.SelectedItem.ToString() == "Phone")
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void tbSeach_TextChanged(object sender, EventArgs e)
        {
            if (tbSeach.Text.Trim() == "" || tbSeach.Text == null)
            {
                dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }
            string filterColumn = "";
            switch (cbFilters.SelectedItem.ToString())
            {
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "National No.":
                    filterColumn = "NationalNo";
                    break;
                case "First Name":
                    filterColumn = "FirstName";
                    break;
                case "Second Name":
                    filterColumn = "SecondName";
                    break;
                case "Third Name":
                    filterColumn = "ThirdName";
                    break;
                case "Last Name":
                    filterColumn = "LastName";
                    break;
                case "Gender":
                    filterColumn = "GenderCaption";
                    break;
                case "Nationality":
                    filterColumn = "CountryName";
                    break;
                default: 
                    filterColumn = cbFilters.SelectedItem.ToString();
                    break;
            }
            if (filterColumn == "PersonID")
                dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, tbSeach.Text.Trim());
            else 
                dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filterColumn, tbSeach.Text.Trim());

            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }



        private void crtlAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersonInofo frmAddPerson = new frmAddUpdatePersonInofo();
            frmAddPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {  
                
                frmAddUpdatePersonInofo frmEditPerson = new frmAddUpdatePersonInofo((int)dgvPeople.SelectedRows[0].Cells["PersonID"].Value);
                frmEditPerson.ShowDialog();
                _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this person?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson((int)dgvPeople.SelectedRows[0].Cells["PersonID"].Value))
                {
                    MessageBox.Show("Person deleted successfully.", "successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                {
                    MessageBox.Show("Failed to delete the person. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails((int)dgvPeople.SelectedRows[0].Cells["PersonID"].Value);
            frmPersonDetails.ShowDialog();
            _RefreshPeopleList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    }

