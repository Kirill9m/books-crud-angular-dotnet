using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;
using BooksApi.BookDto;

namespace BooksApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private static readonly List<Book> books =
    [
        new Book("The Great Gatsby", "F. Scott Fitzgerald"),
        new Book("To Kill a Mockingbird", "Harper Lee"),
        new Book("1984", "George Orwell"),
    ];
    [HttpGet]
    public ActionResult<List<Book>> GetBooks()
    {
        return Ok(books);
    }
    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }
    [HttpPost]
    public ActionResult<Book> AddBook(BookCreateDto newBook)
    {
        if (newBook == null || string.IsNullOrWhiteSpace(newBook.Title) || string.IsNullOrWhiteSpace(newBook.Author))
        {
            return BadRequest();
        }

        var book = new Book(newBook.Title, newBook.Author);
        books.Add(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, BookUpdateDto updatedBook)
    {
        var existingBook = books.FirstOrDefault(b => b.Id == id);
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

        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        books.Remove(book);
        return NoContent();
    }
}