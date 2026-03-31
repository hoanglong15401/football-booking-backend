using Microsoft.AspNetCore.Mvc;

namespace FootballBooking.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Football Booking API is running");
    }
}

