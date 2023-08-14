using CTI.DSF.Application.Features.DSF.TaskList.Commands;
using CTI.DSF.Application.Features.DSF.TaskList.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.TaskList;

[Authorize(Policy = Permission.TaskList.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public TaskListViewModel TaskList { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTaskListByIdQuery(id)), TaskList);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteTaskListCommand { Id = TaskList.Id }), "Index");
    }
}
