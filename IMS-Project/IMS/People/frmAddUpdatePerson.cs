using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS.Global_Classes;
using IMS.Properties;
using IMS_Business;

namespace IMS.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGender { Male = 0, Female = 1 };
        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;


        public frmAddUpdatePerson()
        {
            InitializeComponent();
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }
        private async void _FillCountriesInComboBox()
        {
            DataTable dtCountries = await clsCountry.GetAllCountries();
            foreach (DataRow row in dtCountries.Rows)
                cbCountry.Items.Add(row["CountryName"]);

        }
        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
                lblTitle.Text = "Update Person";
            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male;
            else
                pbPersonImage.Image = Resources.Female;

            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            cbCountry.SelectedIndex = cbCountry.FindString("Syria");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            rtxtAddress.Text = "";
        }
        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            rtxtAddress.Text = _Person.Address;
            
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gender == 1)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);
            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }
            llRemoveImage.Visible = (_Person.ImagePath != "");
        }
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }
        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {

                    }
                }
                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();
                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage())
            {
                return;
            }
            int NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
           
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = rtxtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            if (rbMale.Checked)
                _Person.Gender = (short)enGender.Male;
            else
                _Person.Gender = (short)enGender.Female;
            _Person.NationalityCountryID = NationalityCountryID;
            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";
            if (await _Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();

                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Person.PersonID);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Male;
        }
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Female;
        }
        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFilePath = openFileDialog1.FileName;
                pbPersonImage.ImageLocation = SelectedFilePath;
                llRemoveImage.Visible = true;
            }
        }
        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male;
            else
                pbPersonImage.Image = Resources.Female;
            llRemoveImage.Visible = false;
        }




    }
}
