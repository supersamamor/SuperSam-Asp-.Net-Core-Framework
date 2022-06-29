using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.SubDetailItemPlaceHolder;

[Authorize(Policy = Permission.SubDetailItemPlaceHolder.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public SubDetailItemPlaceHolderViewModel SubDetailItemPlaceHolder { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetSubDetailItemPlaceHolderByIdQuery(id)), SubDetailItemPlaceHolder);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteSubDetailItemPlaceHolderCommand { Id = SubDetailItemPlaceHolder.Id }), "Index");
    }
}
