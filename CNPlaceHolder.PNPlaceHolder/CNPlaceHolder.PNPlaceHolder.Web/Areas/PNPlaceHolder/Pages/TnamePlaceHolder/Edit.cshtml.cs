using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Commands;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Queries;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Pages.TnamePlaceHolder;

[Authorize(Policy = Permission.TnamePlaceHolder.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public TnamePlaceHolderViewModel TnamePlaceHolder { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTnamePlaceHolderByIdQuery(id)), TnamePlaceHolder);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditTnamePlaceHolderCommand>(TnamePlaceHolder)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", TnamePlaceHolder);
    }
	
}
