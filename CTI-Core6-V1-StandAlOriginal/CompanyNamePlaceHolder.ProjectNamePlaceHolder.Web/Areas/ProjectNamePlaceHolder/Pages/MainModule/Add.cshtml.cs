using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.MainModule;

[Authorize(Policy = Permission.MainModule.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public MainModuleViewModel MainModule { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		this.MainModule.SubDetailItemList = new List<SubDetailItemViewModel>() { new SubDetailItemViewModel() { TestForeignKeyTwo = this.MainModule.Id} };
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddMainModuleCommand>(MainModule)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddSubDetailList")
		{
			return AddSubDetailList();
		}
		if (AsyncAction == "RemoveSubDetailList")
		{
			return RemoveSubDetailList();
		}
		
		
        return Partial("_InputFieldsPartial", MainModule);
    }
	
	private IActionResult AddSubDetailList()
	{
		ModelState.Clear();
		if (MainModule!.SubDetailListList == null) { MainModule!.SubDetailListList = new List<SubDetailListViewModel>(); }
		MainModule!.SubDetailListList!.Add(new SubDetailListViewModel() { TestForeignKeyOne = MainModule.Id });
		return Partial("_InputFieldsPartial", MainModule);
	}
	private IActionResult RemoveSubDetailList()
	{
		ModelState.Clear();
		MainModule.SubDetailListList = MainModule!.SubDetailListList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", MainModule);
	}
	
}
