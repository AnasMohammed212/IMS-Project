using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsSupplierData
    {
        public static bool GetSupplierInfoByID(int SupplierID, ref string SupplierName, ref int ContactPersonID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetSupplierInfoByID", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SupplierID", SupplierID);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            SupplierName = (string)reader["SupplierName"];
                            ContactPersonID = (int)reader["ContactPersonID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isFound;
        }
        public static async Task<DataTable> GetAllSuppliers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_GetAllSuppliers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        public static async Task<bool> IsSupplierExist(int SupplierID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsSupplierExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SupplierID", SupplierID);
                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);
                        await command.ExecuteNonQueryAsync();
                        isFound = (int)returnParameter.Value == 1;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return isFound;
        }
        public static async Task<int> AddNewSupplier(string SupplierName,int ContactPersonID)
        {
            int NewSupplierID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_AddNewSupplier", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SupplierName", SupplierName);
                        command.Parameters.AddWithValue("@ContactPersonID", ContactPersonID);
                        SqlParameter outputIdParam = new SqlParameter("@NewSupplierID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        await command.ExecuteNonQueryAsync();
                        NewSupplierID = (int)command.Parameters["@NewSupplierID"].Value;

                    }
                }
            }
            catch (Exception ex)
            {
            Console.WriteLine(ex.ToString());
            }
            return NewSupplierID;

        }
        public static async Task<bool> UpdateSupplier(int SupplierID,string SupplierName, int ContactPersonID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_UpdateSupplier", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SupplierID", SupplierID);
                        command.Parameters.AddWithValue("@SupplierName", SupplierName);
                        command.Parameters.AddWithValue("@ContactPersonID", ContactPersonID);
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
        public static async Task<bool> DeleteSupplier(int SupplierID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeleteSupplier", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SupplierID", SupplierID);
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
