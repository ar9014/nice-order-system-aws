using System;

namespace OrderService.Models;

public class OrderItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
