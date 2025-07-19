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

namespace IMS.SaleOrders
{
    public partial class frmListSaleOrders : Form
    {
        private static DataTable _dtAllSaleOrders;

        public frmListSaleOrders()
        {
            InitializeComponent();
        }

        private async Task _LoadDataAsync()
        {
            _dtAllSaleOrders = await clsSaleOrder.GetAllSaleOrders();
            dgvSaleOrders.DataSource = _dtAllSaleOrders;

            cbFilterBy.SelectedIndex = 0;

            if (dgvSaleOrders.Columns.Count >= 8)
            {
               
                dgvSaleOrders.Columns[5].HeaderText = "Created By";
                
            }


            lblRecordsCount.Text = _dtAllSaleOrders.Rows.Count.ToString();
        }


        private async void frmListSaleOrders_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Order ID":
                    filterColumn = "SaleOrderID";
                    break;
                case "Customer Name":
                    filterColumn = "CustomerName";
                    break;
                case "Created By":
                    filterColumn = "CreatedBy";
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
                _dtAllSaleOrders.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvSaleOrders.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "SaleOrderID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int id))
                {
                    _dtAllSaleOrders.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, id);
                }
                else
                {
                    _dtAllSaleOrders.DefaultView.RowFilter = "1=0";
                }
            }
            else
            {
                _dtAllSaleOrders.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecordsCount.Text = dgvSaleOrders.Rows.Count.ToString();
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
    }
}
