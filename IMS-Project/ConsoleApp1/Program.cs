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
            DataTable Data = await clsCategoryData.GetAllCategories();
            foreach (DataRow dr in Data.Rows)
            {
                Console.WriteLine($"{dr["CategoryID"]}====>{dr["CategoryName"]}");
            }
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //if (await clsCategoryData.IsCategoryExist(3))
            //{
            //    Console.WriteLine("Yes");
            //}
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds.ToString());
            //string name = "";
            //if (clsCategoryData.GetCategoryInfoByID(4,ref name))
            //{
            //    Console.WriteLine($"{name}");
            //}
            //await clsCategoryData.AddNewCategory("dddddddddddddd");
            //await clsCategoryData.UpdateCategory(9,"Sports");
            //await clsCategoryData.DeleteCategory(10);

            Console.WriteLine(4);
        }
    }
}
