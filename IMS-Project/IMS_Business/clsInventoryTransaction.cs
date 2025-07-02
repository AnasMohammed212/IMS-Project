using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsInventoryTransaction
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode = enMode.AddNew;
        public int TransactionID { get; set; }
        public int ProductID { get; set; }
        clsProduct ProductInfo;
        public decimal Quantity { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int PerformedByUserID { get; set; }
        clsUser UserInfo { get; set; }
        public string Notes { get; set; }

        public clsInventoryTransaction()
        {
            this.TransactionID = -1;
            this.ProductID = -1;
            this.Quantity = 0;
            this.TransactionType = string.Empty;
            this.TransactionDate = DateTime.Now;
            this.PerformedByUserID = -1;
            this.Notes = null;
            Mode = enMode.AddNew;
        }

        private clsInventoryTransaction(int transactionID, int productID, decimal quantity, string transactionType,
            DateTime transactionDate, int performedByUserID, string notes)
        {
            this.TransactionID = transactionID;
            this.ProductID = productID;
            this.ProductInfo=clsProduct.Find(productID);
            this.Quantity = quantity;
            this.TransactionType = transactionType;
            this.TransactionDate = transactionDate;
            this.PerformedByUserID = performedByUserID;
            this.UserInfo=clsUser.FindByPersonID(PerformedByUserID);
            this.Notes = notes;
            Mode = enMode.Update;
        }

        public static clsInventoryTransaction Find(int transactionID)
        {
            int productID = -1;
            decimal quantity = 0;
            string transactionType = string.Empty;
            DateTime transactionDate = DateTime.MinValue;
            int performedByUserID = -1;
            string notes = null;

            bool isFound = clsInventoryTransactionData.GetInventoryTransactionInfoByID(transactionID,
                ref productID, ref quantity, ref transactionType, ref transactionDate, ref performedByUserID, ref notes);

            if (isFound)
            {
                return new clsInventoryTransaction(transactionID, productID, quantity, transactionType,
                    transactionDate, performedByUserID, notes);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    this.TransactionID = await clsInventoryTransactionData.AddNewInventoryTransaction(ProductID, Quantity,
                        TransactionType, TransactionDate, PerformedByUserID, Notes);
                    Mode = enMode.Update;
                    return (this.TransactionID != -1);

                case enMode.Update:
                    return await clsInventoryTransactionData.UpdateInventoryTransaction(TransactionID, ProductID, Quantity,
                        TransactionType, TransactionDate, PerformedByUserID, Notes);

                default:
                    return false;
            }
        }

        public static async Task<DataTable> GetAllTransactions()
        {
            return await clsInventoryTransactionData.GetAllInventoryTransactions();
        }

        public static async Task<bool> IsTransactionExist(int transactionID)
        {
            return await clsInventoryTransactionData.IsInventoryTransactionExist(transactionID);
        }

        public static async Task<bool> DeleteTransaction(int transactionID)
        {
            return await clsInventoryTransactionData.DeleteInventoryTransaction(transactionID);
        }
    }
}
