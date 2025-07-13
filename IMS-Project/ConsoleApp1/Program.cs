using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;
using IMS_Business;
namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            clsProduct product = new clsProduct
            {
                ProductName = "Laptop Lenovo ThinkPad",
                Description = "Business-grade laptop with Intel i7",
                PurchasePrice = 500.00m,
                SalePrice = 650.00m
            };

            // تعيين Category
            var category = clsCategory.Find("Electronics");
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }
            product.CategoryID = category.CategoryID;

            // تعيين Supplier
            var supplier = clsSupplier.Find("Global Tech Supplies");
            if (supplier == null)
            {
                Console.WriteLine("Supplier not found.");
                return;
            }
            product.SupplierID = supplier.SupplierID;

            // تعيين Unit
            var unit = clsUnitOfMeasure.Find("Kilogram");
            if (unit == null)
            {
                Console.WriteLine("Unit not found.");
                return;
            }
            product.UnitID = unit.UnitID;
            //Console.WriteLine($"Saving product with UnitID = {product.UnitID}");

            // محاولة الحفظ
            bool result = await product.Save();

            if (result)
            {
                Console.WriteLine("✅ Product saved successfully with ID: " + product.ProductID);
            }
            else
            {
                Console.WriteLine("❌ Failed to save the product.");
            }

        }
    }
    
}
