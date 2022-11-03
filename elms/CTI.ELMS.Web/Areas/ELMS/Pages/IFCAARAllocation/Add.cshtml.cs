using CTI.ELMS.Application.Features.ELMS.IFCAARAllocation.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.IFCAARAllocation;

[Authorize(Policy = Permission.IFCAARAllocation.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public IFCAARAllocationViewModel IFCAARAllocation { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddIFCAARAllocationCommand>(IFCAARAllocation)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", IFCAARAllocation);
    }
	
}
