using CTI.ELMS.Application.Features.ELMS.OperationType.Commands;
using CTI.ELMS.Application.Features.ELMS.OperationType.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.OperationType;

[Authorize(Policy = Permission.OperationType.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public OperationTypeViewModel OperationType { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetOperationTypeByIdQuery(id)), OperationType);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditOperationTypeCommand>(OperationType)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", OperationType);
    }
	
}
