using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class SupplierQuotationItemController : BaseApiController<SupplierQuotationItemController>
{
    [Authorize(Policy = Permission.SupplierQuotationItem.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SupplierQuotationItemState>>> GetAsync([FromQuery] GetSupplierQuotationItemQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SupplierQuotationItem.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierQuotationItemState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSupplierQuotationItemByIdQuery(id)));

    [Authorize(Policy = Permission.SupplierQuotationItem.Create)]
    [HttpPost]
    public async Task<ActionResult<SupplierQuotationItemState>> PostAsync([FromBody] SupplierQuotationItemViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSupplierQuotationItemCommand>(request)));

    [Authorize(Policy = Permission.SupplierQuotationItem.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SupplierQuotationItemState>> PutAsync(string id, [FromBody] SupplierQuotationItemViewModel request)
    {
        var command = Mapper.Map<EditSupplierQuotationItemCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SupplierQuotationItem.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SupplierQuotationItemState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSupplierQuotationItemCommand { Id = id }));
}

public record SupplierQuotationItemViewModel
{
    [Required]
	
	public string SupplierQuotationId { get;set; } = "";
	[Required]
	
	public string ProductId { get;set; } = "";
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Quantity { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Amount { get;set; }
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get;set; }
	   
}
