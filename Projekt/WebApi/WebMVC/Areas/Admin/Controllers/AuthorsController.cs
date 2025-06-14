using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Areas.Admin.Models;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AuthorsController : Controller
{

    private readonly IAuthorService authorService;


    public AuthorsController(IAuthorService AuthorService)
    {
        authorService = AuthorService;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await authorService.GetAuthorsAdmin().ToListAsync(); // List of authors

        var viewModel = new AuthorsViewModel
        {
            Authors = authors,
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] AuthorDto authorDto)
    {

        if (authorDto == null)
        {
            return Json(new { success = false, message = "Invalid data" });
        }

        try
        {
            // Save changes to the database
            await authorService.UpdateAuthor(authorDto.Id.Value, authorDto);

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // POST: /Admin/Authors/Create
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthorDto authorDto)
    {
        if (authorDto == null)
        {
            return Json(new { success = false, message = "Invalid data." });
        }

        var result = await authorService.AddAuthor(authorDto);
        if (result.IsSuccess)
        {
            return Json(new { success = true });
        }
        else
        {
            return Json(new { success = false, message = "Error adding new author." });
        }
    }

    // POST: /Admin/Authors/Delete/{id}
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await authorService.DeleteAuthor(id);
        if (result.IsSuccess)
        {
            return Json(new { success = true });
        }
        else
        {
            return Json(new { success = false, message = "Error deleting the author." });
        }
    }


}
