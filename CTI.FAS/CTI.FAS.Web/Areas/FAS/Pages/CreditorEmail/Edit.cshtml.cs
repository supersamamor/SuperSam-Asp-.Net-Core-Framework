using CTI.FAS.Application.Features.FAS.CreditorEmail.Commands;
using CTI.FAS.Application.Features.FAS.CreditorEmail.Queries;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.CreditorEmail;

[Authorize(Policy = Permission.CreditorEmail.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public CreditorEmailViewModel CreditorEmail { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetCreditorEmailByIdQuery(id)), CreditorEmail);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditCreditorEmailCommand>(CreditorEmail)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", CreditorEmail);
    }
	
}
