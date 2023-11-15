using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.Team.Commands;
using CTI.DSF.Application.Features.DSF.Team.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class TeamController : BaseApiController<TeamController>
{
    [Authorize(Policy = Permission.Team.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TeamState>>> GetAsync([FromQuery] GetTeamQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Team.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TeamState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTeamByIdQuery(id)));

    [Authorize(Policy = Permission.Team.Create)]
    [HttpPost]
    public async Task<ActionResult<TeamState>> PostAsync([FromBody] TeamViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTeamCommand>(request)));

    [Authorize(Policy = Permission.Team.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TeamState>> PutAsync(string id, [FromBody] TeamViewModel request)
    {
        var command = Mapper.Map<EditTeamCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Team.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TeamState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTeamCommand { Id = id }));
}

public record TeamViewModel
{
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SectionCode { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TeamCode { get;set; } = "";
	[Required]
	
	public string TeamName { get;set; } = "";
	public bool Active { get;set; }
	   
}
