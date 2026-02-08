namespace BooksApi.BookDto;
public class BookCreateDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
}

public class BookUpdateDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
}