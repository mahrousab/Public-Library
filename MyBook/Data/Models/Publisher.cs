namespace MyBook.Data.Models
{
    public class Publisher
    {
        // has a one-to-many relationship with Book

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Book> Books { get; set; }   
    }
}
