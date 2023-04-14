using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class InventoryController : BaseApiController<InventoryController>
{
    [Authorize(Policy = Permission.Inventory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<InventoryState>>> GetAsync([FromQuery] GetInventoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Inventory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetInventoryByIdQuery(id)));

    [Authorize(Policy = Permission.Inventory.Create)]
    [HttpPost]
    public async Task<ActionResult<InventoryState>> PostAsync([FromBody] InventoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddInventoryCommand>(request)));

    [Authorize(Policy = Permission.Inventory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<InventoryState>> PutAsync(string id, [FromBody] InventoryViewModel request)
    {
        var command = Mapper.Map<EditInventoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Inventory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<InventoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteInventoryCommand { Id = id }));
}

public record InventoryViewModel
{
    [Required]
	
	public string PurchaseItemId { get;set; } = "";
	[Required]
	
	public string ProductId { get;set; } = "";
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Quantity { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Amount { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? DeliveredByFullName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ReceivedByFullName { get;set; }
	public DateTime? DeliveredDate { get;set; } = DateTime.Now.Date;
	public DateTime? ReceivedDate { get;set; } = DateTime.Now.Date;
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SellByUsername { get;set; }
	   
}
