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

namespace IMS.Suppliers
{
    public partial class frmAddUpdateSupplier : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _SupplierID = -1;
        private clsSupplier _Supplier;
        public frmAddUpdateSupplier()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateSupplier(int supplierID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _SupplierID = supplierID;
        }
        private void ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Supplier";
                this.Text = "Add New Supplier";
                _Supplier = new clsSupplier();
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update Supplier";
                this.Text = "Update Supplier";
            }

            txtSupplierName.Text = "";
        }
        private void _LoadData()
        {
            _Supplier = clsSupplier.Find(_SupplierID);

            if (_Supplier == null)
            {
                MessageBox.Show("No Supplier with ID = " + _SupplierID, "Supplier Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblSupplierID.Text = _Supplier.SupplierID.ToString();
            txtSupplierName.Text = _Supplier.SupplierName;
            ctrlPersonCardWithFilter1.SearchEnabled = false;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_Supplier.ContactPersonID);
        }
        private void frmAddUpdateSupplier_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

            _Supplier.SupplierName = txtSupplierName.Text.Trim();
            _Supplier.ContactPersonID = ctrlPersonCardWithFilter1.PersonID;

            if (await _Supplier.Save())
            {
                lblSupplierID.Text = _Supplier.SupplierID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Supplier";
                this.Text = "Update Supplier";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data was not saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
