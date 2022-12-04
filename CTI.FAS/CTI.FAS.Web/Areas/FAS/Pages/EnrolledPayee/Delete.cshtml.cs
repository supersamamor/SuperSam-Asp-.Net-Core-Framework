using CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayee;

[Authorize(Policy = Permission.EnrolledPayee.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public EnrolledPayeeViewModel EnrolledPayee { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetEnrolledPayeeByIdQuery(id)), EnrolledPayee);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteEnrolledPayeeCommand { Id = EnrolledPayee.Id }), "Index");
    }
}
