namespace RentARaceCar.Interfaces.Authentication;

public interface IJwtTokenService
{
    string CreateAccessToken(string userName, string userId, IEnumerable<string> userRoles);
}