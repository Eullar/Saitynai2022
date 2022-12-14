using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RentARaceCar.Enums;
using RentARaceCar.Extensions;
using RentARaceCar.Helpers;
using RentARaceCar.Interfaces.Services;
using RentARaceCar.Models;
using RentARaceCar.Models.Authentication;
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
    private readonly IAuthorizationService _authorizationServiceService;

    public OrderController(
        IRentOfficeService rentOfficeService, 
        ICarService carService, 
        IOrderService orderService,
        IAuthorizationService authorizationServiceService)
    {
        _rentOfficeService = rentOfficeService;
        _carService = carService;
        _orderService = orderService;
        _authorizationServiceService = authorizationServiceService;
    }
    
    [HttpPost(Name = "AddOrder")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.User}")]
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
            RentDate = request.RentDate,
            UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
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

        return Ok(order.ToOrder());
    }

    [HttpPut("{orderId:guid}", Name = "UpdateOrder")]
    [Authorize(Roles = $"{Roles.Admin},{Roles.User}")]
    public async Task<ActionResult<Order>> UpdateOrder(Guid rentOfficeId, Guid carId, Guid orderId,
        UpdateOrderRequest request)
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

        if (!User.IsInRole(Roles.Admin))
        {
            var authorizationResult =
                await _authorizationServiceService.AuthorizeAsync(User, order, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }   
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
        var authorizationResult =
                await _authorizationServiceService.AuthorizeAsync(User, order, PolicyNames.ResourceOwner);
        if (!authorizationResult.Succeeded)
        {
            return Forbid();
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
        var orders = new List<Order>();
        foreach (var order in orderModels)
        {
            if ((await _authorizationServiceService.AuthorizeAsync(User, order, PolicyNames.ResourceOwner)).Succeeded)
            {
                orders.Add(order.ToOrder());
            }
        }

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