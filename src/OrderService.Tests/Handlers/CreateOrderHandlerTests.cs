using Xunit;
using Moq;
using FluentAssertions;
using OrderService.Commands;
using OrderService.Handlers;
using OrderService.Integration;
using OrderService.Messaging;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Tests.Handlers;

public class CreateOrderHandlerTests
{
    private readonly Mock<INotificationClient> _notificationMock;
    private readonly Mock<IKafkaProducer> _kafkaMock;
    private readonly CreateOrderHandler _handler;

    public CreateOrderHandlerTests()
    {
        _notificationMock = new Mock<INotificationClient>();
        _kafkaMock = new Mock<IKafkaProducer>();

        _handler = new CreateOrderHandler(_notificationMock.Object, _kafkaMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnOrder_AndTriggerNotificationAndKafka()
    {
        // Arrange
        var command = new CreateOrderCommand
        {
            CustomerId = Guid.NewGuid(),
            Items = new List<OrderItem>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.CustomerId.Should().Be(command.CustomerId);
        result.Items.Should().HaveCount(1);
        result.Status.Should().Be(OrderStatus.Pending);

        _notificationMock.Verify(n => n.NotifyAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        _kafkaMock.Verify(k => k.PublishOrderCreatedAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
