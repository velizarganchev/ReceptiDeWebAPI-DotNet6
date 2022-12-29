using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.User;
using ReceptiDeWebAPI.Services.Users.Models;

namespace ReceptiDeWebAPI.Services.Users
{
    public interface IUserService
    {
        Task<ServiceResponse<int>> Register(UserRegisterLoginModel model);
        Task<ServiceResponse<string>> Login(UserRegisterLoginModel model);
        Task<User> UserExist(string email);
        User GetUser(string email);
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
        void SetRefreshToken(User user, RefreshToken refreshToken);
    }
}
