using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.ModuleNamePlaceHolder;

[Authorize(Policy = Permission.ModuleNamePlaceHolder.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ModuleNamePlaceHolderViewModel ModuleNamePlaceHolder { get; set; } = new();
	[BindProperty]
	public string? RemoveSubDetailId { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetModuleNamePlaceHolderByIdQuery(id)), ModuleNamePlaceHolder);
    }
}
