using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            var registeredUserToken = await authService.RegisterAsync(request);

            if (registeredUserToken == null)
            {
                return BadRequest("User already exists");
            }

            return Ok(registeredUserToken);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if (token == null)
            {
                return BadRequest("Invalid username or password");
            }
            return Ok(token);
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDtoResponse?>> GetCurrentUser()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            {
                var user = await authService.GetCurrentUserAsync(token);
                return Ok(new UserDtoResponse { Username = user?.Username ?? string.Empty });
            }
        }
    }
}