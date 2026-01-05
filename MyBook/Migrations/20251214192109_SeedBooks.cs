using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PublicLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CoverUrl", "DateAdded", "DateRead", "Description", "Genre", "IsRead", "Rate", "Title" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", "https://example.com/greatgatsby.jpg", new DateTime(2025, 12, 14, 21, 21, 8, 564, DateTimeKind.Local).AddTicks(131), new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A novel written by American author F. Scott Fitzgerald.", "Classic", true, 5, "The Great Gatsby" },
                    { 2, "Harper Lee", "https://example.com/tokillamockingbird.jpg", new DateTime(2025, 12, 14, 21, 21, 8, 567, DateTimeKind.Local).AddTicks(5261), null, "A novel by Harper Lee published in 1960.", "Classic", false, null, "To Kill a Mockingbird" },
                    { 3, "George Orwell", "https://example.com/1984.jpg", new DateTime(2025, 12, 14, 21, 21, 8, 567, DateTimeKind.Local).AddTicks(5291), null, "A dystopian social science fiction novel and cautionary tale by the English writer George Orwell.", "Dystopian", false, null, "1984" },
                    { 4, "Jane Austen", "https://example.com/prideandprejudice.jpg", new DateTime(2025, 12, 14, 21, 21, 8, 567, DateTimeKind.Local).AddTicks(5295), null, "A romantic novel of manners written by Jane Austen.", "Romance", false, null, "Pride and Prejudice" },
                    { 5, "J. R. R. Tolkien", "https://example.com/thehobbit.jpg", new DateTime(2025, 12, 14, 21, 21, 8, 567, DateTimeKind.Local).AddTicks(5298), null, "A children's fantasy novel by English author J. R. R. Tolkien.", "Fantasy", false, null, "The Hobbit" },
                    { 6, "Naguib Mahfouz", "https://example.com/benelqasreen.jpg", new DateTime(2025, 12, 14, 21, 21, 8, 567, DateTimeKind.Local).AddTicks(5301), null, "A novel written by the famous egypt author Naguib Mahfouz.", "Classic", true, null, "Ben Elqasreen" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
