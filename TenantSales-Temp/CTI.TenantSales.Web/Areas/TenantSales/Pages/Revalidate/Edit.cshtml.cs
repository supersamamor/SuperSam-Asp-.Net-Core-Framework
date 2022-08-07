using CTI.TenantSales.Application.Features.TenantSales.Revalidate.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Revalidate.Queries;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Revalidate;

//[Authorize(Policy = Permission.Revalidate.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public RevalidateViewModel Revalidate { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetRevalidateByIdQuery(id)), Revalidate);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditRevalidateCommand>(Revalidate)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", Revalidate);
    }
	
}
