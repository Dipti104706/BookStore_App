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

        //Delete wishlist
        public string DeleteBookFromWishlist(int wishlistId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {

                using (sqlConnection)
                {
                    string storeprocedure = "spDeleteWishlist";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@WishlistId", wishlistId);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return "Wishlist deleted successfully";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Get wishlist details
        public List<WishlistModel> RetrieveWishlist(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "ShowWishlistbyUserid";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    List<WishlistModel> wishlist = new List<WishlistModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            WishlistModel wish = new WishlistModel();
                            BookModel bookModel = new BookModel();
                            bookModel.BookName = sqlData["BookName"].ToString();
                            bookModel.AuthorName = sqlData["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            bookModel.Image = sqlData["Image"].ToString();
                            wish.UserId = Convert.ToInt32(sqlData["UserId"]);
                            wish.BookId = Convert.ToInt32(sqlData["BookId"]);
                            wish.Book = bookModel;
                            wishlist.Add(wish);
                        }
                        return wishlist;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
