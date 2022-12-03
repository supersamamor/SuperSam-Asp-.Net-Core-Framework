using CTI.FAS.Application.Features.FAS.Batch.Commands;
using CTI.FAS.Application.Features.FAS.Batch.Queries;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Batch;

[Authorize(Policy = Permission.Batch.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public BatchViewModel Batch { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetBatchByIdQuery(id)), Batch);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteBatchCommand { Id = Batch.Id }), "Index");
    }
}
