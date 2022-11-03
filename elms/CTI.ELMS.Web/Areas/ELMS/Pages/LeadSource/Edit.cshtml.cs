using CTI.ELMS.Application.Features.ELMS.LeadSource.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadSource.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.LeadSource;

[Authorize(Policy = Permission.LeadSource.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public LeadSourceViewModel LeadSource { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetLeadSourceByIdQuery(id)), LeadSource);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditLeadSourceCommand>(LeadSource)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", LeadSource);
    }
	
}
