using BooksApi.Models;

namespace BooksApi.Services;
public interface IBookService
{
    Task<List<Book>> GetBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(Book book);
    Task<Book?> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);
}