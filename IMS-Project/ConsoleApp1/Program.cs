﻿using System;
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



            int newProductID = await clsProductData.AddNewProduct("Amazon Echo Dot (4th Gen)", "Smart speaker with Alexa", 1, 4, 29.99m, 49.99m, 1);

            if (newProductID != -1)
            {
                Console.WriteLine($" DONE : {newProductID}");
            }
            else
            {
                Console.WriteLine("❌ Failed ");
            }

        }
    }
}
