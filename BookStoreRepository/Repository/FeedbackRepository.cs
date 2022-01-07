using BookStoreModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public IConfiguration Configuration { get; }
        public FeedbackRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;

        //Adding book to cart api
        public string AddFeedback(FeedbackModel feedback)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SpAddFeedback", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", feedback.UserId);
                    sqlCommand.Parameters.AddWithValue("@BookId", feedback.BookId);
                    sqlCommand.Parameters.AddWithValue("@Comments", feedback.Comments);
                    sqlCommand.Parameters.AddWithValue("@Ratings", feedback.Ratings);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "Already given Feedback for this book";
                    }
                    else
                    {
                        return "Feedback added successfully";
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
