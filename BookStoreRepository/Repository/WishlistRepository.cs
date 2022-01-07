using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        public IConfiguration Configuration { get; }
        public WishlistRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;

        //Adding wishlist api
        public string AddWishlist(WishlistModel wishlist)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "spCreateWishlist";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", wishlist.UserId);
                    sqlCommand.Parameters.AddWithValue("@BookId", wishlist.BookId);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "Book already added to wishlist";
                    }
                    else
                    {
                        return "Book Wishlisted successfully";
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
