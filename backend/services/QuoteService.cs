using BooksApi.Data;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

    public async Task<List<Quote>> CreateQuotesAsync(IEnumerable<Quote> quotes)
    {
        context.Quotes.AddRange(quotes);
        await context.SaveChangesAsync();
        return quotes.ToList();
    }

    public async Task<Quote?> UpdateQuoteAsync(Quote quote, Guid userId)
    {
        var existingQuote = await context.Quotes.FindAsync(quote.Id);
        if (existingQuote == null)
        {
            return null;
        }

        if (existingQuote.UserId != userId)
        {
            return null;
        }
        existingQuote.Text = quote.Text;

        await context.SaveChangesAsync();
        return existingQuote;
    }

    public async Task<bool> DeleteQuoteAsync(int quoteId, Guid userId)
    {
        var quote = await context.Quotes.FindAsync(quoteId);
        if (quote == null)
        {
            return false;
        }

        if (quote.UserId != userId)
        {
            return false;
        }

        context.Quotes.Remove(quote);
        await context.SaveChangesAsync();
        return true;
    }
}
