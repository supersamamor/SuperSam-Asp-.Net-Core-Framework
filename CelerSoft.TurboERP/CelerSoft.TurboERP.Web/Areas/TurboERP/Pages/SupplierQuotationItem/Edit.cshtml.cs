using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.SupplierQuotationItem;

[Authorize(Policy = Permission.SupplierQuotationItem.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public SupplierQuotationItemViewModel SupplierQuotationItem { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetSupplierQuotationItemByIdQuery(id)), SupplierQuotationItem);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditSupplierQuotationItemCommand>(SupplierQuotationItem)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddPurchaseItem")
		{
			return AddPurchaseItem();
		}
		if (AsyncAction == "RemovePurchaseItem")
		{
			return RemovePurchaseItem();
		}
		
		
        return Partial("_InputFieldsPartial", SupplierQuotationItem);
    }
	
	private IActionResult AddPurchaseItem()
	{
		ModelState.Clear();
		if (SupplierQuotationItem!.PurchaseItemList == null) { SupplierQuotationItem!.PurchaseItemList = new List<PurchaseItemViewModel>(); }
		SupplierQuotationItem!.PurchaseItemList!.Add(new PurchaseItemViewModel() { SupplierQuotationItemId = SupplierQuotationItem.Id });
		return Partial("_InputFieldsPartial", SupplierQuotationItem);
	}
	private IActionResult RemovePurchaseItem()
	{
		ModelState.Clear();
		SupplierQuotationItem.PurchaseItemList = SupplierQuotationItem!.PurchaseItemList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", SupplierQuotationItem);
	}
	
}
