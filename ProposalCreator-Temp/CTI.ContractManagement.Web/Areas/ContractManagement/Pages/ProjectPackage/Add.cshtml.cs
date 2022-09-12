using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackage.Commands;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectPackage;

[Authorize(Policy = Permission.ProjectPackage.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ProjectPackageViewModel ProjectPackage { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddProjectPackageCommand>(ProjectPackage)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProjectPackageAdditionalDeliverable")
		{
			return AddProjectPackageAdditionalDeliverable();
		}
		if (AsyncAction == "RemoveProjectPackageAdditionalDeliverable")
		{
			return RemoveProjectPackageAdditionalDeliverable();
		}
		
		
        return Partial("_InputFieldsPartial", ProjectPackage);
    }
	
	private IActionResult AddProjectPackageAdditionalDeliverable()
	{
		ModelState.Clear();
		if (ProjectPackage!.ProjectPackageAdditionalDeliverableList == null) { ProjectPackage!.ProjectPackageAdditionalDeliverableList = new List<ProjectPackageAdditionalDeliverableViewModel>(); }
		ProjectPackage!.ProjectPackageAdditionalDeliverableList!.Add(new ProjectPackageAdditionalDeliverableViewModel() { ProjectPackageId = ProjectPackage.Id });
		return Partial("_InputFieldsPartial", ProjectPackage);
	}
	private IActionResult RemoveProjectPackageAdditionalDeliverable()
	{
		ModelState.Clear();
		ProjectPackage.ProjectPackageAdditionalDeliverableList = ProjectPackage!.ProjectPackageAdditionalDeliverableList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ProjectPackage);
	}
	
}
