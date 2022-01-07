using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository feedbackRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="repository">taking repository as parameter</param>
        public FeedbackManager(IFeedbackRepository feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        /// <summary>
        /// Register method for manager class
        /// </summary>
        /// <param name="userData">passing register model</param>
        /// <returns>Returns string if Registration is successful</returns>
        public string AddFeedback(FeedbackModel feedback)
        {
            try
            {
                return this.feedbackRepository.AddFeedback(feedback);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FeedbackModel> RetrieveOrderDetails(int bookId)
        {
            try
            {
                return this.feedbackRepository.RetrieveOrderDetails(bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
