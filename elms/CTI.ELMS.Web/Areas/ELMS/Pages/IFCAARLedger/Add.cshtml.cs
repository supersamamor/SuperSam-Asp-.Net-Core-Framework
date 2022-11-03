using CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.IFCAARLedger;

[Authorize(Policy = Permission.IFCAARLedger.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public IFCAARLedgerViewModel IFCAARLedger { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddIFCAARLedgerCommand>(IFCAARLedger)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", IFCAARLedger);
    }
	
}
