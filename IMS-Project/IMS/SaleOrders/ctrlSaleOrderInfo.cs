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

namespace IMS.SaleOrders
{
    public partial class ctrlSaleOrderInfo : UserControl
    {
        private clsSaleOrder _SaleOrder;
        private int _SaleOrderID = -1;

        public int SaleOrderID { get { return _SaleOrderID; } }
        public clsSaleOrder SelectedSaleOrderInfo { get { return _SaleOrder; } }
        public ctrlSaleOrderInfo()
        {
            InitializeComponent();
        }
        public void ResetSaleOrderInfo()
        {
            lblSaleOrderID.Text = "[????]";
            lblCustomer.Text = "[????]";
            lblStatus.Text = "[????]";
            lblNotes.Text = "[????]";
            lblOrderDate.Text = "[????]";
        }
        private void _FillSaleOrderInfo()
        {
            _SaleOrderID = _SaleOrder.SaleOrderID;
            lblSaleOrderID.Text = _SaleOrderID.ToString();
            lblCustomer.Text = _SaleOrder.CustomerInfo.CustomerName;
            lblStatus.Text = _SaleOrder.Status;
            lblNotes.Text = _SaleOrder.Notes;
            lblOrderDate.Text = _SaleOrder.OrderDate.ToString();
        }
        public void LoadSaleOrderInfo(int SaleOrderID)
        {
            _SaleOrder = clsSaleOrder.Find(SaleOrderID);

            if (_SaleOrder == null)
            {
                ResetSaleOrderInfo();
                MessageBox.Show("No Sale Order With ID = " + SaleOrderID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillSaleOrderInfo();
        }

        private void ctrlSaleOrderInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
