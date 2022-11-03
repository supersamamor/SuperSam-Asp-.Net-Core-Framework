using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class LeadTaskClientFeedBackController : BaseApiController<LeadTaskClientFeedBackController>
{
    [Authorize(Policy = Permission.LeadTaskClientFeedBack.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<LeadTaskClientFeedBackState>>> GetAsync([FromQuery] GetLeadTaskClientFeedBackQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.LeadTaskClientFeedBack.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LeadTaskClientFeedBackState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetLeadTaskClientFeedBackByIdQuery(id)));

    [Authorize(Policy = Permission.LeadTaskClientFeedBack.Create)]
    [HttpPost]
    public async Task<ActionResult<LeadTaskClientFeedBackState>> PostAsync([FromBody] LeadTaskClientFeedBackViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddLeadTaskClientFeedBackCommand>(request)));

    [Authorize(Policy = Permission.LeadTaskClientFeedBack.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LeadTaskClientFeedBackState>> PutAsync(string id, [FromBody] LeadTaskClientFeedBackViewModel request)
    {
        var command = Mapper.Map<EditLeadTaskClientFeedBackCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.LeadTaskClientFeedBack.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeadTaskClientFeedBackState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteLeadTaskClientFeedBackCommand { Id = id }));
}

public record LeadTaskClientFeedBackViewModel
{
    
	public string? LeadTaskId { get;set; }
	
	public string? ClientFeedbackId { get;set; }
	[StringLength(25, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ActivityStatus { get;set; }
	   
}
