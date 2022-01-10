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
    public class CartController : ControllerBase
    {
        private readonly ICartManager cartManager;

        public CartController(ICartManager cartManager)
        {
            this.cartManager = cartManager;
        }

        //Api for Adding books to carts
        [HttpPost]
        [Route("addToCarts")]
        public IActionResult AddToCart([FromBody] CartModel cart) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.cartManager.AddToCart(cart);
                if (result.Equals("Book Added succssfully to Cart"))
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

        //Api for Update book quantity in the cart
        [HttpPut]
        [Route("updateBookQuantity")]
        public IActionResult UpdateCartQuantity(int cartId, int quantity)
        {
            try
            {
                string result = this.cartManager.UpdateCartQuantity(cartId, quantity);
                if (result.Equals("Quantity Updated Successfully"))
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

        //Api for getting cart details by userid
        [HttpGet]
        [Route("getCartDetails")]
        public IActionResult RetrieveCartDetails(int userId)
        {
            try
            {
                var result = this.cartManager.RetrieveCartDetails(userId);
                if (result != null)
                {
                    return this.Ok(new  { Status = true, Message = "Data retrieved successfully", Data=result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Get cart details is unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Delete cart details api
        [HttpDelete]
        [Route("deleteBook")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                string result = this.cartManager.DeleteCart(cartId);
                if (result.Equals("Cart details deleted successfully"))
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
    }
}
