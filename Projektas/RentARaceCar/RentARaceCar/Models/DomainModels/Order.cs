using System.ComponentModel.DataAnnotations;
using RentARaceCar.Interfaces.Authentication;

namespace RentARaceCar.Models.DomainModels;

public class OrderModel : IUserOwnedResource
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RentDate { get; set; }
    public Guid CarId { get; set; }
    [Required]
    public string UserId { get; set; }

    public CarModel Car { get; set; }
    public UserModel User { get; set; }
}