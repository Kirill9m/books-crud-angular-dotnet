namespace BooksApi.Models;
using System.Text.Json.Serialization;

public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; }

    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }

    public Quote(string text, Guid userId)
    {
        Text = text;
        UserId = userId;
    }

    public Quote()
    {
    }
}