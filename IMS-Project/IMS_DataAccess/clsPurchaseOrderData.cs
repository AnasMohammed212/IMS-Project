using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsPurchaseOrderData
    {
        
            public static async Task<int> AddNewPurchaseOrder(int SupplierID, DateTime OrderDate, int CreatedByUserID, string Status, string Notes)
            {
                int NewID = -1;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand("SP_AddNewPurchaseOrder", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@SupplierID", SupplierID);
                            command.Parameters.AddWithValue("@OrderDate", OrderDate);
                            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                            command.Parameters.AddWithValue("@Status", Status);
                            command.Parameters.AddWithValue("@Notes", Notes ?? (object)DBNull.Value);

                            SqlParameter outputParam = new SqlParameter("@NewPurchaseOrderID", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            command.Parameters.Add(outputParam);

                            await command.ExecuteNonQueryAsync();
                            NewID = (int)command.Parameters["@NewPurchaseOrderID"].Value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return NewID;
            }

            public static async Task<bool> UpdatePurchaseOrder(int PurchaseOrderID, int SupplierID, DateTime OrderDate, int CreatedByUserID, string Status, string Notes)
            {
                int rowsAffected = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand("SP_UpdatePurchaseOrder", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);
                            command.Parameters.AddWithValue("@SupplierID", SupplierID);
                            command.Parameters.AddWithValue("@OrderDate", OrderDate);
                            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                            command.Parameters.AddWithValue("@Status", Status);
                            command.Parameters.AddWithValue("@Notes", Notes ?? (object)DBNull.Value);

                            rowsAffected = await command.ExecuteNonQueryAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return (rowsAffected > 0);
            }

            public static async Task<bool> DeletePurchaseOrder(int PurchaseOrderID)
            {
                int rowsAffected = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand("SP_DeletePurchaseOrder", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);
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

            public static bool GetPurchaseOrderByID(int PurchaseOrderID, ref int SupplierID, ref DateTime OrderDate, ref int CreatedByUserID, ref string Status, ref string Notes)
            {
                bool isFound = false;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("SP_GetPurchaseOrderByID", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                isFound = true;
                                SupplierID = (int)reader["SupplierID"];
                                OrderDate = (DateTime)reader["OrderDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                Status = reader["Status"].ToString();
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null;
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

            public static async Task<bool> IsPurchaseOrderExist(int PurchaseOrderID)
            {
                bool isFound = false;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand("SP_IsPurchaseOrderExist", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@PurchaseOrderID", PurchaseOrderID);

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

            public static async Task<DataTable> GetAllPurchaseOrders()
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand("SP_GetAllPurchaseOrders", connection))
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
        }

    }
