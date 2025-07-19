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

namespace IMS.SaleOrders
{
    public partial class frmAddUpdateSaleOrder : Form
    {
        public delegate void DataBackEventHandler(object sender, int SaleOrderID);
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        private int _SaleOrderID = -1;
        clsSaleOrder _SaleOrder;
        private string _OriginalStatus = "Pending";

        public frmAddUpdateSaleOrder()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateSaleOrder(int SaleOrderID)
        {
            InitializeComponent();
            _SaleOrderID = SaleOrderID;
            _Mode = enMode.Update;
        }

        private async Task _FillCustomersInComboBox()
        {
            var dtCustomers = await clsCustomer.GetAllCustomers();
            foreach (DataRow row in dtCustomers.Rows)
                cbCustomer.Items.Add(row["CustomerName"]);
        }

        private async Task _ResetDefaultValues()
        {
            await _FillCustomersInComboBox();

            cbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCustomer.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            txtNotes.Text = "";

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Sale Order";
                _SaleOrder = new clsSaleOrder();
                lblSaleOrderID.Text = "[????]";
                cbStatus.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Sale Order";
            }
        }

        private void _LoadData()
        {
            _SaleOrder = clsSaleOrder.Find(_SaleOrderID);

            if (_SaleOrder == null)
            {
                MessageBox.Show("No sale order found with ID = " + _SaleOrderID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblSaleOrderID.Text = _SaleOrderID.ToString();
            cbCustomer.Text = clsCustomer.Find(_SaleOrder.CustomerID).CustomerName;
            cbStatus.Text = _SaleOrder.Status;
            txtNotes.Text = _SaleOrder.Notes;
            _OriginalStatus = _SaleOrder.Status;

            if (_SaleOrder.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase) || _SaleOrder.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
            {
                cbCustomer.Enabled = false;
                cbStatus.Enabled = false;
                txtNotes.ReadOnly = true;
                btnSave.Enabled = false;

                MessageBox.Show("This sale order is approved and can no longer be modified.", "Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void frmAddUpdateSaleOrder_Load(object sender, EventArgs e)
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

            var selectedCustomer = clsCustomer.Find(cbCustomer.Text);
            if (selectedCustomer == null)
            {
                MessageBox.Show("Customer not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SaleOrder.CustomerID = selectedCustomer.CustomerID;
            _SaleOrder.OrderDate = DateTime.Now;
            _SaleOrder.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _SaleOrder.Status = cbStatus.Text;
            _SaleOrder.Notes = txtNotes.Text;

            bool saleSaved = await _SaleOrder.Save();

            if (saleSaved)
            {
                lblSaleOrderID.Text = _SaleOrder.SaleOrderID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Sale Order";

                MessageBox.Show("Sale order saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _SaleOrder.SaleOrderID);

                if (!string.Equals(_OriginalStatus, "Approved", StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(_SaleOrder.Status, "Approved", StringComparison.OrdinalIgnoreCase))
                {
                    var dtDetails = await clsSaleOrderDetail.GetAllOrderDetailsBySaleOrderID(_SaleOrder.SaleOrderID);

                    if (dtDetails.Rows.Count == 0)
                    {
                        MessageBox.Show("No sale order details found to update stock.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    foreach (DataRow row in dtDetails.Rows)
                    {
                        int productID = Convert.ToInt32(row["ProductID"]);
                        decimal quantity = Convert.ToDecimal(row["Quantity"]);

                        clsStock stock = clsStock.Find(productID);

                        if (stock != null)
                        {
                            stock.Quantity -= quantity;
                            stock.LastUpdated = DateTime.Now;
                            await stock.Save();
                        }
                    }

                    MessageBox.Show("Stock has been updated successfully after approving the sale order.", "Stock Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _OriginalStatus = _SaleOrder.Status;
            }
            else
            {
                MessageBox.Show("Failed to save the sale order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
