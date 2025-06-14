using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

namespace BusinessLayer.Services.Interfaces;
public interface IAuthorService
{
    public IQueryable<AuthorDisplay> GetAuthors();
    public IQueryable<AuthorDto> GetAuthorsAdmin();
    public Task<OperationResult> DeleteAuthor(int AuthorId);
    public Task<OperationResult<AuthorDisplay>> AddAuthor(AuthorDto authorDto);
    public Task<OperationResult<AuthorDisplay>> UpdateAuthor(int authorId, AuthorDto updateAuthorDto);
}
