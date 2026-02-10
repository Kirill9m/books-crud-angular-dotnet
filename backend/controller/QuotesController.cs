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
    public async Task<ActionResult<List<QuouteResponseDto>>> GetQuotes()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }

        var quotes = await quoteService.GetQuotesAsync(currentUser.Id);
        return Ok(quotes.Select(q => new QuouteResponseDto { Id = q.Id, Text = q.Text }).ToList());
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<QuouteResponseDto>> CreateQuote(QuoteCreateDto request)
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
        return Ok(new QuouteResponseDto { Id = createdQuote.Id, Text = createdQuote.Text });
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<QuouteResponseDto>> UpdateQuote(int id, QuoteUpdateDto request)
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
        var quoteToUpdate = new Quote(request.Text, currentUser.Id) { Id = id };

        var updatedQuote = await quoteService.UpdateQuoteAsync(quoteToUpdate, currentUser.Id);
        if (updatedQuote == null)
        {
            return NotFound("Citatet hittades inte");
        }
        return Ok(new QuouteResponseDto { Id = updatedQuote.Id, Text = updatedQuote.Text });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuote(int id)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }
        var success = await quoteService.DeleteQuoteAsync(id, currentUser.Id);
        if (!success)
        {
            return NotFound("Citatet hittades inte");
        }
        return NoContent();
    }
}