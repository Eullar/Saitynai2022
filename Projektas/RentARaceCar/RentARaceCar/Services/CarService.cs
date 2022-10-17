using Microsoft.EntityFrameworkCore;
using RentARaceCar.DbContext;
using RentARaceCar.Interfaces.Services;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Services;

public class CarService : ICarService
{
    private readonly RentARaceCarDbContext _context;

    public CarService(RentARaceCarDbContext context) =>
        _context = context;
    
    public async Task<CarModel> AddCarModelAsync(CarModel carModel)
    {
        var carModelObject = new CarModel
        {
            Id = Guid.NewGuid(),
            Manufacturer = carModel.Manufacturer,
            Model = carModel.Model,
            DrivetrainType = carModel.DrivetrainType,
            TyreCompound = carModel.TyreCompound,
            TransmissionType = carModel.TransmissionType,
            RentOfficeId = carModel.RentOfficeId
        };

        await _context.Cars.AddAsync(carModelObject);
        await _context.SaveChangesAsync();

        return carModelObject;
    }

    public async Task<CarModel> UpdateCarModelAsync(CarModel carModel)
    {
        _context.Cars.Update(carModel);
        await _context.SaveChangesAsync();

        return carModel;
    }

    public Task<CarModel?> GetCarModelAsync(Guid rentOfficeId, Guid carModelId) => 
        _context.Cars.Include(x => x.Orders).FirstOrDefaultAsync(c => c.Id == carModelId && c.RentOfficeId == rentOfficeId);

    public async Task DeleteCarModelAsync(CarModel carModel)
    {
        _context.Cars.Remove(carModel);
        await _context.SaveChangesAsync();
    }

    public Task<List<CarModel>> GetAllCarModelsAsync(Guid rentOfficeId) => 
        _context.Cars.Include(x => x.Orders).Where(x => x.RentOfficeId == rentOfficeId).ToListAsync();
}