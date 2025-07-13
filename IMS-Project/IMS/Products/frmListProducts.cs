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

namespace IMS.Products
{
    public partial class frmListProducts : Form
    {
        private static DataTable _dtAllProducts;
        private async void _LoadDataAsync()
        {
            _dtAllProducts = await clsProduct.GetAllProducts();
            dgvProducts.DataSource = _dtAllProducts;
            cbFilterBy.SelectedIndex = 0;
            if (dgvProducts.Columns.Count >= 7)
            {
                dgvProducts.Columns[0].HeaderText = "Product ID";
                dgvProducts.Columns[0].Width = 100;

                dgvProducts.Columns[1].HeaderText = "Product Name";
                dgvProducts.Columns[1].Width = 150;

                dgvProducts.Columns[2].HeaderText = "Description";
                dgvProducts.Columns[2].Width = 200;

                dgvProducts.Columns[3].HeaderText = "Category";
                dgvProducts.Columns[3].Width = 150;

                dgvProducts.Columns[4].HeaderText = "Supplier";
                dgvProducts.Columns[4].Width = 150;

                dgvProducts.Columns[5].HeaderText = "Purchase Price";
                dgvProducts.Columns[5].Width = 120;

                dgvProducts.Columns[6].HeaderText = "Sale Price";
                dgvProducts.Columns[6].Width = 120;

                dgvProducts.Columns[7].HeaderText = "Unit";
                dgvProducts.Columns[7].Width = 100;
            }
            lblRecordsCount.Text = _dtAllProducts.Rows.Count.ToString();
        }
        public frmListProducts()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListProducts_Load(object sender, EventArgs e)
        {
            _LoadDataAsync();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Product ID":
                    FilterColumn = "ProductID";
                    break;
                case "Supplier Name":
                    FilterColumn = "SupplierName";
                    break;
                case "Product Name":
                    FilterColumn = "ProductName";
                    break;

                case "Category":
                    FilterColumn = "CategoryName";
                    break;

                default:
                    FilterColumn = "";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllProducts.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvProducts.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "ProductID")
                _dtAllProducts.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllProducts.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = dgvProducts.Rows.Count.ToString();
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

        private void btnShowAddUpdateProduct_Click(object sender, EventArgs e)
        {
            frmAddUpdateProduct frmAddUpdateProduct = new frmAddUpdateProduct();
            frmAddUpdateProduct.ShowDialog();
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateProduct frm = new frmAddUpdateProduct();
            frm.ShowDialog();
            _LoadDataAsync();
        }

        private void editProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateProduct frm =new frmAddUpdateProduct((int)dgvProducts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _LoadDataAsync();
        }

        private async void deleteProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ProductID = (int)dgvProducts.CurrentRow.Cells[0].Value;
            if (await clsProduct.DeleteProduct(ProductID))
            {
                MessageBox.Show("Product has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListProducts_Load(null, null);
            }

            else
                MessageBox.Show("Product is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _LoadDataAsync();
        }

        private void showProductInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowProductInfo frm = new frmShowProductInfo((int)dgvProducts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
