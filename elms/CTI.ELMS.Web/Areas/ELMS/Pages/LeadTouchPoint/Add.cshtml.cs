using CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.LeadTouchPoint;

[Authorize(Policy = Permission.LeadTouchPoint.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public LeadTouchPointViewModel LeadTouchPoint { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddLeadTouchPointCommand>(LeadTouchPoint)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", LeadTouchPoint);
    }
	
}
