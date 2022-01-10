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
        
        //Api for adding wishlist
        [HttpPost]
        [Route("addToWishlist")]
        public IActionResult AddWishlist([FromBody] WishlistModel wishlist) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.wishlistManager.AddWishlist(wishlist);
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
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Api for get wishlist details
        [HttpGet]
        [Route("getWishlistDetails")]
        public IActionResult RetrieveWishlist(int userId) ////frombody attribute says value read from body of the request
        {
            try
            {
                var result = this.wishlistManager.RetrieveWishlist(userId);
                if (result != null)
                {

                    return this.Ok(new { Status = true, Message = "Retrieve successfully", Data= result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrieval unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
