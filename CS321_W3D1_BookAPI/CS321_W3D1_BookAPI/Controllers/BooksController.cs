using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS321_W3D1_BookAPI.Models;
using CS321_W3D1_BookAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CS321_W3D1_BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        // GET api/books
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookService.GetAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            // look up book by id
            var book = _bookService.Get(Id);
            // if not found, return 404 NotFound 
            if (book == null) return NotFound();
            // otherwise return 200 OK and the Book
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public IActionResult Post([FromBody] Book newBook)
        {
            try
            {
                // add the new book
                _bookService.Post(newBook);
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("Add new book", ex.Message);
                return BadRequest(ModelState);
            }

            // return a 201 Created status. This will also add a "location" header
            // with the URI of the new book. E.g., /api/books/99, if the new is 99
            return CreatedAtAction("Get", new { Id = newBook.Id }, newBook);

        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book updatedBook)
        {
            var book = _bookService.Update(updatedBook);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var book = _bookService.Get(Id);
            if (book == null) return NotFound();
            _bookService.Remove(book);
            return NoContent();

        }
    }
}
