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
namespace IMS.People
{
    public partial class frmListPeople : Form
    {
        private static DataTable _dtAllPeople;

        private async void LoadDataAsync()
        {
            _dtAllPeople = await clsPerson.GetAllPeople();
            _dtAllPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID",
                                 "FirstName", "SecondName", "ThirdName", "LastName",
                                 "GenderCaption", "DateOfBirth", "CountryName",
                                 "Phone", "Email","Address","IsActiveCaption","CreatedAt");
            dgvPeople.DataSource = _dtAllPeople;
            lblRecordCount.Text = _dtAllPeople.Rows.Count.ToString();
        }



        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            LoadDataAsync();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void btnShowAddUpdatePerson_Click(object sender, EventArgs e)
        {

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                
                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Country":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                lblRecordCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "PersonID")
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordCount.Text = dgvPeople.Rows.Count.ToString();
        }
    }
    }

