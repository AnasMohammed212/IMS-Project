using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsUnitOfMeasureData
    {
        static public bool GetUnitInfoByID(int UnitID, ref string UnitName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SP_GetUnitInfoByID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UnitID", UnitID);


                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        isFound = true;
                        UnitName = (string)Reader["UnitName"];
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
        static public bool GetUnitInfoByName(ref int UnitID,  string UnitName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SP_GetUnitByName", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UnitName", UnitName);


                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        isFound = true;
                        UnitID = (int)Reader["UnitID"];
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

        public static async Task<DataTable> GetAllUnitsOfMeasure()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_GetAllUnitsOfMeasure", connection);
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
        public static async Task<bool> IsUnitExist(string UnitName)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsUnitExistByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UnitName", UnitName);

                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);

                        await command.ExecuteNonQueryAsync();
                        isFound = ((int)returnParameter.Value == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return isFound;
        }

    }
}
