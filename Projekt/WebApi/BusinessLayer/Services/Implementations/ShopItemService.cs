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
    public class ShopItemService : IShopItemService
    {
        private readonly IMapper mapper;
        private readonly BookHubDbContext dbContext;

        public ShopItemService(IMapper mapper, BookHubDbContext context)
        {
            this.mapper = mapper;
            dbContext = context;
        }

        public IQueryable<ShopItemDisplay> GetShopItems()
        {
            return dbContext.ShopItems
                .Include(si => si.Book)
                    .ThenInclude(b => b.Author)
                .Include(si => si.Book)
                    .ThenInclude(b => b.Genre)
                .ProjectTo<ShopItemDisplay>(mapper.ConfigurationProvider);
        }

        public async Task<OperationResult<ShopItemDisplay>> AddShopItem(ShopItemDto shopItemDto)
        {
            try
            {
                var book = await dbContext.Books.FindAsync(shopItemDto.BookId);
                if (book == null)
                {
                    return OperationResult<ShopItemDisplay>.Failure("Book not found.");
                }

                var newShopItem = mapper.Map<ShopItemEntity>(shopItemDto);
                newShopItem.CreatedAt = DateTime.Now;

                await dbContext.ShopItems.AddAsync(newShopItem);
                await dbContext.SaveChangesAsync();

                var shopItemResult = mapper.Map<ShopItemDisplay>(newShopItem);
                return OperationResult<ShopItemDisplay>.Success(shopItemResult);
            }
            catch (Exception ex)
            {
                return OperationResult<ShopItemDisplay>.Failure("An error occurred while adding the shop item.", ex);
            }
        }

        public async Task<OperationResult> DeleteShopItem(int shopItemId, bool force)
        {
            try
            {
                var shopItem = await dbContext.ShopItems
                    .Include(si => si.OrderItems)
                    .FirstOrDefaultAsync(si => si.Id == shopItemId);

                if (shopItem == null)
                {
                    return OperationResult.Failure("Shop item not found.");
                }

                if (!force && shopItem.OrderItems.Any())
                {
                    return OperationResult.Failure("Shop item has orders.");
                }

                dbContext.OrderItems.RemoveRange(shopItem.OrderItems);
                dbContext.ShopItems.Remove(shopItem);
                await dbContext.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred when deleting the shop item.", ex);
            }
        }

        public async Task<OperationResult<ShopItemDisplay>> UpdateShopItem(int shopItemId, ShopItemDto updatedShopItemDto)
        {
            try
            {
                var book = await dbContext.Books.FindAsync(updatedShopItemDto.BookId);
                if (book == null)
                {
                    return OperationResult<ShopItemDisplay>.Failure("Book not found.");
                }

                var existingShopItem = await dbContext.ShopItems
                    .Include(si => si.Book)
                    .FirstOrDefaultAsync(si => si.Id == shopItemId);

                if (existingShopItem == null)
                {
                    return OperationResult<ShopItemDisplay>.Failure("Shop item not found.");
                }

                mapper.Map(updatedShopItemDto, existingShopItem);
                existingShopItem.UpdatedAt = DateTime.Now;

                await dbContext.SaveChangesAsync();

                var shopItemResult = mapper.Map<ShopItemDisplay>(existingShopItem);
                return OperationResult<ShopItemDisplay>.Success(shopItemResult);
            }
            catch (Exception ex)
            {
                return OperationResult<ShopItemDisplay>.Failure("An error occurred while updating the shop item.", ex);
            }
        }

        public async Task<PaginatedResult<ShopItemDisplay>> GetShopItemsByPage(int page, int itemsPerPage)
        {
            var query = dbContext.ShopItems.AsQueryable();
            var totalItemsCount = await query.CountAsync();

            var items = await query
                .OrderBy(item => item.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ProjectTo<ShopItemDisplay>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginatedResult<ShopItemDisplay>
            {
                Items = items,
                TotalItemsCount = totalItemsCount
            };
        }
    }
}
