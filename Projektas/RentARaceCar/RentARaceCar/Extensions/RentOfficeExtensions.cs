using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Extensions;

public static class RentOfficeExtensions
{
    public static RentOffice? ToRentOffice(this RentOfficeModel? rentOffice)
    {
        return (rentOffice is null ? null : new()
        {
            Id = rentOffice.Id,
            Name = rentOffice.Name,
            Location = rentOffice.Location,
            CarCount = rentOffice.Cars.Count
        });
    }

    public static async Task<RentOffice?> ToRentOfficeAsync(this Task<RentOfficeModel?> rentOffice) => 
        (await rentOffice).ToRentOffice();
}