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
    public partial class frmAddUpdateCustomer : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _CustomerID = -1;
        private clsCustomer _Customer;
        public frmAddUpdateCustomer()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateCustomer(int customerID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _CustomerID = customerID;
        }

        private void ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Customer";
                this.Text = "Add New Customer";
                _Customer = new clsCustomer();
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update Customer";
                this.Text = "Update Customer";
            }

            txtCustomerName.Text = "";
        }

        private void _LoadData()
        {
            _Customer = clsCustomer.Find(_CustomerID);

            if (_Customer == null)
            {
                MessageBox.Show("No Customer with ID = " + _CustomerID, "Customer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblCustomerID.Text = _Customer.CustomerID.ToString();
            txtCustomerName.Text = _Customer.CustomerName;
            ctrlPersonCardWithFilter1.SearchEnabled = false;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_Customer.ContactPersonID);
        }
        private void frmAddUpdateCustomer_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Hover the mouse over the red icon(s) to see the error.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please select a Contact Person.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Customer.CustomerName = txtCustomerName.Text.Trim();
            _Customer.ContactPersonID = ctrlPersonCardWithFilter1.PersonID;

            if (await _Customer.Save())
            {
                lblCustomerID.Text = _Customer.CustomerID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Customer";
                this.Text = "Update Customer";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data was not saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
