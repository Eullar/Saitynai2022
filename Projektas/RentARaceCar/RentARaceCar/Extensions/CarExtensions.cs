using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Extensions;

public static class CarExtensions
{
    public static Car ToCar(this CarModel carModel) =>
        new()
        {
            Id = carModel.Id,
            Manufacturer = carModel.Manufacturer,
            Model = carModel.Model,
            DrivetrainType = carModel.DrivetrainType,
            TransmissionType = carModel.TransmissionType,
            TyreCompound = carModel.TyreCompound,
            RentOfficeId = carModel.RentOfficeId
        };

    public static async Task<Car> ToCarAsync(this Task<CarModel> carModel) => 
        (await carModel).ToCar();
}