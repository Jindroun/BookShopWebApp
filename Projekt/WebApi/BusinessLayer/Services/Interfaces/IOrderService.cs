using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;
public interface IOrderService
{
    IQueryable<OrderEntity> FetchOrders();
    IQueryable<OrderDisplay> FetchOrdersDisplayByUser(int userId);
    Task<OperationResult<IQueryable<OrderEntity>>> OrdersByUserAsync(int userId);
    IQueryable<OrderEntity> OrdersByState(OrderState state);
    Task<OperationResult<OrderDisplay>> GetOrderAsync(int orderId);
    IQueryable<OrderDto> GetOrdersAdmin();
    Task<OperationResult<OrderDisplay>> UpdateOrderAsync(int orderId, OrderDto updateOrderDto);
    Task<OperationResult<OrderEntity>> PlaceOrderAsync(int userId, AddressDto address, List<OrderItemDto> items);
    Task<OperationResult<OrderEntity?>> DeleteOrderAsync(int orderId);

    Task<OperationResult<OrderEntity>> PlaceWholeOrderAsync(OrderDto orderDto);
}
