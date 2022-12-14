using Microsoft.EntityFrameworkCore;
using RentARaceCar.DbContext;
using RentARaceCar.Interfaces.Services;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Services;

public class OrderService : IOrderService
{
    private readonly RentARaceCarDbContext _context;

    public OrderService(RentARaceCarDbContext context) =>
        _context = context;
    
    public async Task<OrderModel> AddOrderModelAsync(OrderModel orderModel)
    {
        var orderModelObject = new OrderModel
        {
            Id = Guid.NewGuid(),
            CarId = orderModel.CarId,
            OrderDate = DateTime.Now,
            RentDate = orderModel.RentDate,
            UserId = orderModel.UserId
        };

        await _context.AddAsync(orderModelObject);
        await _context.SaveChangesAsync();

        return orderModelObject;
    }

    public async Task<OrderModel> UpdateOrderModelAsync(OrderModel orderModel)
    {
        _context.Orders.Update(orderModel);
        await _context.SaveChangesAsync();

        return orderModel;
    }

    public Task<OrderModel?> GetOrderModelAsync(Guid carId, Guid orderModelId) => 
        _context.Orders.FirstOrDefaultAsync(o => o.Id == orderModelId && o.CarId == carId);

    public async Task DeleteOrderModelAsync(OrderModel orderModel)
    {
        _context.Orders.Remove(orderModel);
        await _context.SaveChangesAsync();
    }

    public Task<List<OrderModel>> GetAllOrderModelsAsync(Guid carId) =>
        _context.Orders.Where(x => x.CarId == carId).ToListAsync();
}