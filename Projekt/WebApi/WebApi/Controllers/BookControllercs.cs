using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        // Fetch all books
        [HttpGet("fetch")]
        public async Task<ActionResult<IEnumerable<BookDisplay>>> Fetch()
        {
            var booksQuery = bookService.GetBooks();
            var books = await booksQuery.ToListAsync();
            return Ok(books);
        }
        
        [HttpGet("fetchByAttribute")]
        public async Task<ActionResult<IEnumerable<BookDisplay>>> FetchByAttribute([FromQuery] BookSearchAttributes atr)
        {
            var booksQuery = bookService.GetBooksByAttribute(atr);
            var books = await booksQuery.ToListAsync();
            return Ok(books);
        }



        // Delete a book
        [HttpDelete("{bookId}")]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            var result = await bookService.DeleteBook(bookId);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage});
            }

            return Ok("Book succesfully deleted.");
        }


        // Action method for adding a new book
        [HttpPost("add")]
        public async Task<ActionResult> AddBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await bookService.AddBook(bookDto);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage });
            }

            return CreatedAtAction(nameof(AddBook), new { bookId = result.Data.Id }, result.Data);
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateRating(int id, [FromBody] BookDto updateBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if the model state is invalid
            }

            var result = await bookService.UpdateBook(id, updateBookDto);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage });
            }

            return Ok(result.Data);
        }

        [HttpPost("{bookId}/{genreId}")]
        public async Task<ActionResult> AddSecondaryGenre(int bookId, int genreId)
        {
            var result = await bookService.AddSecondaryGenre(bookId, genreId);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Message = result.CustomErrorMessage });
            }

            return Ok();
        }

        [HttpDelete("{bookId}/{genreId}")]
        public async Task<ActionResult> RemoveSecondaryGenre(int bookId, int genreId)
        {
            var result = await bookService.RemoveSecondaryGenre(bookId, genreId);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Message = result.CustomErrorMessage });
            }

            return Ok();
        }
    }
}


