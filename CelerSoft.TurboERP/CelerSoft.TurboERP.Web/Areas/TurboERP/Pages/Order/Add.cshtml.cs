using CelerSoft.TurboERP.Application.Features.TurboERP.Order.Commands;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Order;

[Authorize(Policy = Permission.Order.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public OrderViewModel Order { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddOrderCommand>(Order)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddOrderItem")
		{
			return AddOrderItem();
		}
		if (AsyncAction == "RemoveOrderItem")
		{
			return RemoveOrderItem();
		}
		
		
        return Partial("_InputFieldsPartial", Order);
    }
	
	private IActionResult AddOrderItem()
	{
		ModelState.Clear();
		if (Order!.OrderItemList == null) { Order!.OrderItemList = new List<OrderItemViewModel>(); }
		Order!.OrderItemList!.Add(new OrderItemViewModel() { OrderId = Order.Id });
		return Partial("_InputFieldsPartial", Order);
	}
	private IActionResult RemoveOrderItem()
	{
		ModelState.Clear();
		Order.OrderItemList = Order!.OrderItemList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Order);
	}
	
}
