﻿using BookStoreManager.Interface;
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

        [HttpPost]
        [Route("addToCarts")]
        public IActionResult AddToCart([FromBody] CartModel cart) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.cartManager.AddToCart(cart);
                //this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
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
                //this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

       
    }
}
