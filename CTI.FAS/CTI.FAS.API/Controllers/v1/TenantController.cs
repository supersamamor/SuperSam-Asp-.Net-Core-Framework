using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.Tenant.Commands;
using CTI.FAS.Application.Features.FAS.Tenant.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class TenantController : BaseApiController<TenantController>
{
    [Authorize(Policy = Permission.Tenant.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TenantState>>> GetAsync([FromQuery] GetTenantQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Tenant.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TenantState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTenantByIdQuery(id)));

    [Authorize(Policy = Permission.Tenant.Create)]
    [HttpPost]
    public async Task<ActionResult<TenantState>> PostAsync([FromBody] TenantViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTenantCommand>(request)));

    [Authorize(Policy = Permission.Tenant.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TenantState>> PutAsync(string id, [FromBody] TenantViewModel request)
    {
        var command = Mapper.Map<EditTenantCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Tenant.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TenantState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTenantCommand { Id = id }));
}

public record TenantViewModel
{
    [Required]
	
	public string ProjectId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[Required]
	public DateTime DateFrom { get;set; } = DateTime.Now.Date;
	[Required]
	public DateTime DateTo { get;set; } = DateTime.Now.Date;
	public bool IsDisabled { get;set; }
	   
}
