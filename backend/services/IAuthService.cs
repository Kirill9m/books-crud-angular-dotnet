using BooksApi.Models;

namespace BooksApi.Services;
public interface IAuthService
{
    Task<string?> RegisterAsync(UserDto request);
    Task<string?> LoginAsync(UserDto request);
    Task<User?> GetCurrentUserAsync(string token);
}