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

                        SqlParameter outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        await command.ExecuteNonQueryAsync();
                        NewUserID = (int)command.Parameters["@NewUserID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return NewUserID;
        }


        public static async Task<bool> UpdateUser(int UserID,int PersonID,string UserName,
    string Password ,bool IsActive)
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
                        command.Parameters.AddWithValue("@PersonID", PersonID);
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


        public static bool GetUserInfoByUserID(int UserID,ref int PersonID,ref string Username,ref string Password,ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                try
                {
                    connection.Open();
                SqlCommand command = new SqlCommand("SP_GetUserByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);

                    SqlDataReader Reader =  command.ExecuteReader();
                    if (Reader.Read())
                    {
                        isFound = true;
                        PersonID=(int)Reader["PersonID"];
                        Username = (string)Reader["UserName"];
                        Password = (string)Reader["Password"];
                        isActive = (bool)Reader["IsActive"];
                    }
                    else
                        isFound = false;
                    Reader.Close();
                }
                catch (Exception ex)
                {
                    isFound = false;
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
            return isFound;
        }
        public static bool GetUserInfoByPersonID(int PersonID,ref int UserID, ref string Username, ref string Password, ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SP_GetUserByPersonID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        isFound = true;
                        UserID = (int)Reader["UserID"];
                        Username = (string)Reader["UserName"];
                        Password = (string)Reader["Password"];
                        isActive = (bool)Reader["IsActive"];
                    }
                    else
                        isFound = false;
                    Reader.Close();
                }
                catch (Exception ex)
                {
                    isFound = false;
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
            return isFound;
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
                        

                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);

                        await command.ExecuteNonQueryAsync();
                        isFound = ((int)returnParameter.Value)==1;
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
        public static async Task<bool> IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsUserExistForPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@PersonID", PersonID);


                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);

                        await command.ExecuteNonQueryAsync();
                        isFound = ((int)returnParameter.Value) == 1;
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
        public static async Task<bool> DeleteUser(int UserID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
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

