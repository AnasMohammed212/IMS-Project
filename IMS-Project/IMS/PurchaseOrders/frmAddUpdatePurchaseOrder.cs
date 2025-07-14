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

            if (await _PurchaseOrder.Save())
            {
                lblPurchaseOrderID.Text = _PurchaseOrder.PurchaseOrderID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Purchase Order";
                MessageBox.Show("Saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _PurchaseOrder.PurchaseOrderID);
            }
            else
            {
                MessageBox.Show("Failed to save data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
