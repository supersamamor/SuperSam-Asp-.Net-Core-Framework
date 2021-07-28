using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Helpers;
using AspNetCoreHero.ToastNotification.Toastify;
using AspNetCoreHero.ToastNotification.Toastify.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Pages.Shared.Components.ToastifySafe
{
    [ViewComponent(Name = "ToastifySafe")]
    public class ToastifyViewComponent : ViewComponent
    {
        private readonly IToastifyService _service;

        public ToastifyViewComponent(IToastifyService service, ToastifyEntity options)
        {
            _service = service;
            _options = options;
        }

        public ToastifyEntity _options { get; }

        public IViewComponentResult Invoke()
        {
            var model = new ToastifyViewModel
            {
                Configuration = _options,
                Notifications = _service.ReadAllNotifications()
            };
            return View("Default", model);
        }
    }
}
