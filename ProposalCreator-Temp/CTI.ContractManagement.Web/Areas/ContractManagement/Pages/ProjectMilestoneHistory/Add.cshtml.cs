using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Commands;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectMilestoneHistory;

[Authorize(Policy = Permission.ProjectMilestoneHistory.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ProjectMilestoneHistoryViewModel ProjectMilestoneHistory { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddProjectMilestoneHistoryCommand>(ProjectMilestoneHistory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ProjectMilestoneHistory);
    }
	
}
