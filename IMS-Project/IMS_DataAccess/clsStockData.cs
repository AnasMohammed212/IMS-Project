using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsStockData
    {
        public static async Task<int> AddNewStock(int ProductID, decimal Quantity)
        {
            int NewStockID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_AddNewStock", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);

                        SqlParameter outputIdParam = new SqlParameter("@NewStockID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        await command.ExecuteNonQueryAsync();
                        NewStockID = (int)command.Parameters["@NewStockID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return NewStockID;
        }


        public static async Task<bool> UpdateStock(int ProductID, decimal Quantity)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_UpdateStock", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);

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

        public static async Task<DataTable> GetAllStock()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_GetAllStock", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (reader.HasRows)
                            dt.Load(reader);

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

        public static bool GetStockInfoByProductID(int ProductID, ref int StockID, ref decimal Quantity, ref DateTime LastUpdated)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetStockInfoByProductID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            StockID = (int)reader["StockID"];
                            Quantity = (decimal)reader["Quantity"];
                            LastUpdated = (DateTime)reader["LastUpdated"];
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return isFound;
        }


        public static async Task<bool> IsStockExist(int ProductID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_IsStockExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);

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
                Console.WriteLine(ex.ToString());
            }

            return isFound;
        }

        public static async Task<bool> DeleteStock(int ProductID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_DeleteStock", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);

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
    }
}
