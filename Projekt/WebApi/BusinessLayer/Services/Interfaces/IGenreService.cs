using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

namespace BusinessLayer.Services.Interfaces;
public interface IGenreService
{
    public IQueryable<GenreDisplay> GetGenres();
    public IQueryable<GenreDto> GetGenresAdmin();
    public Task<OperationResult> DeleteGenre(int GenreId);
    public Task<OperationResult<GenreDisplay>> AddGenre(GenreDto genreDto);
    public Task<OperationResult<GenreDisplay>> UpdateGenre(int genreId, GenreDto upadteGenreDto);
}
