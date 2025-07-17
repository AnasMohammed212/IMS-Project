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

namespace IMS.PurchaseOrders.Purchase_Order_Details
{
    public partial class frmListPurchaseOrderDetails : Form
    {
        private static DataTable _dtAllDetails;
        private int _PurchaseOrderID;
        public frmListPurchaseOrderDetails(int purchaseOrderID)
        {
            InitializeComponent();
            _PurchaseOrderID = purchaseOrderID;
        }
        private async Task _LoadDataAsync()
        {
            _dtAllDetails = await clsPurchaseOrderDetail.GetAllOrderDetailsByPurchaseOrderID(_PurchaseOrderID);
            dgvDetails.DataSource = _dtAllDetails;

            cbFilterBy.SelectedIndex = 0;

            if (dgvDetails.Columns.Count >= 8)
            {
                dgvDetails.Columns[0].HeaderText = "Detail ID";
                dgvDetails.Columns[0].Width = 80;

                dgvDetails.Columns[1].HeaderText = "Purchase Order ID";
                dgvDetails.Columns[1].Visible = false;

                dgvDetails.Columns[2].HeaderText = "Product ID";
                dgvDetails.Columns[2].Width = 150;

                dgvDetails.Columns[3].HeaderText = "Qunatity";
                dgvDetails.Columns[3].Width = 150;

                dgvDetails.Columns[4].HeaderText = "Unit Price";
                dgvDetails.Columns[4].Width = 170;
                

            }

            lblRecordsCount.Text = _dtAllDetails.Rows.Count.ToString();
        }
        private async void frmListPurchaseOrderDetails_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Detail ID":
                    filterColumn = "DetailID";
                    break;
               
                case "Product ID":
                    filterColumn = "ProductID";
                    break;
                default:
                    filterColumn = "";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtAllDetails.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllDetails.Rows.Count.ToString();
                return;
            }

            if (filterColumn== "DetailID"||filterColumn== "ProductID")
            {
                _dtAllDetails.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllDetails.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecordsCount.Text = dgvDetails.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async  void btnShowAddDetail_Click(object sender, EventArgs e)
        {
            frmAddUpdateDetail frm = new frmAddUpdateDetail(_PurchaseOrderID);
            frm.ShowDialog();
            await _LoadDataAsync();
        }

        private async void editDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateDetail frm = new frmAddUpdateDetail(_PurchaseOrderID, (int)dgvDetails.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            await _LoadDataAsync();
        }

        private async void addDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateDetail frm = new frmAddUpdateDetail(_PurchaseOrderID);
            frm.ShowDialog();
            await _LoadDataAsync();
        }

        private async void deleteDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDetails.CurrentRow == null)
            {
                MessageBox.Show("Please select a detail to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int detailID = (int)dgvDetails.CurrentRow.Cells["DetailID"].Value;

            var result = MessageBox.Show($"Are you sure you want to delete detail ID = {detailID}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            bool isDeleted = await clsPurchaseOrderDetail.DeletePurchaseOrderDetail(detailID);

            if (isDeleted)
            {
                MessageBox.Show("Detail deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _LoadDataAsync();
            }
            else
            {
                MessageBox.Show("Cannot delete this detail. It may be linked to other data.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
