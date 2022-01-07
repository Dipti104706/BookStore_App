using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IFeedbackRepository
    {
        IConfiguration Configuration { get; }

        string AddFeedback(FeedbackModel feedback);
        List<FeedbackModel> RetrieveOrderDetails(int bookId);
    }
}