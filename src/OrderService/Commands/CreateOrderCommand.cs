using MediatR;
using OrderService.Models;

namespace OrderService.Commands;

public class CreateOrderCommand : IRequest<Order>
{
    public Guid CustomerId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}
