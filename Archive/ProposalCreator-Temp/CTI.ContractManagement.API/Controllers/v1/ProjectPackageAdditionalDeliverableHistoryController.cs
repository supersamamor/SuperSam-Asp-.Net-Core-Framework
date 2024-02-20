using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectPackageAdditionalDeliverableHistoryController : BaseApiController<ProjectPackageAdditionalDeliverableHistoryController>
{
    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverableHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectPackageAdditionalDeliverableHistoryState>>> GetAsync([FromQuery] GetProjectPackageAdditionalDeliverableHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverableHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectPackageAdditionalDeliverableHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectPackageAdditionalDeliverableHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverableHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectPackageAdditionalDeliverableHistoryState>> PostAsync([FromBody] ProjectPackageAdditionalDeliverableHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectPackageAdditionalDeliverableHistoryCommand>(request)));

    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverableHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectPackageAdditionalDeliverableHistoryState>> PutAsync(string id, [FromBody] ProjectPackageAdditionalDeliverableHistoryViewModel request)
    {
        var command = Mapper.Map<EditProjectPackageAdditionalDeliverableHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectPackageAdditionalDeliverableHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectPackageAdditionalDeliverableHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectPackageAdditionalDeliverableHistoryCommand { Id = id }));
}

public record ProjectPackageAdditionalDeliverableHistoryViewModel
{
    [Required]
	
	public string ProjectPackageHistoryId { get;set; } = "";
	[Required]
	
	public string DeliverableId { get;set; } = "";
	public int? Sequence { get;set; }
	   
}
