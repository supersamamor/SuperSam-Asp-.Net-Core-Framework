using CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.IFCATransactionType;

[Authorize(Policy = Permission.IFCATransactionType.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public IFCATransactionTypeViewModel IFCATransactionType { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetIFCATransactionTypeByIdQuery(id)), IFCATransactionType);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteIFCATransactionTypeCommand { Id = IFCATransactionType.Id }), "Index");
    }
}
