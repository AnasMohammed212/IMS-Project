using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS.PurchaseOrders;
using IMS_Business;

namespace IMS
{
    public partial class frmListPurchaseOrders : Form
    {
        private static DataTable _dtAllPurchaseOrders;
        public frmListPurchaseOrders()
        {
            InitializeComponent();
        }

        private async Task _LoadDataAsync()
        {
            _dtAllPurchaseOrders = await clsPurchaseOrder.GetAllPurchaseOrders();
            dgvPurchaseOrders.DataSource = _dtAllPurchaseOrders;

            cbFilterBy.SelectedIndex = 0;

            if (dgvPurchaseOrders.Columns.Count >= 8)
            {
                dgvPurchaseOrders.Columns[0].HeaderText = "Order ID";
                dgvPurchaseOrders.Columns[0].Width = 80;

                dgvPurchaseOrders.Columns[1].HeaderText = "Supplier ID";
                dgvPurchaseOrders.Columns[1].Visible = false;

                dgvPurchaseOrders.Columns[2].HeaderText = "Supplier Name";
                dgvPurchaseOrders.Columns[2].Width = 150;

                dgvPurchaseOrders.Columns[3].HeaderText = "Order Date";
                dgvPurchaseOrders.Columns[3].Width = 150;

                dgvPurchaseOrders.Columns[4].HeaderText = "Created By (ID)";
                dgvPurchaseOrders.Columns[4].Visible = false; 

                dgvPurchaseOrders.Columns[5].HeaderText = "Created By";
                dgvPurchaseOrders.Columns[5].Width = 150;

                dgvPurchaseOrders.Columns[6].HeaderText = "Status";
                dgvPurchaseOrders.Columns[6].Width = 100;

                dgvPurchaseOrders.Columns[7].HeaderText = "Notes";
                dgvPurchaseOrders.Columns[7].Width = 217;
            }

            lblRecordsCount.Text = _dtAllPurchaseOrders.Rows.Count.ToString();
        }

        private async void frmListPurchaseOrders_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Order ID":
                    filterColumn = "PurchaseOrderID";
                    break;
                case "Supplier Name":
                    filterColumn = "SupplierName";
                    break;
                case "Created By":
                    filterColumn = "CreatedByUserName";
                    break;
                case "Status":
                    filterColumn = "Status";
                    break;
                default:
                    filterColumn = "";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtAllPurchaseOrders.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPurchaseOrders.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "PurchaseOrderID")
            {
                _dtAllPurchaseOrders.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllPurchaseOrders.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecordsCount.Text = dgvPurchaseOrders.Rows.Count.ToString();
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

        private void btnShowAddUpdateProduct_Click(object sender, EventArgs e)
        {
            frmAddUpdatePurchaseOrder frm=new frmAddUpdatePurchaseOrder();
            frm.ShowDialog();
        }

        private void showPurchaseOrderInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPurchaseOrderInfo frm = new frmShowPurchaseOrderInfo((int)dgvPurchaseOrders.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
