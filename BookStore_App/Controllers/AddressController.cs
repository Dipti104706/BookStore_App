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
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager addressManager;

        public AddressController(IAddressManager addressManager)
        {
            this.addressManager = addressManager;
        }

        [HttpPost]
        [Route("addAddress")]

        public IActionResult AddAddress([FromBody] AddressModel address) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.addressManager.AddAddress(address);
                //this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
                if (result.Equals("Address Added succssfully"))
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

        //update address api
        [HttpPut]
        [Route("updateAddress")]

        public IActionResult UpdateAddress([FromBody] AddressModel address) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.addressManager.UpdateAddress(address);
                //this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
                if (result.Equals("Address updated succssfully"))
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

        [HttpGet]
        [Route("getAllAddress")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                var result = this.addressManager.GetAllAddresses();
                //this.logger.LogInformation(reset.Email + "is trying to reset password");
                if (result != null)
                {
                    return this.Ok(new ResponseModel<object>() { Status = true, Message = "Retrieval all addresses succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Retrieval is unsucessful" });
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


        [HttpGet]
        [Route("getAddressbyUserid")]
        public IActionResult GetAddressesbyUserid(int userId)
        {
            try
            {
                var result = this.addressManager.GetAddressesbyUserid(userId);
                //this.logger.LogInformation(reset.Email + "is trying to reset password");
                if (result != null)
                {
                    return this.Ok(new ResponseModel<object>() { Status = true, Message = "Retrieval all addresses succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Userid not Exist" });
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
