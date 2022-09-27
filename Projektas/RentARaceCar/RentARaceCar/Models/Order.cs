namespace RentARaceCar.Models;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid CarId { get; set; }

    public virtual Car Car { get; set; }
}