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
    public class AuthorControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "MyLibraryDb")
        .Options;
        AppDbContext context;
        AuthorRepository authorRepository;
        AuthorController authorController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDataBase();
            authorRepository = new AuthorRepository(context);

            authorController = new AuthorController(authorRepository);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private void SeedDataBase()
        {
            var authors = new List<Author>
            {
                new Author { Id = 1, FullName = "Ibrahim ElKony" },
                new Author { Id = 2, FullName = "Nagiub Mahfouz"},
                new Author { Id = 3, FullName = "Taha hussen"}
            };
            context.Author.AddRange(authors);
            context.SaveChanges();
        }

        [Test, Order(1)]
        public void Author_GetAll_ReturnAllAuthors()
        {
            //Act
            var actionResult = authorController.GetAllAuthors();
            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Expected OkObjectResult");
            var authors = okResult.Value as List<Author>;
            Assert.That(authors.Count, Is.EqualTo(3));
        }

        [Test, Order(2)]
        public void Author_GetById_ReturnAuthor()
        {
            //Act
            var actionResult = authorController.GetAuthorById(2);
            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Expected OkObjectResult");
            var author = okResult.Value as Author;
            Assert.That(author.FullName, Is.EqualTo("Nagiub Mahfouz"));


        }
        [Test, Order(3)]
        public void Author_Add_ReturnNewAuthor()
        {
            //Arrange
            var authorVM = new PublicLibrary.ViewModels.AuthorVM()
            {
                FullName = "Ahmed Morad"
            };
            //Act
            var actionResult = authorController.AddAuthor(authorVM);
            //Assert
            var createdResult = actionResult as CreatedAtActionResult;
            Assert.That(createdResult, Is.Not.Null, "Expected CreatedAtActionResult");
            var createdAuthor = createdResult.Value as Author;
            Assert.That(createdAuthor.FullName, Is.EqualTo("Ahmed Morad"));
        }
   


        [Test, Order(4)]
        public void Author_Delete_ReturnNull()
        {
            //Act
            var actionResult = authorController.DeleteAuthor(1);
            //Assert
            var noContentResult = actionResult as NoContentResult;
            Assert.That(noContentResult, Is.Not.Null, "Expected NoContentResult");
            var getResult = authorController.GetAuthorById(1);
            var notFoundResult = getResult as NotFoundResult;
            Assert.That(notFoundResult, Is.Not.Null, "Expected NotFoundResult");
        }
    }
}