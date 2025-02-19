using BookManagementAPI.Data;
using BookManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(BookDbContext context) : ControllerBase
    {
        private readonly BookDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return NotFound();

            book.BookViews++;
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook is null)
                return BadRequest();

            var checkForexistingBook = await _context.Books.FirstOrDefaultAsync(b => b.Title == newBook.Title);
            if (checkForexistingBook is not null)
                return BadRequest(checkForexistingBook.Title);

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublicationYear = updatedBook.PublicationYear;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
