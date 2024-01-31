using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
using CompanyPL.ProjectPL.Web.Areas.Admin.Queries.Roles;
using CompanyPL.ProjectPL.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.Admin.Pages.Roles;

[Authorize(Policy = Permission.Roles.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public RoleViewModel Role { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetRolesQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Name,
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }
}
