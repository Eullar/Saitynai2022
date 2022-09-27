using Microsoft.EntityFrameworkCore;
using RentARaceCar.Models;

namespace RentARaceCar.DbContext;

public class RentARaceCarDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<RentOffice> RentOffices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "server=localhost;user=root;password=password;database=RentARaceCar";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}