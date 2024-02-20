using CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.Frequency;

[Authorize(Policy = Permission.Frequency.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public FrequencyViewModel Frequency { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetFrequencyByIdQuery(id)), Frequency);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteFrequencyCommand { Id = Frequency.Id }), "Index");
    }
}
