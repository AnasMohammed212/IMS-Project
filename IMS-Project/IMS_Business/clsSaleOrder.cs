using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsSaleOrder
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode = enMode.AddNew;

        public int SaleOrderID { get; set; }
        public int CustomerID { get; set; }
        public clsCustomer CustomerInfo { get; set; }
        public DateTime OrderDate { get; set; }
        public int CreatedByUserID { get; set; }
        clsUser UserInfo { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public clsSaleOrder()
        {
            SaleOrderID = -1;
            CustomerID = -1;
            OrderDate = DateTime.Now;
            CreatedByUserID = -1;
            Status = "";
            Notes = "";
            Mode = enMode.AddNew;
        }

        private clsSaleOrder(int saleOrderID, int customerID, DateTime orderDate, int createdByUserID, string status, string notes)
        {
            SaleOrderID = saleOrderID;
            CustomerID = customerID;
            CustomerInfo = clsCustomer.Find(customerID);
            OrderDate = orderDate;
            CreatedByUserID = createdByUserID;
            UserInfo = clsUser.FindByUserID(createdByUserID);
            Status = status;
            Notes = notes;
            Mode = enMode.Update;
        }

        public static clsSaleOrder Find(int saleOrderID)
        {
            int customerID = -1, createdByUserID = -1;
            DateTime orderDate = DateTime.Now;
            string status = "", notes = "";

            bool isFound = clsSaleOrderData.GetSaleOrderByID(saleOrderID, ref customerID, ref orderDate, ref createdByUserID, ref status, ref notes);

            if (isFound)
                return new clsSaleOrder(saleOrderID, customerID, orderDate, createdByUserID, status, notes);
            else
                return null;
        }

        private async Task<bool> _AddNewSaleOrder()
        {
            SaleOrderID = await clsSaleOrderData.AddNewSaleOrder(CustomerID, OrderDate, CreatedByUserID, Status, Notes);
            return (SaleOrderID != -1);
        }

        private async Task<bool> _UpdateSaleOrder()
        {
            return await clsSaleOrderData.UpdateSaleOrder(SaleOrderID, CustomerID, OrderDate, CreatedByUserID, Status, Notes);
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewSaleOrder())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return await _UpdateSaleOrder();
            }
            return false;
        }

        public static async Task<DataTable> GetAllSaleOrders()
        {
            return await clsSaleOrderData.GetAllSaleOrders();
        }

        public static async Task<bool> IsSaleOrderExist(int saleOrderID)
        {
            return await clsSaleOrderData.IsSaleOrderExist(saleOrderID);
        }

        public static async Task<bool> DeleteSaleOrder(int saleOrderID)
        {
            return await clsSaleOrderData.DeleteSaleOrder(saleOrderID);
        }
    }
}
