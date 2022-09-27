namespace RentARaceCar.Models;

public class RentOffice
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    
    public virtual List<Car> Cars { get; set; }
}