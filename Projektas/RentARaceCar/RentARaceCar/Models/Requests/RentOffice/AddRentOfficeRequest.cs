namespace RentARaceCar.Models.Requests.RentOffice;

public record AddRentOfficeRequest
{
    public string Name { get; set; }
    public string Location { get; set; }
}