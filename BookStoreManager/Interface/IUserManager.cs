using BookStoreModel;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        string Register(RegisterModel userData);
        string Login(LoginModel login);
        string JWTTokenGeneration(string email);
        string ResetPassword(ResetPasswordModel reset);
        string ForgotPassword(string email);
    }
}