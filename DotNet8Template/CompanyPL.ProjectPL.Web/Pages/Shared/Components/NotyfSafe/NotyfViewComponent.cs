using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Helpers;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Notyf.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Pages.Shared.Components.NotyfSafe;

[ViewComponent(Name = "NotyfSafe")]
public class NotyfViewComponent : ViewComponent
{
    private readonly INotyfService _service;

    public NotyfViewComponent(INotyfService service, NotyfEntity options)
    {
        _service = service;
        Options = options;
    }

    public NotyfEntity Options { get; }

    public IViewComponentResult Invoke()
    {
        var model = new NotyfViewModel
        {
            Configuration = Options.ToJson(),
            Notifications = _service.ReadAllNotifications()
        };
        return View("Default", model);
    }
}
