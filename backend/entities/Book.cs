using System.Text.Json.Serialization;

namespace BooksApi.Models;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    [JsonIgnore]
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public Book()
    {

    }
}