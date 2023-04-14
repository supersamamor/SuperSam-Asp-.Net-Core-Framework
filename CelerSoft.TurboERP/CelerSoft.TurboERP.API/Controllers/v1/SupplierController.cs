using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class SupplierController : BaseApiController<SupplierController>
{
    [Authorize(Policy = Permission.Supplier.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SupplierState>>> GetAsync([FromQuery] GetSupplierQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Supplier.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSupplierByIdQuery(id)));

    [Authorize(Policy = Permission.Supplier.Create)]
    [HttpPost]
    public async Task<ActionResult<SupplierState>> PostAsync([FromBody] SupplierViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSupplierCommand>(request)));

    [Authorize(Policy = Permission.Supplier.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SupplierState>> PutAsync(string id, [FromBody] SupplierViewModel request)
    {
        var command = Mapper.Map<EditSupplierCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Supplier.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SupplierState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSupplierCommand { Id = id }));
}

public record SupplierViewModel
{
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Company { get;set; } = "";
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TINNumber { get;set; }
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Address { get;set; } = "";
	   
}
