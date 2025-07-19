using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsCustomer
    {
        enum enMode { AddNew = 1, Update = 0 }
        enMode Mode;

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int  ContactPersonID { get; set; }
        public clsPerson ContactPersonInfo { get; set; }

        public clsCustomer()
        {
            this.CustomerID = -1;
            this.CustomerName = "";
            this.ContactPersonID = -1;
            Mode = enMode.AddNew;
        }

        public clsCustomer(int customerID, string customerName, int contactPersonID)
        {
            this.CustomerID = customerID;
            this.CustomerName = customerName;
            this.ContactPersonID = contactPersonID;
            this.ContactPersonInfo = clsPerson.Find(contactPersonID);
            Mode = enMode.Update;
        }

        public static clsCustomer Find(int CustomerID)
        {
            int contactPersonID = -1;
            string customerName = "";
            bool isFound = clsCustomerData.GetCustomerInfoByID(CustomerID, ref customerName, ref contactPersonID);
            if (isFound)
                return new clsCustomer(CustomerID, customerName, contactPersonID);
            else
                return null;
        }

        public static clsCustomer Find(string CustomerName)
        {
            int contactPersonID = -1, customerID = -1;
            bool isFound = clsCustomerData.GetCustomerInfoByName(ref customerID, CustomerName, ref contactPersonID);
            if (isFound)
                return new clsCustomer(customerID, CustomerName, contactPersonID);
            else
                return null;
        }

        private async Task<bool> _AddNewCustomer()
        {
            this.CustomerID = await clsCustomerData.AddNewCustomer(this.CustomerName, this.ContactPersonID);
            return (this.CustomerID != -1);
        }

        private async Task<bool> _UpdateCustomer()
        {
            return await clsCustomerData.UpdateCustomer(this.CustomerID, this.CustomerName, this.ContactPersonID);
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewCustomer())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateCustomer();
            }
            return false;
        }

        public static async Task<DataTable> GetAllCustomers()
        {
            return await clsCustomerData.GetAllCustomers();
        }

        public static async Task<bool> DeleteCustomer(int CustomerID)
        {
            return await clsCustomerData.DeleteCustomer(CustomerID);
        }

        public static async Task<bool> IsCustomerExist(int CustomerID)
        {
            return await clsCustomerData.IsCustomerExist(CustomerID);
        }

        public static async Task<bool> IsCustomerExist(string customerName)
        {
            return await clsCustomerData.IsCustomerExist(customerName);
        }
    }
}
