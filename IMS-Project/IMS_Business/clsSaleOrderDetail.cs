using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsSaleOrderDetail
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode = enMode.AddNew;

        public int DetailID { get; set; }
        public int SaleOrderID { get; set; }
        clsSaleOrder SaleOrderInfo;
        public int ProductID { get; set; }
        public clsProduct ProductInfo;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public clsSaleOrderDetail()
        {
            DetailID = -1;
            SaleOrderID = -1;
            ProductID = -1;
            Quantity = 0;
            UnitPrice = 0;
            Mode = enMode.AddNew;
        }

        private clsSaleOrderDetail(int detailID, int saleOrderID, int productID, decimal quantity, decimal unitPrice)
        {
            DetailID = detailID;
            SaleOrderID = saleOrderID;
            ProductID = productID;
            Quantity = quantity;
            UnitPrice = unitPrice;
            SaleOrderInfo = clsSaleOrder.Find(saleOrderID);
            ProductInfo = clsProduct.Find(productID);
            Mode = enMode.Update;
        }

        public static clsSaleOrderDetail Find(int detailID)
        {
            int saleOrderID = -1, productID = -1;
            decimal quantity = 0, unitPrice = 0;

            bool isFound = clsSaleOrderDetailsData.GetSaleOrderDetailByID(detailID, ref saleOrderID, ref productID, ref quantity, ref unitPrice);

            if (isFound)
                return new clsSaleOrderDetail(detailID, saleOrderID, productID, quantity, unitPrice);
            else
                return null;
        }
        private async Task<bool> _AddNewSaleOrderDetail()
        {
            this.DetailID = await clsSaleOrderDetailsData.AddSaleOrderDetail(this.SaleOrderID, this.ProductID, this.Quantity, this.UnitPrice);
            return (this.DetailID != -1);
        }

        private async Task<bool> _UpdateSaleOrderDetail()
        {
            return await clsSaleOrderDetailsData.UpdateSaleOrderDetail(this.DetailID, this.ProductID, this.Quantity, this.UnitPrice);
        }
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewSaleOrderDetail())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateSaleOrderDetail();
            }
            return false;
        }

        public static async Task<DataTable> GetAllOrderDetails(int saleOrderID)
        {
            return await clsSaleOrderDetailsData.GetAllOrderDetails(saleOrderID);
        }

        public static async Task<bool> IsDetailExist(int detailID)
        {
            return await clsSaleOrderDetailsData.IsDetailExist(detailID);
        }

        public static async Task<DataTable> GetAllOrderDetailsBySaleOrderID(int saleOrderID)
        {
            return await clsSaleOrderDetailsData.GetDetailsBySaleOrderID(saleOrderID);
        }
        public static async Task<bool> DeleteSaleOrderDetail(int detailID)
        {
            return await clsSaleOrderDetailsData.DeleteSaleOrderDetail(detailID);
        }
    }
}
