namespace BooksApi.Models;

public class QuoteCreateDto
{
    public string? Text { get; set; }
}

public class QuoteResponseDto
{
    public int Id { get; set; }
    public string? Text { get; set; }
}

public class QuoteUpdateDto
{
    public string? Text { get; set; }
}