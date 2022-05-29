using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.User;
using ReceptiDeWebAPI.Services.Users.Models;

namespace ReceptiDeWebAPI.Services.Users
{
    public interface IUserService
    {
        public User Register(UserModel request);
        public User GetUser(UserModel request);
        public bool VerifyPassword(UserModel request, User user);
        public string CreateToken(User user);
        public RefreshToken GenerateRefreshToken();
        public void SetRefreshToken(User user, RefreshToken refreshToken);
    }
}
