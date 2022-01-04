using BookStoreModel;
using BookStoreRepository.Interface;
using Experimental.System.Messaging;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        public IConfiguration Configuration { get; }
        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;

        //Register method 
        public string Register(RegisterModel user)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "Sp_AddCustomers";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    user.Password = EncryptPassword(user.Password);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Name", user.Name);
                    sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                    sqlCommand.Parameters.AddWithValue("@Phone", user.Phone);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Email already exists";
                    }
                    else
                    {
                        return "Registration Successful";
                    }                   
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Password encryption
        public string EncryptPassword(string password)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] bytesRepresentation = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytesRepresentation);
        }

        //Login
        public string Login(LoginModel login)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "spGetAllDetails";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Email", login.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(login.Password));
                    sqlConnection.Open();
                    RegisterModel registerModel = new RegisterModel();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.Read())
                    {
                        registerModel.UserId = Convert.ToInt32(sqlData["UserId"]);
                        registerModel.Name = sqlData["Name"].ToString();
                        registerModel.Email = sqlData["Email"].ToString();
                        registerModel.Phone = Convert.ToInt64(sqlData["Phone"]);
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "Name", registerModel.Name);
                        database.StringSet(key: "User Id", registerModel.UserId.ToString());
                        database.StringSet(key: "Number", registerModel.Phone.ToString());
                        return "Login Successful";
                    }
                    else
                    {
                        return "Login Unsuccessful";
                    }                   
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Reset password
        public string ResetPassword(ResetPasswordModel reset)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {

                using (sqlConnection)
                {
                    string storeprocedure = "spResetPS";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Email", reset.Email);
                    sqlCommand.Parameters.AddWithValue("@NewPs", EncryptPassword(reset.NewPassword));
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        return "Reset Password Unsuccessful";
                    }
                    else
                    {
                        return "Password Updated Successfully";
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Forget password
        public string ForgotPassword(string email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "spForgotPS";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Email", email);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 1)
                    {
                        this.SMTPmail(email);
                        return "Email sent to user";
                    }
                    else
                    {
                        return "Email does not Exists";
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //Token generation
        public string JWTTokenGeneration(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key); ////create new instance
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor ////placeholders to store all atrribute to generate token
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        //Smtp
        public void SMTPmail(string email)
        {
            MailMessage mailId = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"); ////allow App to sent email using SMTP 
            mailId.From = new MailAddress(this.Configuration["Credentials:Email"]); ////contain mail id from where maill will send
            mailId.To.Add(email); //// the user mail to which maill will be send
            mailId.Subject = "Regarding forgot password issue";
            this.SendMSMQ();
            mailId.Body = this.ReceiveMSMQ();
            SmtpServer.Port = 587; ////Port no 
            SmtpServer.Credentials = new System.Net.NetworkCredential(this.Configuration["Credentials:Email"], this.Configuration["Credentials:Password"]);
            SmtpServer.EnableSsl = true; ////specify smtpserver use ssl or not, default setting is false
            SmtpServer.Send(mailId);
        }

        public void SendMSMQ()
        {
            MessageQueue msgQueue;
            if (MessageQueue.Exists(@".\Private$\book"))
            {
                msgQueue = new MessageQueue(@".\Private$\book");
            }
            else
            {
                msgQueue = MessageQueue.Create(@".\Private$\book");
            }
            Message message = new Message();
            var formatter = new BinaryMessageFormatter();
            message.Formatter = formatter;
            message.Body = "This mail is to reset password";
            msgQueue.Label = "MailBody";
            msgQueue.Send(message);
        }

        public string ReceiveMSMQ()
        {
            var receivequeue = new MessageQueue(@".\Private$\book");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();
            return receivemsg.Body.ToString();
        }
    }
}
