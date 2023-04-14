using CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Inventory;

[Authorize(Policy = Permission.Inventory.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public InventoryViewModel Inventory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetInventoryByIdQuery(id)), Inventory);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditInventoryCommand>(Inventory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddInventoryHistory")
		{
			return AddInventoryHistory();
		}
		if (AsyncAction == "RemoveInventoryHistory")
		{
			return RemoveInventoryHistory();
		}
		if (AsyncAction == "AddOrderItem")
		{
			return AddOrderItem();
		}
		if (AsyncAction == "RemoveOrderItem")
		{
			return RemoveOrderItem();
		}
		
		
        return Partial("_InputFieldsPartial", Inventory);
    }
	
	private IActionResult AddInventoryHistory()
	{
		ModelState.Clear();
		if (Inventory!.InventoryHistoryList == null) { Inventory!.InventoryHistoryList = new List<InventoryHistoryViewModel>(); }
		Inventory!.InventoryHistoryList!.Add(new InventoryHistoryViewModel() { InventoryId = Inventory.Id });
		return Partial("_InputFieldsPartial", Inventory);
	}
	private IActionResult RemoveInventoryHistory()
	{
		ModelState.Clear();
		Inventory.InventoryHistoryList = Inventory!.InventoryHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Inventory);
	}

	private IActionResult AddOrderItem()
	{
		ModelState.Clear();
		if (Inventory!.OrderItemList == null) { Inventory!.OrderItemList = new List<OrderItemViewModel>(); }
		Inventory!.OrderItemList!.Add(new OrderItemViewModel() { InventoryId = Inventory.Id });
		return Partial("_InputFieldsPartial", Inventory);
	}
	private IActionResult RemoveOrderItem()
	{
		ModelState.Clear();
		Inventory.OrderItemList = Inventory!.OrderItemList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Inventory);
	}
	
}
