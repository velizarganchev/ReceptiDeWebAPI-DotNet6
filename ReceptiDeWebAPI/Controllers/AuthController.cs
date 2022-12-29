using Microsoft.AspNetCore.Mvc;
using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.User;
using ReceptiDeWebAPI.Services;
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
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterLoginModel request)
        {
            var response =
                await _userService.Register(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserRegisterLoginModel request)
        {
            var response = await _userService.Login(request);

            if (!response.Success)
            {
                return BadRequest("User not found.");
            }
            var refreshToken = _userService.GenerateRefreshToken();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            _userService.SetRefreshToken(_userService.GetUser(request.Email), refreshToken);

            return Ok(response.Data);
        }
    }
}
