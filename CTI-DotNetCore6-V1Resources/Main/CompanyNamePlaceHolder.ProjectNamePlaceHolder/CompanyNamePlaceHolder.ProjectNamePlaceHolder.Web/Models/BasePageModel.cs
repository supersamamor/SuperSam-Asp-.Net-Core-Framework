using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CTI.Common.Core.Base.Models;
using CTI.Common.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

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
