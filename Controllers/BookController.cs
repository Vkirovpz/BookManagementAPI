using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {

            return Ok();
        }

        [HttpPost]
        public ActionResult<Book> AddBook(Book newBook)
        {
            if (newBook == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            return Ok(updatedBook);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            return Ok();
        }


    }
}
