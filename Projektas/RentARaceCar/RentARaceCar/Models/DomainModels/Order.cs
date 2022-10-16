namespace RentARaceCar.Models.DomainModels;

public class OrderModel
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RentDate { get; set; }
    public Guid CarId { get; set; }

    public virtual CarModel Car { get; set; }
}