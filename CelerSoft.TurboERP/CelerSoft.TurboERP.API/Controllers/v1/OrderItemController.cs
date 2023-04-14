using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class OrderItemController : BaseApiController<OrderItemController>
{
    [Authorize(Policy = Permission.OrderItem.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<OrderItemState>>> GetAsync([FromQuery] GetOrderItemQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.OrderItem.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetOrderItemByIdQuery(id)));

    [Authorize(Policy = Permission.OrderItem.Create)]
    [HttpPost]
    public async Task<ActionResult<OrderItemState>> PostAsync([FromBody] OrderItemViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddOrderItemCommand>(request)));

    [Authorize(Policy = Permission.OrderItem.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OrderItemState>> PutAsync(string id, [FromBody] OrderItemViewModel request)
    {
        var command = Mapper.Map<EditOrderItemCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.OrderItem.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OrderItemState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteOrderItemCommand { Id = id }));
}

public record OrderItemViewModel
{
    
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DeliveredByFullName { get;set; } = "";
	
	public string? InventoryId { get;set; }
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string OrderByUserId { get;set; } = "";
	
	public string? OrderId { get;set; }
	public bool Paid { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Quantity { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReceivedByFullName { get;set; } = "";
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalAmount { get;set; }
	   
}
