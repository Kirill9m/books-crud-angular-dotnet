using BooksApi.Models;

namespace BooksApi.Services;
public interface IBookService
{
    Task<List<Book>> GetBooksAsync(Guid userId);
    Task<Book?> GetBookByIdAsync(int id, Guid userId);
    Task<Book> CreateBookAsync(Book book);
    Task<Book?> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id, Guid userId);
}