namespace RentARaceCar.Models.Authentication;

public class Roles
{
    public const string Admin = nameof(Admin);
    public const string User = nameof(User);

    public static IReadOnlyCollection<string> All = new[] { Admin, User };
}