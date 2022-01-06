using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public IConfiguration Configuration { get; }
        public AddressRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;

        //Adding books api
        public string AddAddress(AddressModel address)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "SpAddAddress";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", address.UserId);
                    sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                    sqlCommand.Parameters.AddWithValue("@City", address.City);
                    sqlCommand.Parameters.AddWithValue("@State", address.State);
                    sqlCommand.Parameters.AddWithValue("@TypeId", address.TypeId);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "UserId not exists";
                    }
                    else
                    {
                        return "Address Added succssfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Update address details api
        public string UpdateAddress(AddressModel address)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "UpdateAddress";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@AddressId", address.AddressId);
                    sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                    sqlCommand.Parameters.AddWithValue("@City", address.City);
                    sqlCommand.Parameters.AddWithValue("@State", address.State);
                    sqlCommand.Parameters.AddWithValue("@TypeId", address.TypeId);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Address not exists";
                    }
                    else
                    {
                        return "Address updated succssfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Get all address
        public List<AddressModel> GetAllAddresses()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "GetAllAddresses";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<AddressModel> allAddress = new List<AddressModel>();
                        while (sqlData.Read())
                        {
                            AddressModel address = new AddressModel();
                            address.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                            address.Address = sqlData["Address"].ToString();
                            address.City = sqlData["City"].ToString();
                            address.State = sqlData["State"].ToString();
                            address.TypeId = Convert.ToInt32(sqlData["TypeId"]);
                            allAddress.Add(address);
                        }
                        return allAddress;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Get address by userid
        public List<AddressModel> GetAddressesbyUserid(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "GetAddressbyUserid";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<AddressModel> allAddress = new List<AddressModel>();
                        while (sqlData.Read())
                        {
                            AddressModel address = new AddressModel();
                            address.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                            address.Address = sqlData["Address"].ToString();
                            address.City = sqlData["City"].ToString();
                            address.State = sqlData["State"].ToString();
                            address.TypeId = Convert.ToInt32(sqlData["TypeId"]);
                            allAddress.Add(address);
                        }
                        return allAddress;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
