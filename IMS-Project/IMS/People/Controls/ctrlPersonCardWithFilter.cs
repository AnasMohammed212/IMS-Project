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

namespace IMS.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
                handler(PersonID);
        }
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Visible = _ShowAddPerson;
            }
        }
        private bool _SearchEnabled = true;
        public bool SearchEnabled
        {
            get { return _SearchEnabled; }
            set
            {
                _SearchEnabled = value;
                gbFilters.Enabled = _SearchEnabled;
            }
        }
        private int _PersonID = -1;
        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }
        private void FindNow()
        {
            ctrlPersonCard1.LoadPersonInfo(int.Parse(txtSearchValue.Text));
            if (OnPersonSelected != null && SearchEnabled)
                OnPersonSelected(ctrlPersonCard1.PersonID);

        }
        public void LoadPersonInfo(int PersonID)
        {
            txtSearchValue.Text = PersonID.ToString();
            FindNow();
        }







        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            txtSearchValue.Focus();
        }

        private void txtSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FindNow();
        }

        private void txtSearchValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSearchValue, "This Field is required!!");
            }
            else
            {
                errorProvider1.SetError(txtSearchValue, null);
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            txtSearchValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        public void FilterFocus()
        {
            txtSearchValue.Focus();
        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }

        private void gbFilters_Enter(object sender, EventArgs e)
        {

        }
    }
}
