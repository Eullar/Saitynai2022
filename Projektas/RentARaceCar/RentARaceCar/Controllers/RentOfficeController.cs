using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentARaceCar.Enums;
using RentARaceCar.Extensions;
using RentARaceCar.Helpers;
using RentARaceCar.Interfaces.Services;
using RentARaceCar.Models;
using RentARaceCar.Models.Authentication;
using RentARaceCar.Models.Requests.RentOffice;

namespace RentARaceCar.Controllers;

[ApiController]
[Route("Api/RentOffices")]
public class RentOfficeController : ControllerBase
{
    private readonly IRentOfficeService _rentOfficeService;

    public RentOfficeController(IRentOfficeService rentOfficeService) => 
        _rentOfficeService = rentOfficeService;

    [HttpPost(Name = "AddRentOffice")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<RentOffice>> AddRentOffice(AddRentOfficeRequest rentOffice)
    {
        var rentOfficeModel = await _rentOfficeService.AddRentOfficeAsync(rentOffice);
        
        return Created("",rentOfficeModel.ToRentOffice());
    }
    
    [HttpGet("{rentOfficeId:guid}", Name = "GetRentOffice")]
    public async Task<ActionResult<RentOffice>> GetRentOffice(Guid rentOfficeId)
    {
        if (rentOfficeId == Guid.Empty)
        {
            return BadRequest("Rent Office Id must be Guid");
        }

        var rentOffice = await _rentOfficeService.GetRentOfficeAsync(rentOfficeId);

        if (rentOffice == null)
        {
            return NotFound("Rent Office not found");
        }

        return Ok(new {Resource = rentOffice.ToRentOffice(), Links = CreateLinksForRentOffices(rentOfficeId)});
    }

    [HttpPut("{rentOfficeId:guid}", Name = "UpdateRentOffice")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<RentOffice>> UpdateRentOffice(Guid rentOfficeId, UpdateRentOfficeRequest request)
    {
        if (rentOfficeId == Guid.Empty)
        {
            return BadRequest("Rent Office Id must be Guid");
        }

        var rentOffice = await _rentOfficeService.GetRentOfficeAsync(rentOfficeId);
        
        if (rentOffice is null)
        {
            return NotFound("Rent Office not found");
        }

        rentOffice.Name = request.Name;
        rentOffice.Location = request.Location;

        return Ok(await _rentOfficeService.UpdateRentOfficeAsync(rentOffice)!.ToRentOfficeAsync());
    }
    
    [HttpDelete("{rentOfficeId:guid}", Name = "DeleteRentOffice")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> DeleteRentOffice(Guid rentOfficeId)
    {
        if (rentOfficeId == Guid.Empty)
        {
            return BadRequest("Rent Office Id must be Guid");
        }

        var rentOffice = await _rentOfficeService.GetRentOfficeAsync(rentOfficeId);

        if (rentOffice is null)
        {
            return NotFound("Rent Office not found");
        }

        if (rentOffice.Cars.Count > 0)
        {
            return Unauthorized("There are cars inside the rent office");
        }
        
        await _rentOfficeService.DeleteRentOfficeAsync(rentOffice);
        return NoContent();
    }

    [HttpGet(Name = "GetRentOffices")]
    public async Task<ActionResult<List<RentOffice>>> GetAllRentOffices([FromQuery] PaginationParameters parameters)
    {
        var rentOfficeModels = await _rentOfficeService.GetAllRentOfficesAsync();

        if (rentOfficeModels.Count == 0)
        {
            return Ok(new List<RentOffice>());
        }

        var rentOffices = PagedList<RentOffice>.Create(rentOfficeModels.Select(r => r.ToRentOffice()!).AsQueryable(),
            parameters.PageNumber, parameters.PageSize);

        var previousPageLink =
            rentOffices.HasPrevious ? CreateResourceUri(parameters, ResourceUriTypes.PreviousPage) : null;
        
        var nextPageLink =
            rentOffices.HasNext ? CreateResourceUri(parameters, ResourceUriTypes.NextPage) : null;

        var paginationMetadata = new
        {
            totalCount = rentOffices.TotalCount,
            pageSize = rentOffices.PageSize,
            currentPage = rentOffices.CurrentPage,
            totalPages = rentOffices.TotalPages,
            previousPageLink,
            nextPageLink
        };
        
        Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

        return rentOffices;
    }

    private IEnumerable<Link> CreateLinksForRentOffices(Guid rentOfficeId)
    {
#pragma warning disable CS8601
        yield return new Link { Href = Url.Link("GetRentOffice", new { rentOfficeId }), Rel = "self", Method = "GET" };
        yield return new Link { Href = Url.Link("DeleteRentOffice", new { rentOfficeId }), Rel = "self", Method = "DELETE" };
#pragma warning restore CS8601
    }

    private string? CreateResourceUri(PaginationParameters parameters, ResourceUriTypes type)
    {
        return type switch
        {
            ResourceUriTypes.PreviousPage => Url.Link("GetRentOffices", new
            {
                pageNumber = parameters.PageNumber - 1,
                pageSize = parameters.PageSize
            }),
            ResourceUriTypes.NextPage => Url.Link("GetRentOffices", new
            {
                pageNumber = parameters.PageNumber + 1,
                pageSize = parameters.PageSize
            }),
            _ => Url.Link("GetRentOffices", new
            {
                pageNumber = parameters.PageNumber,
                pageSize = parameters.PageSize
            })
        };
    }
}