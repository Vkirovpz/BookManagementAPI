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
        public async Task<ActionResult<List<BookDto>>> GetBooks(int pageNumber, int pageSize)
        {
            var books = await _context.Books.ToListAsync();

            List<BookDto> booksDto = books.Select(b => new BookDto
            {
                Title = b.Title,
                Author = b.Author,
                PublicationYear = b.PublicationYear,
                BookViews = b.BookViews
            }).ToList();

            int totalBooks = booksDto.Count;

            List<BookDto> paginatedBooks = booksDto.OrderByDescending(b => b.Title).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                TotalBooks = totalBooks,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalBooks / pageSize),
                Books = paginatedBooks.Select(b => b.Title)
            };

            //return Ok(booksDto.OrderByDescending(b => b.PopularityScore).Select(b => b.Title));
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return NotFound();

            book.BookViews++;
            await _context.SaveChangesAsync();

            BookDto bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                BookViews = book.BookViews
            };

            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreateBookDto>> AddBook(CreateBookDto newBook)
        {
            if (newBook is null)
                return BadRequest();

            var checkForexistingBook = await _context.Books.FirstOrDefaultAsync(b => b.Title == newBook.Title);
            if (checkForexistingBook is not null)
                return BadRequest(checkForexistingBook.Title);

            var book = new Book
            {
                Title = newBook.Title,
                Author = newBook.Author,
                PublicationYear = newBook.PublicationYear
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPost("addBulk")]
        public async Task<ActionResult<List<CreateBookDto>>> AddBooks(List<CreateBookDto> newBooks)
        {
            if (!newBooks.Any())
                return BadRequest();

            foreach (var book in newBooks)
            {
                var checkForexistingBook = await _context.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
                if (checkForexistingBook is null)
                {
                    var newBook = new Book
                    {
                        Title = book.Title,
                        Author = book.Author,
                        PublicationYear = book.PublicationYear
                    };
                    _context.Books.Add(newBook);
                }
                else
                    continue;
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooks), newBooks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublicationYear = updatedBook.PublicationYear;

            await _context.SaveChangesAsync();

            return Ok();
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

        [HttpDelete("deleteBulk")]
        public async Task<IActionResult> DeleteBooks(List<int> ids)
        {
            if (!ids.Any())
                return BadRequest();

            foreach (int id in ids)
            {
                var book = await _context.Books.FindAsync(id);
                if (book is null)
                    return NotFound(id);

                _context.Books.Remove(book);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
