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

namespace IMS.PurchaseOrders
{
    public partial class ctrlPurchaseOrderCard : UserControl
    {
        private clsPurchaseOrder _PurchaseOrder;
        private int _PurchaseOrderID = -1;
        public int PurchaseOrderID { get { return _PurchaseOrderID; } }
        public clsPurchaseOrder SelectedPurchaseOrderInfo { get { return _PurchaseOrder; } }
        public ctrlPurchaseOrderCard()
        {
            InitializeComponent();
        }
        public void ResetPurchaseOrderInfo()
        {
            lblPurchaseOrderID.Text = "[????]";
            lblSupplier.Text= "[????]";
            lblStatus.Text= "[????]";
            lblNotes.Text= "[????]";
            lblOrderDate.Text= "[????]";
        }
        private void _FillPurchaseOrderInfo()
        {
            _PurchaseOrderID=_PurchaseOrder.PurchaseOrderID;
            lblPurchaseOrderID.Text=_PurchaseOrderID.ToString();
            lblSupplier.Text = _PurchaseOrder.SupplierInfo.SupplierName;
            lblStatus.Text=_PurchaseOrder.Status;
            lblNotes.Text = _PurchaseOrder.Notes;
            lblOrderDate.Text = _PurchaseOrder.OrderDate.ToString();
        }
        public void LoadPurchaseOrderInfo(int PurchaseOrderID)
        {
            _PurchaseOrder = clsPurchaseOrder.Find(PurchaseOrderID);
            if (_PurchaseOrder == null)
            {
                ResetPurchaseOrderInfo();
                MessageBox.Show("No Purchase Order With ProductID = " + PurchaseOrderID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FillPurchaseOrderInfo();

        }
        private void ctrlPurchaseOrderCard_Load(object sender, EventArgs e)
        {

        }
    }
}
