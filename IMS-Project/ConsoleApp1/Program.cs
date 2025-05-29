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
            //clsSupplierData.GetSupplierInfoByID(Guid.Parse("E085CE12-7AD6-416C-8D0B-7E77D5C88D61"), ref SupplierName,ref ContactPerson,ref Email,ref Phone,ref Address);
            //Console.WriteLine($"  {SupplierName}  {ContactPerson}   {Email}   ");
            await clsSupplierData.DeleteSupplier(1);
           

        } 
    }
}
