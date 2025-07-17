using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsPurchaseOrderDetailsData
    {
        public static bool GetPurchaseOrderDetailByID(int DetailID, ref int PurchaseOrderID, ref int ProductID,
                                              ref decimal Quantity, ref decimal UnitPrice)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetDetailsByOrderID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetailID", DetailID);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            PurchaseOrderID = (int)reader["PurchaseOrderID"];
                            ProductID = (int)reader["ProductID"];
                            Quantity = (decimal)reader["Quantity"];
                            UnitPrice = (decimal)reader["UnitPrice"];
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return isFound;
        }

        public static async Task<DataTable> GetDetailsByPurchaseOrderID(int PurchaseOrderID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_GetDetailsByPurchaseOrderID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                            dt.Load(reader);

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching details: " + ex.Message);
            }

            return dt;
        }
        public static async Task<int> AddPurchaseOrderDetail(int PurchaseOrderID, int ProductID, decimal Quantity, decimal UnitPrice)
        {
            int NewID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_AddPurchaseOrderDetail", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", UnitPrice);

                        SqlParameter outputParam = new SqlParameter("@NewDetailID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        await command.ExecuteNonQueryAsync();
                        NewID = (int)command.Parameters["@NewDetailID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return NewID;
        }

        public static async Task<bool> UpdatePurchaseOrderDetail(int DetailID, int ProductID, decimal Quantity, decimal UnitPrice)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_UpdatePurchaseOrderDetail", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetailID", DetailID);
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", UnitPrice);

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

        public static async Task<bool> DeletePurchaseOrderDetail(int DetailID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeletePurchaseOrderDetail", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetailID", DetailID);
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

        public static async Task<DataTable> GetAllOrderDetails(int PurchaseOrderID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_GetDetailsByOrderID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);

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

        public static async Task<bool> IsDetailExist(int DetailID)
        {
            bool exists = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsDetailExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetailID", DetailID);

                        SqlParameter returnParam = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParam);

                        await command.ExecuteNonQueryAsync();
                        exists = ((int)returnParam.Value == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return exists;
        }

    }
}