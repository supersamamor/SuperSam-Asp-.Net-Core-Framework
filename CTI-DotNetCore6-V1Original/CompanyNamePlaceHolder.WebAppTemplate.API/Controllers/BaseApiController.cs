using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyNamePlaceHolder.WebAppTemplate.API.Controllers;

[ApiConventionType(typeof(DefaultApiConventions))]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BaseApiController<T> : ControllerBase
{
    private IMediator? _mediatorInstance;
    private IMapper? _mapperInstance;
    private ILogger<T>? _loggerInstance;
    protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>()!;
    protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>()!;
    protected ILogger<T> Logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>()!;

    protected ActionResult<A> ToActionResult<A>(Func<Option<A>> option) =>
        option().Match<ActionResult<A>>(some => some, () => NotFound());

    protected async Task<ActionResult<A>> ToActionResult<A>(Func<Task<Option<A>>> option) =>
        await option().Map(x => ToActionResult(() => x));

    protected ActionResult<A> ToActionResult<A>(Func<Validation<Error, A>> validation) =>
        validation().Match<ActionResult<A>>(
            succ => succ,
            errors =>
            {
                errors.Iter(error => ModelState.AddModelError("error", error.ToString()));
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Detail = "One or more validation errors occured.",
                    Instance = HttpContext.Request.Path
                };
                var traceId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
                if (traceId != null)
                {
                    problemDetails.Extensions["traceId"] = traceId;
                }
                return BadRequest(problemDetails);
            });

    protected async Task<ActionResult<A>> ToActionResult<A>(Func<Task<Validation<Error, A>>> validation) =>
        await validation().Map(x => ToActionResult(() => x));
}
