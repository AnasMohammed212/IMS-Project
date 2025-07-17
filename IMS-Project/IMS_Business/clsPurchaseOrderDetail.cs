using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsPurchaseOrderDetail
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode = enMode.AddNew;

        public int DetailID { get; set; }
        public int PurchaseOrderID { get; set; }
        clsPurchaseOrder PurchaseOrderInfo;
        public int ProductID { get; set; }
        public clsProduct ProductInfo;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public clsPurchaseOrderDetail()
        {
            this.DetailID = -1;
            this.PurchaseOrderID = -1;
            this.ProductID = -1;
            this.Quantity = 0;
            this.UnitPrice = 0;
            Mode = enMode.AddNew;
        }

        private clsPurchaseOrderDetail(int detailID, int purchaseOrderID, int productID, decimal quantity, decimal unitPrice)
        {
            this.DetailID = detailID;
            this.PurchaseOrderID = purchaseOrderID;
            this.ProductID = productID;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.PurchaseOrderInfo = clsPurchaseOrder.Find(purchaseOrderID);
            this.ProductInfo = clsProduct.Find(productID);
            Mode = enMode.Update;
        }
        public static clsPurchaseOrderDetail Find(int DetailID)
        {
            int PurchaseOrderID = -1, ProductID = -1;
            decimal quantity = 0,UnitPrice=0;
            bool IsFound = clsPurchaseOrderDetailsData.GetPurchaseOrderDetailByID(DetailID,ref PurchaseOrderID,ref ProductID,ref quantity,ref UnitPrice);
            if(IsFound)
                return new clsPurchaseOrderDetail(DetailID,PurchaseOrderID,ProductID,quantity,UnitPrice);
            else
                return null;
        }
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    this.DetailID = await clsPurchaseOrderDetailsData.AddPurchaseOrderDetail(this.PurchaseOrderID, this.ProductID, this.Quantity, this.UnitPrice);
                    Mode = enMode.Update;
                    return (this.DetailID != -1);

                case enMode.Update:
                    return await clsPurchaseOrderDetailsData.UpdatePurchaseOrderDetail(this.DetailID, this.ProductID, this.Quantity, this.UnitPrice);
            }
            return false;
        }

        public static async Task<DataTable> GetAllOrderDetails(int purchaseOrderID)
        {
            return await clsPurchaseOrderDetailsData.GetAllOrderDetails(purchaseOrderID);
        }

        public static async Task<bool> IsDetailExist(int detailID)
        {
            return await clsPurchaseOrderDetailsData.IsDetailExist(detailID);
        }

        public static async Task<DataTable> GetAllOrderDetailsByPurchaseOrderID(int purchaseOrderID)
        {
            return await clsPurchaseOrderDetailsData.GetDetailsByPurchaseOrderID(purchaseOrderID);
        }
        public static async Task<bool> DeletePurchaseOrderDetail(int detailID)
        {
            return await clsPurchaseOrderDetailsData.DeletePurchaseOrderDetail(detailID);
        }
    }
}
