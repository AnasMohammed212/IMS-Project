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
using IMS.Login;
using IMS.People;
using IMS.Products;
using IMS.Users;

namespace IMS
{
    public partial class frmMain : Form
    {
        private frmLogin _frmLogin;
        public frmMain(frmLogin frmlogin)
        {
            InitializeComponent();
            _frmLogin = frmlogin;
        }

      

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListPeople people = new frmListPeople();
            people.Show();
        }

        private void usersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm=new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListProducts frm=new frmListProducts();
            frm.Show();
        }

        private void purchaseOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPurchaseOrders frm=new frmListPurchaseOrders();
            frm.ShowDialog();
        }
    }
}
