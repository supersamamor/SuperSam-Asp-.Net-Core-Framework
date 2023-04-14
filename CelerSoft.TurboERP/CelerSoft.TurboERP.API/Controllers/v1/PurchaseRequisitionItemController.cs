using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisitionItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisitionItem.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class PurchaseRequisitionItemController : BaseApiController<PurchaseRequisitionItemController>
{
    [Authorize(Policy = Permission.PurchaseRequisitionItem.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PurchaseRequisitionItemState>>> GetAsync([FromQuery] GetPurchaseRequisitionItemQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PurchaseRequisitionItem.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseRequisitionItemState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPurchaseRequisitionItemByIdQuery(id)));

    [Authorize(Policy = Permission.PurchaseRequisitionItem.Create)]
    [HttpPost]
    public async Task<ActionResult<PurchaseRequisitionItemState>> PostAsync([FromBody] PurchaseRequisitionItemViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPurchaseRequisitionItemCommand>(request)));

    [Authorize(Policy = Permission.PurchaseRequisitionItem.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PurchaseRequisitionItemState>> PutAsync(string id, [FromBody] PurchaseRequisitionItemViewModel request)
    {
        var command = Mapper.Map<EditPurchaseRequisitionItemCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PurchaseRequisitionItem.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PurchaseRequisitionItemState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePurchaseRequisitionItemCommand { Id = id }));
}

public record PurchaseRequisitionItemViewModel
{
    [Required]
	
	public string PurchaseRequisitionId { get;set; } = "";
	[Required]
	
	public string ProductId { get;set; } = "";
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Quantity { get;set; }
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get;set; }
	   
}
