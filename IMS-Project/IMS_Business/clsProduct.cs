using System.Data;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace IMS_Business
{
    public class clsProduct
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode = enMode.AddNew;

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public clsCategory CategoryInfo;
        public int SupplierID { get; set; }
        public clsSupplier SupplierInfo;
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int UnitID { get; set; }
        public clsUnitOfMeasure UnitInfo;
        
        public clsProduct()
        {
            this.ProductID = -1;
            this.ProductName = "";
            this.Description = "";
            this.CategoryID = -1;
            this.SupplierID = -1;
            this.PurchasePrice = 0;
            this.SalePrice = 0;
            this.UnitID = -1;
            Mode = enMode.AddNew;
        }

        private clsProduct(int productID, string productName, string description, int categoryID,
            int supplierID, decimal purchasePrice, decimal salePrice, int unitID)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.Description = description;
            this.CategoryID = categoryID;
            this.CategoryInfo=clsCategory.Find(this.CategoryID);
            this.SupplierID = supplierID;
            this.SupplierInfo=clsSupplier.Find(this.SupplierID);
            this.PurchasePrice = purchasePrice;
            this.SalePrice = salePrice;
            this.UnitID = unitID;
            this.UnitInfo=clsUnitOfMeasure.Find(this.UnitID);
            Mode = enMode.Update;
        }

        public static clsProduct Find(int ProductID)
        {
            string productName = "", description = "";
            int categoryID = -1, supplierID = -1, unitID = -1;
            decimal purchasePrice = 0, salePrice = 0;

            bool isFound = clsProductData.GetProductInfoByID(ProductID, ref productName, ref description, ref categoryID,
                ref supplierID, ref purchasePrice, ref salePrice, ref unitID);

            if (isFound)
                return new clsProduct(ProductID, productName, description, categoryID, supplierID, purchasePrice, salePrice, unitID);
            else
                return null;
        }

        private async Task<bool> _AddNewProduct()
        {
            this.ProductID = await clsProductData.AddNewProduct(this.ProductName, this.Description, this.CategoryID,
                this.SupplierID, this.PurchasePrice, this.SalePrice, this.UnitID);

            return (this.ProductID != -1);
        }

        private async Task<bool> _UpdateProduct()
        {
            return await clsProductData.UpdateProduct(this.ProductID, this.ProductName, this.Description, this.CategoryID,
                this.SupplierID, this.PurchasePrice, this.SalePrice, this.UnitID);
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewProduct())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateProduct();
            }
            return false;
        }

        public static async Task<DataTable> GetAllProducts()
        {
            return await clsProductData.GetAllProducts();
        }

        public static async Task<bool> IsProductExist(int ProductID)
        {
            return await clsProductData.IsProductExist(ProductID);
        }

        public static async Task<bool> DeleteProduct(int ProductID)
        {
            return await clsProductData.DeleteProduct(ProductID);
        }
    }
}
