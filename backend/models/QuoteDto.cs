namespace BooksApi.Models;

public class QuoteCreateDto
{
    public string? Text { get; set; }
}

public class QuouteResponseDto
{
    public int Id { get; set; }
    public string? Text { get; set; }
}

public class QuoteUpdateDto
{
    public int Id { get; set; }
    public string? Text { get; set; }
}