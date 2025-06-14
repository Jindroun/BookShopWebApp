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
    public class OrderService : IOrderService
    {
        private readonly BookHubDbContext dbContext;
        private readonly IMapper mapper;

        public OrderService(BookHubDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private IQueryable<OrderEntity> IncludeRelatedEntities(IQueryable<OrderEntity> query)
        {
            return query
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.ShopItems)
                .Include(o => o.OrderItems);
        }

        public IQueryable<OrderEntity> FetchOrders()
        {
            return IncludeRelatedEntities(dbContext.Orders);
        }

        public IQueryable<OrderDisplay> FetchOrdersDisplayByUser(int userId)
        {
            return IncludeRelatedEntities(dbContext.Orders)
                .Where(o => o.User != null && o.User.Id == userId)
                .ProjectTo<OrderDisplay>(mapper.ConfigurationProvider);
        }

        public IQueryable<OrderEntity> OrdersByState(OrderState state)
        {
            return IncludeRelatedEntities(dbContext.Orders)
                .Where(o => o.State == state);
        }

        public async Task<OperationResult<IQueryable<OrderEntity>>> OrdersByUserAsync(int userId)
        {
            var orders = IncludeRelatedEntities(dbContext.Orders)
                .Where(o => o.UserId == userId);

            if (!await orders.AnyAsync())
            {
                return OperationResult<IQueryable<OrderEntity>>.Failure("No orders found");
            }

            return OperationResult<IQueryable<OrderEntity>>.Success(orders);
        }

        public async Task<OperationResult<OrderDisplay>> GetOrderAsync(int orderId)
        {
            var order = await dbContext.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.ShopItems)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ShopItem)
                .ThenInclude(si => si.Book)
                .Include(o => o.CouponCode)
                .ThenInclude(cc => cc!.GiftCard)
                .SingleOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return OperationResult<OrderDisplay>.Failure("Order not found");
            }

            var orderDto = mapper.Map<OrderDisplay>(order);
            return OperationResult<OrderDisplay>.Success(orderDto);
        }

        public IQueryable<OrderDto> GetOrdersAdmin()
        {
            return IncludeRelatedEntities(dbContext.Orders)
                .ProjectTo<OrderDto>(mapper.ConfigurationProvider);
        }

        public async Task<OperationResult<OrderEntity>> PlaceOrderAsync(int userId, AddressDto address, List<OrderItemDto> items)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(userId);
                if (user == null)
                {
                    return OperationResult<OrderEntity>.Failure("User not found");
                }

                var foundAddress = await dbContext.Addresses
                    .FirstOrDefaultAsync(a => a.Street == address.Street && a.City == address.City && a.PostalCode == address.PostalCode && a.Country == address.Country && a.UserId == userId);

                var order = mapper.Map<OrderEntity>(new OrderDto
                {
                    UserId = userId,
                    Address = address,
                    OrderItems = items
                });

                if (foundAddress == null)
                {
                    order.Address = mapper.Map<AddressEntity>(address);
                    order.Address.UserId = userId;
                    order.Address.User = user;
                    await dbContext.Addresses.AddAsync(order.Address);
                }
                else
                {
                    order.Address = foundAddress;
                }

                decimal total = 0;
                foreach (var orderItem in order.OrderItems)
                {
                    var shopItem = await dbContext.ShopItems.FindAsync(orderItem.ShopItemId);
                    if (shopItem == null)
                    {
                        return OperationResult<OrderEntity>.Failure($"Shop item {orderItem.ShopItemId} not found");
                    }

                    orderItem.PricePerItem = shopItem.Price;
                    total += orderItem.PricePerItem * orderItem.Count;
                    shopItem.Stock -= orderItem.Count;
                }

                order.TotalPrice = total;

                await dbContext.Orders.AddAsync(order);
                await dbContext.SaveChangesAsync();

                return OperationResult<OrderEntity>.Success(order);
            }
            catch (Exception ex)
            {
                return OperationResult<OrderEntity>.Failure($"Adding entity failed: {ex.InnerException?.Message}");
            }
        }

        public async Task<OperationResult<OrderDisplay>> UpdateOrderAsync(int orderId, OrderDto updateOrderDto)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(updateOrderDto.UserId);
                if (user == null)
                {
                    return OperationResult<OrderDisplay>.Failure("User not found.");
                }

                var existingOrder = await IncludeRelatedEntities(dbContext.Orders)
                    .FirstOrDefaultAsync(b => b.Id == orderId);

                if (existingOrder == null)
                {
                    return OperationResult<OrderDisplay>.Failure("Order not found.");
                }

                mapper.Map(updateOrderDto, existingOrder);
                existingOrder.UpdatedAt = DateTime.Now;

                await dbContext.SaveChangesAsync();

                var orderDtoResult = mapper.Map<OrderDisplay>(existingOrder);
                return OperationResult<OrderDisplay>.Success(orderDtoResult);
            }
            catch (Exception ex)
            {
                return OperationResult<OrderDisplay>.Failure("An error occurred while updating the order.", ex);
            }
        }

        public async Task<OperationResult<OrderEntity>> PlaceWholeOrderAsync(OrderDto orderDto)
        {
            try
            {
                var user = await dbContext.Users.
                    Include(u => u.Addresses).
                    SingleOrDefaultAsync(u => u.Id == orderDto.UserId);
                if (user == null)
                {
                    return OperationResult<OrderEntity>.Failure("User not found");
                }

                var order = mapper.Map<OrderEntity>(orderDto);

                // set user's address if not provided
                if (orderDto.Address == null)
                {
                    order.AddressId = user.Addresses.First().Id;
                }
                else
                {
                    order.Address.UserId = user.Id;
                }

                // check if order items' shop items exist and set order item price
                decimal total = 0;
                foreach (var orderItem in order.OrderItems)
                {
                    var shopItem = await dbContext.ShopItems.FindAsync(orderItem.ShopItemId);
                    if (shopItem == null)
                    {
                        return OperationResult<OrderEntity>.Failure($"Shop item {orderItem.ShopItemId} not found");
                    }
                    //orderItem.OrderId = order.Id;
                    orderItem.PricePerItem = shopItem.Price;
                    total += orderItem.PricePerItem * orderItem.Count;
                }

                order.TotalPrice = total;
                
                // apply coupon code if provided
                if (orderDto.CouponCode != null)
                {
                    var coupon = await dbContext.CouponCodes
                        .Include(cc => cc.GiftCard)
                        .Include(cc => cc.Order)
                        .FirstOrDefaultAsync(cc => cc.Code == orderDto.CouponCode);
                    if (coupon == null)
                    {
                        return OperationResult<OrderEntity>.Failure("Coupon code not found");
                    }
                    if (coupon.Order != null)
                    {
                        return OperationResult<OrderEntity>.Failure("Coupon code already used");
                    }
                    if (!(coupon.GiftCard.ValidFrom <= order.PlacedDate && order.PlacedDate <= coupon.GiftCard.ValidTo))
                    {
                        return OperationResult<OrderEntity>.Failure("Coupon code expired");
                    }

                    order.CouponCode = coupon;
                }
                dbContext.Orders.Add(order);
                Console.WriteLine(dbContext.ChangeTracker.ToDebugString());
                await dbContext.SaveChangesAsync();

                return OperationResult<OrderEntity>.Success(order);
            }
            catch (Exception ex)
            {
                return OperationResult<OrderEntity>.Failure(ex.ToString());
            }
        }

        public async Task<OperationResult<OrderEntity?>> DeleteOrderAsync(int orderId)
        {
            try
            {
                var order = await dbContext.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null)
                {
                    return OperationResult<OrderEntity?>.Failure("Order not found");
                }

                dbContext.OrderItems.RemoveRange(order.OrderItems);
                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync();

                return OperationResult<OrderEntity?>.Success(order);
            }
            catch (Exception ex)
            {
                return OperationResult<OrderEntity?>.Failure("An error occurred while deleting the order.", ex);
            }
        }
    }
}
