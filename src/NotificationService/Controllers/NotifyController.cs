using Microsoft.AspNetCore.Mvc;

namespace NotificationService.Controllers;

[ApiController]
[Route("notify")]
public class NotifyController : ControllerBase
{
    [HttpPost]
    public IActionResult ReceiveNotification([FromBody] object payload)
    {
        Console.WriteLine($"[NotificationService] Received: {payload}");
        return Ok(new { Message = "Notification received." });
    }
}
