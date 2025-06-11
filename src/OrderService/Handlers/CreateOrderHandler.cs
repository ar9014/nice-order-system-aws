using MediatR;
using OrderService.Commands;
using OrderService.Integration;
using OrderService.Messaging;
using OrderService.Models;

namespace OrderService.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly INotificationClient _notificationClient;
    private readonly IKafkaProducer _kafkaProducer;

    public CreateOrderHandler(INotificationClient notificationClient, IKafkaProducer kafkaProducer)
    {
        _notificationClient = notificationClient;
        _kafkaProducer = kafkaProducer;
    }
    
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

        await _notificationClient.NotifyAsync(order, cancellationToken);
        await _kafkaProducer.PublishOrderCreatedAsync(order, cancellationToken);

        return order;
    }
}
