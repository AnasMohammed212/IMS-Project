using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS.SaleOrders
{
    public partial class frmShowSaleOrderInfo : Form
    {
        private int _SaleOrderID;
        public frmShowSaleOrderInfo(int SaleOrderID)
        {
            InitializeComponent();
            _SaleOrderID = SaleOrderID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowSaleOrderInfo_Load(object sender, EventArgs e)
        {
            ctrlSaleOrderInfo1.LoadSaleOrderInfo(_SaleOrderID);
        }
    }
}
