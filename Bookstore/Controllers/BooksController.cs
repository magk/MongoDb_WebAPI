using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public ActionResult<List<Book>> GetAll() => _bookService.GetAll();
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> GetById(string id)
        {
            var book = _bookService.GetById(id);
            if (book == null) return NotFound();
            return book;
        }
        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Update(id, bookIn);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Remove(book);
            return NoContent();
        }
    }
}
