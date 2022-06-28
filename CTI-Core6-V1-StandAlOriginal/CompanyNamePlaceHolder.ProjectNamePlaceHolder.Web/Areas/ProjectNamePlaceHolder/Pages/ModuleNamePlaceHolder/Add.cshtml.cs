using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.ModuleNamePlaceHolder;

[Authorize(Policy = Permission.ModuleNamePlaceHolder.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ModuleNamePlaceHolderViewModel ModuleNamePlaceHolder { get; set; } = new();

    public IActionResult OnGet()
    {

        return Page();
    }

    public async Task<IActionResult> OnPost(string? handler)
    {

        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddModuleNamePlaceHolderCommand>(ModuleNamePlaceHolder)), "Details", true);
    }
    public async Task<IActionResult> OnPostChangeFormValue()
    {
        ModelState.Clear();
        return Partial("_InputFieldsPartial", ModuleNamePlaceHolder);
    }
}
