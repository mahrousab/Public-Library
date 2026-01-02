using MyBook.Data.Models;
using MyBook.IRepositories;
using MyBook.ViewModels;

namespace MyBook.Repository
{
    public class AuthorRepository : IRepositories.IAuthorRepository
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Author.ToList();
        }
        public Author GetById(int id)
        {

            return _context.Author.FirstOrDefault(a => a.Id == id);

        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var author = new Author
            {
                FullName = authorVM.FullName

            };

            _context.Author.Add(author);
            _context.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            if (_context.Author.Any(a => a.Id == id))
            {
                var author = _context.Author.FirstOrDefault(a => a.Id == id);
                _context.Author.Remove(author);
                _context.SaveChanges();
            }
        }

        public void UpdateAuthor(int id, AuthorVM authorVM)
        {
            if (_context.Author.Any(a => a.Id == id))
            {
                var author = _context.Author.FirstOrDefault(a => a.Id == id);
                author.FullName = authorVM.FullName;
                _context.SaveChanges();
            }
        }

        public AuthorWithBooksVM GetAuthorWithBooksVM()
        {
            var authorWithBooks = _context.Author
                .Select(a => new AuthorWithBooksVM
                {
                    FullName = a.FullName,
                    BookTitles = a.Author_Book != null
                        ? new List<string> { a.Author_Book.book.Title }
                        : new List<string>()
                }).FirstOrDefault();
            return authorWithBooks;
        }

        object IAuthorRepository.GetAuthorWithBooksVM()
        {
            return GetAuthorWithBooksVM();
        }
    }
}