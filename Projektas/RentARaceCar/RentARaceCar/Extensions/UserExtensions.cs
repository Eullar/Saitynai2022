using RentARaceCar.Models.Authentication;
using RentARaceCar.Models.DomainModels;

namespace RentARaceCar.Extensions;

public static class UserExtensions
{
    public static User ToUser(this UserModel userModel) =>
        new()
        {
            Username = userModel.UserName,
            Email = userModel.Email,
            Id = userModel.Id
        };

    public static SuccessfulLogin ToLogin(this string token) =>
        new()
        {
            AuthenticationToken = token
        };
}