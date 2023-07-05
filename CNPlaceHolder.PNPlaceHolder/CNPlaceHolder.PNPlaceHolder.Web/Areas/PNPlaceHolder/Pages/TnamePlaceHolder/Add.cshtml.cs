using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Commands;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Pages.TnamePlaceHolder;

[Authorize(Policy = Permission.TnamePlaceHolder.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public TnamePlaceHolderViewModel TnamePlaceHolder { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddTnamePlaceHolderCommand>(TnamePlaceHolder)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", TnamePlaceHolder);
    }
	
}
