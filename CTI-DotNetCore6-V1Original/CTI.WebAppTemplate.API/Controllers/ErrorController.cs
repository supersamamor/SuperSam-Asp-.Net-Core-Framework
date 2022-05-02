using Microsoft.AspNetCore.Mvc;

namespace CTI.WebAppTemplate.API.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error() => Problem();
}
