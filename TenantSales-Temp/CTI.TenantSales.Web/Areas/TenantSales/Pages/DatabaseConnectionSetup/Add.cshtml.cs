using CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Commands;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.DatabaseConnectionSetup;

[Authorize(Policy = Permission.DatabaseConnectionSetup.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public DatabaseConnectionSetupViewModel DatabaseConnectionSetup { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddDatabaseConnectionSetupCommand>(DatabaseConnectionSetup)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", DatabaseConnectionSetup);
    }
	
}
