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
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        //Api for adding orders 
        [HttpPost]
        [Route("addOrders")]
        public IActionResult AddOrder([FromBody] OrderModel order) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.orderManager.AddOrder(order);
                if (result.Equals("Ordered successfully"))
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

        //Api for getting all orders by userid
        [HttpGet]
        [Route("GetOrders")]
        public IActionResult RetrieveOrderDetails(int userId) ////frombody attribute says value read from body of the request
        {
            try
            {
                var result = this.orderManager.RetrieveOrderDetails(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieved successfully",Data=result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Retrieval unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
