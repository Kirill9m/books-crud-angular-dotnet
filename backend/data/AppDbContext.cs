using Microsoft.EntityFrameworkCore;
using BooksApi.Models;
using api.Migrations;

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

        modelBuilder.Entity<Quote>()
            .HasOne(q => q.User)
            .WithMany(u => u.Quotes)
            .HasForeignKey(q => q.UserId)
            .IsRequired();
    }
    public DbSet<Book> Books => Set<Book>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Quote> Quotes => Set<Quote>();

}