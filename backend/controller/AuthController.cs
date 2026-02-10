using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IQuoteService quoteService) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            if (request.Username == null || request.Password == null)
            {
                return BadRequest("Användarnamn och lösenord krävs");
            }

            if (request.Username.Length < 3 || request.Password.Length < 8)
            {
                return BadRequest("Användarnamn måste vara minst 3 tecken och lösenord måste vara minst 8 tecken");
            }

            var (registeredUserToken, registeredUser) = await authService.RegisterAsync(request);

            if (registeredUserToken == null)
            {
                return BadRequest("Användaren finns redan");
            }

            if (registeredUser == null)
            {
                return StatusCode(500, "Ett oväntat fel inträffade vid skapande av användare.");
            }
            var initialQuotes = new List<Quote>
            {
                new Quote("Välkommen till din personliga citat-lista!", registeredUser.Id),
                new Quote("Här kommer din första citat.", registeredUser.Id),
                new Quote("Lägg till dina egna citat för att komma igång.", registeredUser.Id),
                new Quote("Tips: du kan redigera eller ta bort citat senare.", registeredUser.Id),
                new Quote("Ha en fantastisk dag!", registeredUser.Id)
            };
            await quoteService.CreateQuotesAsync(initialQuotes);

            return Ok(registeredUserToken);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (request.Username == null || request.Password == null)
            {
                return BadRequest("Användarnamn och lösenord krävs");
            }

            var token = await authService.LoginAsync(request);
            if (token == null)
            {
                return BadRequest("Ogiltigt användarnamn eller lösenord");
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
                    return Unauthorized("Användaren är inte auktoriserad");
                }
                var claimedUsername = HttpContext.User?.Identity?.Name;
                var usernameToReturn = string.IsNullOrEmpty(claimedUsername) ? currentUser.Username : claimedUsername;
                return Ok(new UserDtoResponse { Username = usernameToReturn });
            }
        }
    }
}