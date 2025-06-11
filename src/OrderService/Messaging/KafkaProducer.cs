using Confluent.Kafka;
using OrderService.Models;
using System.Text.Json;

namespace OrderService.Messaging;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;
    private const string Topic = "orders.created";

    public KafkaProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishOrderCreatedAsync(Order order, CancellationToken cancellationToken)
    {
        var message = new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(new
            {
                OrderId = order.OrderId,
                Timestamp = order.Timestamp
            })
        };

        await _producer.ProduceAsync(Topic, message, cancellationToken);
        Console.WriteLine($"[Kafka] Published order {order.OrderId}");
    }
}
