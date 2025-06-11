using System.Text;
using System.Text.Json;
using OrderService.Models;
using Polly;
using Polly.Retry;

namespace OrderService.Integration;

public class NotificationClient : INotificationClient
{
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy _retryPolicy;

    public NotificationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));
    }

    public async Task NotifyAsync(Order order, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(order);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await _retryPolicy.ExecuteAsync(async () =>
        {
            var response = await _httpClient.PostAsync("http://localhost:7000/notify", content, cancellationToken);
            response.EnsureSuccessStatusCode();
        });
    }
}
