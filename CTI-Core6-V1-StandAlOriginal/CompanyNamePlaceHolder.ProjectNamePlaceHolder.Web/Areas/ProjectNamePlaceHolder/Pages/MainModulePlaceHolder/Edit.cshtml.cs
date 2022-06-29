using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.MainModulePlaceHolder;

[Authorize(Policy = Permission.MainModulePlaceHolder.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public MainModulePlaceHolderViewModel MainModulePlaceHolder { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetMainModulePlaceHolderByIdQuery(id)), MainModulePlaceHolder);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditMainModulePlaceHolderCommand>(MainModulePlaceHolder)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddSubDetailListPlaceHolder")
		{
			return AddSubDetailListPlaceHolder();
		}
		if (AsyncAction == "RemoveSubDetailListPlaceHolder")
		{
			return RemoveSubDetailListPlaceHolder();
		}
		
		
        return Partial("_InputFieldsPartial", MainModulePlaceHolder);
    }
	
	private IActionResult AddSubDetailListPlaceHolder()
	{
		ModelState.Clear();
		if (MainModulePlaceHolder!.SubDetailListPlaceHolderList == null) { MainModulePlaceHolder!.SubDetailListPlaceHolderList = new List<SubDetailListPlaceHolderViewModel>(); }
		MainModulePlaceHolder!.SubDetailListPlaceHolderList!.Add(new SubDetailListPlaceHolderViewModel() { MainModulePlaceHolderId = MainModulePlaceHolder.Id });
		return Partial("_InputFieldsPartial", MainModulePlaceHolder);
	}
	private IActionResult RemoveSubDetailListPlaceHolder()
	{
		ModelState.Clear();
		MainModulePlaceHolder.SubDetailListPlaceHolderList = MainModulePlaceHolder!.SubDetailListPlaceHolderList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", MainModulePlaceHolder);
	}
	
}
