namespace PublicLibrary.IRepositories
{
    public interface IAuthorRepository
    {
        List<Data.Models.Author> GetAllAuthors();
        Data.Models.Author GetById(int id);
        void AddAuthor(ViewModels.AuthorVM authorVM);
        void DeleteAuthor(int id);
        void UpdateAuthor(int id, ViewModels.AuthorVM authorVM);
        object GetAuthorWithBooksVM();
    }
}
