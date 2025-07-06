using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS.People;
using IMS.Users;

namespace IMS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
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
    }
}
