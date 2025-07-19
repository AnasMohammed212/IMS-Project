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

namespace IMS.SaleOrders.Sale_Order_Details
{
    public partial class frmListSaleOrdersDetails : Form
    {
        private static DataTable _dtAllDetails;
        private int _SaleOrderID;
        public frmListSaleOrdersDetails(int SaleOrderID)
        {
            InitializeComponent();
            _SaleOrderID = SaleOrderID;
        }
        private async Task _LoadDataAsync()
        {
            _dtAllDetails = await clsSaleOrderDetail.GetAllOrderDetailsBySaleOrderID(_SaleOrderID);
            dgvDetails.DataSource = _dtAllDetails;

            cbFilterBy.SelectedIndex = 0;

            if (dgvDetails.Columns.Count >= 5)
            {
                dgvDetails.Columns[0].HeaderText = "Detail ID";
                dgvDetails.Columns[0].Width = 80;

                dgvDetails.Columns[1].HeaderText = "Sale Order ID";
                dgvDetails.Columns[1].Visible = false;

                dgvDetails.Columns[2].HeaderText = "Product ID";
                dgvDetails.Columns[2].Width = 150;

                dgvDetails.Columns[3].HeaderText = "Quantity";
                dgvDetails.Columns[3].Width = 150;

                dgvDetails.Columns[4].HeaderText = "Unit Price";
                dgvDetails.Columns[4].Width = 170;
            }

            lblRecordsCount.Text = _dtAllDetails.Rows.Count.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmListSaleOrdersDetails_Load(object sender, EventArgs e)
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

            if (filterColumn == "DetailID" || filterColumn == "ProductID")
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

        private async void btnShowAddDetail_Click(object sender, EventArgs e)
        {
            frmAddUpdateDetail frm = new frmAddUpdateDetail(_SaleOrderID);
            frm.ShowDialog();
            await _LoadDataAsync();
        }
    }
}
