using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsUserData
    {
        public static async Task<int> AddNewUser(int PersonID, string UserName, string Password, bool IsActive = true)
        {
            int NewUserID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@IsActive", IsActive);

                        SqlParameter outputIdParam = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        await command.ExecuteNonQueryAsync();
                        NewUserID = (int)command.Parameters["@UserID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return NewUserID;
        }


        public static async Task<bool> UpdateUser(int UserID, string UserName,
    string Password = null, bool IsActive = true)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_UpdateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@IsActive", IsActive);

                        if (!string.IsNullOrEmpty(Password))
                            command.Parameters.AddWithValue("@Password", Password);
                        else
                            command.Parameters.AddWithValue("@Password", DBNull.Value);

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


        public static async Task<DataTable> GetUserByID(int UserID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_GetUserByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);

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

        public static async Task<bool> IsUserExist(int UserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsUserExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        
                            command.Parameters.AddWithValue("@UserID", UserID);
                        

                        SqlParameter returnParameter = new SqlParameter("@Exists", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(returnParameter);

                        await command.ExecuteNonQueryAsync();
                        isFound = (bool)returnParameter.Value;
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


        public static async Task<DataTable> GetAllUsers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_GetAllUsers", connection);
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



    }
}
