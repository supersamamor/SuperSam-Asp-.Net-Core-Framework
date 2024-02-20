using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CTI.Common.API.Controllers;


/// <summary>
/// A base class for an API controller. Defines default route and other common attributes.
/// Gets IMediator, IMapper, and ILogger from DI container.
/// </summary>
/// <typeparam name="T">Type of the controller extending this base class. Passed to ILogger.</typeparam>
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public abstract class BaseApiController<T> : ControllerBase
{
    private IMediator? _mediatorInstance;
    private IMapper? _mapperInstance;
    private ILogger<T>? _loggerInstance;
    /// <summary>
    /// Instance of <see cref="IMediator"/>
    /// </summary>
    protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>()!;
    /// <summary>
    /// Instance of <see cref="IMapper"/>
    /// </summary>
    protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>()!;
    /// <summary>
    /// Instance of <see cref="ILogger"/>
    /// </summary>
    protected ILogger<T> Logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>()!;

    /// <summary>
    /// Converts output of a function returning an <see cref="Option{A}"/> to an <see cref="ActionResult"/>&lt;<typeparamref name="A"/>&gt;.
    /// Returns <see cref="NotFoundResult"/> if Option is None.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <param name="option">A function returning an <see cref="Option{A}"/></param>
    /// <returns><see cref="ActionResult"/>&lt;<typeparamref name="A"/>&gt;</returns>
    protected ActionResult<A> ToActionResult<A>(Func<Option<A>> option) =>
        option().Match<ActionResult<A>>(some => some, () => NotFound());

    /// <summary>
    /// Converts output of an async function returning an <see cref="Option{A}"/> to an <see cref="ActionResult"/>&lt;<typeparamref name="A"/>&gt;.
    /// Returns <see cref="NotFoundResult"/> if Option is None.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <param name="option">An async function returning an <see cref="Option{A}"/></param>
    /// <returns><see cref="ActionResult"/>&lt;<typeparamref name="A"/>&gt;</returns>
    protected async Task<ActionResult<A>> ToActionResult<A>(Func<Task<Option<A>>> option) =>
        await option().Map(x => ToActionResult(() => x));

    /// <summary>
    /// Converts output of a function returning a <see cref="Validation"/>&lt;<see cref="Error"/>, <typeparamref name="A"/>&gt; 
    /// to an ActionResult&lt;<typeparamref name="A"/>&gt;. If status of Validation is FAIL, returns a
    /// <see cref="BadRequestObjectResult"/>. Validation errors are placed in <see cref="ValidationProblemDetails"/> object.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <param name="validation">A function returning a <see cref="Validation"/>&lt;<see cref="Error"/>, <typeparamref name="A"/>&gt;</param>
    /// <returns><see cref="ActionResult"/>&lt;<typeparamref name="A"/>&gt;</returns>
    protected ActionResult<A> ToActionResult<A>(Func<Validation<Error, A>> validation) =>
        validation().Match<ActionResult<A>>(
            succ => succ,
            errors => BadRequest(GetProblemDetails(errors)));

    /// <summary>
    /// Converts output of an async function returning a <see cref="Validation"/>&lt;<see cref="Error"/>, <typeparamref name="A"/>&gt; 
    /// to an ActionResult&lt;<typeparamref name="A"/>&gt;. If status of Validation is FAIL, returns a
    /// <see cref="BadRequestObjectResult"/>. Validation errors are placed in <see cref="ValidationProblemDetails"/> object.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <param name="validation">An async function returning a <see cref="Validation"/>&lt;<see cref="Error"/>, <typeparamref name="A"/>&gt;</param>
    /// <returns><see cref="ActionResult"/>&lt;<typeparamref name="A"/>&gt;</returns>
    protected async Task<ActionResult<A>> ToActionResult<A>(Func<Task<Validation<Error, A>>> validation) =>
        await validation().Map(x => ToActionResult(() => x));

    private ValidationProblemDetails GetProblemDetails(Seq<Error> errors)
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

        return problemDetails;
    }
}
