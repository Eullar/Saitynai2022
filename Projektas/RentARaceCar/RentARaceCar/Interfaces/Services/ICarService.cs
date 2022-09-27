using RentARaceCar.Models;

namespace RentARaceCar.Interfaces.Services;

public interface ICarService
{
    public Guid AddCar(Car car);
    public Car UpdateCar(Car car);
    public Car GetCar(Guid carId);
    public bool DeleteCar(Guid carId);
    public List<Car> GetAllCars();
}