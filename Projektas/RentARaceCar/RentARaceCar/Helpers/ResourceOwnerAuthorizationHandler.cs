using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using RentARaceCar.Interfaces.Authentication;
using RentARaceCar.Models.Authentication;

namespace RentARaceCar.Helpers;

public class ResourceOwnerAuthorizationHandler : AuthorizationHandler<ResourceOwnerRequirement, IUserOwnedResource>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOwnerRequirement requirement,
        IUserOwnedResource resource)
    {
        if (context.User.IsInRole(Roles.Admin) ||
            context.User.FindFirstValue(JwtRegisteredClaimNames.Sub) == resource.UserId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public record ResourceOwnerRequirement : IAuthorizationRequirement;