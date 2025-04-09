using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsCategoryData
    {
        static public bool GetCategoryInfoByID(int CategoryID,ref string CategoryName)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                SqlCommand command = new SqlCommand("SP_GetCategoryInfoByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryID", CategoryID);
               
                    
                    SqlDataReader Reader = command.ExecuteReader();
                    if(Reader.Read())
                    {
                        isFound = true;
                        CategoryName = (string)Reader["CategoryName"];
                    }
                    else
                        isFound = false;
                    Reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isFound = false;
                }
            }
            return isFound;
        }

        static public async Task<bool> IsCategoryExist(int CategoryID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command=new SqlCommand("SP_IsCategoryExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
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
                Console.WriteLine(ex.Message);
                isFound = false;
            }

            return isFound;
        }

        public static async Task<DataTable> GetAllCategories()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_GetAllCategories",connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    
                    SqlDataReader Reader = await command.ExecuteReaderAsync();
                    if (Reader.HasRows)
                    {
                        dt.Load(Reader);
                    }
                    Reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
                
            }
            return dt;
        }
            
        public static async Task<int> AddNewCategory(string CategoryName)
        {
            int NewCategoryID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using(SqlCommand command = new SqlCommand("SP_AddNewCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryName", CategoryName);
                        SqlParameter outputIdParam = new SqlParameter("@NewCategoryID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        await command.ExecuteNonQueryAsync();
                        NewCategoryID = (int)command.Parameters["@NewCategoryID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return NewCategoryID;
        }

        public static async Task<bool> UpdateCategory(int CategoryID, string CategoryName)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_UpdateCategory", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
                        command.Parameters.AddWithValue("@CategoryName", CategoryName);
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
        public static async Task<bool> DeleteCategory(int CategoryID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeleteCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
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

        dddfdf
    }
}
