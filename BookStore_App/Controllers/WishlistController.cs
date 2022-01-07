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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistManager wishlistManager;

        public WishlistController(IWishlistManager wishlistManager)
        {
            this.wishlistManager = wishlistManager;
            //this.logger = logger;
        }

        [HttpPost]
        [Route("addToWishlist")]
        public IActionResult AddWishlist([FromBody] WishlistModel wishlist) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.wishlistManager.AddWishlist(wishlist);
                //this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
                if (result.Equals("Book Wishlisted successfully"))
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

        //Delete wishlist details api
        [HttpDelete]
        [Route("deleteWishlist")]
        public IActionResult DeleteBookFromWishlist(int wishlistId) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.wishlistManager.DeleteBookFromWishlist(wishlistId);
                //this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
                if (result.Equals("Wishlist deleted successfully"))
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
    }
}
