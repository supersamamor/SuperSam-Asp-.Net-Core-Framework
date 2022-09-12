using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectMilestoneController : BaseApiController<ProjectMilestoneController>
{
    [Authorize(Policy = Permission.ProjectMilestone.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectMilestoneState>>> GetAsync([FromQuery] GetProjectMilestoneQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectMilestone.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectMilestoneState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectMilestoneByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectMilestone.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectMilestoneState>> PostAsync([FromBody] ProjectMilestoneViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectMilestoneCommand>(request)));

    [Authorize(Policy = Permission.ProjectMilestone.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectMilestoneState>> PutAsync(string id, [FromBody] ProjectMilestoneViewModel request)
    {
        var command = Mapper.Map<EditProjectMilestoneCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectMilestone.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectMilestoneState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectMilestoneCommand { Id = id }));
}

public record ProjectMilestoneViewModel
{
    [Required]
	
	public string ProjectId { get;set; } = "";
	[Required]
	
	public string MilestoneStageId { get;set; } = "";
	public int? Sequence { get;set; }
	[Required]
	
	public string FrequencyId { get;set; } = "";
	public int? FrequencyQuantity { get;set; }
	   
}
