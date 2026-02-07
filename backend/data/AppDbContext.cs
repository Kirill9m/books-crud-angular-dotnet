using Microsoft.EntityFrameworkCore;
using BooksApi.Models;

namespace BooksApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>().HasData(
            new Book("The Great Gatsby", "F. Scott Fitzgerald") { Id = -1 },
            new Book("To Kill a Mockingbird", "Harper Lee") { Id = -2 },
            new Book("1984", "George Orwell") { Id = -3 }
        );
    }
    public DbSet<Book> Books => Set<Book>();
}