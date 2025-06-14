using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BooksController : Controller
{
    private readonly IBookService bookService;
    private readonly IAuthorService authorService;
    private readonly IPublisherService publisherService;
    private readonly IGenreService genreService;

    public BooksController(IBookService BookService, IAuthorService AuthorService,IPublisherService PublisherService, IGenreService GenreService)
    {
        bookService = BookService;
        authorService = AuthorService;
        publisherService = PublisherService;
        genreService = GenreService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await bookService.GetBooksAdmin().ToListAsync(); // List of BookDto
        var authors = await authorService.GetAuthors().ToListAsync(); // List of authors
        var publishers = await publisherService.GetPublishers().ToListAsync(); // List of publishers
        var genres = await genreService.GetGenres().ToListAsync(); // List of genres

        var viewModel = new BooksViewModel
        {
            Books = books,
            Authors = authors,
            Publishers = publishers,
            Genres = genres
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] BookDto bookDto)
    {

        if (bookDto == null)
        {
            return Json(new { success = false, message = "Invalid data" });
        }

        try
        {
            // Save changes to the database
            await bookService.UpdateBook(bookDto.Id.Value, bookDto);

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // POST: /Admin/Books/Create
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookDto newBook)
    {
        if (newBook == null)
        {
            return Json(new { success = false, message = "Invalid data." });
        }

        var result = await bookService.AddBook(newBook);
        if (result.IsSuccess)
        {
            return Json(new { success = true });
        }
        else
        {
            return Json(new { success = false, message = "Error adding new book." });
        }
    }

    // POST: /Admin/Books/Delete/{id}
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await bookService.DeleteBook(id);
        if (result.IsSuccess)
        {
            return Json(new { success = true });
        }
        else
        {
            return Json(new { success = false, message = "Error deleting the book." });
        }
    }


}
