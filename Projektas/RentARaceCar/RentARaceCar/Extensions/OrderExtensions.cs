using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Extensions;

public static class OrderExtensions
{
    public static Order ToOrder(this OrderModel orderModel) =>
        new ()
        {
            Id = orderModel.Id,
            CarId = orderModel.CarId,
            OrderDate = orderModel.OrderDate,
            RentDate = orderModel.RentDate.Date,
            UserId = orderModel.UserId
        };

    public static async Task<Order> ToOrderAsync(this Task<OrderModel> orderModel) =>
        (await orderModel).ToOrder();
}