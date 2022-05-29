using Microsoft.IdentityModel.Tokens;
using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.User;
using ReceptiDeWebAPI.Services.Users.Hasher;
using ReceptiDeWebAPI.Services.Users.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ReceptiDeWebAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHasher _hasher;

        public UserService(AppDbContext context, IConfiguration configuration, IHasher hasher)
        {
            _context = context;
            _configuration = configuration;
            _hasher = hasher;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }      
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        public User GetUser(UserModel request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == request.Username);
            return user;
        }

        public User Register(UserModel request)
        {
            _hasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool VerifyPassword(UserModel request, User user)
            => _hasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

        public void SetRefreshToken(User user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;

            _context.SaveChanges();
        }
    }
}
