namespace RentARaceCar.Models.DomainModels;

public class RentOfficeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    
    public List<CarModel> Cars { get; set; }
}