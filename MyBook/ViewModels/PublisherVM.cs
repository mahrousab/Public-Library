namespace MyBook.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PublisherWithBooksAndAuthorsVM : PublisherVM
    {
        public List<BookWithAuthorVM> Books { get; set; }
    }

    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}
