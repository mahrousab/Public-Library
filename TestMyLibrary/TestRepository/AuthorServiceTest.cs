using Microsoft.EntityFrameworkCore;
using MyBook.Data.Models;
using MyBook.IRepositories;
using MyBook.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestMyLibrary.TestServices
{
    public class AuthorServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
         .UseInMemoryDatabase(databaseName: "MyLibraryDb")
         .Options;
        AppDbContext context;
        AuthorRepository authorRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDataBase();
            authorRepository = new AuthorRepository(context);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
        [Test, Order(1)]
        public void Author_GetAll_ReturnAllAuthors()
        {
            //Act
            var authors = authorRepository.GetAllAuthors();
            //Assert
            Assert.That(authors.Count, Is.EqualTo(3));
        }

        [Test, Order(2)]

        public void Author_GetById_ReturnAuthor()
        {
            //Act
            var author = authorRepository.GetById(2);
            //Assert
            Assert.That(author.FullName, Is.EqualTo("Nagiub Mahfouz"));
        }

        [Test, Order(3)]
        public void Author_Add_ReturnNewAuthor()
        {
            //Arrange
            var authorVM = new MyBook.ViewModels.AuthorVM()
            {
                FullName = "Ahmed Morad"
            };
            //Act
            authorRepository.AddAuthor(authorVM);
            var addedAuthor = authorRepository.GetById(4);
            //Assert
            Assert.That(addedAuthor.FullName, Is.EqualTo("Ahmed Samir"));
        }

        [Test, Order(4)]
        public void Author_Delete_ReturnNull()
        {
            //Act
            authorRepository.DeleteAuthor(1);
            var deletedAuthor = authorRepository.GetById(1);
            //Assert
            Assert.That(deletedAuthor, Is.Null);
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
    }
}
