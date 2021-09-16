using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Applications;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Applications
{
    [Authorize(Policy = Permission.Applications.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [DataTablesRequest]
        public DataTablesRequest? DataRequest { get; set; }

        public ApplicationViewModel Application { get; set; } = new();

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var result = await Mediatr.Send(DataRequest!.ToQuery<GetApplicationsQuery>());
            return new JsonResult(result.Data
                .Select(e => new
                {
                    e.ClientId,
                    e.DisplayName,
                })
                .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
        }
    }
}
