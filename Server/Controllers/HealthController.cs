using Microsoft.AspNetCore.Mvc;

namespace Closavy.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet(Name = "HealthCheck")]
    public IActionResult Get()
    {
        return new OkObjectResult("Healthy");
    }
}
