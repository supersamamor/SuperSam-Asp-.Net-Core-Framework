using CTI.FAS.Application.Features.FAS.Creditor.Commands;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.Creditor;

[Authorize(Policy = Permission.Creditor.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public CreditorViewModel Creditor { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddCreditorCommand>(Creditor)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddCheckReleaseOption")
		{
			return AddCheckReleaseOption();
		}
		if (AsyncAction == "RemoveCheckReleaseOption")
		{
			return RemoveCheckReleaseOption();
		}
		if (AsyncAction == "AddCreditorEmail")
		{
			return AddCreditorEmail();
		}
		if (AsyncAction == "RemoveCreditorEmail")
		{
			return RemoveCreditorEmail();
		}
		
		
        return Partial("_InputFieldsPartial", Creditor);
    }
	
	private IActionResult AddCheckReleaseOption()
	{
		ModelState.Clear();
		if (Creditor!.CheckReleaseOptionList == null) { Creditor!.CheckReleaseOptionList = new List<CheckReleaseOptionViewModel>(); }
		Creditor!.CheckReleaseOptionList!.Add(new CheckReleaseOptionViewModel() { CreditorId = Creditor.Id });
		return Partial("_InputFieldsPartial", Creditor);
	}
	private IActionResult RemoveCheckReleaseOption()
	{
		ModelState.Clear();
		Creditor.CheckReleaseOptionList = Creditor!.CheckReleaseOptionList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Creditor);
	}

	private IActionResult AddCreditorEmail()
	{
		ModelState.Clear();
		if (Creditor!.CreditorEmailList == null) { Creditor!.CreditorEmailList = new List<CreditorEmailViewModel>(); }
		Creditor!.CreditorEmailList!.Add(new CreditorEmailViewModel() { CreditorId = Creditor.Id });
		return Partial("_InputFieldsPartial", Creditor);
	}
	private IActionResult RemoveCreditorEmail()
	{
		ModelState.Clear();
		Creditor.CreditorEmailList = Creditor!.CreditorEmailList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Creditor);
	}
	
}
