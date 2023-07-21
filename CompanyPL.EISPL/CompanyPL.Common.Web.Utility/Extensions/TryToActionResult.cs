using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CompanyPL.Common.Web.Utility.Extensions.Common;

namespace CompanyPL.Common.Web.Utility.Extensions;

/// <summary>
/// Extension methods for converting <see cref="Try{A}"/> to <see cref="IActionResult"/>
/// </summary>
public static class TryToActionResult
{
    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// Returns <see cref="OkObjectResult"/> or <see cref="StatusCodes.Status500InternalServerError"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this Try<T> @try) =>
        @try.Match(Ok, ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this Try<T> @try, Func<T, IActionResult>? success = null, Func<Exception, IActionResult>? fail = null) =>
        @try.Match(success ?? Ok, fail ?? ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Try<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, IActionResult>? fail = null) =>
        @try.MatchAsync(async t => successAsync != null ? await successAsync!(t) : Ok(t), fail ?? ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="failAsync">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Try<T> @try, Func<T, IActionResult>? success = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
        @try.MatchAsync(success ?? Ok, async e => failAsync != null ? await failAsync!(e) : ServerError(e));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="failAsync">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Try<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
        @try.MatchAsync(async t => successAsync != null ? await successAsync!(t) : Ok(t), async e => failAsync != null ? await failAsync!(e) : ServerError(e));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The delegate to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this Try<T> @try, Func<T, IActionResult>? success = null, Action<Exception>? fail = null) =>
        @try.Match(success ?? Ok, e => ServerErrorWithLogging(e, fail));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The delegate to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Try<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Action<Exception>? fail = null) =>
        @try.MatchAsync(
            async t => successAsync != null ? await successAsync!(t) : Ok(t),
            e => ServerErrorWithLogging(e, fail));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try) =>
        @try.Match(Ok, ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, IActionResult>? success = null, Func<Exception, IActionResult>? fail = null) =>
        @try.Match(success ?? Ok, fail ?? ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, IActionResult>? fail = null) =>
        @try.Match(async t => successAsync != null ? await successAsync!(t) : Ok(t), fail ?? ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="failAsync">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, IActionResult>? success = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
        @try.Match(success ?? Ok, async e => failAsync != null ? await failAsync!(e) : ServerError(e));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="failAsync">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
        @try.Match(async t => successAsync != null ? await successAsync!(t) : Ok(t), async e => failAsync != null ? await failAsync!(e) : ServerError(e));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The delegate to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, IActionResult>? success = null, Action<Exception>? fail = null) =>
        @try.Match(success ?? Ok, e => ServerErrorWithLogging(e, fail));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The delegate to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, Task<IActionResult>>? successAsync = null, Action<Exception>? fail = null) =>
        @try.Match(
            async t => successAsync != null ? await successAsync!(t) : Ok(t),
            e => ServerErrorWithLogging(e, fail));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try) =>
        @try.Match(Ok, ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, IActionResult>? success = null, Func<Exception, IActionResult>? fail = null) =>
        @try.Match(success ?? Ok, fail ?? ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, IActionResult>? fail = null) =>
        @try.Match(async t => successAsync != null ? await successAsync!(t) : Ok(t), fail ?? ServerError);

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="failAsync">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, IActionResult>? success = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
        @try.Match(success ?? Ok, async e => failAsync != null ? await failAsync!(e) : ServerError(e));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="failAsync">The function to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
        @try.Match(async t => successAsync != null ? await successAsync!(t) : Ok(t), async e => failAsync != null ? await failAsync!(e) : ServerError(e));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="success">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The delegate to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, IActionResult>? success = null, Action<Exception>? fail = null) =>
        @try.Match(success ?? Ok, e => ServerErrorWithLogging(e, fail));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <param name="successAsync">The function to execute if Succ. If this is null, return <see cref="OkObjectResult"/></param>
    /// <param name="fail">The delegate to execute if Fail. If this is null, return <see cref="StatusCodes.Status500InternalServerError"/></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Action<Exception>? fail = null) =>
        @try.Match(
            async t => successAsync != null ? await successAsync!(t) : Ok(t),
            e => ServerErrorWithLogging(e, fail));

    /// <summary>
    /// Converts <see cref="Try{A}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="try"></param>
    /// <returns></returns>
    public static Task<IActionResult> ToActionResult<T>(this Try<Task<T>> @try) =>
        @try.ToAsync().ToActionResult();
}