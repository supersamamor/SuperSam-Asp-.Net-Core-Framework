using CelerSoft.Common.Web.Utility.Extensions;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using static CelerSoft.Common.Web.Utility.Extensions.Common;

namespace CelerSoft.Common.Web.Utility.Extensions;

/// <summary>
/// Extension methods for converting an <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
/// </summary>
public static class EitherToActionResult
{
    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="BadRequestObjectResult"/> or <see cref="OkObjectResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<L, R>(this Either<L, R> either) =>
        either.Match(Ok, BadRequest);

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="BadRequestObjectResult"/> or <see cref="OkObjectResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either) =>
        either.Map(ToActionResult);

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="BadRequestObjectResult"/> or <see cref="OkObjectResult"/>.
    /// </summary>
    /// <param name="either"></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult(this Task<Either<Error, Task>> either) =>
        either.Bind(Match);

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="right">The function to execute if Right. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="left">The function to execute if Left. If this is null, return <see cref="BadRequestObjectResult"/></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<L, R>(this Either<L, R> either, Func<R, IActionResult>? right = null, Func<L, IActionResult>? left = null) =>
        either.Match(right ?? Ok, left ?? BadRequest);

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="right">The function to execute if Right. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="leftAsync">The function to execute if Left. If this is null, return <see cref="BadRequestObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<L, R>(this Either<L, R> either, Func<R, IActionResult>? right = null, Func<L, Task<IActionResult>>? leftAsync = null) =>
        either.MatchAsync(right ?? Ok, async l => leftAsync != null ? await leftAsync!(l) : BadRequest(l));

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="rightAsync">The function to execute if Right. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="left">The function to execute if Left. If this is null, return <see cref="BadRequestObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<L, R>(this Either<L, R> either, Func<R, Task<IActionResult>>? rightAsync = null, Func<L, IActionResult>? left = null) =>
        either.MatchAsync(async r => rightAsync != null ? await rightAsync!(r) : Ok(r), left ?? BadRequest);

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="rightAsync">The function to execute if Right. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="leftAsync">The function to execute if Left. If this is null, return <see cref="BadRequestObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<L, R>(this Either<L, R> either, Func<R, Task<IActionResult>>? rightAsync = null, Func<L, Task<IActionResult>>? leftAsync = null) =>
        either.MatchAsync(
            RightAsync: async r => rightAsync != null ? await rightAsync!(r) : Ok(r),
            LeftAsync: async l => leftAsync != null ? await leftAsync!(l) : BadRequest(l));

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="right">The function to execute if Right. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="left">The function to execute if Left. If this is null, return <see cref="BadRequestObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<R, IActionResult>? right = null, Func<L, IActionResult>? left = null) =>
        either.Map(e => e.ToActionResult(right, left));

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="right">The function to execute if Right. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="leftAsync">The function to execute if Left. If this is null, return <see cref="BadRequestObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<R, IActionResult>? right = null, Func<L, Task<IActionResult>>? leftAsync = null) =>
        either.MapAsync(async e => await e.ToActionResult(right, leftAsync));

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="rightAsync">The function to execute if Right. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="left">The function to execute if Left. If this is null, return <see cref="BadRequestObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<R, Task<IActionResult>>? rightAsync = null, Func<L, IActionResult>? left = null) =>
        either.MapAsync(async e => await e.ToActionResult(rightAsync, left));

    /// <summary>
    /// Converts <see cref="Either{L, R}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="rightAsync">The function to execute if Right. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="leftAsync">The function to execute if Left. If this is null, return <see cref="BadRequestObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<R, Task<IActionResult>>? rightAsync = null, Func<L, Task<IActionResult>>? leftAsync = null) =>
        either.MapAsync(async e => await e.ToActionResult(rightAsync, leftAsync));

    private async static Task<IActionResult> Match(Either<Error, Task> either) =>
        await either.MatchAsync(
            RightAsync: async t => { await t; return Ok(Unit.Default); },
            Left: e => BadRequest(e));
}