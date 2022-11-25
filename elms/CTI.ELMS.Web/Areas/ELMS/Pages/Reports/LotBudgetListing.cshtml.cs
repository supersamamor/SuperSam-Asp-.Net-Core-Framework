using CTI.ELMS.Application.Features.ELMS.Reports.Queries;
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

    public async Task<IActionResult> OnPostListAllAsync(string projectId)
    {
        var query = DataRequest!.ToQuery<GetLotBudgetListingQuery>();
        query.ProjectId = projectId;
        var result = await Mediatr.Send(query);
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Project?.ProjectName,
                e.Unit?.UnitNo,
                e.January,
                e.February,
                e.March,
                e.April,
                e.May,
                e.June,
                e.July,
                e.August,
                e.September,
                e.October,
                e.November,
                e.December,
                e.Unit?.LotArea,
                e.Year,
                e.Unit?.Location,
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }
}
