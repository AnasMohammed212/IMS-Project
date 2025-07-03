using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsStock
    {
        private enum enMode { AddNew = 1, Update = 0 }
        private enMode Mode;

        public int ProductID { get; set; }
        public clsProduct ProductInfo;
        public decimal Quantity { get; set; }
        public DateTime LastUpdated { get; set; }

        public clsStock()
        {
            this.ProductID = -1;
            this.Quantity = 0;
            this.LastUpdated = DateTime.Now;
            Mode = enMode.AddNew;
        }

        public clsStock(int productID, decimal quantity, DateTime lastUpdated)
        {
            this.ProductID = productID;
            this.ProductInfo=clsProduct.Find(ProductID);
            this.Quantity = quantity;
            this.LastUpdated = lastUpdated;
            Mode = enMode.Update;
        }

        public static clsStock Find(int productID)
        {
            decimal quantity = 0;
            DateTime lastUpdated = DateTime.Now;

            bool isFound = clsStockData.GetStockInfoByProductID(productID, ref quantity, ref lastUpdated);
            if (isFound)
                return new clsStock(productID, quantity, lastUpdated);
            else
                return null;
        }

        private async Task<bool> _AddNewStock()
        {
            this.ProductID = await clsStockData.AddNewStock(this.ProductID, this.Quantity);
            return (this.ProductID != -1);
        }

        private async Task<bool> _UpdateStock()
        {
            return await clsStockData.UpdateStock(this.ProductID, this.Quantity);
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewStock())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return await _UpdateStock();
            }

            return false;
        }

        public static async Task<DataTable> GetAllStock()
        {
            return await clsStockData.GetAllStock();
        }

        public static async Task<bool> DeleteStock(int productID)
        {
            return await clsStockData.DeleteStock(productID);
        }

        public static async Task<bool> IsStockExist(int productID)
        {
            return await clsStockData.IsStockExist(productID);
        }
    }
}
