using RentARaceCar.Models;

namespace RentARaceCar.Interfaces.Services;

public interface IRentOfficeService
{
    public Guid AddRentOffice(RentOffice rentOffice);
    public RentOffice UpdateRentOffice(RentOffice rentOffice);
    public RentOffice GetRentOffice(Guid rentOfficeId);
    public bool DeleteRentOffice(Guid rentOfficeId);
    public List<RentOffice> GetAllRentOffices();
}