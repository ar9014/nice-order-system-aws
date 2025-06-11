using MediatR;
using OrderService.Commands;
using OrderService.Models;

namespace OrderService.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Order>
{
    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerId = request.CustomerId,
            Items = request.Items,
            Timestamp = DateTime.UtcNow,
            Status = OrderStatus.Pending
        };

        // TODO: persist to DB or simulate storage
        Console.WriteLine($"[Handler] Created order {order.OrderId}");

        return await Task.FromResult(order);
    }
}
