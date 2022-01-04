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



    }
}
