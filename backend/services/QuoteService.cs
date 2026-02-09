using BooksApi.Data;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Services;

public class QuoteService(AppDbContext context) : IQuoteService
{
    public async Task<List<Quote>> GetQuotesAsync(Guid userId)
    {
        return await context.Quotes
            .Where(q => q.UserId == userId)
            .ToListAsync();
    }

    public async Task<Quote> CreateQuoteAsync(Quote quote)
    {
        context.Quotes.Add(quote);
        await context.SaveChangesAsync();
        return quote;
    }
}
