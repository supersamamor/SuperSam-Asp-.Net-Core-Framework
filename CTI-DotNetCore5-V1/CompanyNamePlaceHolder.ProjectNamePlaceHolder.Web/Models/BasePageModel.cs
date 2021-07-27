using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web
{
    public class BasePageModel<T> : PageModel where T : class
    {
        private ILogger<T>? _logger;
        private IStringLocalizer<SharedResource>? _localizer;
        private INotyfService? _notyfService;
        private IMediator? _mediatr;
        private IMapper? _mapper;

        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>()!;
        protected IStringLocalizer<SharedResource> Localizer => _localizer ??= HttpContext.RequestServices.GetService<IStringLocalizer<SharedResource>>()!;
        protected INotyfService NotyfService => _notyfService ??= HttpContext.RequestServices.GetService<INotyfService>()!;
        protected IMediator Mediatr => _mediatr ??= HttpContext.RequestServices.GetService<IMediator>()!;
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>()!;
    }
}
