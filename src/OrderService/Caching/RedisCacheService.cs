using StackExchange.Redis;
using System.Text.Json;

namespace OrderService.Caching;

public class RedisCacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task<OrderService.Models.Order?> GetOrderAsync(Guid orderId)
    {
        var cached = await _db.StringGetAsync(orderId.ToString());
        if (cached.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<OrderService.Models.Order>(cached!);
    }

    public async Task CacheOrderAsync(OrderService.Models.Order order)
    {
        var json = JsonSerializer.Serialize(order);
        await _db.StringSetAsync(order.OrderId.ToString(), json, TimeSpan.FromMinutes(5));
    }
}
