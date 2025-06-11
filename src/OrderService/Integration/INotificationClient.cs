using OrderService.Models;

namespace OrderService.Integration;

public interface INotificationClient
{
    Task NotifyAsync(Order order, CancellationToken cancellationToken);
}
