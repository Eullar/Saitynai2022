using RentARaceCar.Enums;

namespace RentARaceCar.Models;

public class Car
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public TransmissionTypes TransmissionType { get; set; }
    public DrivetrainTypes DrivetrainType { get; set; }
    public TyreCompounds TyreCompound { get; set; }
    public Guid RentOfficeId { get; set; }

    public virtual RentOffice RentOffice { get; set; }
    public virtual List<Order> Orders { get; set; }
}