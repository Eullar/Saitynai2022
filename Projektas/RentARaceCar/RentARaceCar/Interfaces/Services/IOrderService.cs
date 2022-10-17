using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Interfaces.Services;

public interface IOrderService
{
    public Task<OrderModel> AddOrderModelAsync(OrderModel orderModel);
    public Task<OrderModel> UpdateOrderModelAsync(OrderModel orderModel);
    public Task<OrderModel?> GetOrderModelAsync(Guid carId, Guid orderModelId);
    public Task DeleteOrderModelAsync(OrderModel orderModel);
    public Task<List<OrderModel>> GetAllOrderModelsAsync(Guid carId);
}