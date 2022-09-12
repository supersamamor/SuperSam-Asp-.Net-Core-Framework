using CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Queries;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ApplicationConfiguration;

[Authorize(Policy = Permission.ApplicationConfiguration.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ApplicationConfigurationViewModel ApplicationConfiguration { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetApplicationConfigurationByIdQuery(id)), ApplicationConfiguration);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		if (ApplicationConfiguration.LogoForm != null && await UploadFile<ApplicationConfigurationViewModel>(WebConstants.ApplicationConfiguration, nameof(ApplicationConfiguration.Logo), ApplicationConfiguration.Id, ApplicationConfiguration.LogoForm) == "") { return Page(); }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditApplicationConfigurationCommand>(ApplicationConfiguration)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", ApplicationConfiguration);
    }
	
}
