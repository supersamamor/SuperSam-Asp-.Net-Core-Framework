using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Commands;
using CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectBusinessUnitController : BaseApiController<ProjectBusinessUnitController>
{
    [Authorize(Policy = Permission.ProjectBusinessUnit.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectBusinessUnitState>>> GetAsync([FromQuery] GetProjectBusinessUnitQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectBusinessUnit.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectBusinessUnitState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectBusinessUnitByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectBusinessUnit.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectBusinessUnitState>> PostAsync([FromBody] ProjectBusinessUnitViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectBusinessUnitCommand>(request)));

    [Authorize(Policy = Permission.ProjectBusinessUnit.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectBusinessUnitState>> PutAsync(string id, [FromBody] ProjectBusinessUnitViewModel request)
    {
        var command = Mapper.Map<EditProjectBusinessUnitCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectBusinessUnit.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectBusinessUnitState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectBusinessUnitCommand { Id = id }));
}

public record ProjectBusinessUnitViewModel
{
    [Required]
	
	public string BusinessUnitId { get;set; } = "";
	[Required]
	
	public string ProjectId { get;set; } = "";
	   
}
