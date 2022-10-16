using Microsoft.EntityFrameworkCore;
using RentARaceCar.DbContext;
using RentARaceCar.Extensions;
using RentARaceCar.Interfaces.Services;
using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;
using RentARaceCar.Models.Requests;
using RentARaceCar.Models.Requests.RentOffice;

namespace RentARaceCar.Services;

public class RentOfficeService : IRentOfficeService
{
    private readonly RentARaceCarDbContext _context;

    public RentOfficeService(RentARaceCarDbContext dbContext) =>
        _context = dbContext;
    public async Task<RentOfficeModel> AddRentOfficeAsync(AddRentOfficeRequest rentOffice)
    {
        var rentOfficeObject = new RentOfficeModel
        {
            Id = Guid.NewGuid(),
            Name = rentOffice.Name,
            Location = rentOffice.Location,
            Cars = new List<CarModel>()
        };
        
        await _context.RentOffices.AddAsync(rentOfficeObject);
        await _context.SaveChangesAsync();

        return rentOfficeObject;
    }

    public async Task<RentOfficeModel> UpdateRentOfficeAsync(RentOfficeModel rentOffice)
    {
        _context.RentOffices.Update(rentOffice);
        await _context.SaveChangesAsync();

        return rentOffice;
    }

    public Task<RentOfficeModel?> GetRentOfficeAsync(Guid rentOfficeId) => 
        _context.RentOffices.Include(x => x.Cars).FirstOrDefaultAsync(r => r.Id == rentOfficeId);

    public async Task DeleteRentOfficeAsync(RentOfficeModel rentOfficeModel)
    {
        _context.RentOffices.Remove(rentOfficeModel);
        await _context.SaveChangesAsync();
    }

    public Task<List<RentOfficeModel>> GetAllRentOfficesAsync() =>
        _context.RentOffices.Include(x => x.Cars).ToListAsync();
}