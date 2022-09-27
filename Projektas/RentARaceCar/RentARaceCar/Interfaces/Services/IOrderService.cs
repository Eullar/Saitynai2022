using RentARaceCar.Models;

namespace RentARaceCar.Interfaces.Services;

public interface IOrderService
{
    public Guid AddOrder(Order order);
    public Order UpdateOrder(Order order);
    public Order GetOrder(Guid orderId);
    public bool DeleteOrder(Guid orderId);
    public List<Order> GetAllOrders();
}