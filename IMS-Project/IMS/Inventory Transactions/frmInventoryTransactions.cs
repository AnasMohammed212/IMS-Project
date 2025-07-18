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
    public partial class frmInventoryTransactions : Form
    {
        private static DataTable _dtAllTransactions;
        public frmInventoryTransactions()
        {
            InitializeComponent();
        }
        private async Task _LoadDataAsync()
        {
            _dtAllTransactions = await clsInventoryTransaction.GetAllTransactions();
            dgvInventoryTransactions.DataSource = _dtAllTransactions;
            cbFilterBy.SelectedIndex = 0;
            if (dgvInventoryTransactions.Columns.Count > 0)
            {
                dgvInventoryTransactions.Columns[0].HeaderText = "Transaction ID";
                dgvInventoryTransactions.Columns[0].Width = 80;

                dgvInventoryTransactions.Columns[1].HeaderText = "Product Name";
                dgvInventoryTransactions.Columns[1].Width = 150;

                dgvInventoryTransactions.Columns[2].HeaderText = "Quantity";
                dgvInventoryTransactions.Columns[2].Width = 80;

                dgvInventoryTransactions.Columns[3].HeaderText = "Transaction Type";
                dgvInventoryTransactions.Columns[3].Width = 100;

                dgvInventoryTransactions.Columns[4].HeaderText = "Transaction Date";
                dgvInventoryTransactions.Columns[4].Width = 150;

                dgvInventoryTransactions.Columns[5].HeaderText = "Performed By";
                dgvInventoryTransactions.Columns[5].Width = 150;

                dgvInventoryTransactions.Columns[6].HeaderText = "Notes";
                dgvInventoryTransactions.Columns[6].Width = 270;



            }
            lblRecordsCount.Text = _dtAllTransactions.Rows.Count.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmInventoryTransactions_Load(object sender, EventArgs e)
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
            else
            {
                _dtAllTransactions.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvInventoryTransactions.Rows.Count.ToString();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Transaction ID":
                    filterColumn = "TransactionID";
                    break;
                case "Product Name":
                    filterColumn = "ProductName";
                    break;
                case "Performed By":
                    filterColumn = "UserName";
                    break;
                case "Transaction Type":
                    filterColumn = "TransactionType";
                    break;
                default:
                    filterColumn = "";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtFilterValue.Text) || string.IsNullOrEmpty(filterColumn))
            {
                
                _dtAllTransactions.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvInventoryTransactions.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "TransactionID")
            {
                
                _dtAllTransactions.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
               
                _dtAllTransactions.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecordsCount.Text = dgvInventoryTransactions.Rows.Count.ToString();
        }
    }
}
