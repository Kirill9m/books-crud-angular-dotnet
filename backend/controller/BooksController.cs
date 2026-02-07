using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksApi.Models;
using BooksApi.BookDto;
using BooksApi.Data;

namespace BooksApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly AppDbContext db;

    public BooksController(AppDbContext db)
    {
        this.db = db;
    }
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetBooks()
    {
        var books = await db.Books.ToListAsync();
        return Ok(books);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }
    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(BookCreateDto newBook)
    {
        if (newBook == null || string.IsNullOrWhiteSpace(newBook.Title) || string.IsNullOrWhiteSpace(newBook.Author))
        {
            return BadRequest();
        }

        var book = new Book(newBook.Title, newBook.Author);
        db.Books.Add(book);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> UpdateBook(int id, BookUpdateDto updatedBook)
    {
        var existingBook = await db.Books.FindAsync(id);
        if (existingBook == null)
        {
            return NotFound();
        }

        if (updatedBook == null || string.IsNullOrWhiteSpace(updatedBook.Title) || string.IsNullOrWhiteSpace(updatedBook.Author))
        {
            return BadRequest();
        }

        existingBook.Title = updatedBook.Title;
        existingBook.Author = updatedBook.Author;
        await db.SaveChangesAsync();

        return Ok(existingBook);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return NoContent();
    }
}