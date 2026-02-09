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
            if (request.Username == null || request.Password == null)
            {
                return BadRequest("Username and password are required");
            }

            if (request.Username.Length < 3 || request.Password.Length < 8)
            {
                return BadRequest("Username must be at least 3 characters and password must be at least 8 characters");
            }

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
            if (request.Username == null || request.Password == null)
            {
                return BadRequest("Username and password are required");
            }

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
                var currentUser = await authService.GetCurrentUserAsync(token);
                if (currentUser == null)
                {
                    return Unauthorized();
                }
                var claimedUsername = HttpContext.User?.Identity?.Name;
                var usernameToReturn = string.IsNullOrEmpty(claimedUsername) ? currentUser.Username : claimedUsername;
                return Ok(new UserDtoResponse { Username = usernameToReturn });
            }
        }
    }
}