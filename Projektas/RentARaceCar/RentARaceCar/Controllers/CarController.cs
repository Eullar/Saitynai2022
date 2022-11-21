using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentARaceCar.Enums;
using RentARaceCar.Extensions;
using RentARaceCar.Helpers;
using RentARaceCar.Interfaces.Services;
using RentARaceCar.Models;
using RentARaceCar.Models.Authentication;
using RentARaceCar.Models.DomainModels;
using RentARaceCar.Models.Requests.Car;

namespace RentARaceCar.Controllers;

[ApiController]
[Route("Api/RentOffices/{rentOfficeId}/Cars")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IRentOfficeService _rentOfficeService;

    public CarController(ICarService carService, IRentOfficeService rentOfficeService)
    {
        _carService = carService;
        _rentOfficeService = rentOfficeService;
    }

    [HttpPost(Name = "AddCar")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<Car>> AddCar(Guid rentOfficeId, AddCarRequest request)
    {
        if (rentOfficeId == Guid.Empty)
        {
            return BadRequest();
        }
        
        var rentOffice = await _rentOfficeService.GetRentOfficeAsync(rentOfficeId);

        if (rentOffice is null)
        {
            return NotFound("Rent Office not found");
        }
        
        var carModelObject = new CarModel
        {
            Manufacturer = request.Manufacturer,
            Model = request.Model,
            DrivetrainType = request.DrivetrainType,
            TransmissionType = request.TransmissionType,
            TyreCompound = request.TyreCompound,
            RentOfficeId = rentOfficeId
        };

        var carModel = await _carService.AddCarModelAsync(carModelObject);

        return Created("", carModel.ToCar());
    }

    [HttpGet("{carId:guid}",Name = "GetCar")]
    public async Task<ActionResult<Car>> GetCar(Guid rentOfficeId, Guid carId)
    {
        if (rentOfficeId == Guid.Empty || carId == Guid.Empty)
        {
            return BadRequest();
        }
        var rentOffice = await _rentOfficeService.GetRentOfficeAsync(rentOfficeId);

        if (rentOffice is null)
        {
            return NotFound("Rent Office not found");
        }

        var car = await _carService.GetCarModelAsync(rentOfficeId, carId);

        if (car is null)
        {
            return NotFound("Car not found");
        }

        return Ok(new {Resource = car.ToCar(), Links = CreateLinksForCars(rentOfficeId, carId)});
    }

    [HttpPut("{carId:guid}", Name = "UpdateCar")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<Car>> UpdateCar(Guid rentOfficeId, Guid carId, UpdateCarRequest request)
    {
        if (rentOfficeId == Guid.Empty || carId == Guid.Empty)
        {
            return BadRequest();
        }

        var rentOffice = await _rentOfficeService.GetRentOfficeAsync(rentOfficeId);

        if (rentOffice is null)
        {
            return NotFound("Rent Office not found");
        }

        var car = await _carService.GetCarModelAsync(rentOfficeId, carId);

        if (car is null)
        {
            return NotFound("Car not found");
        }

        car.Manufacturer = request.Manufacturer;
        car.Model = request.Model;
        car.DrivetrainType = request.DrivetrainType;
        car.TransmissionType = request.TransmissionType;
        car.TyreCompound = request.TyreCompound;

        return Ok(await _carService.UpdateCarModelAsync(car).ToCarAsync());
    }

    [HttpDelete("{carId:guid}", Name = "DeleteCar")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> DeleteCar(Guid rentOfficeId, Guid carId)
    {
        if (rentOfficeId == Guid.Empty || carId == Guid.Empty)
        {
            return BadRequest();
        }

        var rentOffice = await _rentOfficeService.GetRentOfficeAsync(rentOfficeId);

        if (rentOffice is null)
        {
            return NotFound("Rent Office not found");
        }

        var car = await _carService.GetCarModelAsync(rentOfficeId, carId);

        if (car is null)
        {
            return NotFound("Car not found");
        }
        
        if (car.Orders.Count > 0)
        {
            return Unauthorized("There are orders for this car");
        }

        await _carService.DeleteCarModelAsync(car);
        return NoContent();
    }

    [HttpGet(Name = "GetCars")]
    public async Task<ActionResult<List<Car>>> GetAllCars([FromQuery] PaginationParameters parameters, Guid rentOfficeId)
    {
        if (rentOfficeId == Guid.Empty)
        {
            return BadRequest();
        }

        var rentOffice = await _rentOfficeService.GetRentOfficeAsync(rentOfficeId);

        if (rentOffice is null)
        {
            return NotFound("Rent Office not found");
        }

        var carModels = await _carService.GetAllCarModelsAsync(rentOfficeId);

        var cars = PagedList<Car>.Create(carModels.Select(c => c.ToCar()).AsQueryable(),
            parameters.PageNumber, parameters.PageSize);

        var previousPageLink =
            cars.HasPrevious ? CreateResourceUri(parameters, ResourceUriTypes.PreviousPage) : null;
        
        var nextPageLink =
            cars.HasNext ? CreateResourceUri(parameters, ResourceUriTypes.NextPage) : null;

        var paginationMetadata = new
        {
            totalCount = cars.TotalCount,
            pageSize = cars.PageSize,
            currentPage = cars.CurrentPage,
            totalPages = cars.TotalPages,
            previousPageLink,
            nextPageLink
        };
        
        Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

        return cars;
    }
    
    private IEnumerable<Link> CreateLinksForCars(Guid rentOfficeId, Guid carId)
    {
#pragma warning disable CS8601
        yield return new Link { Href = Url.Link("GetCar", new { rentOfficeId, carId }), Rel = "self", Method = "GET" };
        yield return new Link { Href = Url.Link("DeleteCar", new { rentOfficeId, carId }), Rel = "self", Method = "DELETE" };
#pragma warning restore CS8601
    }

    private string? CreateResourceUri(PaginationParameters parameters, ResourceUriTypes type)
    {
        return type switch
        {
            ResourceUriTypes.PreviousPage => Url.Link("GetCars", new
            {
                pageNumber = parameters.PageNumber - 1,
                pageSize = parameters.PageSize
            }),
            ResourceUriTypes.NextPage => Url.Link("GetCars", new
            {
                pageNumber = parameters.PageNumber + 1,
                pageSize = parameters.PageSize
            }),
            _ => Url.Link("GetCars", new
            {
                pageNumber = parameters.PageNumber,
                pageSize = parameters.PageSize
            })
        };
    }
}