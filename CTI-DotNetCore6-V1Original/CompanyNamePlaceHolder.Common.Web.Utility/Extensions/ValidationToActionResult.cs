using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static CompanyNamePlaceHolder.Common.Web.Utility.Extensions.Common;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Extensions
{
    public static class ValidationToActionResult
    {
        public static IActionResult ToActionResult<T>(this Validation<Error, T> validation) =>
            validation.Match(Ok, BadRequest);

        public static Task<IActionResult> ToActionResult<T>(this Task<Validation<Error, T>> validation) =>
            validation.Map(ToActionResult);

        public static Task<IActionResult> ToActionResult(this Task<Validation<Error, Task>> validation) =>
            validation.Bind(ToActionResult);

        public static IActionResult ToActionResult<T>(this Validation<Error, T> validation, Func<T, IActionResult>? success = null, Func<Seq<Error>, IActionResult>? fail = null) =>
            validation.Match(success ?? Ok, fail ?? BadRequest);

        public static Task<IActionResult> ToActionResult<T>(this Validation<Error, T> validation, Func<T, Task<IActionResult>>? successAsync = null, Func<Seq<Error>, IActionResult>? fail = null) =>
            validation.MatchAsync(async t => successAsync != null ? await successAsync!(t) : Ok(t), fail ?? BadRequest);

        public static Task<IActionResult> ToActionResult<T>(this Validation<Error, T> validation, Func<T, Task<IActionResult>>? successAsync = null, Func<Seq<Error>, Task<IActionResult>>? failAsync = null) =>
            validation.MatchAsync(
                SuccAsync: async t => successAsync != null ? await successAsync!(t) : Ok(t),
                FailAsync: async e => failAsync != null ? await failAsync!(e) : BadRequest(e));

        public static Task<IActionResult> ToActionResult<T>(this Task<Validation<Error, T>> validation, Func<T, IActionResult>? success = null, Func<Seq<Error>, IActionResult>? fail = null) =>
            validation.Map(v => v.ToActionResult(success, fail));

        public static Task<IActionResult> ToActionResult<T>(this Task<Validation<Error, T>> validation, Func<T, Task<IActionResult>>? successAsync = null, Func<Seq<Error>, IActionResult>? fail = null) =>
            validation.MapAsync(async v => await v.ToActionResult(successAsync, fail));

        public static Task<IActionResult> ToActionResult<T>(this Task<Validation<Error, T>> validation, Func<T, Task<IActionResult>>? successAsync = null, Func<Seq<Error>, Task<IActionResult>>? failAsync = null) =>
            validation.MapAsync(async v => await v.ToActionResult(successAsync, failAsync));

        private static Task<IActionResult> ToActionResult(Validation<Error, Task> validation) =>
            validation.MatchAsync(
                SuccAsync: async t => { await t; return Ok(Unit.Default); },
                Fail: e => BadRequest(e));
    }
}