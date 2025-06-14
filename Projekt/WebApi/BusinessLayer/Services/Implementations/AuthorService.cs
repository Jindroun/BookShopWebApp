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
public class AuthorService : IAuthorService
{
    private IMapper mapper;
    private BookHubDbContext dbContext;

    public AuthorService(IMapper mapper, BookHubDbContext context)
    {
        this.mapper = mapper;
        dbContext = context;
    }

    public IQueryable<AuthorDisplay> GetAuthors()
    {
        return dbContext.Authors
            .AsNoTracking()
            .ProjectTo<AuthorDisplay>(mapper.ConfigurationProvider);
    }

    public IQueryable<AuthorDto> GetAuthorsAdmin()
    {
        return dbContext.Authors
            .AsNoTracking()
            .ProjectTo<AuthorDto>(mapper.ConfigurationProvider);
    }
    public async Task<OperationResult> DeleteAuthor(int AuthorId)
    {
        try
        {
            var author = await dbContext.Authors.FindAsync(AuthorId);

            if (author == null)
            {
                return OperationResult.Failure("Author not found.");
            }

            dbContext.Authors.Remove(author);
            await dbContext.SaveChangesAsync();

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            return OperationResult.Failure("An error occured when deleting the author.", ex);
        }
    }
    public async Task<OperationResult<AuthorDisplay>> AddAuthor(AuthorDto authorDto)
    {
        try
        {
         
             // Create the new author entity
            var newAuthor = mapper.Map<AuthorEntity>(authorDto);
            newAuthor.CreatedAt = DateTime.Now;

            dbContext.Authors.Add(newAuthor);
            await dbContext.SaveChangesAsync();

            var authorDtoResult = mapper.Map<AuthorDisplay>(newAuthor);
            return OperationResult<AuthorDisplay>.Success(authorDtoResult);
        }
        catch (Exception ex)
        {
            return OperationResult<AuthorDisplay>.Failure("An error occured while adding the author.", ex);
        }
    }
    public async Task<OperationResult<AuthorDisplay>> UpdateAuthor(int authorId, AuthorDto updateAuthorDto)
    {
        try
        {
            // Fetch the existing book with related entities
            var existingAuthor = await dbContext.Authors
                .FirstOrDefaultAsync(b => b.Id == authorId);

            if (existingAuthor == null)
            {
                return OperationResult<AuthorDisplay>.Failure("Author not found.");
            }

            // Update only the properties from the DTO without replacing the entire entity
            mapper.Map(updateAuthorDto, existingAuthor);
            existingAuthor.UpdatedAt = DateTime.Now;

            // Save the changes to the database
            await dbContext.SaveChangesAsync();

            // Map the updated book to BookDisplay
            var authorDtoResult = mapper.Map<AuthorDisplay>(existingAuthor);

            return OperationResult<AuthorDisplay>.Success(authorDtoResult);
        }
        catch (Exception ex)
        {
            return OperationResult<AuthorDisplay>.Failure("An error occured while updating the book.", ex);
        }
    }
}

