using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsPersonData
    {
        public static async Task<DataTable> GetAllPeople()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_GetAllPeople", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = await command.ExecuteReaderAsync();
                dt.Load(reader);
                reader.Close();
            }
            return dt;
        }

        public static bool GetPersonInfoByID(int personID, ref string firstName, ref string secondName, ref string thirdName,
            ref string lastName, ref DateTime dateOfBirth, ref short gender, ref string address,
            ref string phone, ref string email, ref int nationalityCountryID, ref string imagePath,ref DateTime CreatedAt ,ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SP_GetPersonByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@PersonID", personID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    firstName = (string)reader["FirstName"];
                    secondName = (string)reader["SecondName"];
                    thirdName = reader["ThirdName"] == DBNull.Value ? "" : (string)reader["ThirdName"];
                    lastName = (string)reader["LastName"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gender = Convert.ToInt16(reader["Gender"]);
                    address = (string)reader["Address"];
                    phone = reader["Phone"] == DBNull.Value ? "" : (string)reader["Phone"];
                    email = reader["Email"] == DBNull.Value ? "" : (string)reader["Email"];
                    nationalityCountryID = (int)reader["NationalityCountryID"];
                    imagePath = reader["ImagePath"] == DBNull.Value ? "" : (string)reader["ImagePath"];
                    CreatedAt = (DateTime)reader["CreatedAt"];
                    isActive = (bool)reader["IsActive"];
                }
                reader.Close();
            }

            return isFound;
        }

        public static async Task<int> AddNewPerson(string firstName, string secondName, string thirdName, string lastName,
            DateTime dateOfBirth, short gender, string address, string phone, string email,
            int nationalityCountryID, string imagePath,DateTime CreatedAt ,bool isActive)
        {
            int newPersonID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_AddNewPerson", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@SecondName", secondName);
                command.Parameters.AddWithValue("@ThirdName", thirdName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);
                command.Parameters.AddWithValue("@ImagePath", imagePath);
                command.Parameters.AddWithValue("@CreatedAt", CreatedAt);
                command.Parameters.AddWithValue("@IsActive", isActive);

                SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

               await command.ExecuteNonQueryAsync();
                newPersonID = (int)outputIdParam.Value;
            }

            return newPersonID;
        }

        public static async Task<bool> UpdatePerson(int personID, string firstName, string secondName, string thirdName, string lastName,
            DateTime dateOfBirth, short gender, string address, string phone, string email,
            int nationalityCountryID, string imagePath, DateTime CreatedAt, bool isActive)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_UpdatePerson", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@SecondName", secondName);
                command.Parameters.AddWithValue("@ThirdName", thirdName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);
                command.Parameters.AddWithValue("@ImagePath", imagePath);
                command.Parameters.AddWithValue("@CreatedAt", CreatedAt);
                command.Parameters.AddWithValue("@IsActive", isActive);

                rowsAffected = await command.ExecuteNonQueryAsync();
            }

            return rowsAffected > 0;
        }

        public static async Task<bool> DeletePerson(int personID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_DeletePerson", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@PersonID", personID);
                rowsAffected = await command.ExecuteNonQueryAsync();
            }

            return rowsAffected > 0;
        }

        public static async Task<bool> IsPersonExist(int personID)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_IsPersonExist", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@PersonID", personID);

                SqlParameter returnParam = new SqlParameter("@ReturnVal", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnParam);

                await command.ExecuteNonQueryAsync();
                exists = (int)returnParam.Value == 1;
            }

            return exists;
        }

    }
}
