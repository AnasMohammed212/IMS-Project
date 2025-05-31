using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS_DataAccess;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //DataTable Data = await clsSupplierData.GetAllSuppliers();
            //foreach (DataRow dr in Data.Rows)
            //{
            //    Console.WriteLine($"{dr["SupplierName"]}====>{dr["ContactPerson"]}");
            //}
            //string SupplierName = "", ContactPerson = "", Email = "", Phone = "", Address = "";
            //clsSupplierData.GetSupplierInfoByID(1, ref SupplierName, ref ContactPerson, ref Email, ref Phone, ref Address);
            //Console.WriteLine($"  {SupplierName}  {ContactPerson}   {Email}   ");



            int newProductID = await clsProductData.AddNewProduct(
                       "Redragon Fizz K617",
                       "60% RGB Mechanical Keyboard - Red Switches",
                       1,      // CategoryID
                       2,      // SupplierID
                       18.75m, // PurchasePrice
                       25.99m, // SalePrice
                       1       // UnitID
                   );

            if (newProductID != -1)
            {
                Console.WriteLine($"✅ تمت إضافة المنتج بنجاح. رقم المنتج الجديد: {newProductID}");
            }
            else
            {
                Console.WriteLine("❌ فشل في إضافة المنتج.");
            }

        }
    }
}
