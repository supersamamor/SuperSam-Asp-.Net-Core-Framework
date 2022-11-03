using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.LeadSource.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadSource.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class LeadSourceController : BaseApiController<LeadSourceController>
{
    [Authorize(Policy = Permission.LeadSource.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<LeadSourceState>>> GetAsync([FromQuery] GetLeadSourceQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.LeadSource.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LeadSourceState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetLeadSourceByIdQuery(id)));

    [Authorize(Policy = Permission.LeadSource.Create)]
    [HttpPost]
    public async Task<ActionResult<LeadSourceState>> PostAsync([FromBody] LeadSourceViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddLeadSourceCommand>(request)));

    [Authorize(Policy = Permission.LeadSource.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LeadSourceState>> PutAsync(string id, [FromBody] LeadSourceViewModel request)
    {
        var command = Mapper.Map<EditLeadSourceCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.LeadSource.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeadSourceState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteLeadSourceCommand { Id = id }));
}

public record LeadSourceViewModel
{
    [Required]
	
	public string LeadSourceName { get;set; } = "";
	   
}
