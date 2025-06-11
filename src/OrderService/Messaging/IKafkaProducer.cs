using OrderService.Models;

namespace OrderService.Messaging;

public interface IKafkaProducer
{
    Task PublishOrderCreatedAsync(Order order, CancellationToken cancellationToken);
}
