using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
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
                    sqlCommand.ExecuteNonQuery();
                    return "Registration Successful";
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
                    login.Password = EncryptPassword(login.Password);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Email", login.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(login.Password));
                    sqlConnection.Open();
                    RegisterModel registerModel = new RegisterModel();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        registerModel.UserId = Convert.ToInt32(sqlData["UserId"]);
                        registerModel.Name = sqlData["Name"].ToString();
                        registerModel.Email = sqlData["Email"].ToString();
                        registerModel.Phone = Convert.ToInt32(sqlData["Phone"]);
                    }
                    if (registerModel != null)
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "Name", registerModel.Name);
                        database.StringSet(key: "User Id", registerModel.UserId.ToString());
                        return "Login Successful";
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

    }
}
