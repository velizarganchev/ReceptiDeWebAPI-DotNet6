using Microsoft.AspNetCore.Mvc;
using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.User;
using ReceptiDeWebAPI.Services.Users;

namespace ReceptiDeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserModel request)
        {
            var user = _userService.Register(request);

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserModel request)
        {
            var user = _userService.GetUser(request);

            if (user.Username != request.Username)
            {
                return BadRequest("User not found.");
            }

            if (!_userService.VerifyPassword(request, user))
            {
                return BadRequest("Wrong password.");
            }

            string token = _userService.CreateToken(user);

            var refreshToken = _userService.GenerateRefreshToken();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            _userService.SetRefreshToken(user, refreshToken);

            return Ok(token);
        }
    }
}
