using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_DataAccess
{
    public class clsProductData
    {
        static public bool GetProductInfoByID(int ProductID,ref string ProductName,ref string Description,ref int CategoryID,ref int SupplierID, ref decimal PurchasePrice,ref decimal SalePrice,ref int UnitID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SP_GetProductInfoByID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductID", ProductID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        ProductName = reader["ProductName"].ToString();
                        Description = reader["Description"].ToString();
                        CategoryID = (int)reader["CategoryID"];
                        SupplierID = (int)reader["SupplierID"];
                        PurchasePrice = (decimal)reader["PurchasePrice"];
                        SalePrice = (decimal)reader["SalePrice"];
                        UnitID = (int)reader["UnitID"];
                    }
                    else
                    {
                        isFound = false;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }

        public static async Task<bool> IsProductExist(int productID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_IsProductExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", productID);

                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);

                        await command.ExecuteNonQueryAsync();
                        isFound = (int)returnParameter.Value == 1;
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

        public static async Task<DataTable> GetAllProducts()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SP_GetAllProducts", connection);
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

        public static async Task<int> AddNewProduct(string ProductName, string Description, int CategoryID,int SupplierID, decimal PurchasePrice, decimal SalePrice,int UnitID)
        {
            int NewProductID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_AddNewProduct", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ProductName", ProductName);
                        command.Parameters.AddWithValue("@Description", Description);
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
                        command.Parameters.AddWithValue("@SupplierID", SupplierID);
                        command.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
                        command.Parameters.AddWithValue("@SalePrice", SalePrice);
                        command.Parameters.AddWithValue("@UnitID", UnitID);

                        SqlParameter outputIdParam = new SqlParameter("@NewProductID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        await command.ExecuteNonQueryAsync();
                        NewProductID = (int)command.Parameters["@NewProductID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"Adding Product: {ProductName}, {Description}, CatID: {CategoryID}, SupplierID: {SupplierID}, PurchasePrice: {PurchasePrice}, SalePrice: {SalePrice}, UnitID: {UnitID}");

            return NewProductID;
        }

        public static async Task<bool> UpdateProduct(int productID,string productName, string description, int categoryID,int supplierID,decimal purchasePrice,decimal salePrice,int unitID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_UpdateProduct", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

           
                        command.Parameters.AddWithValue("@ProductID", productID);
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@CategoryID", categoryID);
                        command.Parameters.AddWithValue("@SupplierID", supplierID);
                        command.Parameters.AddWithValue("@PurchasePrice", purchasePrice);
                        command.Parameters.AddWithValue("@SalePrice", salePrice);
                        command.Parameters.AddWithValue("@UnitID", unitID);

                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
           
                Console.WriteLine(ex.Message);
             
            }

            return rowsAffected > 0;
        }

        public static async Task<bool> DeleteProduct(int productID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeleteProduct", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", productID);
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }

            return rowsAffected > 0;
        }

        public static bool GetProductInfoByName(string productName, ref int productID, ref string description, ref int categoryID,
                                        ref int supplierID, ref decimal purchasePrice, ref decimal salePrice, ref int unitID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SP_GetProductInfoByName", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductName", productName);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        productID = (int)reader["ProductID"];
                        description = reader["Description"].ToString();
                        categoryID = (int)reader["CategoryID"];
                        supplierID = (int)reader["SupplierID"];
                        purchasePrice = (decimal)reader["PurchasePrice"];
                        salePrice = (decimal)reader["SalePrice"];
                        unitID = (int)reader["UnitID"];
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return isFound;
        }
    }
}
