using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_App.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        private readonly ILogger<UserController> logger;

        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        /// <summary>
        /// Performs the Registration of a new user
        /// </summary>
        /// <param name="userData">passing a register model data</param>
        /// <returns>This method returns the IAction Result according to Http</returns>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterModel userData) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = this.manager.Register(userData);
                this.logger.LogInformation("New user added successfully with name" + " & Name:" + userData.Name);
                if (result.Equals("Registration Successful"))
                {
                   
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Api for log in
        [HttpPost]
        [Route("Login")]
        public IActionResult LogIn([FromBody] LoginModel login)
        {
            try
            {
                var result = this.manager.Login(login);
                this.logger.LogInformation(login.Email + "Trying to log in");
                if (result.Equals("Login Successful"))
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string Name = database.StringGet("Name");
                    int userId = Convert.ToInt32(database.StringGet("User Id"));
                    long Number = Convert.ToInt64(database.StringGet("Number"));
                    RegisterModel data = new RegisterModel
                    {
                        Name = Name,
                        Email = login.Email,
                        UserId = userId,
                        Phone = Number
                    };
                    string token = this.manager.JWTTokenGeneration(login.Email);
                    return this.Ok(new { Status = true, Message = result, Data = data, Token = token });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Api for resetting password
        [HttpPut]
        [Route("reset")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel reset)
        {
            try
            {
                string result = this.manager.ResetPassword(reset);
                this.logger.LogInformation(reset.Email + "is trying to reset password");
                if (result.Equals("Password Updated Successfully"))
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
                this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        //Api for forgot password
        [HttpPost]
        [Route("forgot")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                string result = this.manager.ForgotPassword(email);
                this.logger.LogInformation(email + "trying to access forgot password");
                if (result.Equals("Email sent to user"))
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
                this.logger.LogWarning("Exception caught while adding new user" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
