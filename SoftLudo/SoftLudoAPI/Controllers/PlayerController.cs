using Microsoft.AspNetCore.Mvc;

namespace SoftLudoApi.Controllers;

[Route("[controller]s")]
[ApiController]
public class PlayerController : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
