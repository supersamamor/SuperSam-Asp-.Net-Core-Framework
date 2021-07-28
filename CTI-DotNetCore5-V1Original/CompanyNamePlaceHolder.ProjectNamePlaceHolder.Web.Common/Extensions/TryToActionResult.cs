using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions.Common;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions
{
    public static class TryToActionResult
    {
        public static IActionResult ToActionResult<T>(this Try<T> @try) =>
            @try.Match(Ok, ServerError);

        public static IActionResult ToActionResult<T>(this Try<T> @try, Func<T, IActionResult>? success = null, Func<Exception, IActionResult>? fail = null) =>
            @try.Match(success ?? Ok, fail ?? ServerError);

        public static Task<IActionResult> ToActionResult<T>(this Try<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, IActionResult>? fail = null) =>
            @try.MatchAsync(async t => successAsync != null ? await successAsync!(t) : Ok(t), fail ?? ServerError);

        public static Task<IActionResult> ToActionResult<T>(this Try<T> @try, Func<T, IActionResult>? success = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
            @try.MatchAsync(success ?? Ok, async e => failAsync != null ? await failAsync!(e) : ServerError(e));

        public static Task<IActionResult> ToActionResult<T>(this Try<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
            @try.MatchAsync(async t => successAsync != null ? await successAsync!(t) : Ok(t), async e => failAsync != null ? await failAsync!(e) : ServerError(e));

        public static IActionResult ToActionResult<T>(this Try<T> @try, Func<T, IActionResult>? success = null, Action<Exception>? fail = null) =>
            @try.Match(success ?? Ok, e => ServerErrorWithLogging(e, fail));

        public static Task<IActionResult> ToActionResult<T>(this Try<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Action<Exception>? fail = null) =>
            @try.MatchAsync(
                async t => successAsync != null ? await successAsync!(t) : Ok(t),
                e => ServerErrorWithLogging(e, fail));

        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try) =>
            @try.Match(Ok, ServerError);

        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, IActionResult>? success = null, Func<Exception, IActionResult>? fail = null) =>
            @try.Match(success ?? Ok, fail ?? ServerError);

        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, IActionResult>? fail = null) =>
            @try.Match(async t => successAsync != null ? await successAsync!(t) : Ok(t), fail ?? ServerError);

        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, IActionResult>? success = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
            @try.Match(success ?? Ok, async e => failAsync != null ? await failAsync!(e) : ServerError(e));

        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
            @try.Match(async t => successAsync != null ? await successAsync!(t) : Ok(t), async e => failAsync != null ? await failAsync!(e) : ServerError(e));

        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, IActionResult>? success = null, Action<Exception>? fail = null) =>
            @try.Match(success ?? Ok, e => ServerErrorWithLogging(e, fail));

        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, Task<IActionResult>>? successAsync = null, Action<Exception>? fail = null) =>
            @try.Match(
                async t => successAsync != null ? await successAsync!(t) : Ok(t),
                e => ServerErrorWithLogging(e, fail));

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try) =>
            @try.Match(Ok, ServerError);

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, IActionResult>? success = null, Func<Exception, IActionResult>? fail = null) =>
            @try.Match(success ?? Ok, fail ?? ServerError);

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, IActionResult>? fail = null) =>
            @try.Match(async t => successAsync != null ? await successAsync!(t) : Ok(t), fail ?? ServerError);

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, IActionResult>? success = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
            @try.Match(success ?? Ok, async e => failAsync != null ? await failAsync!(e) : ServerError(e));

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Func<Exception, Task<IActionResult>>? failAsync = null) =>
            @try.Match(async t => successAsync != null ? await successAsync!(t) : Ok(t), async e => failAsync != null ? await failAsync!(e) : ServerError(e));

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, IActionResult>? success = null, Action<Exception>? fail = null) =>
            @try.Match(success ?? Ok, e => ServerErrorWithLogging(e, fail));

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, Task<IActionResult>>? successAsync = null, Action<Exception>? fail = null) =>
            @try.Match(
                async t => successAsync != null ? await successAsync!(t) : Ok(t),
                e => ServerErrorWithLogging(e, fail));

        public static Task<IActionResult> ToActionResult<T>(this Try<Task<T>> @try) =>
            @try.ToAsync().ToActionResult();
    }
}
