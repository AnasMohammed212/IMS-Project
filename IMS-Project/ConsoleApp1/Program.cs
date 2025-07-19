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

            int customerID = 7;

            Console.WriteLine($"Attempting to delete customer with ID = {customerID}...");

            bool isDeleted = await clsCustomerData.DeleteCustomer(customerID);

            if (isDeleted)
                Console.WriteLine("✔ Customer deleted successfully from DAL.");
            else
                Console.WriteLine("✖ Customer could not be deleted (maybe linked data exists or ID not found).");

            Console.WriteLine("Test completed.");

        }
    }
    
}
