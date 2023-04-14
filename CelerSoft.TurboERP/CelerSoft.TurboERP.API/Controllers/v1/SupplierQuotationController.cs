using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class SupplierQuotationController : BaseApiController<SupplierQuotationController>
{
    [Authorize(Policy = Permission.SupplierQuotation.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SupplierQuotationState>>> GetAsync([FromQuery] GetSupplierQuotationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SupplierQuotation.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierQuotationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSupplierQuotationByIdQuery(id)));

    [Authorize(Policy = Permission.SupplierQuotation.Create)]
    [HttpPost]
    public async Task<ActionResult<SupplierQuotationState>> PostAsync([FromBody] SupplierQuotationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSupplierQuotationCommand>(request)));

    [Authorize(Policy = Permission.SupplierQuotation.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SupplierQuotationState>> PutAsync(string id, [FromBody] SupplierQuotationViewModel request)
    {
        var command = Mapper.Map<EditSupplierQuotationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SupplierQuotation.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SupplierQuotationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSupplierQuotationCommand { Id = id }));
}

public record SupplierQuotationViewModel
{
    [Required]
	
	public string PurchaseRequisitionId { get;set; } = "";
	[Required]
	
	public string SupplierId { get;set; } = "";
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Canvasser { get;set; } = "";
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	   
}
