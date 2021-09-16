using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using System;
using System.Threading.Tasks;
using static CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions.Common;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions
{
    public static class EitherToActionResult
    {
        public static IActionResult ToActionResult<L, R>(this Either<L, R> either) =>
            either.Match(Ok, BadRequest);

        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either) =>
            either.Map(ToActionResult);

        public static Task<IActionResult> ToActionResult(this Task<Either<Error, Task>> either) =>
            either.Bind(Match);

        public static IActionResult ToActionResult<L, R>(this Either<L, R> either, Func<R, IActionResult>? right = null, Func<L, IActionResult>? left = null) =>
            either.Match(right ?? Ok, left ?? BadRequest);

        public static Task<IActionResult> ToActionResult<L, R>(this Either<L, R> either, Func<R, IActionResult>? right = null, Func<L, Task<IActionResult>>? leftAsync = null) =>
            either.MatchAsync(right ?? Ok, async l => leftAsync != null ? await leftAsync!(l) : BadRequest(l));

        public static Task<IActionResult> ToActionResult<L, R>(this Either<L, R> either, Func<R, Task<IActionResult>>? rightAsync = null, Func<L, IActionResult>? left = null) =>
            either.MatchAsync(async r => rightAsync != null ? await rightAsync!(r) : Ok(r), left ?? BadRequest);

        public static Task<IActionResult> ToActionResult<L, R>(this Either<L, R> either, Func<R, Task<IActionResult>>? rightAsync = null, Func<L, Task<IActionResult>>? leftAsync = null) =>
            either.MatchAsync(
                RightAsync: async r => rightAsync != null ? await rightAsync!(r) : Ok(r),
                LeftAsync: async l => leftAsync != null ? await leftAsync!(l) : BadRequest(l));

        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<R, IActionResult>? right = null, Func<L, IActionResult>? left = null) =>
            either.Map(e => e.ToActionResult(right, left));

        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<R, IActionResult>? right = null, Func<L, Task<IActionResult>>? leftAsync = null) =>
            either.MapAsync(async e => await e.ToActionResult(right, leftAsync));

        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<R, Task<IActionResult>>? rightAsync = null, Func<L, IActionResult>? left = null) =>
            either.MapAsync(async e => await e.ToActionResult(rightAsync, left));

        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<R, Task<IActionResult>>? rightAsync = null, Func<L, Task<IActionResult>>? leftAsync = null) =>
            either.MapAsync(async e => await e.ToActionResult(rightAsync, leftAsync));

        private async static Task<IActionResult> Match(Either<Error, Task> either) =>
            await either.MatchAsync(
                RightAsync: async t => { await t; return Ok(Unit.Default); },
                Left: e => BadRequest(e));
    }
}
