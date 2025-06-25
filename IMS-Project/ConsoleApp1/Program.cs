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
            //DataTable Data = await clsUserData.GetAllUsers();
            //foreach (DataRow dr in Data.Rows)
            //{
            //    Console.WriteLine($"{dr["UserID"]}====>{dr["UserName"]}");
            //}
            //string Password = "", Username="";
            //int PersonID = -1;
            //bool IsActive=false;
            //clsUserData.GetUserInfoByID(4,ref PersonID,ref Username,ref Password,ref IsActive);
            //Console.WriteLine($"  {PersonID}  {Password}   {IsActive}   ");

            int newTransactionID = await clsInventoryTransactionData.AddNewInventoryTransaction(
           ProductID: 17,
           Quantity: 10,
           TransactionType: "IN",
           TransactionDate: DateTime.Now,
           PerformedByUserID: 1,
           Notes: "First inventory transaction"
       );
            Console.WriteLine($"New Transaction ID: {newTransactionID}");

        }
    }
}
