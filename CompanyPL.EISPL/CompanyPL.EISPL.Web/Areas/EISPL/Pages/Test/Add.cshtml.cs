using CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.Test;

[Authorize(Policy = Permission.Test.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public TestViewModel Test { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddTestCommand>(Test)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", Test);
    }
	
}
