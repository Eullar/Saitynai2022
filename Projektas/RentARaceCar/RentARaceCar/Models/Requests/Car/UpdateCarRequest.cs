using RentARaceCar.Enums;

namespace RentARaceCar.Models.Requests.Car;

public class UpdateCarRequest
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public TransmissionTypes TransmissionType { get; set; }
    public DrivetrainTypes DrivetrainType { get; set; }
    public TyreCompounds TyreCompound { get; set; }
}