using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;
using BooksApi.BookDto;
using BooksApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace BooksApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService, IAuthService authService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetBooks()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }
        var books = await bookService.GetBooksAsync(currentUser.Id);
        return Ok(books);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }
        var book = await bookService.GetBookByIdAsync(id, currentUser.Id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(BookCreateDto newBook)
    {
        if (newBook == null || string.IsNullOrWhiteSpace(newBook.Title) || string.IsNullOrWhiteSpace(newBook.Author))
        {
            return BadRequest();
        }

        var book = new Book(newBook.Title, newBook.Author);
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }
        book.UserId = currentUser.Id;
        await bookService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> UpdateBook(int id, BookUpdateDto updatedBook)
    {
        if (updatedBook == null || string.IsNullOrWhiteSpace(updatedBook.Title) || string.IsNullOrWhiteSpace(updatedBook.Author))
        {
            return BadRequest();
        }

        var newBook = new Book(updatedBook.Title, updatedBook.Author);

        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }
        newBook.UserId = currentUser.Id;

        var existingBook = await bookService.UpdateBookAsync(id, newBook);

        if (existingBook == null)
        {
            return NotFound();
        }
        return Ok(existingBook);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var currentUser = await authService.GetCurrentUserAsync(token);
        if (currentUser == null)
        {
            return Unauthorized("Användaren är inte auktoriserad");
        }
        var success = await bookService.DeleteBookAsync(id, currentUser.Id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}