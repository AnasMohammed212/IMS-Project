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


            clsCustomer foundCustomer=clsCustomer.Find(6);

            foundCustomer.CustomerName = "tttttttttttt";
            await foundCustomer.Save();
            //// 4. ✅ Update customer data
            //if (foundCustomer != null)
            //{
            //    foundCustomer.CustomerName = "gggggggggggg";
            //    foundCustomer.ContactPersonID = 1; // another valid contact person ID

            //    bool updated = await foundCustomer.Save();
            //    Console.WriteLine(updated
            //        ? "✅ Customer updated successfully"
            //        : "❌ Failed to update customer");
            //}

            //// 5. ✅ Display all customers
            //Console.WriteLine("\n📋 All Customers:");
            //DataTable allCustomers = await clsCustomer.GetAllCustomers();
            //foreach (DataRow row in allCustomers.Rows)
            //{
            //    Console.WriteLine($"- ID: {row["CustomerID"]}, Name: {row["CustomerName"]}, ContactPersonID: {row["ContactPersonID"]}");
            //}

            //// 6. ✅ Delete the customer
            //bool deleted = await clsCustomer.DeleteCustomer(11);
            //Console.WriteLine(deleted
            //    ? $"🗑️ Customer ID {testCustomerID} deleted successfully"
            //    : "❌ Failed to delete customer");

        }
    }
    
}
