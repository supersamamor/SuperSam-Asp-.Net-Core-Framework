using CTI.ELMS.Application.Features.ELMS.Project.Queries;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Reports;

[Authorize(Policy = Permission.Reports.LotBudgetListing)]
public class LotBudgetListingModel : BasePageModel<LotBudgetListingModel>
{
    

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {

        var result = await Mediatr.Send(DataRequest!.ToQuery<GetProjectQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.ProjectName,
                e.DatabaseSource,
                EntityGroupId = e.EntityGroup?.Id,
                e.IFCAProjectCode,
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }
}
