using CompanyPL.Common.Web.Utility.Extensions;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using static CompanyPL.Common.Web.Utility.Extensions.Common;

namespace CompanyPL.Common.Web.Utility.Extensions;

/// <summary>
/// Extension methods for converting <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>
/// </summary>
public static class ValidationToActionResult
{
    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="OkObjectResult"/> or <see cref="BadRequestObjectResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validation"></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this Validation<Error, T> validation) =>
        validation.Match(Ok, BadRequest);

    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="OkObjectResult"/> or <see cref="BadRequestObjectResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validation"></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Validation<Error, T>> validation) =>
        validation.Map(ToActionResult);

    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="OkObjectResult"/> or <see cref="BadRequestObjectResult"/>.
    /// </summary>
    /// <param name="validation"></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult(this Task<Validation<Error, Task>> validation) =>
        validation.Bind(ToActionResult);

    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validation"></param>
    /// <param name="success">The function to execute if SUCCESS. If this is null, return <see cref="OkObjectResult"/>.</param>
    /// <param name="fail">The function to execute if FAIL. If this is null, return <see cref="BadRequestObjectResult"/>.</param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this Validation<Error, T> validation, Func<T, IActionResult>? success = null, Func<Seq<Error>, IActionResult>? fail = null) =>
        validation.Match(success ?? Ok, fail ?? BadRequest);

    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validation"></param>
    /// <param name="successAsync">The function to execute if SUCCESS. If this is null, return <see cref="OkObjectResult"/>.</param>
    /// <param name="fail">The function to execute if FAIL. If this is null, return <see cref="BadRequestObjectResult"/>.</param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Validation<Error, T> validation, Func<T, Task<IActionResult>>? successAsync = null, Func<Seq<Error>, IActionResult>? fail = null) =>
        validation.MatchAsync(async t => successAsync != null ? await successAsync!(t) : Ok(t), fail ?? BadRequest);

    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validation"></param>
    /// <param name="successAsync">The function to execute if SUCCESS. If this is null, return <see cref="OkObjectResult"/>.</param>
    /// <param name="failAsync">The function to execute if FAIL. If this is null, return <see cref="BadRequestObjectResult"/>.</param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Validation<Error, T> validation, Func<T, Task<IActionResult>>? successAsync = null, Func<Seq<Error>, Task<IActionResult>>? failAsync = null) =>
        validation.MatchAsync(
            SuccAsync: async t => successAsync != null ? await successAsync!(t) : Ok(t),
            FailAsync: async e => failAsync != null ? await failAsync!(e) : BadRequest(e));

    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validation"></param>
    /// <param name="success">The function to execute if SUCCESS. If this is null, return <see cref="OkObjectResult"/>.</param>
    /// <param name="fail">The function to execute if FAIL. If this is null, return <see cref="BadRequestObjectResult"/>.</param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Validation<Error, T>> validation, Func<T, IActionResult>? success = null, Func<Seq<Error>, IActionResult>? fail = null) =>
        validation.Map(v => v.ToActionResult(success, fail));

    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validation"></param>
    /// <param name="successAsync">The function to execute if SUCCESS. If this is null, return <see cref="OkObjectResult"/>.</param>
    /// <param name="fail">The function to execute if FAIL. If this is null, return <see cref="BadRequestObjectResult"/>.</param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Validation<Error, T>> validation, Func<T, Task<IActionResult>>? successAsync = null, Func<Seq<Error>, IActionResult>? fail = null) =>
        validation.MapAsync(async v => await v.ToActionResult(successAsync, fail));

    /// <summary>
    /// Converts <see cref="Validation{FAIL, SUCCESS}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validation"></param>
    /// <param name="successAsync">The function to execute if SUCCESS. If this is null, return <see cref="OkObjectResult"/>.</param>
    /// <param name="failAsync">The function to execute if FAIL. If this is null, return <see cref="BadRequestObjectResult"/>.</param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Validation<Error, T>> validation, Func<T, Task<IActionResult>>? successAsync = null, Func<Seq<Error>, Task<IActionResult>>? failAsync = null) =>
        validation.MapAsync(async v => await v.ToActionResult(successAsync, failAsync));

    private static Task<IActionResult> ToActionResult(Validation<Error, Task> validation) =>
        validation.MatchAsync(
            SuccAsync: async t => { await t; return Ok(Unit.Default); },
            Fail: e => BadRequest(e));
}