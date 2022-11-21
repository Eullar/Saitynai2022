using System.ComponentModel.DataAnnotations;

namespace RentARaceCar.Models.Requests.Authentication;

public class LoginUserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}