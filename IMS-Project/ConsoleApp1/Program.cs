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

            int newOrderID = await clsPurchaseOrderData.AddNewPurchaseOrder(
             SupplierID: 1,
             OrderDate: DateTime.Now,
             CreatedByUserID: 1,
             Status: "Pending",
             Notes: "Test order from Program.cs"
         );
            Console.WriteLine($"✅ Added New Purchase Order with ID: {newOrderID}");

        }
    }
}
