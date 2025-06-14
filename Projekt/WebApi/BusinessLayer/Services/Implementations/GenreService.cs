using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;
public class GenreService : IGenreService
{
    private IMapper mapper;
    private BookHubDbContext dbContext;

    public GenreService(IMapper mapper, BookHubDbContext context)
    {
        this.mapper = mapper;
        dbContext = context;
    }

    public IQueryable<GenreDisplay> GetGenres()
    {
        return dbContext.Genres
            .ProjectTo<GenreDisplay>(mapper.ConfigurationProvider);
    }
    public IQueryable<GenreDto> GetGenresAdmin()
    {
        return dbContext.Genres
            .ProjectTo<GenreDto>(mapper.ConfigurationProvider);
    }
    public async Task<OperationResult> DeleteGenre(int GenreId)
    {
        try
        {
            var genre = await dbContext.Genres.FindAsync(GenreId);

            if (genre == null)
            {
                return OperationResult.Failure("Genre not found.");
            }

            dbContext.Genres.Remove(genre);
            await dbContext.SaveChangesAsync();

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            return OperationResult.Failure("An error occured when deleting the genre.", ex);
        }
    }
    public async Task<OperationResult<GenreDisplay>> AddGenre(GenreDto genreDto)
    {
        try
        {
            // Create the new author entity
            var newGenre = mapper.Map<GenreEntity>(genreDto);
            newGenre.CreatedAt = DateTime.Now;

            dbContext.Genres.Add(newGenre);
            await dbContext.SaveChangesAsync();

            var genreDtoResult = mapper.Map<GenreDisplay>(newGenre);
            return OperationResult<GenreDisplay>.Success(genreDtoResult);
        }
        catch (Exception ex)
        {
            return OperationResult<GenreDisplay>.Failure("An error occured while adding the genre.", ex);
        }
    }
    public async Task<OperationResult<GenreDisplay>> UpdateGenre(int genreId, GenreDto updateGenreDto)
    {
        try
        {
            // Fetch the existing book with related entities
            var existingGenre = await dbContext.Genres
                .FirstOrDefaultAsync(b => b.Id == genreId);

            if (existingGenre == null)
            {
                return OperationResult<GenreDisplay>.Failure("Genre not found.");
            }

            // Update only the properties from the DTO without replacing the entire entity
            mapper.Map(updateGenreDto, existingGenre);
            existingGenre.UpdatedAt = DateTime.Now;

            // Save the changes to the database
            await dbContext.SaveChangesAsync();

            // Map the updated book to BookDisplay
            var genreDtoResult = mapper.Map<GenreDisplay>(existingGenre);

            return OperationResult<GenreDisplay>.Success(genreDtoResult);
        }
        catch (Exception ex)
        {
            return OperationResult<GenreDisplay>.Failure("An error occured while updating the genre.", ex);
        }
    }
}

