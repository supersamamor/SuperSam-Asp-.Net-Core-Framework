using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Entities
{
    [Authorize(Policy = Permission.Entities.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [DataTablesRequest]
        public DataTablesRequest? DataRequest { get; set; }

        [BindProperty]
        public EntityViewModel? Entity { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var result = await Mediatr.Send(DataRequest!.ToQuery<GetEntitiesQuery>());
            return new JsonResult(result.Data
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                })
                .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
        }
    }
}
