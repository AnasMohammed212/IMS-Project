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
using IMS_Business;

namespace IMS.SaleOrders.Sale_Order_Details
{
    public partial class frmAddUpdateDetail : Form
    {
        public delegate void DataBackEventHandler(object sender, int DetailID);
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        private int _DetailID = -1;
        private int _SaleOrderID = -1;
        private clsSaleOrderDetail _Detail;
        public frmAddUpdateDetail(int SaleOrderID)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _SaleOrderID = SaleOrderID;
        }
        public frmAddUpdateDetail(int SaleOrderID, int DetailID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _DetailID = DetailID;
            _SaleOrderID = SaleOrderID;
        }
        private async Task _FillProductsComboBox()
        {
            var dtProducts = await clsProduct.GetAllProducts();
            foreach (DataRow row in dtProducts.Rows)
                cbProducts.Items.Add(row["ProductName"]);
        }

        private void FillQuantityComboBox()
        {
            cbQuantity.Items.Clear();
            for (int i = 1; i <= 10; i++)
                cbQuantity.Items.Add(i);
            cbQuantity.DropDownStyle = ComboBoxStyle.DropDownList;
            cbQuantity.SelectedIndex = 0;
        }
        private async Task _ResetDefaultValues()
        {
            await _FillProductsComboBox();
            FillQuantityComboBox();
            cbProducts.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProducts.SelectedIndex = 0;

            txtUnitPrice.Text = "0.00";
            txtSaleOrderID.Text = _SaleOrderID.ToString();
            txtSaleOrderID.ReadOnly = true;

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Sale Detail";
                _Detail = new clsSaleOrderDetail();
                lblDetailID.Text = "[????]";
            }
            else
            {
                lblTitle.Text = "Update Sale Detail";
            }
        }
        private void _LoadData()
        {
            _Detail = clsSaleOrderDetail.Find(_DetailID);

            if (_Detail == null)
            {
                MessageBox.Show("No sale detail found with ID = " + _DetailID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblDetailID.Text = _DetailID.ToString();
            txtSaleOrderID.Text = _SaleOrderID.ToString();
            cbProducts.Text = clsProduct.Find(_Detail.ProductID)?.ProductName;
            cbQuantity.Text = _Detail.Quantity.ToString();
            txtUnitPrice.Text = _Detail.UnitPrice.ToString();
        }
        private async void frmAddUpdateDetail_Load(object sender, EventArgs e)
        {
            await _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fix validation errors.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedProduct = clsProduct.Find(cbProducts.Text);
            if (selectedProduct == null)
            {
                MessageBox.Show("Product not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Detail.ProductID = selectedProduct.ProductID;
            _Detail.SaleOrderID = _SaleOrderID;
            _Detail.Quantity = Convert.ToDecimal(cbQuantity.Text);
            _Detail.UnitPrice = decimal.Parse(txtUnitPrice.Text);

            if (await _Detail.Save())
            {
                lblDetailID.Text = _Detail.DetailID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Sale Detail";
                MessageBox.Show("Saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Detail.DetailID);

               
                clsInventoryTransaction inventoryTransaction = new clsInventoryTransaction
                {
                    ProductID = _Detail.ProductID,
                    Quantity = _Detail.Quantity,
                    TransactionType = "OUT",
                    TransactionDate = DateTime.Now,
                    PerformedByUserID = clsGlobal.CurrentUser.UserID,
                };

                bool transactionSaved = await inventoryTransaction.Save();
                if (!transactionSaved)
                {
                    MessageBox.Show("Failed to save inventory transaction.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Failed to save sale detail.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
