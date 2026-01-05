using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicLibrary.Data.Models;
using PublicLibrary.DTOS;
using PublicLibrary.IRepositories;
using PublicLibrary.Repository;
using PublicLibrary.ViewModels;

namespace PublicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorService;
        public AuthorController(IAuthorRepository authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        [Route("Get-All-Authors")]
        public IActionResult GetAllAuthors()
        {
            var authors = _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorService.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Author)]
        public IActionResult AddAuthor([FromBody] ViewModels.AuthorVM authorVM)
        {
            _authorService.AddAuthor(authorVM);
            return Ok();
        }

        [HttpPut]

        public IActionResult UpdateAuthor(int id,AuthorVM authorVM)
        {
           var existingAuthor = _authorService.GetById(id);
              if (existingAuthor == null)
              {
                return NotFound();
            }
              _authorService.UpdateAuthor(id, authorVM);
            return Ok();

        }
        [HttpDelete]
        public IActionResult DeleteAuthor(int id)
        {
            var existingAuthor = _authorService.GetById(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }
            _authorService.DeleteAuthor(id);
            return Ok();
        }

        [HttpGet]
        [Route("Get-Author-With-Books/{id:int}")]
        public IActionResult GetAuthor(int id)
        {
            var authorWithBooks = _authorService.GetAuthorWithBooksVM();
            return Ok(authorWithBooks);
        }
    }
}