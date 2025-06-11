using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Caching;
using OrderService.Commands;
using OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly RedisCacheService _cacheService;

    public OrdersController(IMediator mediator, RedisCacheService cacheService)
    {
        _mediator = mediator;
        _cacheService = cacheService;
    }

    // POST /orders
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var createdOrder = await _mediator.Send(command);
        await _cacheService.CacheOrderAsync(createdOrder);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
    }

    // GET /orders/{id} (mock implementation for now)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
{
    var cached = await _cacheService.GetOrderAsync(id);
    if (cached != null)
    {
        Console.WriteLine($"[Redis] Cache hit for {id}");
        return Ok(cached);
    }

    Console.WriteLine($"[Redis] Cache miss for {id}");
    return NotFound(new { Message = $"Order {id} not found in cache." });
}
}
