using Microsoft.AspNetCore.Identity;

namespace RentARaceCar.Models.Authentication;

public class User
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}