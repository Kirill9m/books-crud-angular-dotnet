using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UserTableUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Title", "UserId" },
                values: new object[,]
                {
                    { -3, "George Orwell", "1984", new Guid("00000000-0000-0000-0000-000000000000") },
                    { -2, "Harper Lee", "To Kill a Mockingbird", new Guid("00000000-0000-0000-0000-000000000000") },
                    { -1, "F. Scott Fitzgerald", "The Great Gatsby", new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }
    }
}
