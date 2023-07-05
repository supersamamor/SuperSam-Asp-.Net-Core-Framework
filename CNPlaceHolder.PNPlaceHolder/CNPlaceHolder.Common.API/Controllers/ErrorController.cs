using Microsoft.AspNetCore.Mvc;

namespace CNPlaceHolder.Common.API.Controllers;

/// <summary>
/// The controller for the default error handler.
/// </summary>
[ApiController]
public class ErrorController : ControllerBase
{
    /// <summary>
    /// The default API error handler. Produces a <see cref="ProblemDetails"/> response.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error() => Problem();
}
