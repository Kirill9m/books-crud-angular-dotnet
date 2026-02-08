using BooksApi.Models;

namespace BooksApi.Services;
public interface IAuthService
{
    Task<String?> RegisterAsync(UserDto request);
    Task<String?> LoginAsync(UserDto request);
    Task<User?> GetCurrentUserAsync(String token);
}