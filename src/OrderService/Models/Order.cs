using System;
using System.Collections.Generic;

namespace OrderService.Models;

public class Order
{
    public Guid OrderId { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}
