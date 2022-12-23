using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.DbContext;

public class RentARaceCarDbContext : IdentityDbContext<UserModel>
{
    private readonly IConfiguration _configuration;
    public DbSet<CarModel> Cars { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<RentOfficeModel> RentOffices { get; set; }

    public RentARaceCarDbContext(IConfiguration configuration, DbContextOptions<RentARaceCarDbContext> dbContextOptions)
        : base(dbContextOptions) =>
        _configuration = configuration;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
        var connectionString = _configuration.GetValue<string>("RentARaceCarConnectionString");
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}