using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverable.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverable.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectPackageAdditionalDeliverableController : BaseApiController<ProjectPackageAdditionalDeliverableController>
{
    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverable.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectPackageAdditionalDeliverableState>>> GetAsync([FromQuery] GetProjectPackageAdditionalDeliverableQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverable.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectPackageAdditionalDeliverableState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectPackageAdditionalDeliverableByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverable.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectPackageAdditionalDeliverableState>> PostAsync([FromBody] ProjectPackageAdditionalDeliverableViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectPackageAdditionalDeliverableCommand>(request)));

    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverable.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectPackageAdditionalDeliverableState>> PutAsync(string id, [FromBody] ProjectPackageAdditionalDeliverableViewModel request)
    {
        var command = Mapper.Map<EditProjectPackageAdditionalDeliverableCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverable.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectPackageAdditionalDeliverableState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectPackageAdditionalDeliverableCommand { Id = id }));
}

public record ProjectPackageAdditionalDeliverableViewModel
{
    [Required]
	
	public string ProjectPackageId { get;set; } = "";
	[Required]
	
	public string DeliverableId { get;set; } = "";
	public int? Sequence { get;set; }
	   
}
