using CTI.TenantSales.Application.Features.TenantSales.Theme.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Theme.Queries;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Theme;

[Authorize(Policy = Permission.Theme.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ThemeViewModel Theme { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetThemeByIdQuery(id)), Theme);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditThemeCommand>(Theme)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddClassification")
		{
			return AddClassification();
		}
		if (AsyncAction == "RemoveClassification")
		{
			return RemoveClassification();
		}
		
		
        return Partial("_InputFieldsPartial", Theme);
    }
	
	private IActionResult AddClassification()
	{
		ModelState.Clear();
		if (Theme!.ClassificationList == null) { Theme!.ClassificationList = new List<ClassificationViewModel>(); }
		Theme!.ClassificationList!.Add(new ClassificationViewModel() { ThemeId = Theme.Id });
		return Partial("_InputFieldsPartial", Theme);
	}
	private IActionResult RemoveClassification()
	{
		ModelState.Clear();
		Theme.ClassificationList = Theme!.ClassificationList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Theme);
	}
	
}
