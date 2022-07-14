using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Base.Models;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using DataTables.AspNetCore.Mvc.Binder;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;

public class BasePageModel<T> : PageModel where T : class
{
    private ILogger<T>? _logger;
    private IStringLocalizer<SharedResource>? _localizer;
    private INotyfService? _notyfService;
    private IMediator? _mediatr;
    private IMapper? _mapper;
    private string? _traceId;

    protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>()!;
    protected IStringLocalizer<SharedResource> Localizer => _localizer ??= HttpContext.RequestServices.GetService<IStringLocalizer<SharedResource>>()!;
    protected INotyfService NotyfService => _notyfService ??= HttpContext.RequestServices.GetService<INotyfService>()!;
    protected IMediator Mediatr => _mediatr ??= HttpContext.RequestServices.GetService<IMediator>()!;
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>()!;
    protected string TraceId => _traceId ??= Activity.Current?.Id ?? HttpContext.TraceIdentifier;

    protected async Task<IActionResult> PageFrom<TEntity, TModel>(Func<Task<Option<TEntity>>> f, TModel model) =>
        await f().ToActionResult(
            e =>
            {
                Mapper.Map(e, model);
                return Page();
            },
            none: null);

    protected async Task<DataTablesResponse<TModel>> ToDataTablesResponse<TEntity, TModel>(DataTablesRequest? request, Func<Task<PagedListResponse<TEntity>>> f)
    {
        var result = await f();
        return ToDataTablesResponse<TEntity, TModel>(request, result);
    }

    protected DataTablesResponse<TModel> ToDataTablesResponse<TEntity, TModel>(DataTablesRequest? request, PagedListResponse<TEntity> result) =>
        Mapper.Map<IEnumerable<TModel>>(result.Data).ToDataTablesResponse(request, result.TotalCount, result.MetaData.TotalItemCount);

    protected async Task<IActionResult> TryThenRedirectToPage<TEntity>(Func<Task<Validation<Error, TEntity>>> f, string pageName, bool isDetailsPage = false)
        where TEntity : IEntity =>
        await TryAsync(() => f()).IfFail(ex =>
        {
            Logger.LogError(ex, "Exception encountered");
            return Fail<Error, TEntity>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
        }).ToActionResult(
            success: succ =>
            {
                NotyfService.Success(Localizer["Transaction successful"]);
                Logger.LogInformation("Details of affected record. ID: {ID}, Record: {Record}", succ.Id, succ.ToString());
                return isDetailsPage ? RedirectToPage(pageName, new { id = succ.Id }) : RedirectToPage(pageName);
            },
            fail: errors =>
            {
                errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                Logger.LogError("Error encountered. Errors: {Errors}", errors.Join().ToString());
                return Page();
            });
}

public class BasePageModel<TContext, TPageModel> : BasePageModel<TPageModel>
    where TPageModel : class
    where TContext : DbContext
{
    TContext? _context;
    protected TContext Context => _context ??= HttpContext.RequestServices.GetService<TContext>()!;

    protected SelectList CreateDefaultOption<TEntity>(string id, Func<TEntity, SelectListItem> defaultItem)
        where TEntity : BaseEntity =>
        Context.GetSingle<TEntity>(e => e.Id == id, new()).Result.Match(
            Some: e => new SelectList(new List<SelectListItem> { defaultItem(e) }, "Value", "Text", e.Id),
            None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
            );
}
