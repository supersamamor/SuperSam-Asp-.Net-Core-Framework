using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class PurchaseController : BaseApiController<PurchaseController>
{
    [Authorize(Policy = Permission.Purchase.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PurchaseState>>> GetAsync([FromQuery] GetPurchaseQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Purchase.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPurchaseByIdQuery(id)));

    [Authorize(Policy = Permission.Purchase.Create)]
    [HttpPost]
    public async Task<ActionResult<PurchaseState>> PostAsync([FromBody] PurchaseViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPurchaseCommand>(request)));

    [Authorize(Policy = Permission.Purchase.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PurchaseState>> PutAsync(string id, [FromBody] PurchaseViewModel request)
    {
        var command = Mapper.Map<EditPurchaseCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Purchase.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PurchaseState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePurchaseCommand { Id = id }));
}

public record PurchaseViewModel
{
    [StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Code { get;set; }
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string NotedByUsername { get;set; } = "";
	
	public string? PurchaseRequisitionId { get;set; }
	
	public string? SupplierQuotationId { get;set; }
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReferenceInvoiceNumber { get;set; } = "";
	   
}
