using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsCountryData
    {
        public static bool GetCountryInfoByID(int ID, ref string CountryName)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetCountryInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CountryID", ID);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            CountryName = (string)reader["CountryName"];
                            isFound = true;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
         

            return isFound;

        }
        
        public static bool GetCountryInfoByName(string CountryName, ref int CountryID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetCountryInfoByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            CountryID = (int)reader["CountryID"];
                            isFound = true;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }


            return isFound;
        }

        public static async Task<DataTable> GetAllCountries()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP_GetAllCountries", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return dt;
        }
    }
}
