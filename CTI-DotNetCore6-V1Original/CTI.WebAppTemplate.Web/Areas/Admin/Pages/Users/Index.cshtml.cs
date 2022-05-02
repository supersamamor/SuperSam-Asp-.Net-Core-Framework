using CTI.WebAppTemplate.Web.Areas.Admin.Queries.Users;
using CTI.WebAppTemplate.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CTI.WebAppTemplate.Web.Areas.Admin.Pages.Users;

[Authorize(Policy = Permission.Users.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public UserViewModel UserModel { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetUsersQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Name,
                e.Email,
                e.IsActive
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }
}

public record UserViewModel
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    [Display(Name = "Name")]
    public string? Name { get; set; }
    [Display(Name = "Email")]
    public string? Email { get; set; }
    [Display(Name = "Status")]
    public bool IsActive { get; set; } = false;
}
