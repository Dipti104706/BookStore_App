using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IFeedbackManager
    {
        string AddFeedback(FeedbackModel feedback);
        List<FeedbackModel> RetrieveOrderDetails(int bookId);
    }
}