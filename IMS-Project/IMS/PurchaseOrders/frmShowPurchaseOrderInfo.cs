using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS.PurchaseOrders
{
    public partial class frmShowPurchaseOrderInfo : Form
    {
        private int _PurchaseOrderID;
        public frmShowPurchaseOrderInfo(int PurchaseOrder)
        {
            InitializeComponent();
            _PurchaseOrderID = PurchaseOrder;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPurchaseOrderInfo_Load(object sender, EventArgs e)
        {
            ctrlPurchaseOrderCard1.LoadPurchaseOrderInfo(_PurchaseOrderID);
        }
    }
}
