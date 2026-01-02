using Microsoft.EntityFrameworkCore;

namespace MyBook.Data.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Author_Book> Author_Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Author_Book>()
            //    .HasOne(c => c.Book)
            //    .WithMany(b=>b.Author_Book).
            //    HasForeignKey(ab=>ab.BookId);
            modelBuilder.Entity<Book>().HasData(new Book
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
            }, new Book {

                Id = 2,
            
                Title = "To Kill a Mockingbird",
                Description = "A novel by Harper Lee published in 1960.",
                IsRead = false,
                DateRead = null,
                Rate = null,
                Genre = "Classic",
            
                CoverUrl = "https://example.com/tokillamockingbird.jpg",
                DateAdded = new DateTime(1980, 12, 12)
            }, new Book
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
            }, new Book
            {
                Id = 4,
                Title = "Pride and Prejudice",
                Description = "A romantic novel of manners written by Jane Austen.",
                IsRead = false,
                DateRead = null,
                Rate = null,
                Genre = "Romance",
              
                CoverUrl = "https://example.com/prideandprejudice.jpg",
                DateAdded = new DateTime(1965, 10, 30)

            }, new Book
            {
                 Id = 5,
                 Title = "The Hobbit",
                    Description = "A children's fantasy novel by English author J. R. R. Tolkien.",
                    IsRead = false,
                    DateRead = null,
                    Rate = null,
                    Genre = "Fantasy",
                   
                    CoverUrl = "https://example.com/thehobbit.jpg",
                    DateAdded = new DateTime(1999, 10, 30)

            }, new Book
            {
                Id = 6,
                Title = "Ben Elqasreen",
                Description = "A novel written by the famous egypt author Naguib Mahfouz.",
                IsRead = true,
                DateRead = null,
                Rate = null,
                Genre = "Classic",
             
                CoverUrl = "https://example.com/benelqasreen.jpg",
                DateAdded = new DateTime(1965, 10, 30)
            });
           
        }
    }
}
