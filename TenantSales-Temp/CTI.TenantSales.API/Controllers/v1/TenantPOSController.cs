using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOS.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOS.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class TenantPOSController : BaseApiController<TenantPOSController>
{
    [Authorize(Policy = Permission.TenantPOS.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TenantPOSState>>> GetAsync([FromQuery] GetTenantPOSQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TenantPOS.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TenantPOSState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTenantPOSByIdQuery(id)));

    [Authorize(Policy = Permission.TenantPOS.Create)]
    [HttpPost]
    public async Task<ActionResult<TenantPOSState>> PostAsync([FromBody] TenantPOSViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTenantPOSCommand>(request)));

    [Authorize(Policy = Permission.TenantPOS.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TenantPOSState>> PutAsync(string id, [FromBody] TenantPOSViewModel request)
    {
        var command = Mapper.Map<EditTenantPOSCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TenantPOS.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TenantPOSState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTenantPOSCommand { Id = id }));
}

public record TenantPOSViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[Required]
	
	public string TenantId { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SerialNumber { get;set; }
	public bool IsDisabled { get;set; }
	   
}
