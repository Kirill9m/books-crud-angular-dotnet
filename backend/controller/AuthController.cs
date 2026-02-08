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
        public async Task<ActionResult<UserDtoResponse>> Register(UserDto request)
        {
            var registeredUser = await authService.RegisterAsync(request);

            if (registeredUser == null)
            {
                return BadRequest("User already exists");
            }

            return Ok(new UserDtoResponse { Username = registeredUser.Username });
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
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }
    }
}