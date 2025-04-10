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
        public static bool GetSupplierInfoByID(Guid SupplierID, ref string SupplierName, ref string ContactPerson, ref string Email, ref string Phone, ref string Address)
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
                            ContactPerson = reader["ContactPerson"] != DBNull.Value ? (string)reader["ContactPerson"] : null;
                            Email = (string)reader["Email"];
                            Phone = (string)reader["Phone"];
                            Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : null;
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
        public static async Task<bool> IsSupplierExist(Guid SupplierID)
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
        public static async Task<int> AddNewSupplier(string SupplierName,string ContactPerson,string Email, string Phone, string Address)
        {
            int NewSupplierID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsSupplierExist", connection))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
            }
            return NewSupplierID;

        }
    }
}
