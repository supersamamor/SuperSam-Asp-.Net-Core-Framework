using CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Commands;
using CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.PreSelectedUnit;

[Authorize(Policy = Permission.PreSelectedUnit.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public PreSelectedUnitViewModel PreSelectedUnit { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetPreSelectedUnitByIdQuery(id)), PreSelectedUnit);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditPreSelectedUnitCommand>(PreSelectedUnit)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", PreSelectedUnit);
    }
	
}
