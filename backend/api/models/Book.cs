namespace BooksApi.Models;
public class Book
{
    public int Id { get; private set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}