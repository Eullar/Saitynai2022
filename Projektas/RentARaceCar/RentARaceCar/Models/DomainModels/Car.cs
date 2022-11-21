using RentARaceCar.Enums;

namespace RentARaceCar.Models.DomainModels;

public class CarModel
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public TransmissionTypes TransmissionType { get; set; }
    public DrivetrainTypes DrivetrainType { get; set; }
    public TyreCompounds TyreCompound { get; set; }
    public Guid RentOfficeId { get; set; }

    public RentOfficeModel RentOffice { get; set; }
    public List<OrderModel> Orders { get; set; }
}