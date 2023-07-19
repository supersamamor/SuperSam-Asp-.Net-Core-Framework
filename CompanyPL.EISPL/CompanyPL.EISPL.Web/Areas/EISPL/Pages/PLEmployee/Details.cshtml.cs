using CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Queries;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.PLEmployee;

[Authorize(Policy = Permission.PLEmployee.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public PLEmployeeViewModel PLEmployee { get; set; } = new();
	[BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetPLEmployeeByIdQuery(id)), PLEmployee);
    }
}
