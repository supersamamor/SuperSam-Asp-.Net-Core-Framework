using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Commands;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Queries;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Pages.ModPlaceHolder;

[Authorize(Policy = Permission.ModPlaceHolder.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ModPlaceHolderViewModel ModPlaceHolder { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetModPlaceHolderByIdQuery(id)), ModPlaceHolder);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditModPlaceHolderCommand>(ModPlaceHolder)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ModPlaceHolder);
    }
	
}
