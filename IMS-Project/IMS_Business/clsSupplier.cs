using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;
namespace IMS_Business
{
    public class clsSupplier
    {
        enum enMode { AddNew = 1, Update = 0 }
        enMode Mode;
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int ContactPersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public clsSupplier() {
            this.SupplierID = -1;
            this.SupplierName = "";
            this.ContactPersonID = -1;
            Mode=enMode.AddNew;
        }
        public clsSupplier(int supplierID, string supplierName, int contactPersonID)
        {
            this.SupplierID = supplierID;
            this.SupplierName = supplierName;
            this.ContactPersonID = contactPersonID;
            this.PersonInfo=clsPerson.Find(contactPersonID);
            Mode = enMode.Update;
        }
        public static  clsSupplier Find(int SupplierID)
        {
            int ContactPersonID = -1;
            string SupplierName="";
            bool IsFound= clsSupplierData.GetSupplierInfoByID(SupplierID,ref SupplierName, ref ContactPersonID);
            if (IsFound)
                return new clsSupplier(SupplierID,SupplierName,ContactPersonID);
            else
                return null;
        }
        public static clsSupplier Find(string SupplierName)
        {
            int ContactPersonID = -1, SupplierID = -1;
            
            bool IsFound = clsSupplierData.GetSupplierInfoByName(ref SupplierID, SupplierName, ref ContactPersonID);
            if (IsFound)
                return new clsSupplier(SupplierID, SupplierName, ContactPersonID);
            else
                return null;
        }
        private async Task<bool> _AddNewSupplier()
        {
            this.SupplierID= await clsSupplierData.AddNewSupplier(this.SupplierName,this.ContactPersonID);
            return (this.SupplierID != -1);
        }
        private async Task<bool> _UpdateSupplier()
        {
            return await clsSupplierData.UpdateSupplier(this.SupplierID,this.SupplierName,this.ContactPersonID);
        }
        public  async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(await _AddNewSupplier())
                    {
                        Mode= enMode.Update;
                        return true;
                    }
                    else
                        return false;
                    case enMode.Update:
                    return await _UpdateSupplier();
                    
            }
            return false;
        }
        public static async Task<DataTable> GetAllSuppliers()
        {
            return await clsSupplierData.GetAllSuppliers();
        }
        public static async Task<bool> DeleteSupplier(int SupplierID)
        {
            return await clsSupplierData.DeleteSupplier(SupplierID);
        }
        public static async Task<bool> IsSupplierExist(int SupplierID)
        {
            return await clsSupplierData.IsSupplierExist(SupplierID);
        }
        public static async Task<bool> IsSupplierExist(string supplierName)
        {
            return await clsSupplierData.IsSupplierExist(supplierName);
        }

    }
}
