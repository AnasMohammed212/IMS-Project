using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS.Products
{
    public partial class frmShowProductInfo : Form
    {
        private int _ProductID;
        public frmShowProductInfo(int ProductID)
        {
            InitializeComponent();
            _ProductID = ProductID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowProductInfo_Load(object sender, EventArgs e)
        {
            ctrlProductCard1.LoadProductInfo(_ProductID);
        }
    }
}
