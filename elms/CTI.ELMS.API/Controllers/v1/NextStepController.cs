using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.NextStep.Commands;
using CTI.ELMS.Application.Features.ELMS.NextStep.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class NextStepController : BaseApiController<NextStepController>
{
    [Authorize(Policy = Permission.NextStep.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<NextStepState>>> GetAsync([FromQuery] GetNextStepQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.NextStep.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<NextStepState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetNextStepByIdQuery(id)));

    [Authorize(Policy = Permission.NextStep.Create)]
    [HttpPost]
    public async Task<ActionResult<NextStepState>> PostAsync([FromBody] NextStepViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddNextStepCommand>(request)));

    [Authorize(Policy = Permission.NextStep.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<NextStepState>> PutAsync(string id, [FromBody] NextStepViewModel request)
    {
        var command = Mapper.Map<EditNextStepCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.NextStep.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<NextStepState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteNextStepCommand { Id = id }));
}

public record NextStepViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string NextStepTaskName { get;set; } = "";
	   
}
