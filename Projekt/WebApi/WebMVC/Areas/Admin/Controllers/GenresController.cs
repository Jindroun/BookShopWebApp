using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Areas.Admin.Models;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class GenresController : Controller
{

    private readonly IGenreService genreService;


    public GenresController(IGenreService GenreService)
    {
        genreService = GenreService;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await genreService.GetGenresAdmin().ToListAsync(); // List of authors

        var viewModel = new GenresViewModel
        {
            Genres = genres,
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] GenreDto genreDto)
    {

        if (genreDto == null)
        {
            return Json(new { success = false, message = "Invalid data" });
        }

        try
        {
            // Save changes to the database
            await genreService.UpdateGenre(genreDto.Id.Value, genreDto);

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // POST: /Admin/Authors/Create
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GenreDto genreDto)
    {
        if (genreDto == null)
        {
            return Json(new { success = false, message = "Invalid data." });
        }

        var result = await genreService.AddGenre(genreDto);
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
        var result = await genreService.DeleteGenre(id);
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
