using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

namespace BusinessLayer.Services.Interfaces;
public interface IBookService
{
    public IQueryable<BookDisplay> GetBooks();
    public IQueryable<BookDisplay> GetBooksByAttribute(BookSearchAttributes atr);
    public IQueryable<BookDto> GetBooksAdmin();
    public Task<OperationResult> DeleteBook(int BookId);
    public Task<OperationResult<BookDisplay>> AddBook(BookDto bookDto);
    public Task<OperationResult<BookDisplay>> UpdateBook(int bookId, BookDto updateBookDto);
    public Task<OperationResult> AddSecondaryGenre(int bookId, int genreId);
    public Task<OperationResult> RemoveSecondaryGenre(int bookId, int genreId);
}
