using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        public IConfiguration Configuration { get; }
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;

        //Adding books api
        public string AddBook(BookModel book)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "Sp_AddBooks";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookName", book.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", book.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@Rating", book.Rating);
                    sqlCommand.Parameters.AddWithValue("@Reviewer", book.Reviewer);
                    sqlCommand.Parameters.AddWithValue("@Image", book.Image);
                    sqlCommand.Parameters.AddWithValue("@BookCount", book.BookCount);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return "Book Added succssfully";
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

        //Update book details api
        public string UpdateBookDetails(BookModel update)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_UpdateBooks";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookId", update.BookId);
                    sqlCommand.Parameters.AddWithValue("@BookName", update.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", update.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", update.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", update.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", update.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@Rating", update.Rating);
                    sqlCommand.Parameters.AddWithValue("@Reviewer", update.Reviewer);
                    sqlCommand.Parameters.AddWithValue("@Image", update.Image);
                    sqlCommand.Parameters.AddWithValue("@BookCount", update.BookCount);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Update book details Unsuccessful";
                    }
                    else
                    {
                        return "Details Updated Successfully";
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

        //Retrieve book details
        public object RetrieveBookDetails(int bookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "spGetBookDetails";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                    sqlConnection.Open();
                    BookModel bookModel = new BookModel();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            bookModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            bookModel.BookName = sqlData["BookName"].ToString();
                            bookModel.AuthorName = sqlData["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            bookModel.BookDescription = sqlData["BookDescription"].ToString();
                            bookModel.Rating = Convert.ToInt32(sqlData["Rating"]);
                            bookModel.Reviewer = Convert.ToInt32(sqlData["Reviewer"]);
                            bookModel.Image = sqlData["Image"].ToString();
                            bookModel.BookCount = Convert.ToInt32(sqlData["BookCount"]);
                        }
                        return bookModel;
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
