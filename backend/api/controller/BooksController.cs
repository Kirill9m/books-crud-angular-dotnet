using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;

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
}