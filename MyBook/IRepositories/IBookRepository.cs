using PublicLibrary.ViewModels;

namespace PublicLibrary.IRepositories
{
    public interface IBookRepository
    {
        List<Data.Models.Book> GetAllBooks();
        Data.Models.Book GetById(int id);
        void AddBook(ViewModels.BookVM bookVM);
        void DeleteBook(int id);
        void UpdateBook(int id, ViewModels.BookVM bookVM);
        void AddBookWithAuthors(BookVM book);
        object GetBookById(int id);
        void UpdateBookById(int id, BookVM book);
        void DeleteBookById(int id);
    }
}
