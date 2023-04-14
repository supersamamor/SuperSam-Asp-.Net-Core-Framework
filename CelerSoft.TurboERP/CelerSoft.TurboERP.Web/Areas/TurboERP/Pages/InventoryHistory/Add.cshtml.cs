using CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Commands;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.InventoryHistory;

[Authorize(Policy = Permission.InventoryHistory.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public InventoryHistoryViewModel InventoryHistory { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddInventoryHistoryCommand>(InventoryHistory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", InventoryHistory);
    }
	
}
