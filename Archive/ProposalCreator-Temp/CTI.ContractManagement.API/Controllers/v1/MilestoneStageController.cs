using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class MilestoneStageController : BaseApiController<MilestoneStageController>
{
    [Authorize(Policy = Permission.MilestoneStage.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<MilestoneStageState>>> GetAsync([FromQuery] GetMilestoneStageQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.MilestoneStage.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<MilestoneStageState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetMilestoneStageByIdQuery(id)));

    [Authorize(Policy = Permission.MilestoneStage.Create)]
    [HttpPost]
    public async Task<ActionResult<MilestoneStageState>> PostAsync([FromBody] MilestoneStageViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddMilestoneStageCommand>(request)));

    [Authorize(Policy = Permission.MilestoneStage.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<MilestoneStageState>> PutAsync(string id, [FromBody] MilestoneStageViewModel request)
    {
        var command = Mapper.Map<EditMilestoneStageCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.MilestoneStage.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MilestoneStageState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteMilestoneStageCommand { Id = id }));
}

public record MilestoneStageViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
