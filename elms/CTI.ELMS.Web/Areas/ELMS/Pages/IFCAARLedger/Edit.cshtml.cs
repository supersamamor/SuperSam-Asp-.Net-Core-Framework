using CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.IFCAARLedger;

[Authorize(Policy = Permission.IFCAARLedger.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public IFCAARLedgerViewModel IFCAARLedger { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetIFCAARLedgerByIdQuery(id)), IFCAARLedger);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditIFCAARLedgerCommand>(IFCAARLedger)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", IFCAARLedger);
    }
	
}
