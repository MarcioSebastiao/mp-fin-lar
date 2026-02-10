using Microsoft.AspNetCore.Mvc;

namespace MpFinLar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MainController : ControllerBase
{
    [HttpGet()]
    public ActionResult Get()
    {
        return Ok("Main");
    }
}
