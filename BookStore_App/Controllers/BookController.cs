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
    public class BookController : ControllerBase
    {
        private readonly IBookManager bookManager;

        public BookController(IBookManager bookManager)
        {
            this.bookManager = bookManager;
        }

        //Api for adding new book details
        [HttpPost]
        [Route("addBooks")]
        public IActionResult AddBook([FromBody] BookModel book) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.bookManager.AddBook(book);
                //this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
                if (result.Equals("Book Added succssfully"))
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
                //this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Update book details
        [HttpPut]
        [Route("updateBook")]
        public IActionResult UpdateBookDetails([FromBody] BookModel update)
        {
            try
            {
                string result = this.bookManager.UpdateBookDetails(update);
                //this.logger.LogInformation(reset.Email + "is trying to reset password");
                if (result.Equals("Details Updated Successfully"))
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
                //this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Get all book details by book id
        [HttpGet]
        [Route("getBookDetails")]
        public IActionResult RetrieveBookDetails(int bookId)
        {
            try
            {
                object result = this.bookManager.RetrieveBookDetails(bookId);
                //this.logger.LogInformation(reset.Email + "is trying to reset password");
                if (result != null)
                {
                    return this.Ok(new ResponseModel<object>() { Status = true, Message ="Retrieval of book details succssful", Data=result  });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message ="Bookid doesnt exists" });
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Delete book 
        [HttpDelete]
        [Route("deleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                string result = this.bookManager.DeleteBook(bookId);
                //this.logger.LogInformation(reset.Email + "is trying to reset password");
                if (result.Equals("Book details deleted successfully"))
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
                //this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Get all books
        [HttpGet]
        [Route("getAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.bookManager.GetAllBooks();
                //this.logger.LogInformation(reset.Email + "is trying to reset password");
                if (result != null)
                {
                    return this.Ok(new ResponseModel<object>() { Status = true, Message = "Retrieval all book details succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No book exists" });
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
