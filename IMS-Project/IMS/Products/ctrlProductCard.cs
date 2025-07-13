using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS.Properties;
using IMS_Business;

namespace IMS.Products
{
    public partial class ctrlProductCard : UserControl
    {
        private clsProduct _Product;
        private int _ProductID = -1;
        public int ProductID { get { return _ProductID; } }
        public clsProduct SelectedProductInfo { get { return _Product; } }
        public ctrlProductCard()
        {
            InitializeComponent();
        }
        public void ResetProductInfo()
        {
            _ProductID = -1;
            lblProductID.Text = "[????]";
            lblDescription.Text = "[????]";
            lblProductName.Text = "[????]";
            lblPurchasePrice.Image = Resources.Male;
            lblSalePrice.Text = "[????]";
            lblCategory.Text = "[????]";
            lblSupplier.Text = "[????]";
            lblUnit.Text = "[????]";
        }
        private void _FillProductInfo()
        {
            
            _ProductID = _Product.ProductID;
            lblProductID.Text = _Product.ProductID.ToString();
            lblDescription.Text = _Product.Description;
            lblProductName.Text = _Product.ProductName;
            lblPurchasePrice.Text = _Product.PurchasePrice.ToString();
            lblSalePrice.Text = _Product.SalePrice.ToString();
            lblCategory.Text = _Product.CategoryInfo.CategoryName;
            lblSupplier.Text = _Product.SupplierInfo.SupplierName;
            lblUnit.Text = _Product.UnitInfo.UnitName;
            
        }
        public void LoadProductInfo(int ProductID)
        {
            _Product = clsProduct.Find(ProductID);
            if (_Product == null)
            {
                ResetProductInfo();
                MessageBox.Show("No Product With ProductID = " + ProductID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FillProductInfo();
        }
        private void ctrlProductCard_Load(object sender, EventArgs e)
        {

        }
    }
}
