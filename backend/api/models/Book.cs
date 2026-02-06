namespace BooksApi.Models;
public class Book
{
    private static int _nextId = 1;
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(string title, string author)
    {
        Id = _nextId++;
        Title = title;
        Author = author;
    }
}