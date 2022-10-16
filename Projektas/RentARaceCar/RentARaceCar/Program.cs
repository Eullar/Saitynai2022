using Microsoft.EntityFrameworkCore;
using RentARaceCar.DbContext;
using RentARaceCar.Interfaces.Services;
using RentARaceCar.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = "server=localhost;user=root;password=password;database=RentARaceCar";

builder.Services.AddControllers();

builder.Services.AddDbContext<RentARaceCarDbContext>(dbContextOptions =>
    dbContextOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

builder.Services.AddScoped<IRentOfficeService, RentOfficeService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllers();

app.UseAuthorization();

app.Run();