using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS.People.Controls;
using IMS_Business;

namespace IMS.Products.Controls
{
    public partial class ctrlProductCardWithFilter : UserControl
    {
        public event Action<int> OnProductSelected;
        protected virtual void ProductSelected(int ProductID)
        {
            OnProductSelected?.Invoke(ProductID);
        }
        private clsProduct _SelectedProduct;

        public clsProduct SelectedProductInfo
        {
            get { return _SelectedProduct; }
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        public ctrlProductCardWithFilter()
        {
            InitializeComponent();
        }
        private void FindNow()
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text))
            {
                MessageBox.Show("Please enter a filter value.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (cbFilterBy.Text)
            {
                case "Product ID":
                    if (int.TryParse(txtFilterValue.Text.Trim(), out int productId))
                    {
                        ctrlProductCard1.LoadProductInfo(int.Parse(txtFilterValue.Text));
                        _SelectedProduct = clsProduct.Find(productId);
                    }
                    else
                    {
                        MessageBox.Show("Product ID must be a number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;

                case "Product Name":
                    ctrlProductCard1.LoadProductInfo(txtFilterValue.Text);
                    _SelectedProduct = clsProduct.Find(txtFilterValue.Text);
                    break;

                default:
                    _SelectedProduct = null;
                    break;
            }

            if (OnProductSelected != null && FilterEnabled)
                OnProductSelected(ctrlProductCard1.ProductID);
        }
        public void LoadProductInfo(int ProductID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = ProductID.ToString();
            FindNow();
        }
        public void LoadProductInfo(string ProductName)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = ProductName.ToString();
            FindNow();
        }
        private void ctrlProductCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

           
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            if (cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FindNow();
        }

        private void ctrlProductCard1_Load(object sender, EventArgs e)
        {

        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This Field is required!!");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmAddUpdateProduct frm = new frmAddUpdateProduct();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }
        private void DataBackEvent(object sender, int ProductID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = ProductID.ToString();
            ctrlProductCard1.LoadProductInfo(ProductID);
        }
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
    }
}
