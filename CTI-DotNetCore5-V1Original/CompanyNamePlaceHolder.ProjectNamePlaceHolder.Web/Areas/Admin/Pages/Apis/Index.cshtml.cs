using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Scopes;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Apis
{
    [Authorize(Policy = Permission.Apis.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [DataTablesRequest]
        public DataTablesRequest? DataRequest { get; set; }

        [BindProperty]
        public ScopeViewModel? Scope { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var result = await Mediatr.Send(DataRequest!.ToQuery<GetScopesQuery>());
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
