using BooksApi.Models;

namespace BooksApi.Services;
public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto request);
    Task<String?> LoginAsync(UserDto request);
}