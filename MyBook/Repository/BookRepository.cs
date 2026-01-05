using PublicLibrary.Data.Models;
using PublicLibrary.IRepositories;
using PublicLibrary.ViewModels;

namespace PublicLibrary.Repository
{
    public class BookRepository :IBookRepository
    {
        private readonly  AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
                _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
        public BookWithAuthorVM GetBookById (int bookid)
        {
            var bookwithauthor =_context.Books.Where(b => b.Id == bookid)
                .Select(book => new BookWithAuthorVM()
                {
                    Title = book.Title,
                    Description = book.Description,
                    IsRead = book.IsRead,
                    DateRead = book.IsRead ? book.DateRead.Value : null,
                    Rate = book.IsRead ? book.Rate.Value : null,
                    Genre = book.Genre,
                    CoverUrl = book.CoverUrl,
                    PublisherName = book.Publisher.Name,
                    AuthorNames = book.Author_Book != null
    ? new List<string> { book.Author_Book.author.FullName }
    : new List<string>()
                }).FirstOrDefault();
            return bookwithauthor;
        }
        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
               
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();
           foreach (var authorId in book.AuthorIds)
            {
                var _author_book = new Author_Book()
                {
                    BookId = _book.Id,
                    AuthorId = authorId
                };
                _context.Author_Book.Add(_author_book);
            }
            _context.SaveChanges();
        }

        public void UpdateBookById(int id, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.Genre = book.Genre;
              
                _book.CoverUrl = book.CoverUrl;
                _context.SaveChanges();
            }
        }

        public void DeleteBookById(int id) {
            var _book = _context.Books.FirstOrDefault(b => b.Id == id);
                if (_book != null)
                {
                    _context.Books.Remove(_book);
                    _context.SaveChanges();
            }
        }

        public Book GetById(int id)
        {

            try { 
                var book = _context.Books.FirstOrDefault(b => b.Id == id);
                return book;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddBook(BookVM bookVM)
        {
            _context.Books.Add(new Book
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead : null,
                Rate = bookVM.IsRead ? bookVM.Rate : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId
            });
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
          _context.Books.Remove(_context.Books.FirstOrDefault(b => b.Id == id));
            _context.SaveChanges();
        }

        public void UpdateBook(int id, BookVM bookVM)
        {
           _context.Books.Update(new Book
            {
                Id = id,
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead : null,
                Rate = bookVM.IsRead ? bookVM.Rate : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId
            });
            _context.SaveChanges();
        }

        object IBookRepository.GetBookById(int id)
        {
            return GetBookById(id);
        }
    }
}
