using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using BooksApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace BooksApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class QuotesController(IQuoteService quoteService, IAuthService authService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Quote>>> GetQuotes()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }

        var quotes = await quoteService.GetQuotesAsync(currentUser.Id);
        return Ok(quotes);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Quote>> CreateQuote(QuoteCreateDto request)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }

        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest("Text krävs");
        }

        var quote = new Quote(request.Text, currentUser.Id);
        var createdQuote = await quoteService.CreateQuoteAsync(quote);
        return Ok(createdQuote);
    }
}