using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicLibrary.Controllers;
using PublicLibrary.Data.Models;
using PublicLibrary.DTOS;
using PublicLibrary.Repository;
using PublicLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestMyLibrary.TestControllers
{
    public class publisherControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
             .UseInMemoryDatabase(databaseName: "MyLibraryDb")
             .Options;
        AppDbContext context;

        PublisherRepository publisherRepository;

        PublisherController publisherController;
        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDataBase();
            publisherRepository = new PublisherRepository(context);

            // Create a mock or stub logger for testing
            var logger = new LoggerFactory().CreateLogger<PublisherController>();
            publisherController = new PublisherController(publisherRepository, logger);
        }
        [Test,Order(1)]
        public void GetPublisherById_ShouldReturnCorrectPublisher()
        {
            // Arrange
            var publisherId = 2;
            // Act
            var actionResult = publisherController.GetPublisherById(publisherId);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Expected OkObjectResult");
            var publisher = okResult.Value as Publisher;
            Assert.That(publisher, Is.Not.Null, "Expected Publisher object");
            Assert.That(publisher.Name, Is.EqualTo("Publisher Two"));
        }
        [Test,Order(2)]
        public void GetAllPublishers_ShouldReturnAllPublishers()
        {
            // Arrange
            var queryParameters = new QueryParameters(); // Create an instance with default or desired values

            // Act
            var actionResult = publisherController.GetAllPublishers(queryParameters);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Expected OkObjectResult");
            var publishers = okResult.Value as List<Publisher>;
            Assert.That(publishers.Count, Is.EqualTo(3));
            Assert.That(publishers.Exists(p => p.Name == "Publisher One"));
        }
        [Test,Order(3)]
        public void AddPublisher_ShouldAddNewPublisher()
        {
            // Arrange
            var newPublisherVM = new PublisherVM
            {
                Name = "Publisher Four",
                Description = "This is the fourth publisher."
            };
            // Act
            var actionResult = publisherController.AddPublisher(newPublisherVM);
            // Assert
            var createdAtActionResult = actionResult as CreatedAtActionResult;
            Assert.That(createdAtActionResult, Is.Not.Null, "Expected CreatedAtActionResult");
            var createdPublisher = createdAtActionResult.Value as Publisher;
            Assert.That(createdPublisher, Is.Not.Null, "Expected Publisher object");
            Assert.That(createdPublisher.Name, Is.EqualTo("Publisher Four"));
        }

        [Test,Order(4)]
        public void AddPublisher_InvalidModel_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidPublisherVM = new PublisherVM
            {
                // Name is missing to simulate invalid model
                Description = "This publisher has no name."
            };
            publisherController.ModelState.AddModelError("Name", "The Name field is required.");
            // Act
            var actionResult = publisherController.AddPublisher(invalidPublisherVM);
            // Assert
            var badRequestResult = actionResult as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Expected BadRequestObjectResult");
        }

        [Test,Order(5)]
        public void RemovePublisher_ShouldDeletePublisher()
        {
            // Arrange
            var publisherIdToDelete = 1;
            // Act
            var actionResult = publisherController.DeletePublisher(publisherIdToDelete);
            // Assert
            var noContentResult = actionResult as NoContentResult;
            Assert.That(noContentResult, Is.Not.Null, "Expected NoContentResult");
            var deletedPublisher = context.Publisher.Find(publisherIdToDelete);
            Assert.That(deletedPublisher, Is.Null, "Publisher should be deleted from the database");
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
        public void SeedDataBase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher{ Id=1, Name="Publisher One", Description="that is one Book in the Library" },
                new Publisher{ Id=2, Name="Publisher Two", Description ="that is two BooK in my Libray" },
                new Publisher{ Id=3, Name="Publisher Three",Description = "that is three ................"  }
            };
            context.Publisher.AddRange(publishers);
            context.SaveChanges();
        }

    }
}
