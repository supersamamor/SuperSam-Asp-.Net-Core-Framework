using CTI.TenantSales.Application.Features.TenantSales.TenantContact.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantContact.Queries;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.TenantContact;

[Authorize(Policy = Permission.TenantContact.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public TenantContactViewModel TenantContact { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTenantContactByIdQuery(id)), TenantContact);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditTenantContactCommand>(TenantContact)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", TenantContact);
    }
	
}
