using CTI.TenantSales.Application.Features.TenantSales.Classification.Commands;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Classification;

[Authorize(Policy = Permission.Classification.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ClassificationViewModel Classification { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddClassificationCommand>(Classification)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddCategory")
		{
			return AddCategory();
		}
		if (AsyncAction == "RemoveCategory")
		{
			return RemoveCategory();
		}
		
		
        return Partial("_InputFieldsPartial", Classification);
    }
	
	private IActionResult AddCategory()
	{
		ModelState.Clear();
		if (Classification!.CategoryList == null) { Classification!.CategoryList = new List<CategoryViewModel>(); }
		Classification!.CategoryList!.Add(new CategoryViewModel() { ClassificationId = Classification.Id });
		return Partial("_InputFieldsPartial", Classification);
	}
	private IActionResult RemoveCategory()
	{
		ModelState.Clear();
		Classification.CategoryList = Classification!.CategoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Classification);
	}
	
}
