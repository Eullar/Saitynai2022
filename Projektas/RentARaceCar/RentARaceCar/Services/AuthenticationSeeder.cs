using Microsoft.AspNetCore.Identity;
using RentARaceCar.Models.Authentication;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Services;

public class AuthenticationSeeder
{
    private readonly UserManager<UserModel> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthenticationSeeder(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        await AddRolesAsync();
        await AddAdminAccountAsync();
    }

    private async Task AddAdminAccountAsync()
    {
        var adminUser = new UserModel
        {
            UserName = "Admin",
            Email = "admin@RentARaceCar.com"
        };

        var existingUser = await _userManager.FindByNameAsync(adminUser.UserName);
        if (existingUser is null)
        {
            var createdUser = await _userManager.CreateAsync(adminUser, "FastRacecars1~");
            if (createdUser.Succeeded)
            {
                await _userManager.AddToRolesAsync(adminUser, Roles.All);
            }
        }
    }

    private async Task AddRolesAsync()
    {
        foreach (var role in Roles.All)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}