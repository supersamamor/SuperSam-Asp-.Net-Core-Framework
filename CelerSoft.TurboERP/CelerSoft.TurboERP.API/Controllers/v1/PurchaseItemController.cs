using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class PurchaseItemController : BaseApiController<PurchaseItemController>
{
    [Authorize(Policy = Permission.PurchaseItem.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PurchaseItemState>>> GetAsync([FromQuery] GetPurchaseItemQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PurchaseItem.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseItemState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPurchaseItemByIdQuery(id)));

    [Authorize(Policy = Permission.PurchaseItem.Create)]
    [HttpPost]
    public async Task<ActionResult<PurchaseItemState>> PostAsync([FromBody] PurchaseItemViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPurchaseItemCommand>(request)));

    [Authorize(Policy = Permission.PurchaseItem.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PurchaseItemState>> PutAsync(string id, [FromBody] PurchaseItemViewModel request)
    {
        var command = Mapper.Map<EditPurchaseItemCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PurchaseItem.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PurchaseItemState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePurchaseItemCommand { Id = id }));
}

public record PurchaseItemViewModel
{
    
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get;set; }
	
	public string? ProductId { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Quantity { get;set; }
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get;set; }
	
	public string? SupplierQuotationItemId { get;set; }
	   
}
