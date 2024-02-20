using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectDeliverableController : BaseApiController<ProjectDeliverableController>
{
    [Authorize(Policy = Permission.ProjectDeliverable.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectDeliverableState>>> GetAsync([FromQuery] GetProjectDeliverableQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectDeliverable.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDeliverableState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectDeliverableByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectDeliverable.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectDeliverableState>> PostAsync([FromBody] ProjectDeliverableViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectDeliverableCommand>(request)));

    [Authorize(Policy = Permission.ProjectDeliverable.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDeliverableState>> PutAsync(string id, [FromBody] ProjectDeliverableViewModel request)
    {
        var command = Mapper.Map<EditProjectDeliverableCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectDeliverable.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectDeliverableState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectDeliverableCommand { Id = id }));
}

public record ProjectDeliverableViewModel
{
    [Required]
	
	public string ProjectId { get;set; } = "";
	[Required]
	
	public string DeliverableId { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get;set; }
	public int? Sequence { get;set; }
	   
}
