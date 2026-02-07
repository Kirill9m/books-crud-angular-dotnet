using Microsoft.EntityFrameworkCore;
using BooksApi.Models;

namespace BooksApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Book> Books => Set<Book>();
}