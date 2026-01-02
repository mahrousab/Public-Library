namespace MyBook.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }    
        public Author_Book? Author_Book { get; set; }
    }
}
