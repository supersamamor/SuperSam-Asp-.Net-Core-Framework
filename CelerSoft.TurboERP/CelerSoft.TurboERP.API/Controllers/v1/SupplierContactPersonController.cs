using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class SupplierContactPersonController : BaseApiController<SupplierContactPersonController>
{
    [Authorize(Policy = Permission.SupplierContactPerson.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SupplierContactPersonState>>> GetAsync([FromQuery] GetSupplierContactPersonQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SupplierContactPerson.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContactPersonState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSupplierContactPersonByIdQuery(id)));

    [Authorize(Policy = Permission.SupplierContactPerson.Create)]
    [HttpPost]
    public async Task<ActionResult<SupplierContactPersonState>> PostAsync([FromBody] SupplierContactPersonViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSupplierContactPersonCommand>(request)));

    [Authorize(Policy = Permission.SupplierContactPerson.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SupplierContactPersonState>> PutAsync(string id, [FromBody] SupplierContactPersonViewModel request)
    {
        var command = Mapper.Map<EditSupplierContactPersonCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SupplierContactPerson.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SupplierContactPersonState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSupplierContactPersonCommand { Id = id }));
}

public record SupplierContactPersonViewModel
{
    
	public string? SupplierId { get;set; }
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FullName { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Position { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string MobileNumber { get;set; } = "";
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PhoneNumber { get;set; }
	   
}
