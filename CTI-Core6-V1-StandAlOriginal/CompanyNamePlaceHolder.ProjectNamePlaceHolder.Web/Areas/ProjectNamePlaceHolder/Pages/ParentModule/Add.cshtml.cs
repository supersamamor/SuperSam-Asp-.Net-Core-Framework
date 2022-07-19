using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.ParentModule;

[Authorize(Policy = Permission.ParentModule.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ParentModuleViewModel ParentModule { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddParentModuleCommand>(ParentModule)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ParentModule);
    }
	
}
