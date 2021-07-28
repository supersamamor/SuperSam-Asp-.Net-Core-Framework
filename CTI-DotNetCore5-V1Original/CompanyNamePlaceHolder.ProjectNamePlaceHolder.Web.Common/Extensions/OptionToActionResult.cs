using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions.Common;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions
{
    public static class OptionToActionResult
    {
        public static IActionResult ToActionResult<T>(this Option<T> option) =>
            option.Match(Ok, NotFound);

        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option) =>
            option.Map(ToActionResult);

        public static IActionResult ToActionResult<T>(this Option<T> option, Func<T, IActionResult>? some = null, Func<IActionResult>? none = null) =>
            option.Match(some ?? Ok, none ?? NotFound);

        public static Task<IActionResult> ToActionResult<T>(this Option<T> option, Func<T, Task<IActionResult>>? someAsync = null, Func<IActionResult>? none = null) =>
            option.MatchAsync(async t => someAsync != null ? await someAsync!(t) : Ok(t), none ?? NotFound);

        public static Task<IActionResult> ToActionResult<T>(this Option<T> option, Func<T, IActionResult>? some = null, Func<Task<IActionResult>>? noneAsync = null) =>
            option.MatchAsync(some ?? Ok, async () => noneAsync != null ? await noneAsync!() : NotFound());

        public static Task<IActionResult> ToActionResult<T>(this Option<T> option, Func<T, Task<IActionResult>>? someAsync = null, Func<Task<IActionResult>>? noneAsync = null) =>
            option.MatchAsync(
                Some: async t => someAsync != null ? await someAsync!(t) : Ok(t),
                None: async () => noneAsync != null ? await noneAsync!() : NotFound());

        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, IActionResult>? some = null, Func<IActionResult>? none = null) =>
            option.Map(o => o.ToActionResult(some, none));

        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, Task<IActionResult>>? someAsync = null, Func<IActionResult>? none = null) =>
            option.MapAsync(async o => await o.ToActionResult(someAsync, none));

        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, IActionResult>? some = null, Func<Task<IActionResult>>? noneAsync = null) =>
            option.MapAsync(async o => await o.ToActionResult(some, noneAsync));

        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, Task<IActionResult>>? someAsync = null, Func<Task<IActionResult>>? noneAsync = null) =>
            option.MapAsync(async o => await o.ToActionResult(someAsync, noneAsync));
    }
}
