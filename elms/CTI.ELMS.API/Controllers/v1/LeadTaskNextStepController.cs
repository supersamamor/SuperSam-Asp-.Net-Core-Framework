using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.LeadTaskNextStep.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTaskNextStep.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class LeadTaskNextStepController : BaseApiController<LeadTaskNextStepController>
{
    [Authorize(Policy = Permission.LeadTaskNextStep.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<LeadTaskNextStepState>>> GetAsync([FromQuery] GetLeadTaskNextStepQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.LeadTaskNextStep.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LeadTaskNextStepState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetLeadTaskNextStepByIdQuery(id)));

    [Authorize(Policy = Permission.LeadTaskNextStep.Create)]
    [HttpPost]
    public async Task<ActionResult<LeadTaskNextStepState>> PostAsync([FromBody] LeadTaskNextStepViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddLeadTaskNextStepCommand>(request)));

    [Authorize(Policy = Permission.LeadTaskNextStep.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LeadTaskNextStepState>> PutAsync(string id, [FromBody] LeadTaskNextStepViewModel request)
    {
        var command = Mapper.Map<EditLeadTaskNextStepCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.LeadTaskNextStep.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeadTaskNextStepState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteLeadTaskNextStepCommand { Id = id }));
}

public record LeadTaskNextStepViewModel
{
    
	public string? LeadTaskId { get;set; }
	
	public string? NextStepId { get;set; }
	public int? PCTDay { get;set; }
	
	public string? ClientFeedbackId { get;set; }
	   
}
