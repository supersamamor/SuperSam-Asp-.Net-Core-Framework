using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.Common.API.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error() => Problem();
}
