using CTI.Common.Web.Utility.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using static CTI.Common.Web.Utility.Extensions.Common;

namespace CTI.Common.Web.Utility.Extensions;

/// <summary>
/// Extension methods for converting an <see cref="Option{A}"/> to <see cref="IActionResult"/>.
/// </summary>
public static class OptionToActionResult
{
    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="OkObjectResult"/> or <see cref="NotFoundObjectResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this Option<T> option) =>
        option.Match(Ok, NotFound);

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="OkObjectResult"/> or <see cref="NotFoundObjectResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option) =>
        option.Map(ToActionResult);

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <param name="some">The function to execute if Some. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="none">The function to execute if None. If this is null, return <see cref="NotFoundObjectResult"/></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this Option<T> option, Func<T, IActionResult>? some = null, Func<IActionResult>? none = null) =>
        option.Match(some ?? Ok, none ?? NotFound);

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <param name="someAsync">The function to execute if Some. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="none">The function to execute if None. If this is null, return <see cref="NotFoundObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Option<T> option, Func<T, Task<IActionResult>>? someAsync = null, Func<IActionResult>? none = null) =>
        option.MatchAsync(async t => someAsync != null ? await someAsync!(t) : Ok(t), none ?? NotFound);

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <param name="some">The function to execute if Some. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="noneAsync">The function to execute if None. If this is null, return <see cref="NotFoundObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Option<T> option, Func<T, IActionResult>? some = null, Func<Task<IActionResult>>? noneAsync = null) =>
        option.MatchAsync(some ?? Ok, async () => noneAsync != null ? await noneAsync!() : NotFound());

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <param name="someAsync">The function to execute if Some. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="noneAsync">The function to execute if None. If this is null, return <see cref="NotFoundObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Option<T> option, Func<T, Task<IActionResult>>? someAsync = null, Func<Task<IActionResult>>? noneAsync = null) =>
        option.MatchAsync(
            Some: async t => someAsync != null ? await someAsync!(t) : Ok(t),
            None: async () => noneAsync != null ? await noneAsync!() : NotFound());

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <param name="some">The function to execute if Some. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="none">The function to execute if None. If this is null, return <see cref="NotFoundObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, IActionResult>? some = null, Func<IActionResult>? none = null) =>
        option.Map(o => o.ToActionResult(some, none));

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <param name="someAsync">The function to execute if Some. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="none">The function to execute if None. If this is null, return <see cref="NotFoundObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, Task<IActionResult>>? someAsync = null, Func<IActionResult>? none = null) =>
        option.MapAsync(async o => await o.ToActionResult(someAsync, none));

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <param name="some">The function to execute if Some. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="noneAsync">The function to execute if None. If this is null, return <see cref="NotFoundObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, IActionResult>? some = null, Func<Task<IActionResult>>? noneAsync = null) =>
        option.MapAsync(async o => await o.ToActionResult(some, noneAsync));

    /// <summary>
    /// Converts <see cref="Option{A}"/> to <see cref="IActionResult"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <param name="someAsync">The function to execute if Some. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="noneAsync">The function to execute if None. If this is null, return <see cref="NotFoundObjectResult"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, Task<IActionResult>>? someAsync = null, Func<Task<IActionResult>>? noneAsync = null) =>
        option.MapAsync(async o => await o.ToActionResult(someAsync, noneAsync));
}