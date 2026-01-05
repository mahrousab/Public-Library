using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicLibrary.Controllers;
using PublicLibrary.Data.Models;
using PublicLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestMyLibrary.TestControllers
{
    public class BookControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
          .UseInMemoryDatabase(databaseName: "MyLibraryDb")
          .Options;
        AppDbContext context;

        BookRepository bookRepository;

        BookController bookController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDataBase();
            bookRepository = new BookRepository(context);
            bookController = new BookController(bookRepository);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
        private void SeedDataBase()
        {
            var Books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Description = "A novel written by American author F. Scott Fitzgerald.",
                    IsRead = true,
                    DateRead = new DateTime(2020, 5, 1),
                    Rate = 5,
                    Genre = "Classic",
                    CoverUrl = "https://example.com/greatgatsby.jpg",
                    DateAdded = new DateTime(1999, 10, 30)
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Description = "A novel by Harper Lee published in 1960.",
                    IsRead = false,
                    DateRead = null,
                    Rate = null,
                    Genre = "Classic",
                    CoverUrl = "https://example.com/tokillamockingbird.jpg",
                    DateAdded = new DateTime(1980, 12, 12)
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Description = "A dystopian social science fiction novel and cautionary tale by the English writer George Orwell.",
                    IsRead = false,
                    DateRead = null,
                    Rate = null,
                    Genre = "Dystopian",
                    CoverUrl = "https://example.com/1984.jpg",
                    DateAdded = new DateTime(1977, 10, 30)
                }
            };
            context.Books.AddRange(Books);
            context.SaveChanges();
        }

        [Test, Order(1)]
        public void GetBookById_ShouldReturnCorrectBook()
        {
            // Arrange
            var bookId = 2;
            // Act
            var actionResult = bookController.GetBook(bookId);
            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Expected OkObjectResult");
            var book = okResult.Value as Book;
            Assert.That(book, Is.Not.Null, "Expected Book object");
            Assert.That(book.Title, Is.EqualTo("To Kill a Mockingbird"));
        }

        [Test, Order(2)]

        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Act
            var actionResult = bookController.GetAllBooks();
            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Expected OkObjectResult");
            var books = okResult.Value as List<Book>;
            Assert.That(books.Count, Is.EqualTo(3));
            Assert.That(books.Exists(b => b.Title == "1984"));
        }

        [Test, Order(3)]
        public void AddBook_ShouldAddNewBook()
        {
            // Arrange
            var newBookVM = new PublicLibrary.ViewModels.BookVM
            {
                Title = "Brave New World",
                Description = "A dystopian novel by Aldous Huxley.",
                IsRead = false,
                DateRead = null,
                Rate = null,
                Genre = "Dystopian",
                CoverUrl = "https://example.com/bravenewworld.jpg"
            };
            // Act
            var actionResult = bookController.AddBook(newBookVM);
            // Assert
            var createdResult = actionResult as CreatedAtActionResult;
            Assert.That(createdResult, Is.Not.Null, "Expected CreatedAtActionResult");
            var addedBook = createdResult.Value as Book;
            Assert.That(addedBook, Is.Not.Null);
            Assert.That(addedBook.Title, Is.EqualTo("Brave New World"));
        }

        [Test, Order(4)]
        public void DeleteBook_ShouldRemoveBook()
        {
            // Arrange
            var bookIdToDelete = 1;
            // Act
            var actionResult = bookController.DeleteBook(bookIdToDelete);
            // Assert
            var noContentResult = actionResult as NoContentResult;
            Assert.That(noContentResult, Is.Not.Null, "Expected NoContentResult");
            var deletedBook = context.Books.FirstOrDefault(b => b.Id == bookIdToDelete);
            Assert.That(deletedBook, Is.Null);
        }
    }
}