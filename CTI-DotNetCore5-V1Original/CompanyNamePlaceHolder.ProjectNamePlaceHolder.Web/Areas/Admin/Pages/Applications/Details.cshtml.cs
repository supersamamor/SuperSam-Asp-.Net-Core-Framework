using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Applications
{
    [Authorize(Policy = Permission.Applications.View)]
    public class DetailsModel : BasePageModel<AddModel>
    {
        public ApplicationViewModel? Application { get; set; }

        public IActionResult OnGet()
        {
            Application = TempData.Get<ApplicationViewModel>("Application");
            return Page();
        }
    }
}
