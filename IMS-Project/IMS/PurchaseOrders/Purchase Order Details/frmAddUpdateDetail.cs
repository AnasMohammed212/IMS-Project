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
using IMS.People.Controls;
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
        public frmAddUpdateDetail(int PurchaseOrderID, int DetailID)
        {
             InitializeComponent();
            _Mode = enMode.Update;
            _DetailID = DetailID;
            _PurchaseOrderID = PurchaseOrderID;
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
        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Detail";
                this.Text = "Add New Detail";
                _Detail = new clsPurchaseOrderDetail();
                lblDetailID.Text = "[????]";
                ctrlProductCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update Detail";
                this.Text = "Update Detail";
                btnSave.Enabled = true;
               
            }

            FillQuantityComboBox();
           
            cbQuantity.SelectedIndex = 0;
            txtPurchaseOrderID.Text = _PurchaseOrderID.ToString();
            txtPurchaseOrderID.ReadOnly = true;
          
        }

        private void _LodaData()
        {
            _Detail = clsPurchaseOrderDetail.Find(_DetailID);
            ctrlProductCardWithFilter1.FilterEnabled = false;
            if (_Detail == null)
            {
                MessageBox.Show("No Detail with ID = " + _DetailID, "Detail Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            ctrlProductCardWithFilter1.LoadProductInfo(_Detail.ProductID);
            lblDetailID.Text = _Detail.DetailID.ToString();
            txtPurchaseOrderID.Text = _Detail.PurchaseOrderID.ToString();
           cbQuantity.Text = _Detail.Quantity.ToString();
        }
        private void AddUpdatePurchaseDetail_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LodaData();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fix validation errors.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctrlProductCardWithFilter1.SelectedProductInfo == null)
            {
                MessageBox.Show("Please select a product before saving.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedProductID = ctrlProductCardWithFilter1.SelectedProductInfo.ProductID;
            var selectedProduct = clsProduct.Find(selectedProductID);
            if (selectedProduct == null)
            {
                MessageBox.Show("Product not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Detail.ProductID = selectedProductID;
            _Detail.PurchaseOrderID = _PurchaseOrderID;
            _Detail.Quantity = Convert.ToDecimal(cbQuantity.Text);
            _Detail.UnitPrice = (ctrlProductCardWithFilter1.SelectedProductInfo.PurchasePrice) * Convert.ToDecimal(cbQuantity.Text);




            if (await _Detail.Save())
            {
                lblDetailID.Text = _Detail.DetailID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Detail";
                MessageBox.Show("Saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Detail.DetailID);
                clsInventoryTransaction inventoryTransaction = new clsInventoryTransaction
                {
                    ProductID = _Detail.ProductID,
                    Quantity = _Detail.Quantity,
                    TransactionType = "IN",
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
                MessageBox.Show("Failed to save detail.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
