using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksApi.Models;
using BooksApi.BookDto;
using BooksApi.Data;
using BooksApi.Services;

namespace BooksApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetBooks()
    {
        var books = await bookService.GetBooksAsync();
        return Ok(books);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        var book = await bookService.GetBookByIdAsync(id);
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
        await bookService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> UpdateBook(int id, BookUpdateDto updatedBook)
    {
        if (updatedBook == null || string.IsNullOrWhiteSpace(updatedBook.Title) || string.IsNullOrWhiteSpace(updatedBook.Author))
        {
            return BadRequest();
        }

        var newBook = new Book(updatedBook.Title, updatedBook.Author);

        var existingBook = await bookService.UpdateBookAsync(id, newBook);

        if (existingBook == null)
        {
            return NotFound();
        }
        return Ok(existingBook);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await bookService.DeleteBookAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}