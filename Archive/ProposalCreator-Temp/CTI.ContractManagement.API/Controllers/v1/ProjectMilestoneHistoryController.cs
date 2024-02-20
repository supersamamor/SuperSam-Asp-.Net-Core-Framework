using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectMilestoneHistoryController : BaseApiController<ProjectMilestoneHistoryController>
{
    [Authorize(Policy = Permission.ProjectMilestoneHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectMilestoneHistoryState>>> GetAsync([FromQuery] GetProjectMilestoneHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectMilestoneHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectMilestoneHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectMilestoneHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectMilestoneHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectMilestoneHistoryState>> PostAsync([FromBody] ProjectMilestoneHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectMilestoneHistoryCommand>(request)));

    [Authorize(Policy = Permission.ProjectMilestoneHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectMilestoneHistoryState>> PutAsync(string id, [FromBody] ProjectMilestoneHistoryViewModel request)
    {
        var command = Mapper.Map<EditProjectMilestoneHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectMilestoneHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectMilestoneHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectMilestoneHistoryCommand { Id = id }));
}

public record ProjectMilestoneHistoryViewModel
{
    [Required]
	
	public string ProjectHistoryId { get;set; } = "";
	[Required]
	
	public string MilestoneStageId { get;set; } = "";
	public int? Sequence { get;set; }
	[Required]
	
	public string FrequencyId { get;set; } = "";
	public int? FrequencyQuantity { get;set; }
	   
}
