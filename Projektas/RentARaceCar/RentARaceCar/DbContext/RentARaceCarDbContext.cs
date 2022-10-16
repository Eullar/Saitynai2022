using Microsoft.EntityFrameworkCore;
using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.DbContext;

public class RentARaceCarDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<CarModel> Cars { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<RentOfficeModel> RentOffices { get; set; }

    public RentARaceCarDbContext(DbContextOptions<RentARaceCarDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<RentOfficeModel>()
            .ToTable("RentOffices");

        modelBuilder
            .Entity<CarModel>()
            .ToTable("Cars")
            .HasOne(c => c.RentOffice)
            .WithMany(r => r.Cars)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<CarModel>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Car);

        modelBuilder
            .Entity<OrderModel>()
            .ToTable("Orders")
            .HasOne(o => o.Car)
            .WithMany(c => c.Orders)
            .OnDelete(DeleteBehavior.NoAction);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "server=localhost;user=root;password=password;database=RentARaceCar";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}