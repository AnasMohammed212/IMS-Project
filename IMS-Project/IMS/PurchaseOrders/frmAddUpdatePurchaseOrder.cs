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

namespace IMS.PurchaseOrders
{
    public partial class frmAddUpdatePurchaseOrder : Form
    {
        public delegate void DataBackEventHandler(object sender, int PurchaseOrderID);
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        private int _PurchaseOrderID = -1;
        clsPurchaseOrder _PurchaseOrder;
        private string _OriginalStatus = "Pending"; // افتراضيًا

        public frmAddUpdatePurchaseOrder()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdatePurchaseOrder(int PurchaseOrderID)
        {
            InitializeComponent();
            _PurchaseOrderID = PurchaseOrderID;
            _Mode = enMode.Update;
        }
        private async Task _FillSuppliersInComboBox()
        {
            var dtSuppliers = await clsSupplier.GetAllSuppliers();
            foreach (DataRow row in dtSuppliers.Rows)
                cbSupplier.Items.Add(row["SupplierName"]);
        }
        private async Task _ResetDefaultValues()
        {
            await _FillSuppliersInComboBox();

            cbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSupplier.SelectedIndex = 0;

            cbStatus.SelectedIndex=0;
            txtNotes.Text = "";

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Purchase Order";
                _PurchaseOrder = new clsPurchaseOrder();
                lblPurchaseOrderID.Text = "[????]";
                cbStatus.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Purchase Order";
            }
        }
        private void _LoadData()
        {
            _PurchaseOrder = clsPurchaseOrder.Find(_PurchaseOrderID);

            if (_PurchaseOrder == null)
            {
                MessageBox.Show("No purchase order found with ID = " + _PurchaseOrderID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPurchaseOrderID.Text = _PurchaseOrderID.ToString();
            cbSupplier.Text = clsSupplier.Find(_PurchaseOrder.SupplierID).SupplierName;

            cbStatus.Text = _PurchaseOrder.Status;
            txtNotes.Text = _PurchaseOrder.Notes;
            _OriginalStatus = _PurchaseOrder.Status;

            if (_PurchaseOrder.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase) || _PurchaseOrder.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase)) {
                cbSupplier.Enabled = false;
                cbStatus.Enabled = false;
                txtNotes.ReadOnly = true;
                btnSave.Enabled = false;

                MessageBox.Show("This purchase order is approved and can no longer be modified.", "Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void frmAddUpdatePurchaseOrder_Load(object sender, EventArgs e)
        {
            await _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Validation error. Hover over red icons to see issues.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedSupplier = clsSupplier.Find(cbSupplier.Text);
            if (selectedSupplier == null)
            {
                MessageBox.Show("Supplier not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _PurchaseOrder.SupplierID = selectedSupplier.SupplierID;
            _PurchaseOrder.OrderDate = DateTime.Now;
            _PurchaseOrder.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _PurchaseOrder.Status = cbStatus.Text;
            _PurchaseOrder.Notes = txtNotes.Text;




            bool purchaseSaved = await _PurchaseOrder.Save();

            if (purchaseSaved)
            {
                lblPurchaseOrderID.Text = _PurchaseOrder.PurchaseOrderID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Purchase Order";

                MessageBox.Show("Purchase order saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _PurchaseOrder.PurchaseOrderID);

                // ✅ Only update stock if status changed from non-Approved to Approved
                if (!string.Equals(_OriginalStatus, "Approved", StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(_PurchaseOrder.Status, "Approved", StringComparison.OrdinalIgnoreCase))
                {
                    var dtDetails = await clsPurchaseOrderDetail.GetAllOrderDetailsByPurchaseOrderID(_PurchaseOrder.PurchaseOrderID);

                    if (dtDetails.Rows.Count == 0)
                    {
                        MessageBox.Show("No purchase order details found to update stock.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    foreach (DataRow row in dtDetails.Rows)
                    {
                        int productID = Convert.ToInt32(row["ProductID"]);
                        decimal quantity = Convert.ToDecimal(row["Quantity"]);

                        clsStock stock = clsStock.Find(productID);

                        if (stock == null)
                        {
                            stock = new clsStock
                            {
                                ProductID = productID,
                                Quantity = quantity
                            };
                        }
                        else
                        {
                            stock.Quantity += quantity;
                        }

                        stock.LastUpdated = DateTime.Now;

                        await stock.Save();
                    }

                    MessageBox.Show("Stock has been updated successfully after approving the purchase order.", "Stock Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Update the original status after saving
                _OriginalStatus = _PurchaseOrder.Status;
            }
            else
            {
                MessageBox.Show("Failed to save the purchase order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }









        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
