using BooksApi.Models;
using System.Collections.Generic;

namespace BooksApi.Services;
public interface IQuoteService
{
    Task<List<Quote>> GetQuotesAsync(Guid userId);
    Task<Quote> CreateQuoteAsync(Quote quote);
    Task<List<Quote>> CreateQuotesAsync(IEnumerable<Quote> quotes);
    Task<Quote?> UpdateQuoteAsync(Quote quote, Guid userId);
    Task<bool> DeleteQuoteAsync(int quoteId, Guid userId);
}