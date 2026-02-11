using BooksApi.Data;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Services;

public class BookService(AppDbContext context) : IBookService
{
    public async Task<List<Book>> GetBooksAsync(Guid userId)
    {
        return await context.Books.Where(b => b.UserId == userId).ToListAsync();
    }
    public async Task<Book?> GetBookByIdAsync(int id, Guid userId)
    {
        return await context.Books.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        context.Books.Add(book);
        await context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> UpdateBookAsync(int id, Book book)
    {
        var existingBook = await context.Books.FirstOrDefaultAsync(b => b.Id == id && b.UserId == book.UserId);
        if (existingBook == null)
        {
            return null;
        }

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;

        await context.SaveChangesAsync();
        return existingBook;
    }

    public async Task<bool> DeleteBookAsync(int id, Guid userId)
    {
        var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        if (book == null)
        {
            return false;
        }

        context.Books.Remove(book);
        await context.SaveChangesAsync();
        return true;
    }
}