using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.SubDetailList;

[Authorize(Policy = Permission.SubDetailList.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public SubDetailListViewModel SubDetailList { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetSubDetailListByIdQuery(id)), SubDetailList);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditSubDetailListCommand>(SubDetailList)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", SubDetailList);
    }
	
}
