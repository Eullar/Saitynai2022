using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Extensions;

public static class Extensions
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

    public static Order ToOrder(this OrderModel orderModel) =>
        new ()
        {
            Id = orderModel.Id,
            CarId = orderModel.CarId,
            OrderDate = orderModel.OrderDate,
            RentDate = orderModel.RentDate
        };

    public static async Task<Order> ToOrderAsync(this Task<OrderModel> orderModel) =>
        (await orderModel).ToOrder();
}