using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Interface
{
    public interface IFeedbackRepository
    {
        IConfiguration Configuration { get; }

        string AddFeedback(FeedbackModel feedback);
    }
}