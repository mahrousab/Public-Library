using Microsoft.EntityFrameworkCore;
using PublicLibrary.Data.Models;
using PublicLibrary.DTOS;
using PublicLibrary.Repository;
using PublicLibrary.ViewModels;

namespace TestMyLibrary.TestServices
{
    public class PublisherServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "MyLibraryDb")
            .Options;
         AppDbContext context;

        PublisherRepository publisherRepository;
        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDataBase();
            publisherRepository = new PublisherRepository(context);
        }

        [Test,Order(5)]

       public void GetPublisherById_ShouldReturnCorrectPublisher()
        {
            // Arrange
            var publisherId = 2;
            // Act
            var publisher = publisherRepository.GetPublisherById(publisherId);
            // Assert
            Assert.That(publisher, Is.Not.Null);
            Assert.That(publisher.Name, Is.EqualTo("Publisher Two"));
        }
        [Test,Order(6)]
        public void GetAllPublishers_ShouldReturnAllPublishers()
        { 
            // Arrange
            var queryParameters = new QueryParameters(); // Assuming default constructor
            // Act
            var publishers = publisherRepository.GetAllPublishers(queryParameters).ToList();
            // Assert
            Assert.That(publishers.Count, Is.EqualTo(3));
            // Additional assertions can be made to verify the contents of the list
            Assert.That(publishers.Any(p => p.Name == "Publisher One"));
        }


        [Test,Order(7)]
        public void AddPublisher_ShouldAddNewPublisher()
        {
            // Arrange
            var newPublisherVM = new PublisherVM
            {
                Name = "Publisher Four",
                Description = "This is the fourth publisher."
            };
            // Act
            publisherRepository.AddPublisher(newPublisherVM);
            var addedPublisher = publisherRepository.GetAllPublishers(new QueryParameters())
                .FirstOrDefault(p => p.Name == "Publisher Four");
            // Assert
            Assert.That(addedPublisher, Is.Not.Null);
            Assert.That(addedPublisher.Name, Is.EqualTo("Publisher Four"));
        }

        [Test,Order(8)]
      public void DeletePublisher_ShouldRemovePublisher()
        {
            // Arrange
            var publisherIdToDelete = 1;
            // Act
            publisherRepository.DeletePublisher(publisherIdToDelete);
            var deletedPublisher = publisherRepository.GetPublisherById(publisherIdToDelete);
            // Assert
            Assert.That(deletedPublisher, Is.Null);
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
