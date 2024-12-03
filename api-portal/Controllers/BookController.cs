using api_portal.Dto;
using api_portal.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(BookService bookService) : ControllerBase
    {
        private readonly BookService _bookService = bookService;

        [HttpGet]
        public IActionResult FindAll()
        {
            var books = _bookService.FindAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            var book = _bookService.FindById(id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Save([FromBody] BookRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Title) || request.Price <= 0 || request.StockQuantity < 0)
            {
                return BadRequest("Invalid book data.");
            }

            var savedBook = _bookService.Save(request);
            return CreatedAtAction(nameof(FindById), new { id = savedBook.BookID }, savedBook);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BookRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Title) || request.Price <= 0 || request.StockQuantity < 0)
            {
                return BadRequest("Invalid book data.");
            }

            var updatedBook = _bookService.Update(id, request);

            if (updatedBook == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(updatedBook);
        }

        [HttpGet("search")]
        public IActionResult SearchAndFilter([FromQuery] string searchTerm,
            [FromQuery] string genre,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var books = _bookService.SearchAndFilter(searchTerm, genre, minPrice, maxPrice);

            if (books == null || !books.Any())
            {
                return NotFound("No books match the given criteria.");
            }

            return Ok(books);
        }
    }
}
