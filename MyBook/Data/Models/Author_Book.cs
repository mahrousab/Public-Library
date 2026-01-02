namespace MyBook.Data.Models
{
    public class Author_Book
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book book { get; set; }
        public int AuthorId { get; set; }
        public Author author { get; set; }
    }
}
