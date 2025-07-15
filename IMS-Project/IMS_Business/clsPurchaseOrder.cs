using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsPurchaseOrder
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode = enMode.AddNew;

        public int PurchaseOrderID { get; set; }
        public int SupplierID { get; set; }
        public clsSupplier SupplierInfo { get; set; }
        public DateTime OrderDate { get; set; }
        public int CreatedByUserID { get; set; }
        clsUser UserInfo { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }


        public clsPurchaseOrder()
        {
            this.PurchaseOrderID = -1;
            this.SupplierID = -1;
            this.OrderDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.Status = "";
            this.Notes = "";
            Mode = enMode.AddNew;
        }

        private clsPurchaseOrder(int purchaseOrderID, int supplierID, DateTime orderDate,int CreatedByUserID,string status,string Notes)
        {
            this.PurchaseOrderID = purchaseOrderID;
            this.SupplierID = supplierID;
            this.SupplierInfo = clsSupplier.Find(supplierID);
            this.OrderDate = orderDate;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.Status = status;
            this.Notes = Notes;
            Mode = enMode.Update;
        }

        public static clsPurchaseOrder Find(int purchaseOrderID)
        {
            int supplierID = -1, CreatedByUserID=-1;
            DateTime orderDate = DateTime.Now;
            string Status = "", Notes = "";
            bool isFound = clsPurchaseOrderData.GetPurchaseOrderByID(purchaseOrderID, ref supplierID ,ref orderDate,ref CreatedByUserID, ref Status,ref Notes);

            if (isFound)
                return new clsPurchaseOrder(purchaseOrderID, supplierID, orderDate,CreatedByUserID,Status,Notes);
            else
                return null;
        }

        private async Task<bool> _AddNewPurchaseOrder()
        {
            this.PurchaseOrderID = await clsPurchaseOrderData.AddNewPurchaseOrder(this.SupplierID, this.OrderDate,this.CreatedByUserID,this.Status,this.Notes);
            return (this.PurchaseOrderID != -1);
        }

        private async Task<bool> _UpdatePurchaseOrder()
        {
            return await clsPurchaseOrderData.UpdatePurchaseOrder(this.PurchaseOrderID,this.SupplierID, this.OrderDate, this.CreatedByUserID, this.Status, this.Notes);
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewPurchaseOrder())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdatePurchaseOrder();
            }
            return false;
        }

        public static async Task<DataTable> GetAllPurchaseOrders()
        {
            return await clsPurchaseOrderData.GetAllPurchaseOrders();
        }

        public static async Task<bool> IsPurchaseOrderExist(int purchaseOrderID)
        {
            return await clsPurchaseOrderData.IsPurchaseOrderExist(purchaseOrderID);
        }

        public static async Task<bool> DeletePurchaseOrder(int purchaseOrderID)
        {
            return await clsPurchaseOrderData.DeletePurchaseOrder(purchaseOrderID);
        }
    }
}
