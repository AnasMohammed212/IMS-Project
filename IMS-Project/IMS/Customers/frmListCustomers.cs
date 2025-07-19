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

namespace IMS.Customers
{
    public partial class frmListCustomers : Form
    {
        private static DataTable _dtAllCustomers;
        public frmListCustomers()
        {
            InitializeComponent();
        }
        private async Task _LoadDataAsync()
        {
            _dtAllCustomers = await clsCustomer.GetAllCustomers();
            dgvCustomers.DataSource = _dtAllCustomers;

            cbFilterBy.SelectedIndex = 0;

            if (dgvCustomers.Columns.Count >= 6)
            {
                dgvCustomers.Columns[0].HeaderText = "Customer ID";
                dgvCustomers.Columns[0].Width = 100;

                dgvCustomers.Columns[1].HeaderText = "Customer Name";
                dgvCustomers.Columns[1].Width = 150;

                dgvCustomers.Columns[2].HeaderText = "Contact Person ID";
                

                dgvCustomers.Columns[3].HeaderText = "Contact Person";
                dgvCustomers.Columns[3].Width = 150;

                dgvCustomers.Columns[4].HeaderText = "Phone";
                dgvCustomers.Columns[4].Width = 120;

                dgvCustomers.Columns[5].HeaderText = "Email";
                dgvCustomers.Columns[5].Width = 200;
            }
            lblRecordsCount.Text = _dtAllCustomers.Rows.Count.ToString();
        }
        private async void frmListCustomers_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Customer ID":
                    filterColumn = "CustomerID";
                    break;
                case "Customer Name":
                    filterColumn = "CustomerName";
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
                _dtAllCustomers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvCustomers.Rows.Count.ToString();
                return;
            }


            if (filterColumn == "CustomerID" || filterColumn == "ContactPersonID")
            {
                if (!int.TryParse(txtFilterValue.Text.Trim(), out _))
                {

                    _dtAllCustomers.DefaultView.RowFilter = "";
                    errorProvider1.SetError(txtFilterValue, "Invalid Number.");
                    return;


                }
            }


                    if (filterColumn == "CustomerID"|| filterColumn == "ContactPersonID")
            {
                _dtAllCustomers.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllCustomers.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecordsCount.Text = dgvCustomers.Rows.Count.ToString();
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
