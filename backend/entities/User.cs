namespace BooksApi.Models;
using System.Text.Json.Serialization;
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    [JsonIgnore]
    public List<Quote> Quotes { get; set; } = new();
    [JsonIgnore]
    public List<Book> Books { get; set; } = new();

    public User(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    public User()
    {
    }
}