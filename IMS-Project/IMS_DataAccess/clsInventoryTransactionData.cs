using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsInventoryTransactionData
    {
        public static async Task<int> AddNewInventoryTransaction(int ProductID, decimal Quantity, string TransactionType, DateTime TransactionDate, int PerformedByUserID, string Notes)
        {
            int NewTransactionID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_AddNewInventoryTransaction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@TransactionType", TransactionType);
                        command.Parameters.AddWithValue("@TransactionDate", TransactionDate);
                        command.Parameters.AddWithValue("@PerformedByUserID", PerformedByUserID);
                        command.Parameters.AddWithValue("@Notes", Notes ?? (object)DBNull.Value);

                        SqlParameter outputParam = new SqlParameter("@NewTransactionID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        await command.ExecuteNonQueryAsync();
                        NewTransactionID = (int)command.Parameters["@NewTransactionID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return NewTransactionID;
        }
        public static async Task<bool> UpdateInventoryTransaction(int TransactionID, int ProductID, decimal Quantity, string TransactionType, DateTime TransactionDate, int PerformedByUserID, string Notes)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_UpdateInventoryTransaction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TransactionID", TransactionID);
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@TransactionType", TransactionType);
                        command.Parameters.AddWithValue("@TransactionDate", TransactionDate);
                        command.Parameters.AddWithValue("@PerformedByUserID", PerformedByUserID);
                        command.Parameters.AddWithValue("@Notes", Notes ?? (object)DBNull.Value);

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return (rowsAffected > 0);
        }

        public static async Task<bool> DeleteInventoryTransaction(int TransactionID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeleteInventoryTransaction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TransactionID", TransactionID);
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return (rowsAffected > 0);
        }

        public static async Task<bool> IsInventoryTransactionExist(int TransactionID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsInventoryTransactionExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TransactionID", TransactionID);

                        SqlParameter returnParam = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParam);

                        await command.ExecuteNonQueryAsync();
                        isFound = ((int)returnParam.Value == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return isFound;
        }

        public static async Task<DataTable> GetAllInventoryTransactions()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_GetAllInventoryTransactions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return dt;
        }

        public static bool GetInventoryTransactionInfoByID(int TransactionID, ref int ProductID, ref decimal Quantity, ref string TransactionType, ref DateTime TransactionDate, ref int PerformedByUserID, ref string Notes)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetInventoryTransactionInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TransactionID", TransactionID);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            ProductID = (int)reader["ProductID"];
                            Quantity = (decimal)reader["Quantity"];
                            TransactionType = reader["TransactionType"].ToString();
                            TransactionDate = (DateTime)reader["TransactionDate"];
                            PerformedByUserID = (int)reader["PerformedByUserID"];
                            Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null;
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isFound = false;
                }
            }
            return isFound;
        }
    }
}
