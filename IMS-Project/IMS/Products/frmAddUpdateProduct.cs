using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS.Global_Classes;
using IMS.Properties;
using IMS_Business;

namespace IMS.Products
{
    public partial class frmAddUpdateProduct : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _ProductID = -1;
        clsProduct _Product;
        public frmAddUpdateProduct()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateProduct(int ProducID)
        {
            InitializeComponent();
            _ProductID = ProducID;
        }
        private async Task _FillCategoriesInComboBox()
        {
            DataTable dtCountries = await clsCategory.GetAllCategories();
            foreach (DataRow row in dtCountries.Rows)
                cbCategory.Items.Add(row["CategoryName"]);
        }
        private async Task _FillSuppliersInComboBox()
        {
            DataTable dtCountries = await clsSupplier.GetAllSuppliers();
            foreach (DataRow row in dtCountries.Rows)
                cbSupplier.Items.Add(row["SupplierName"]);
        }
        private async Task _FillUnitsInComboBox()
        {
            DataTable dbUnits = await clsUnitOfMeasure.GetAllUnitsOfMeasure();
            foreach (DataRow row in dbUnits.Rows)
                cbUnit.Items.Add(row["UnitName"]);
        }
        private async Task _ResetDefaultValues()
        {
            await _FillCategoriesInComboBox();
            await _FillSuppliersInComboBox();
            await  _FillUnitsInComboBox();
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Product";
                _Product = new clsProduct();
            }
            else
                lblTitle.Text = "Update Product";
           
            cbCategory.SelectedIndex = 0;
            cbSupplier.SelectedIndex = 0;
            cbUnit.SelectedIndex = 0;
            txtProductName.Text = "";
            txtPurchasePrice.Text = "";
            txtSalePrice.Text = "";
            txtDescription.Text = "";
            
        }
        private void _LoadData()
        {
            _Product = clsProduct.Find(_ProductID);
            if (_Product == null)
            {
                MessageBox.Show("No Product with ID = " + _ProductID, "Product Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblProductID.Text = _ProductID.ToString();
            txtProductName.Text = _Product.ProductName;
            txtPurchasePrice.Text = _Product.PurchasePrice.ToString();
            txtSalePrice.Text = _Product.SalePrice.ToString();
            txtDescription.Text = _Product.Description;
            cbUnit.Text=_Product.UnitInfo.UnitID.ToString();
            cbCategory.Text = _Product.CategoryInfo.CategoryName;
            cbSupplier.Text = _Product.SupplierInfo.SupplierName;
           
        }
        private async void frmAddUpdateProduct_Load(object sender, EventArgs e)
        {
            await _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Product.ProductName = txtProductName.Text;
            _Product.PurchasePrice= Convert.ToDecimal(txtPurchasePrice.Text);
            _Product.SalePrice= Convert.ToDecimal(txtSalePrice.Text);
            _Product.Description=txtDescription.Text;
            

            var category = clsCategory.Find(cbCategory.Text.Trim());
            if (category == null)
            {
                MessageBox.Show("The specified category does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Product.CategoryID = category.CategoryID;


            var supplier = clsSupplier.Find(cbSupplier.Text.Trim());
            if (supplier == null)
            {
                MessageBox.Show("The specified supplier does not exist!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Product.SupplierID = supplier.SupplierID;

            var unit = clsUnitOfMeasure.Find(cbUnit.Text.Trim());
            if (unit == null)
            {
                MessageBox.Show("The specified unit does not exist!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Product.UnitID = unit.UnitID;
            if (await _Product.Save())
            {
                lblProductID.Text = _Product.ProductID.ToString();

                _Mode = enMode.Update;
                lblTitle.Text = "Update Product";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Product.ProductID);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPurchasePrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPurchasePrice.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPurchasePrice, "Purchase Price cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtPurchasePrice, null);
            };


            if (!clsValidation.IsNumber(txtPurchasePrice.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPurchasePrice, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtPurchasePrice, null);
            };
        }

        private void txtSalePrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSalePrice.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSalePrice, "Sale Price cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtSalePrice, null);
            };


            if (!clsValidation.IsNumber(txtSalePrice.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSalePrice, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtSalePrice, null);
            };
        }

        private async void cbCategory_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbCategory.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbCategory, "Category cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(cbCategory, null);
            };
            if (! await clsCategory.IsCategoryExist(cbCategory.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbCategory, "Category Not Found!");
                return;
            }
        }

        private async void cbSupplier_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbSupplier.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbSupplier, "Supplier cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(cbSupplier, null);
            };
            if (!await clsSupplier.IsSupplierExist(cbSupplier.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbSupplier, "Supplier Not Found!");
                return;
            }
        }

        private async void cbUnit_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbUnit.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbUnit, "Unit cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(cbUnit, null);
            };
            if (!await clsUnitOfMeasure.IsUnitExist(cbUnit.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(cbUnit, "Unit Not Found!");
                return;
            }
        }
    }
}
