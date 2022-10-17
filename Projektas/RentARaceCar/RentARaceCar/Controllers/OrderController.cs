using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RentARaceCar.Enums;
using RentARaceCar.Extensions;
using RentARaceCar.Helpers;
using RentARaceCar.Interfaces.Services;
using RentARaceCar.Models;
using RentARaceCar.Models.DomainModels;
using RentARaceCar.Models.Requests.Order;

namespace RentARaceCar.Controllers;

[ApiController]
[Route("Api/RentOffices/{rentOfficeId}/Cars/{carId}/Orders")]
public class OrderController : ControllerBase
{
    private readonly IRentOfficeService _rentOfficeService;
    private readonly ICarService _carService;
    private readonly IOrderService _orderService;

    public OrderController(IRentOfficeService rentOfficeService, ICarService carService, IOrderService orderService)
    {
        _rentOfficeService = rentOfficeService;
        _carService = carService;
        _orderService = orderService;
    }
    
    [HttpPost(Name = "AddOrder")]
    public async Task<ActionResult<Order>> AddOrder(Guid rentOfficeId, Guid carId, AddOrderRequest request)
    {
        if (carId == Guid.Empty || rentOfficeId == Guid.Empty)
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

        var orderModel = new OrderModel
        {
            CarId = carId,
            OrderDate = DateTime.UtcNow,
            RentDate = request.RentDate.Date
        };

        var order = await _orderService.AddOrderModelAsync(orderModel);

        return Created("", order.ToOrder());
    }
    
    [HttpGet("{orderId:guid}", Name = "GetOrder")]
    public async Task<ActionResult<Order>> GetOrder(Guid rentOfficeId, Guid carId, Guid orderId)
    {
        if (rentOfficeId == Guid.Empty || carId == Guid.Empty || orderId == Guid.Empty)
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

        var order = await _orderService.GetOrderModelAsync(carId, orderId);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(new {Resource = order.ToOrder(), Links = CreateLinksForOrders(rentOfficeId, carId, orderId)});
    }
    
    [HttpPut("{orderId:guid}", Name = "UpdateOrder")]
    public async Task<ActionResult<Order>> UpdateOrder(Guid rentOfficeId, Guid carId, Guid orderId, UpdateOrderRequest request)
    {
        if (rentOfficeId == Guid.Empty || carId == Guid.Empty || orderId == Guid.Empty)
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

        var order = await _orderService.GetOrderModelAsync(carId, orderId);

        if (order is null)
        {
            return NotFound();
        }
        
        order.RentDate = request.RentDate;

        return Ok(await _orderService.UpdateOrderModelAsync(order).ToOrderAsync());
    }
    
    [HttpDelete("{orderId:guid}", Name = "DeleteOrder")]
    public async Task<ActionResult> DeleteOrder(Guid rentOfficeId, Guid carId, Guid orderId)
    {
        if (rentOfficeId == Guid.Empty || carId == Guid.Empty || orderId == Guid.Empty)
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

        var order = await _orderService.GetOrderModelAsync(carId, orderId);

        if (order is null)
        {
            return NotFound();
        }

        await _orderService.DeleteOrderModelAsync(order);
        return NoContent();
    }
    
    [HttpGet(Name = "GetOrders")]
    public async Task<ActionResult<List<Order>>> GetAllOrders([FromQuery] PaginationParameters parameters, Guid rentOfficeId, Guid carId)
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

        var orderModels = await _orderService.GetAllOrderModelsAsync(carId);

        var orders = PagedList<Order>.Create(orderModels.Select(c => c.ToOrder()).AsQueryable(),
            parameters.PageNumber, parameters.PageSize);

        var previousPageLink =
            orders.HasPrevious ? CreateResourceUri(parameters, ResourceUriTypes.PreviousPage) : null;
        
        var nextPageLink =
            orders.HasNext ? CreateResourceUri(parameters, ResourceUriTypes.NextPage) : null;

        var paginationMetadata = new
        {
            totalCount = orders.TotalCount,
            pageSize = orders.PageSize,
            currentPage = orders.CurrentPage,
            totalPages = orders.TotalPages,
            previousPageLink,
            nextPageLink
        };
        
        Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

        return orders;
    }
    
    private IEnumerable<Link> CreateLinksForOrders(Guid rentOfficeId, Guid carId, Guid orderId)
    {
#pragma warning disable CS8601
        yield return new Link { Href = Url.Link("GetOrder", new { rentOfficeId, carId, orderId }), Rel = "self", Method = "GET" };
        yield return new Link { Href = Url.Link("DeleteOrder", new { rentOfficeId, carId, orderId }), Rel = "self", Method = "DELETE" };
#pragma warning restore CS8601
    }

    private string? CreateResourceUri(PaginationParameters parameters, ResourceUriTypes type)
    {
        return type switch
        {
            ResourceUriTypes.PreviousPage => Url.Link("GetOrders", new
            {
                pageNumber = parameters.PageNumber - 1,
                pageSize = parameters.PageSize
            }),
            ResourceUriTypes.NextPage => Url.Link("GetOrders", new
            {
                pageNumber = parameters.PageNumber + 1,
                pageSize = parameters.PageSize
            }),
            _ => Url.Link("GetOrders", new
            {
                pageNumber = parameters.PageNumber,
                pageSize = parameters.PageSize
            })
        };
    }
}