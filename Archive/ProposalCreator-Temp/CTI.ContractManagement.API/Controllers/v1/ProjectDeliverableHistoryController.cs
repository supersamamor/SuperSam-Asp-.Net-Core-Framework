using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectDeliverableHistoryController : BaseApiController<ProjectDeliverableHistoryController>
{
    [Authorize(Policy = Permission.ProjectDeliverableHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectDeliverableHistoryState>>> GetAsync([FromQuery] GetProjectDeliverableHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectDeliverableHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDeliverableHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectDeliverableHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectDeliverableHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectDeliverableHistoryState>> PostAsync([FromBody] ProjectDeliverableHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectDeliverableHistoryCommand>(request)));

    [Authorize(Policy = Permission.ProjectDeliverableHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDeliverableHistoryState>> PutAsync(string id, [FromBody] ProjectDeliverableHistoryViewModel request)
    {
        var command = Mapper.Map<EditProjectDeliverableHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectDeliverableHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectDeliverableHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectDeliverableHistoryCommand { Id = id }));
}

public record ProjectDeliverableHistoryViewModel
{
    [Required]
	
	public string ProjectHistoryId { get;set; } = "";
	[Required]
	
	public string DeliverableId { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get;set; }
	public int? Sequence { get;set; }
	   
}
