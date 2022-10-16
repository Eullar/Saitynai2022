using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Interfaces.Services;

public interface ICarService
{
    public Task<CarModel> AddCarModelAsync(CarModel carModel);
    public Task<CarModel> UpdateCarModelAsync(CarModel carModel);
    public Task<CarModel?> GetCarModelAsync(Guid carModelId);
    public Task DeleteCarModelAsync(CarModel carModel);
    public Task<List<CarModel>> GetAllCarModelsAsync();
}