using CTI.FAS.Application.Features.FAS.Company.Commands;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Company;

[Authorize(Policy = Permission.Company.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public CompanyViewModel Company { get; set; } = new();
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
		if (Company.ImageLogoForm != null && await UploadFile<CompanyViewModel>(WebConstants.Company, nameof(Company.ImageLogo), Company.Id, Company.ImageLogoForm) == "") { return Page(); }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddCompanyCommand>(Company)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddUserEntity")
		{
			return AddUserEntity();
		}
		if (AsyncAction == "RemoveUserEntity")
		{
			return RemoveUserEntity();
		}
		
		
        return Partial("_InputFieldsPartial", Company);
    }
	
	private IActionResult AddUserEntity()
	{
		ModelState.Clear();
		if (Company!.UserEntityList == null) { Company!.UserEntityList = new List<UserEntityViewModel>(); }
		Company!.UserEntityList!.Add(new UserEntityViewModel() { CompanyId = Company.Id });
		return Partial("_InputFieldsPartial", Company);
	}
	private IActionResult RemoveUserEntity()
	{
		ModelState.Clear();
		Company.UserEntityList = Company!.UserEntityList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Company);
	}
	
}
