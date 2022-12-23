using System.Text.Json.Serialization;

namespace RentARaceCar.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TyreCompounds
{
    Road,
    SemiSlick,
    Slick
}