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

namespace IMS
{
    public partial class frmListStock : Form
    {
        private static DataTable _dtAllStock;
        public frmListStock()
        {
            InitializeComponent();
        }
        private async Task _LoadDataAsync()
        {
            _dtAllStock = await clsStock.GetAllStock();
            dgvStock.DataSource = _dtAllStock;
            cbFilterBy.SelectedIndex = 0;

            if (dgvStock.Columns.Count >= 5)
            {
                dgvStock.Columns[0].HeaderText = "Stock ID";
                dgvStock.Columns[0].Width = 100;

                dgvStock.Columns[1].HeaderText = "Product ID";
                dgvStock.Columns[1].Width = 100;

                dgvStock.Columns[2].HeaderText = "Product Name";
                dgvStock.Columns[2].Width = 300;

                dgvStock.Columns[3].HeaderText = "Quantity";
                dgvStock.Columns[3].Width = 130;

                dgvStock.Columns[4].HeaderText = "Last Updated";
                dgvStock.Columns[4].Width = 220;
            }

            lblRecordsCount.Text = _dtAllStock.Rows.Count.ToString();
        }
        private async void frmListStock_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();

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

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Stock ID":
                    filterColumn = "StockID";
                    break;
                case "Product ID":
                    filterColumn = "ProductID";
                    break;
                case "Product Name":
                    filterColumn = "ProductName";
                    break;
                default:
                    filterColumn = "";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtFilterValue.Text) || filterColumn == "")
            {
                _dtAllStock.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvStock.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "StockID" || filterColumn == "ProductID")
                _dtAllStock.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllStock.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvStock.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
