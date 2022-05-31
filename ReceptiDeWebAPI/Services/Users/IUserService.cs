using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.User;
using ReceptiDeWebAPI.Services.Users.Models;

namespace ReceptiDeWebAPI.Services.Users
{
    public interface IUserService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExist(string username);
        User GetUser(UserRegisterModel request);
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
        void SetRefreshToken(User user, RefreshToken refreshToken);
    }
}
