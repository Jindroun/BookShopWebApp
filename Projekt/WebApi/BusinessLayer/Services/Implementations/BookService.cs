using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations
{
    public class BookService : IBookService
    {
        private IMapper mapper;
        private BookHubDbContext dbContext;

        public BookService(IMapper mapper, BookHubDbContext context)
        {
            this.mapper = mapper;
            dbContext = context;
        }

        public IQueryable<BookDisplay> GetBooks()
        {
            return dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.SecondaryGenres)
                .Include(b => b.Publisher)
                .ProjectTo<BookDisplay>(mapper.ConfigurationProvider);
        }

        public IQueryable<BookDto> GetBooksAdmin()
        {
            return dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .ProjectTo<BookDto>(mapper.ConfigurationProvider);
        }

        public IQueryable<BookDisplay> GetBooksByAttribute(BookSearchAttributes atr)
        {
            // Start building the query
            var query = dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.SecondaryGenres)
                .Include(b => b.Publisher)
                .Include(b => b.ShopItem)
                .AsQueryable();

            // Helper function for splitting and normalizing input strings
            Func<string?, IEnumerable<string>> normalizeInput = input =>
                input?.Trim().ToLower().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries) ?? Enumerable.Empty<string>();

            var genres = normalizeInput(atr.Genre);
            var authorNames = normalizeInput(atr.Author);
            var titles = normalizeInput(atr.Title);
            var descriptions = normalizeInput(atr.Description);

            // Apply filters using conditional Where clauses, if attribute was empty
            //  the first condition evaluates to TRUE and the where clause does nothing
            query = query
              .Where(b => !genres.Any() || genres.Any(g => b.Genre.Name.ToLower().Contains(g)))
              .Where(b => !authorNames.Any() ||
                          authorNames.Any(name => b.Author.FirstName.ToLower().Contains(name)) ||
                          authorNames.Any(name => b.Author.LastName.ToLower().Contains(name)))
              .Where(b => !titles.Any() || titles.Any(t => b.Title.ToLower().Contains(t)))
              .Where(b => !descriptions.Any() || descriptions.Any(d => b.Description.ToLower().Contains(d)))
              .Where(b => b.ShopItem != null && b.ShopItem.Stock > 0 &&
                          (atr.MinPrice == null || b.ShopItem.Price >= atr.MinPrice) &&
                          (atr.MaxPrice == null || b.ShopItem.Price <= atr.MaxPrice));

            // Project to the display model using AutoMapper
            return query.ProjectTo<BookDisplay>(mapper.ConfigurationProvider);
        }

        public async Task<OperationResult> DeleteBook(int BookId)
        {
            try
            {
                var book = await dbContext.Books.FindAsync(BookId);

                if (book == null)
                {
                    return OperationResult.Failure("Book not found.");
                }

                dbContext.Books.Remove(book);
                await dbContext.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occured when deleting the book.", ex);
            }
        }
        public async Task<OperationResult<BookDisplay>> AddBook(BookDto bookDto)
        {
            try
            {
                // Validate the author
                var author = await dbContext.Authors.FindAsync(bookDto.AuthorId);
                if (author == null)
                {
                    return OperationResult<BookDisplay>.Failure("Author not found.");
                }

                // Validate the genre
                var genre = await dbContext.Genres.FindAsync(bookDto.GenreId);
                if (genre == null)
                {
                    return OperationResult<BookDisplay>.Failure("Genre not found.");
                }

                // Validate the publisher
                var publisher = await dbContext.Publishers.FindAsync(bookDto.PublisherId);
                if (publisher == null)
                {
                    return OperationResult<BookDisplay>.Failure("Publisher not found.");
                }

                // Create the new book entity
                var newBook = mapper.Map<BookEntity>(bookDto);
                newBook.CreatedAt = DateTime.Now;

                dbContext.Books.Add(newBook);
                await dbContext.SaveChangesAsync();

                var bookDtoResult = mapper.Map<BookDisplay>(newBook);
                return OperationResult<BookDisplay>.Success(bookDtoResult);
            }
            catch (Exception ex)
            {
                return OperationResult<BookDisplay>.Failure("An error occured while adding the book.", ex);
            }
        }
        public async Task<OperationResult<BookDisplay>> UpdateBook(int bookId, BookDto updateBookDto)
        {
            try
            {
                // Validate the author
                var author = await dbContext.Authors.FindAsync(updateBookDto.AuthorId);
                if (author == null)
                {
                    return OperationResult<BookDisplay>.Failure("Author not found.");
                }

                // Validate the genre
                var genre = await dbContext.Genres.FindAsync(updateBookDto.GenreId);
                if (genre == null)
                {
                    return OperationResult<BookDisplay>.Failure("Genre not found.");
                }

                // Validate the publisher
                var publisher = await dbContext.Publishers.FindAsync(updateBookDto.PublisherId);
                if (publisher == null)
                {
                    return OperationResult<BookDisplay>.Failure("Publisher not found.");
                }

                // Fetch the existing book with related entities
                var existingBook = await dbContext.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Include(b => b.Publisher)
                    .Include(b => b.ShopItem)
                    .FirstOrDefaultAsync(b => b.Id == bookId);

                if (existingBook == null)
                {
                    return OperationResult<BookDisplay>.Failure("Book not found.");
                }

                // Update only the properties from the DTO without replacing the entire entity
                mapper.Map(updateBookDto, existingBook);
                existingBook.UpdatedAt = DateTime.Now;

                // Save the changes to the database
                await dbContext.SaveChangesAsync();

                // Map the updated book to BookDisplay
                var bookDtoResult = mapper.Map<BookDisplay>(existingBook);

                return OperationResult<BookDisplay>.Success(bookDtoResult);
            }
            catch (Exception ex)
            {
                return OperationResult<BookDisplay>.Failure("An error occured while updating the book.", ex);
            }
        }

        public async Task<OperationResult> AddSecondaryGenre(int bookId, int genreId)
        {
            try 
            {
                var book = await dbContext.Books
                    .Include(b => b.SecondaryGenres)
                    .FirstOrDefaultAsync(b => b.Id == bookId);

                if (book == null) {
                    return OperationResult.Failure("Book not found.");
                }

                var genre = await dbContext.Genres.FindAsync(genreId);
                if (genre == null) {
                    return OperationResult.Failure("Genre not found.");
                }

                if (book.SecondaryGenres.Any(g => g.Id == genreId)) {
                    return OperationResult.Failure("Genre already added.");
                }

                book.SecondaryGenres.Add(genre);
                await dbContext.SaveChangesAsync();
                return OperationResult.Success();
            }
            catch (Exception ex) 
            {
                return OperationResult.Failure("An error occured while adding the genre.", ex);
            }
        }

        public async Task<OperationResult> RemoveSecondaryGenre(int bookId, int genreId) {
            try 
            {
                var book = await dbContext.Books
                    .Include(b => b.SecondaryGenres)
                    .FirstOrDefaultAsync(b => b.Id == bookId);

                if (book == null) {
                    return OperationResult.Failure("Book not found.");
                }

                var genre = await dbContext.Genres.FindAsync(genreId);
                if (genre == null) {
                    return OperationResult.Failure("Genre not found.");
                }

                if (!book.SecondaryGenres.Any(g => g.Id == genreId)) {
                    return OperationResult.Failure("Book does not have this genre.");
                }

                book.SecondaryGenres.Remove(genre);
                await dbContext.SaveChangesAsync();
                return OperationResult.Success();
            }
            catch (Exception ex) 
            {
                return OperationResult.Failure("An error occured while removing the genre.", ex);
            }
        }
    }
}
