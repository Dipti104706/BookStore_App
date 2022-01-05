using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class CartRepository : ICartRepository
    {
        public IConfiguration Configuration { get; }
        public CartRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;

        //Adding book to cart api
        public string AddToCart(CartModel cartModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));         
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddingCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", cartModel.UserId);
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity", cartModel.OrderQuantity);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return "Book Added succssfully to Cart";
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

        //Update order quantity
        public string UpdateCartQuantity(int cartId,int quantity)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateQuantity", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CartID", cartId);
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity", quantity);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Update is Unsuccessful";
                    }
                    else
                    {
                        return "Quantity Updated Successfully";
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
