using BooksApi.Models;

namespace BooksApi.Services;
public interface IQuoteService
{
    Task<List<Quote>> GetQuotesAsync(Guid userId);
    Task<Quote> CreateQuoteAsync(Quote quote);
}