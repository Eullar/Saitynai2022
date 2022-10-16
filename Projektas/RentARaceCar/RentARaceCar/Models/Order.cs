﻿namespace RentARaceCar.Models;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RentDate { get; set; }
    public Guid CarId { get; set; }
}