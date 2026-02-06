var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add CORS for development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

// Serve static files (Angular app)
app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseHttpsRedirection();

var books = new[]
{
    new Book("The Great Gatsby", "F. Scott Fitzgerald"),
    new Book("To Kill a Mockingbird", "Harper Lee"),
    new Book("1984", "George Orwell"),
};

app.MapGet("/books", () => books)
   .WithName("GetBooks");
// Fallback to index.html for Angular routing
app.MapFallbackToFile("index.html");

app.Run();
public record Book(string Title, string Author);