using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_Business;

namespace IMS.Suppliers
{
    public partial class frmListSuppliers : Form
    {
        private static DataTable _dtAllSuppliers;
        public frmListSuppliers()
        {
            InitializeComponent();
        }
        private async Task _LoadDataAsync()
        {
            _dtAllSuppliers = await clsSupplier.GetAllSuppliers();
            dgvSuppliers.DataSource = _dtAllSuppliers;

            cbFilterBy.SelectedIndex = 0;

            if (dgvSuppliers.Columns.Count >= 6)
            {
                dgvSuppliers.Columns[0].HeaderText = "Supplier ID";
                dgvSuppliers.Columns[0].Width = 100;

                dgvSuppliers.Columns[1].HeaderText = "Supplier Name";
                dgvSuppliers.Columns[1].Width = 150;

                dgvSuppliers.Columns[2].HeaderText = "Contact Person ID";


                dgvSuppliers.Columns[3].HeaderText = "Contact Person";
                dgvSuppliers.Columns[3].Width = 150;

                dgvSuppliers.Columns[4].HeaderText = "Phone";
                dgvSuppliers.Columns[4].Width = 120;

                dgvSuppliers.Columns[5].HeaderText = "Email";
                dgvSuppliers.Columns[5].Width = 230;
            }
            lblRecordsCount.Text = _dtAllSuppliers.Rows.Count.ToString();
        }
        private async void frmListSuppliers_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Supplier ID":
                    filterColumn = "SupplierID";
                    break;
                case "Supplier Name":
                    filterColumn = "SupplierName";
                    break;
                case "Contact Person ID":
                    filterColumn = "ContactPersonID";
                    break;
                case "Contact Person Name":
                    filterColumn = "ContactPersonName";
                    break;
                case "Phone":
                    filterColumn = "Phone";
                    break;
                case "Email":
                    filterColumn = "Email";
                    break;
                default:
                    filterColumn = "";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "")
            {
                _dtAllSuppliers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvSuppliers.Rows.Count.ToString();
                return;
            }


            if (filterColumn == "SupplierID" || filterColumn == "ContactPersonID")
            {
                if (!int.TryParse(txtFilterValue.Text.Trim(), out _))
                {

                    _dtAllSuppliers.DefaultView.RowFilter = "";
                    errorProvider1.SetError(txtFilterValue, "Invalid Number.");
                    return;


                }
            }


            if (filterColumn == "SupplierID" || filterColumn == "ContactPersonID")
            {
                _dtAllSuppliers.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllSuppliers.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecordsCount.Text = dgvSuppliers.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnShowAddUpdateProduct_Click(object sender, EventArgs e)
        {
            frmAddUpdateSupplier frm = new frmAddUpdateSupplier();
            frm.ShowDialog();
            await _LoadDataAsync();
        }

        private async void addSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateSupplier frm = new frmAddUpdateSupplier();
            frm.ShowDialog();
            await _LoadDataAsync();
        }

        private async void editSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateSupplier frm = new frmAddUpdateSupplier((int)dgvSuppliers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            await _LoadDataAsync();
        }

        private async void deleteSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SupplierID = (int)dgvSuppliers.CurrentRow.Cells[0].Value;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (await clsSupplier.DeleteSupplier(SupplierID))
                {
                    MessageBox.Show("Supplier has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await _LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("Supplier is not deleted due to data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
