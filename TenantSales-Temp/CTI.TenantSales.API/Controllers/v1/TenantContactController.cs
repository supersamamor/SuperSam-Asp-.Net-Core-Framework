using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.TenantContact.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantContact.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class TenantContactController : BaseApiController<TenantContactController>
{
    [Authorize(Policy = Permission.TenantContact.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TenantContactState>>> GetAsync([FromQuery] GetTenantContactQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TenantContact.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TenantContactState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTenantContactByIdQuery(id)));

    [Authorize(Policy = Permission.TenantContact.Create)]
    [HttpPost]
    public async Task<ActionResult<TenantContactState>> PostAsync([FromBody] TenantContactViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTenantContactCommand>(request)));

    [Authorize(Policy = Permission.TenantContact.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TenantContactState>> PutAsync(string id, [FromBody] TenantContactViewModel request)
    {
        var command = Mapper.Map<EditTenantContactCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TenantContact.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TenantContactState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTenantContactCommand { Id = id }));
}

public record TenantContactViewModel
{
    [Required]
	public int Group { get;set; }
	[Required]
	public int Type { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Detail { get;set; }
	[Required]
	
	public string TenantId { get;set; } = "";
	   
}
