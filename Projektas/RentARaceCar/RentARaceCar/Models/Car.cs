using System.Text.Json.Serialization;
using RentARaceCar.Enums;

namespace RentARaceCar.Models;

public class Car
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransmissionTypes TransmissionType { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DrivetrainTypes DrivetrainType { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TyreCompounds TyreCompound { get; set; }
    public Guid RentOfficeId { get; set; }
}