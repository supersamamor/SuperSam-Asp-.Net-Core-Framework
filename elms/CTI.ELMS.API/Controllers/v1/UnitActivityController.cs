using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.UnitActivity.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitActivity.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class UnitActivityController : BaseApiController<UnitActivityController>
{
    [Authorize(Policy = Permission.UnitActivity.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UnitActivityState>>> GetAsync([FromQuery] GetUnitActivityQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UnitActivity.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitActivityState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUnitActivityByIdQuery(id)));

    [Authorize(Policy = Permission.UnitActivity.Create)]
    [HttpPost]
    public async Task<ActionResult<UnitActivityState>> PostAsync([FromBody] UnitActivityViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUnitActivityCommand>(request)));

    [Authorize(Policy = Permission.UnitActivity.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitActivityState>> PutAsync(string id, [FromBody] UnitActivityViewModel request)
    {
        var command = Mapper.Map<EditUnitActivityCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UnitActivity.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UnitActivityState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUnitActivityCommand { Id = id }));
}

public record UnitActivityViewModel
{
    [Required]
	
	public string UnitID { get;set; } = "";
	
	public string? ActivityHistoryID { get;set; }
	[Required]
	
	public string ActivityID { get;set; } = "";
	   
}
