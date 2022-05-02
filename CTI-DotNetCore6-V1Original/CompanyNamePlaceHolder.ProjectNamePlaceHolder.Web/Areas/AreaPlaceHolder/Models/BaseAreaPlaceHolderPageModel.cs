using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models
{
    public class BaseAreaPlaceHolderPageModel<T> : BasePageModel<T> where T : class
    {
        ApplicationContext? _context;
        protected ApplicationContext Context => _context ??= HttpContext.RequestServices.GetService<ApplicationContext>()!;

        protected SelectList CreateDefaultOption<TEntity>(string id, Func<TEntity, SelectListItem> defaultItem)
            where TEntity : BaseEntity =>
            Context.GetSingle<TEntity>(e => e.Id == id, new()).Result.Match(
                Some: e => new SelectList(new List<SelectListItem> { defaultItem(e) }, "Value", "Text", e.Id),
                None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
                );
    }
}
