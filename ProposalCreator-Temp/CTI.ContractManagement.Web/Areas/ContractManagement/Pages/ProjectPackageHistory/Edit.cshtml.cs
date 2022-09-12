using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectPackageHistory;

[Authorize(Policy = Permission.ProjectPackageHistory.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ProjectPackageHistoryViewModel ProjectPackageHistory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetProjectPackageHistoryByIdQuery(id)), ProjectPackageHistory);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditProjectPackageHistoryCommand>(ProjectPackageHistory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProjectPackageAdditionalDeliverableHistory")
		{
			return AddProjectPackageAdditionalDeliverableHistory();
		}
		if (AsyncAction == "RemoveProjectPackageAdditionalDeliverableHistory")
		{
			return RemoveProjectPackageAdditionalDeliverableHistory();
		}
		
		
        return Partial("_InputFieldsPartial", ProjectPackageHistory);
    }
	
	private IActionResult AddProjectPackageAdditionalDeliverableHistory()
	{
		ModelState.Clear();
		if (ProjectPackageHistory!.ProjectPackageAdditionalDeliverableHistoryList == null) { ProjectPackageHistory!.ProjectPackageAdditionalDeliverableHistoryList = new List<ProjectPackageAdditionalDeliverableHistoryViewModel>(); }
		ProjectPackageHistory!.ProjectPackageAdditionalDeliverableHistoryList!.Add(new ProjectPackageAdditionalDeliverableHistoryViewModel() { ProjectPackageHistoryId = ProjectPackageHistory.Id });
		return Partial("_InputFieldsPartial", ProjectPackageHistory);
	}
	private IActionResult RemoveProjectPackageAdditionalDeliverableHistory()
	{
		ModelState.Clear();
		ProjectPackageHistory.ProjectPackageAdditionalDeliverableHistoryList = ProjectPackageHistory!.ProjectPackageAdditionalDeliverableHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ProjectPackageHistory);
	}
	
}
