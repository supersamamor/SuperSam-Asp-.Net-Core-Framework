using CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.IFCATransactionType;

[Authorize(Policy = Permission.IFCATransactionType.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public IFCATransactionTypeViewModel IFCATransactionType { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddIFCATransactionTypeCommand>(IFCATransactionType)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", IFCATransactionType);
    }
	
}
