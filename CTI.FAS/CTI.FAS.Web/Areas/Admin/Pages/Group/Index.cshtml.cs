using CTI.FAS.Web.Areas.Admin.Models;
using CTI.FAS.Web.Areas.Admin.Queries.Group;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.Admin.Pages.Group;

[Authorize(Policy = Permission.Group.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public GroupViewModel Group { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetGroupQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Name,
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }
}
