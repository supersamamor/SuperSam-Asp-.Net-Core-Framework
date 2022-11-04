using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.LeadTask.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTask.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class LeadTaskController : BaseApiController<LeadTaskController>
{
    [Authorize(Policy = Permission.LeadTask.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<LeadTaskState>>> GetAsync([FromQuery] GetLeadTaskQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.LeadTask.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LeadTaskState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetLeadTaskByIdQuery(id)));

    [Authorize(Policy = Permission.LeadTask.Create)]
    [HttpPost]
    public async Task<ActionResult<LeadTaskState>> PostAsync([FromBody] LeadTaskViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddLeadTaskCommand>(request)));

    [Authorize(Policy = Permission.LeadTask.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LeadTaskState>> PutAsync(string id, [FromBody] LeadTaskViewModel request)
    {
        var command = Mapper.Map<EditLeadTaskCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.LeadTask.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeadTaskState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteLeadTaskCommand { Id = id }));
}

public record LeadTaskViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LeadTaskName { get;set; } = "";
	   
}
