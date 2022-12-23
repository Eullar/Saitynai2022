using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentARaceCar.Extensions;
using RentARaceCar.Interfaces.Authentication;
using RentARaceCar.Models.Authentication;
using RentARaceCar.Models.DomainModels;
using RentARaceCar.Models.Requests.Authentication;

namespace RentARaceCar.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticationController(UserManager<UserModel> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register(RegisterUserRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is not null)
        {
            return BadRequest($"User with this username already exists");
        }
        user = await _userManager.FindByEmailAsync(request.Email);
        if (user is not null)
        {
            return BadRequest("User with this email already exists");
        }

        var newUser = new UserModel
        {
            Email = request.Email,
            UserName = request.Username
        };
        var createdUser = await _userManager.CreateAsync(newUser, request.Password);
        if (!createdUser.Succeeded)
        {
            return BadRequest("Could not create user");
        }

        await _userManager.AddToRoleAsync(newUser, Roles.User);

        return CreatedAtAction(nameof(Register), newUser.ToUser());
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login(LoginUserRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null)
        {
            return BadRequest("Username or password is invalid");
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            return BadRequest("Username or password is invalid");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

        return Ok(new
        {
            authenticationToken = accessToken.ToLogin().AuthenticationToken,
            roles
        });
    }
}