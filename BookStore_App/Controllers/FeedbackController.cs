using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_App.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager feedbackManager;

        public FeedbackController(IFeedbackManager feedbackManager)
        {
            this.feedbackManager = feedbackManager;
        }

        //Api for adding feedbacks
        [HttpPost]
        [Route("addFeedbacks")]
        public IActionResult AddFeedback([FromBody] FeedbackModel feedback) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.feedbackManager.AddFeedback(feedback);
                if (result.Equals("Feedback added successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Api for get all feedback by bookid
        [HttpGet]
        [Route("getFeedbacks")]
        public IActionResult RetrieveOrderDetails(int bookId) ////frombody attribute says value read from body of the request
        {
            try
            {
                var result = this.feedbackManager.RetrieveOrderDetails(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrival successful", Data=result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Retrival unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


    }
}
