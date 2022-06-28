using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.MainModulePlaceHolder;

[Authorize(Policy = Permission.MainModulePlaceHolder.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public MainModulePlaceHolderViewModel MainModulePlaceHolder { get; set; } = new();

    public IActionResult OnGet()
    {
		Template:[InsertNewSubDetailItemAddInitial]
        return Page();
    }

    public async Task<IActionResult> OnPost(string? handler)
    {
		Template:[InsertNewSubDetailAddRemovePostHandlerFromPage]
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddMainModulePlaceHolderCommand>(MainModulePlaceHolder)), "Details", true);
    }
	Template:[InsertNewSubDetailAddRemoveMethodFromPage]
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
        return Partial("_InputFieldsPartial", ModuleNamePlaceHolder);
    }
}
