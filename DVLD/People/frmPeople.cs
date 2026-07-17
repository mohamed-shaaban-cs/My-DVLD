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
            if (cbFilters.SelectedIndex == 0)
                tbSeach.Visible = false;
            else
            {
                tbSeach.Visible = true;
                tbSeach.Text = "";
            }
        }

        private void tbSeach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilters.SelectedItem.ToString() == "Person ID"|| cbFilters.SelectedItem.ToString() == "Phone")
            {
                if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void tbSeach_TextChanged(object sender, EventArgs e)
        {
            if (tbSeach.Text == "" || tbSeach.Text == null)
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
                dtPeople.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, tbSeach.Text);
            else if (filterColumn != "PersonID")
                dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filterColumn, tbSeach.Text);

            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }



        private void crtlAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersonInofo frmAddPerson = new frmAddUpdatePersonInofo(-1);
            frmAddPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void crtlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crtlAddNewPerson_Click(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count > 0)
            {
                int personID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);
                frmAddUpdatePersonInofo frmEditPerson = new frmAddUpdatePersonInofo(personID);
                frmEditPerson.ShowDialog();
                _RefreshPeopleList();
            }
            else
            {
                MessageBox.Show("Please select a person to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count > 0)
            {
                int personID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);
               
                if (MessageBox.Show("Are you sure you want to delete this person?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(clsPerson.DeletePerson(personID))
                    {
                        MessageBox.Show("Person deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RefreshPeopleList();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the person. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count > 0)
            {
                int personID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);
                frmPersonDetails frmPersonDetails = new frmPersonDetails(personID);
                frmPersonDetails.ShowDialog();
            }
        }

        private void dgvPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }
    }
    }

