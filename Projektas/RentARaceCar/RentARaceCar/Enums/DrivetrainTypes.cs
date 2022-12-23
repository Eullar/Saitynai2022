using System.Text.Json.Serialization;

namespace RentARaceCar.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DrivetrainTypes
{
    FF,
    FR,
    MR,
    RR,
    AWD
}