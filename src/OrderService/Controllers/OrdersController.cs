using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Commands;
using OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST /orders
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var createdOrder = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
    }

    // GET /orders/{id} (mock implementation for now)
    [HttpGet("{id}")]
    public IActionResult GetOrderById(Guid id)
    {
        // Real implementation will use Redis (coming soon)
        return Ok(new { Message = $"Mock response for order {id}" });
    }
}
