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

namespace IMS.PurchaseOrders.Purchase_Order_Details
{
    public partial class frmAddUpdateDetail : Form
    {
        public delegate void DataBackEventHandler(object sender, int DetailID);
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        private int _DetailID = -1;
        private int _PurchaseOrderID = -1;
        private clsPurchaseOrderDetail _Detail;
        public frmAddUpdateDetail(int PurchaseOrderID)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _PurchaseOrderID = PurchaseOrderID;
        }

        public frmAddUpdateDetail(int PurchaseOrderID,int DetailID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _DetailID = DetailID;
            _PurchaseOrderID = PurchaseOrderID;
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
            {
                cbQuantity.Items.Add(i);
            }

            cbQuantity.DropDownStyle = ComboBoxStyle.DropDownList;
            cbQuantity.SelectedIndex = 0;
        }
        private async Task _ResetDefaultValues()
        {
            await _FillProductsComboBox();
            FillQuantityComboBox();
            cbProducts.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProducts.SelectedIndex = 0;

            cbQuantity.SelectedIndex=0;
            txtUnitPrice.Text = "0.00";
            txtPurchaseOrderID.Text = _PurchaseOrderID.ToString();
            txtPurchaseOrderID.ReadOnly = true;
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Detail";
                _Detail = new clsPurchaseOrderDetail();
                lblDetailID.Text = "[????]";
            }
            else
            {
                lblTitle.Text = "Update Detail";
            }
        }
        private void _LoadData()
        {
            _Detail = clsPurchaseOrderDetail.Find(_DetailID);

            if (_Detail == null)
            {
                MessageBox.Show("No detail found with ID = " + _DetailID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblDetailID.Text = _DetailID.ToString();
            txtPurchaseOrderID.Text = _PurchaseOrderID.ToString();
            cbProducts.Text = clsProduct.Find(_Detail.ProductID)?.ProductName;
            cbQuantity.Text = _Detail.Quantity.ToString();
            txtUnitPrice.Text = _Detail.UnitPrice.ToString();
           
            txtPurchaseOrderID.Text = _PurchaseOrderID.ToString();
            txtPurchaseOrderID.ReadOnly = true;
            
                
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
            _Detail.PurchaseOrderID = _PurchaseOrderID;
            _Detail.Quantity = Convert.ToDecimal(cbQuantity.Text);
            _Detail.UnitPrice = decimal.Parse(txtUnitPrice.Text);

            if (await _Detail.Save())
            {
                lblDetailID.Text = _Detail.DetailID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Detail";
                MessageBox.Show("Saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Detail.DetailID);
            }
            else
            {
                MessageBox.Show("Failed to save detail.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
