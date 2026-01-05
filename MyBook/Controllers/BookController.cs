using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicLibrary.DTOS;
using PublicLibrary.IRepositories;
using PublicLibrary.Repository;
using PublicLibrary.ViewModels;

namespace PublicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookService;
        public BookController(IBookRepository bookService)
        {
            _bookService = bookService;
        }
        [HttpPost("Add-Book-With-Authors")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _bookService.AddBookWithAuthors(book);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);

        }
        [HttpGet()]
        [Route("{id:int}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPut]

       
        public IActionResult UpdateBook(int id, [FromBody] BookVM book)
        {
            var existingBook = _bookService.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            _bookService.UpdateBookById(id, book);
            return Ok();
        }

        [HttpDelete]
       
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBookById(id);
            return Ok();
        }
    }
}