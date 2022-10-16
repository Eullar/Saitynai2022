namespace RentARaceCar.Helpers;

public class PaginationParameters
{
    private int _pageSize = 4;
    private const int MaxPageSize = 20;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}