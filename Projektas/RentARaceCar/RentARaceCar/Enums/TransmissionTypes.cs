using System.Text.Json.Serialization;

namespace RentARaceCar.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransmissionTypes
{
    Automatic,
    Manual,
    Sequencial
}