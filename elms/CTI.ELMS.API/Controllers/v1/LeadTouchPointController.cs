using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class LeadTouchPointController : BaseApiController<LeadTouchPointController>
{
    [Authorize(Policy = Permission.LeadTouchPoint.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<LeadTouchPointState>>> GetAsync([FromQuery] GetLeadTouchPointQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.LeadTouchPoint.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LeadTouchPointState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetLeadTouchPointByIdQuery(id)));

    [Authorize(Policy = Permission.LeadTouchPoint.Create)]
    [HttpPost]
    public async Task<ActionResult<LeadTouchPointState>> PostAsync([FromBody] LeadTouchPointViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddLeadTouchPointCommand>(request)));

    [Authorize(Policy = Permission.LeadTouchPoint.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LeadTouchPointState>> PutAsync(string id, [FromBody] LeadTouchPointViewModel request)
    {
        var command = Mapper.Map<EditLeadTouchPointCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.LeadTouchPoint.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeadTouchPointState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteLeadTouchPointCommand { Id = id }));
}

public record LeadTouchPointViewModel
{
    [Required]
	
	public string LeadTouchPointName { get;set; } = "";
	   
}
