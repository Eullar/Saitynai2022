using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;
using RentARaceCar.Models.Requests;
using RentARaceCar.Models.Requests.RentOffice;

namespace RentARaceCar.Interfaces.Services;

public interface IRentOfficeService
{
    public Task<RentOfficeModel> AddRentOfficeAsync(AddRentOfficeRequest rentOffice);
    public Task<RentOfficeModel> UpdateRentOfficeAsync(RentOfficeModel rentOffice);
    public Task<RentOfficeModel?> GetRentOfficeAsync(Guid rentOfficeId);
    public Task DeleteRentOfficeAsync(RentOfficeModel rentOfficeModel);
    public Task<List<RentOfficeModel>> GetAllRentOfficesAsync();
}